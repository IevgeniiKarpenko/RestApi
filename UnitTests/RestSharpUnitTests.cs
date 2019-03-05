using System;
using RestSharpTestApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RestSharpUnitTests
    {
        private DataModel Model;

        [TestInitialize]
        public void TestInitialize()
        {
            Model = new DataModel();
        }

        [TestMethod]
        public void SendRequest_Test()
        {
            Model.SendRequest(DateTime.Now);

            Assert.IsFalse(string.IsNullOrEmpty(Model.Result));
        }

        [TestMethod]
        public void ProccessResponce_emptyResult_Test()
        {
            Model.ProccessResponce();

            Assert.IsTrue(string.IsNullOrEmpty(Model.Output));
            Assert.AreEqual( 0, Model.Data.Rows.Count);
        }

        [TestMethod]
        public void ProccessResponce_notxml_Test()
        {
            Model.Result = "NOT_XML";
            Model.ProccessResponce();

            Assert.IsTrue(string.IsNullOrEmpty(Model.Output));
            Assert.AreEqual(0, Model.Data.Rows.Count);
        }

        [TestMethod]
        public void ProccessResponce_Test()
        {
            int numberOfUniqueSymbols = 6;
            string value = "asd1223a"; 
            Model.Result = "<OutputValue>"+value+"</OutputValue>";

            Model.ProccessResponce();

            Assert.IsFalse(string.IsNullOrEmpty(Model.Output));
            Assert.AreEqual(numberOfUniqueSymbols, Model.Data.Rows.Count);
        }
    }
}
