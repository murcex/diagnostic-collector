using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlyQor.Storage.Test
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void Execute()
        {
            var mockRequest = new Dictionary<string, string>()
            {
                { "Container", "TestContainer" },
                { "Token", "password1234" }
            };

            var validator = new Validator(new MockStorage());

            var result = validator.Validate(mockRequest);

            Assert.AreEqual(true, result.result);
            Assert.AreEqual("OK", result.code);
        }
    }
}
