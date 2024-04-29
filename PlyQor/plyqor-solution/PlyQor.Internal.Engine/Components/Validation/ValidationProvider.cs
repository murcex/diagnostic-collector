namespace PlyQor.Engine.Components.Validation
{
	using PlyQor.Engine.Components.Validation.Internals;
	using System.Collections.Generic;

	class ValidationProvider
	{
		/// <summary>
		/// Convert json string to dictionary.
		/// </summary>
		public static Dictionary<string, string> GenerateDictionary(string input)
		{
			return InternalValidationService.GenerateDictionary(input);
		}


		/// <summary>
		/// Check if token is valid.
		/// </summary>
		public static bool CheckToken(string container, string token)
		{
			return InternalValidationService.CheckToken(container, token);
		}

		/// <summary>
		/// Check if operation is valid.
		/// </summary>
		public static bool CheckOperation(string operation)
		{
			return InternalValidationService.CheckOperation(operation);
		}

		/// <summary>
		/// Check if token is valid.
		/// </summary>
		public static bool CheckAdminToken(string token)
		{
			return InternalValidationService.CheckAdminToken(token);
		}

		/// <summary>
		/// Check if operation is valid.
		/// </summary>
		public static bool CheckSystemOperation(string operation)
		{
			return InternalValidationService.CheckSystemOperation(operation);
		}
	}
}
