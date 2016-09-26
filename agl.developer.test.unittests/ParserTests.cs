using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using agl.developer.test.parser;

namespace agl.developer.test.unittests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckEmptyParams()
        {
            AglPetOwnerParser p = new AglPetOwnerParser("");
            Assert.IsTrue(p.ParseAndPrint(""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckNullParams()
        {
            AglPetOwnerParser p = new AglPetOwnerParser("");
            Assert.IsTrue(p.ParseAndPrint(null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckInvalidParams()
        {
            AglPetOwnerParser p = new AglPetOwnerParser("");
            Assert.IsTrue(p.ParseAndPrint("abc"));
        }

        [TestMethod]
        public void CheckValidParams()
        {
            AglPetOwnerParser p = new AglPetOwnerParser("cat");
            var result = p.ParseAndPrint(@"[{
name: ""Bob"",
gender: ""Ma:le"",
age: 23,
pets: [
{name: ""Garfield"",type: ""Cat""},
{name: ""Fido"",type: ""Dog""}]
}]");

            Assert.IsTrue(result);
            Assert.AreEqual("Ma:le\r\n\tGarfield\r\n", p.Result.ToString());

            p = new AglPetOwnerParser("dog");
            result = p.ParseAndPrint(@"[{
name: ""Bob"",
gender: ""Ma:le"",
age: 23,
pets: [
{name: ""Garfield"",type: ""Cat""},
{name: ""Fido"",type: ""Dog""}]
}]");

            Assert.IsTrue(result);
            Assert.AreEqual("Ma:le\r\n\tFido\r\n", p.Result.ToString());
        }
    }
}
