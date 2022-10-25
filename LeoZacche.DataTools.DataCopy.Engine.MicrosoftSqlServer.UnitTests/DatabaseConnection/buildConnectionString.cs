using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;
using LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer.UnitTests.DatabaseConnection
{
    [TestClass()]
    public class buildConnectionString
    {
        [TestMethod()]
        public void buildConnectionStringTest()
        {
            // Arrange
            var esperado = "Server=servername; User ID=username; Password=password; ";
            var x = new LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer.DatabaseConnection();
            string server = "servername";
            ConnectionAuthenticationEnum authType = ConnectionAuthenticationEnum.NamedUser;
            string username = "username";
            string password = "password";

            // Act
            var resultado = x.buildConnectionString(server, authType, username, password);

            // Asset
            Assert.AreEqual(esperado, resultado);
        }
    }
}
