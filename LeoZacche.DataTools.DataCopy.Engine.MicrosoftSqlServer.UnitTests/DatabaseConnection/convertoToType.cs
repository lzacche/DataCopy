using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;
using LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer.UnitTests.DatabaseConnection
{
    [TestClass()]
    public class convertoToType
    {
        [TestMethod()]
        public void convertoToTypeTest()
        {
            // Arrange
            var esperado = typeof(int);
            var x = new LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer.DatabaseConnection();
            string typeToTest = "int";

            // Act
            var resultado = x.convertoToType(typeToTest);

            // Asset
            Assert.AreEqual(esperado, resultado);
        }
    }
}
