using Newtonsoft.Json;
using PlyQor.Test.Utility;

namespace PlyQor.Test.Mock
{
    public static class MockData
    {
        private static List<string> testContainer1Tokens = new(){ "fabc37eb-0dbb-455a-bbdf-c9853a0b5f36" };

        private static List<string> testContainer2Tokens = new() { "32a2b57e-f676-4ead-acfd-6606fa37435e", "fb299155-7df9-4b0e-a518-82abded478cb" };

        private static List<string> testContainer3Tokens = new() { "e2122416-3e21-440a-96c2-570746d6c023" };

        public static Dictionary<string, Dictionary<string, string>> SetupContainersConfig()
        {
            Dictionary<string, Dictionary<string, string>> containers = new();

            Dictionary<string, string> container1 = new()
            {
                { "name", "testcontainer1" },
                { "tokens", JsonConvert.SerializeObject(testContainer1Tokens) },
                { "retention", "3" }
            };
            containers.Add("testcontainer1", container1);

            Dictionary<string, string> container2 = new()
            {
                { "name", "testcontainer2" },
                { "tokens", JsonConvert.SerializeObject(testContainer2Tokens) },
                { "retention", "7" }
            };
            containers.Add("testcontainer2", container2);

            Dictionary<string, string> container3 = new()
            {
                { "name", "testcontainer3" },
                { "tokens", JsonConvert.SerializeObject(testContainer3Tokens) },
                { "retention", "0" }
            };
            containers.Add("testcontainer3", container3);

            return containers;
        }

        public static Dictionary<string, List<int>> SetupContainerWindows()
        {
            Dictionary<string, List<int>> containers = new()
            {
                { "testcontainer1", DataUtility.GetWindows(-4, 9, true, true) },
                { "testcontainer2", DataUtility.GetWindows(-8, 9, true, true) },
                { "testcontainer3", DataUtility.GetWindows(0, 1, true, true) }
            };

            return containers;
        }

        public static Dictionary<string, List<string>> SetupContainerTokens()
        {
            Dictionary<string, List<string>> tokens = new();

            tokens["TestContainer"] = new List<string>() { "password1234" };

            return tokens;
        }

        public static string SetupRequest()
        {
            var data = JsonConvert.SerializeObject(SetupContainersConfig());

            Dictionary<string, string> request = new()
            {
                { "data", data }
            };

            return JsonConvert.SerializeObject(request);
        }

        public static Dictionary<string, Dictionary<string, string>> SetupOperationSelectionContainers()
        {
            Dictionary<string, Dictionary<string, string>> containers = new();

            // updated retention value
            Dictionary<string, string> container1 = new()
            {
                { "name", "testcontainer1" },
                { "tokens", JsonConvert.SerializeObject(testContainer1Tokens) },
                { "retention", "4" }
            };
            containers.Add("testcontainer1", container1);
            
            // updated tokens value
            Dictionary<string, string> container2 = new()
            {
                { "name", "testcontainer2" },
                { "tokens", JsonConvert.SerializeObject(new List<string>() { Guid.NewGuid().ToString() }) },
                { "retention", "7" }
            };
            containers.Add("testcontainer2", container2);

            // removed testcontainer3 and adding testcontainer4 as a new record
            Dictionary<string, string> container3 = new()
            {
                { "name", "testcontainer4" },
                { "tokens", JsonConvert.SerializeObject(new List<string>() { Guid.NewGuid().ToString() }) },
                { "retention", "0" }
            };
            containers.Add("testcontainer4", container3);

            return containers;
        }
    }
}
