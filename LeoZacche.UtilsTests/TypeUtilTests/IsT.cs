using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LeoZacche.Utils.UnitTests.TypeUtilTests
{
    [TestClass]
    public class IsT
    {
        [TestMethod]
        public void Converter_string_123_para_int_retorna_true()
        {
            // Arrange
            var esperado = true;

            // Act
            var resultado = TypeUtil.Is<int>("123");

            // Assert
            Assert.AreEqual(esperado, resultado);
        }

    }
}
