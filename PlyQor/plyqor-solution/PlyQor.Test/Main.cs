global using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlyQor.Storage.Model;
using PlyQor.Storage.Models;
using PlyQor.Test.Mock;
using PlyQor.Test.Utility;
using System.ComponentModel;

namespace PlyQor.Test
{
    [TestClass]
    public class Main
    {
        [TestMethod]
        public void TestRequestManager()
        {
            RequestManager requestManager = new RequestManager();

            string input = MockData.SetupRequest();

            var request = requestManager.GetDictionaryFromString(input);

            var containers = requestManager.GetDictionariesFromDictionary(request, "data");

            Assert.AreEqual(3, containers.Count);
            Assert.IsTrue(containers.ContainsKey("testcontainer1"));

            var record = containers["testcontainer2"];

            var value = record["retention"];

            Assert.AreEqual("7", value);
        }

        [TestMethod]
        public void TestValiator()
        {
            var mockRequest = new Dictionary<string, string>()
            {
                { "Container", "TestContainer" },
                { "Token", "password1234" }
            };

            var validator = new Validator(new MockStorage());

            var result = validator.ValidateToken(mockRequest);

            Assert.AreEqual(true, result.result);
            Assert.AreEqual("OK", result.code);

            var containers = MockData.SetupContainersConfig();

            result = validator.ValidateConfiguration(containers);

            Assert.AreEqual(true, result.result);

            // TODO: add new operation method test

            // get missing container name

            // get 20+ char container name
        }

        [TestMethod]
        public void TestPartitionManager()
        {
            var partitionManager = new PartitionManager();

            var expectedCutOffWindow = DataUtility.GetWindow(-3);

            var cutoffWindow = partitionManager.CutOffWindow(-3);
            Assert.AreEqual(expectedCutOffWindow, cutoffWindow);

            cutoffWindow = partitionManager.CutOffWindow(3);
            Assert.AreEqual(expectedCutOffWindow, cutoffWindow);

            var currentFutureWindows = DataUtility.GetWindows(1, 10, true, true);
            var expectedMissingFutureWindow = DataUtility.GetWindow(10);

            var futureWindows = partitionManager.FutureWindows();

            var missingFutureWindows = partitionManager.FindMissingWindows(currentFutureWindows, futureWindows);

            Assert.AreEqual(1, missingFutureWindows.Count());
            Assert.AreEqual(expectedMissingFutureWindow, missingFutureWindows[0]);

            var currentRetentionWindows = DataUtility.GetWindows(3, -4, false, false);
            var retnetionCutOffWindow = DataUtility.GetWindow(-2);
            var expectedRetentionWindow = DataUtility.GetWindow(-3);

            var retentionWindows = partitionManager.SelectRetentionWindows(currentRetentionWindows, retnetionCutOffWindow);

            Assert.AreEqual(1, retentionWindows.Count());
            Assert.AreEqual(expectedRetentionWindow, retentionWindows[0]);
        }

        [TestMethod]
        public void TestOperationSelector()
        {
            var operationSelector = new OperationSelector();

            var currentContainerConfig = MockData.SetupContainersConfig();

            operationSelector.SetCurrentContainerConfiguration(currentContainerConfig);

            var newContainerConfig = MockData.SetupOperationSelectionContainers();

            Dictionary<string, string> results = new();
            
            // TODO: redo test
            //foreach (var containerConfig in newContainerConfig)
            //{
            //    var result = operationSelector.SelectContainerStatus(containerConfig.Key, containerConfig.Value);

            //    results[containerConfig.Key] = result;
            //}

            //var deleteContainers = operationSelector.GetContainersToDelete();

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("modified", results["testcontainer1"]);
            Assert.AreEqual("modified", results["testcontainer2"]);
            Assert.AreEqual("new", results["testcontainer4"]);

            //Assert.AreEqual(1, deleteContainers.Count());
            //Assert.IsTrue(deleteContainers.Contains("testcontainer3"));
            var test = true;
        }

        [TestMethod]
        public void TestMaintanceManager()
        {
            var mockWindows = MockData.SetupContainerWindows();

            var storageManager = new MockStorage(mockWindows);

            var partitionManager = new PartitionManager();

            var maintenanceManager = new MaintenanceManager(storageManager, partitionManager);

            var containers = MockData.SetupContainersConfig();

            Dictionary<string, int> results = new();
            foreach (var container in containers)
            {
                if (container.Value.TryGetValue("retention", out var value))
                {
                    int.TryParse(value, out var retentionValue);

                    results[container.Key] = retentionValue;
                }
            }

            var partitionCheckResult = maintenanceManager.PartitionCheck(results);

            var retentionResult = maintenanceManager.Retention(results);

            // ensure future windows are 10

            // ensure past windows match retention policy
        }
    }
}