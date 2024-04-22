namespace KirokuG2.Loader
{
	using KirokuG2.Internal.Loader.Interface;

	public class KLoaderManager
	{
		private static ILogProvider _logProvider;

		private static ISQLProvider _sqlProvider;

		public static bool Configuration(ILogProvider logProvider, ISQLProvider sqlProvider)
		{
			_logProvider = logProvider ?? throw new ArgumentNullException($"LogProvider is Null");

			_sqlProvider = sqlProvider ?? throw new ArgumentNullException($"SQLProvider is Null");

			return true;
		}

		/// <summary>
		/// Process KLOG's marked for uploading into Kiroku database
		/// </summary>
		public static bool ProcessLogs(IKLog klog)
		{
			try
			{
				// get all id's for tag=upload
				var logIds = _logProvider.GetLogIds("upload", 500);

				klog.Metric("kload_doc_cnt", logIds.Count);

				Dictionary<string, Dictionary<string, List<string>>> logSets = new();

				foreach (var logId in logIds)
				{
					var logs = _logProvider.GetLogsById(logId);

					logSets[logId] = new Dictionary<string, List<string>>(logs);
				}

				var logCount = 0;
				foreach (var logSet in logSets)
				{
					var isMultiLog = logSet.Value.Count > 1;

					foreach (var log in logSet.Value)
					{
						try
						{
							// get log instance
							var log_lines = log.Value;

							var instance = isMultiLog ? $"{logSet.Key.ToUpper()}.{log.Key.ToUpper()}" : logSet.Key.ToUpper();
							var source = "NotSet";
							var function = "NotSet";
							var normal_log_type = true;

							// setup
							LogInstance logInstance = new(instance);
							Dictionary<string, LogBlock> logBlocks = new();
							List<LogError> logErrors = new();
							List<LogMetric> logMetrics = new();

							// foreach line
							foreach (var log_line in log_lines)
							{
								var log_components = log_line.Split(',');

								// timestamp
								var datetime = DateTime.Parse(log_components[0]);

								// log event type
								var type = log_components[1].ToUpper();

								// clear preffix data
								var position = log_components[0].Count() + 1 + log_components[1].Count() + 1;

								// log data contents
								var data = log_line.Remove(0, position);

								// start instance
								if (type == "I")
								{
									var start_instance_components = data.Split('$');
									source = start_instance_components[0];
									function = start_instance_components[1];

									logInstance.Update(
										datetime,
										source,
										function);
								}

								// stop instance
								if (type == "SI")
								{
									logInstance.StopInstance(datetime);
								}

								// start block
								if (type == "B")
								{
									var block_components = data.Split('$');
									var block_tag = block_components[0].ToUpper();
									var block_name = block_components[1];

									LogBlock logBlock = new(
										datetime,
										instance,
										block_tag,
										block_name);

									logBlocks[block_tag] = logBlock;
								}

								// stop block
								if (type == "SB")
								{
									var block_tag = data.ToUpper();

									if (logBlocks.TryGetValue(block_tag, out LogBlock logBlockStop))
									{
										logBlockStop.StopBlock(datetime);

										logBlocks[block_tag] = logBlockStop;
									}
									else
									{
										throw new Exception($"Block not found during Stop Block dictionary lookup");
									}
								}

								// error
								if (type == "E")
								{
									LogError logError = new()
									{
										Timestamp = datetime,
										Source = source,
										Function = function,
										Id = instance,
										Message = data
									};

									logErrors.Add(logError);
								}

								// metrics
								if (type == "M")
								{
									var metric_components = data.Split('$');
									var metric_type = metric_components[0];
									var key = metric_components[1];
									var value = metric_components[2];

									LogMetric logMetric = new LogMetric
									{
										Timestamp = datetime,
										Source = source,
										Function = function,
										Id = instance,
										Type = metric_type,
										Key = key,
										Value = value
									};

									logMetrics.Add(logMetric);
								}

								// activation
								if (type == "A")
								{
									normal_log_type = false;

									_sqlProvider.InsertActivation(datetime, instance, data);
								}

								// critical
								if (type == "C")
								{
									normal_log_type = false;

									LogError logError = new()
									{
										Timestamp = datetime,
										Id = instance
									};

									var activation_components = data.Split('$');
									logError.Source = activation_components[0];

									// clear preffix data
									var activation_position = activation_components[0].Count() + 1;

									// log data contents
									logError.Message = data.Remove(0, activation_position);

									_sqlProvider.InsertCritical(logError);
								}
							}



							// ------



							// if the log isn't activation or critical
							// act as a normal logging event that contains instance, blocks, errors, metrics, etc.
							if (normal_log_type)
							{
								// insert instance with duration into sql, add source and function
								if (!logInstance.Result)
								{
									var error_message = $"INSTANCE FAILURE";

									var error_log = CreateErrorLog(
										logInstance.Start,
										source,
										function,
										instance,
										error_message);

									logErrors.Add(error_log);
								}

								// insert blocks with duration
								foreach (var logBlock in logBlocks)
								{
									if (!logBlock.Value.Result)
									{
										var error_message = $"BLOCK FAILURE";

										var error_log = CreateErrorLog(
											logBlock.Value.Start,
											source,
											function,
											instance,
											error_message);

										logErrors.Add(error_log);
									}

									_sqlProvider.InsertBlock(logBlock.Value);
								}

								logInstance.AddErrorCount(logErrors.Count);

								_sqlProvider.InsertInstance(logInstance);

								// insert errors to sql
								foreach (var logError in logErrors)
								{
									_sqlProvider.InsertError(logError);
								}

								// insert metrics to sql
								foreach (var logMetric in logMetrics)
								{
									_sqlProvider.InsertMetric(logMetric);
								}
							}
						}
						catch (Exception ex)
						{
							if (isMultiLog)
							{
								// log error as multi log index
								klog.Error($"{logSet.Key.ToUpper()}.{log.Key.ToUpper()} EXCEPTION: {ex}");
							}
							else
							{
								_sqlProvider.InsertQuarantine(DateTime.UtcNow, logSet.Key);

								_logProvider.UpdateTag(logSet.Key, "upload", "quarantine");

								klog.Error($"{logSet} EXCEPTION: {ex}");
							}
						}

						logCount++;
					}

					_logProvider.UpdateTag(logSet.Key, "upload", "archive");
				}

				klog.Metric("kload_log_cnt", logCount);
			}
			catch (Exception ex)
			{
				klog.Error($"SESSION EXCEPTION: {ex}");

				return false;
			}

			return true;
		}

		/// <summary>
		/// Create Error event inline with KLOG processing
		/// </summary>
		private static LogError CreateErrorLog(
			DateTime datetime,
			string source,
			string function,
			string id,
			string data)
		{
			LogError logError = new()
			{
				Timestamp = datetime,
				Source = source,
				Function = function,
				Id = id,
				Message = data
			};

			return logError;
		}
	}
}
