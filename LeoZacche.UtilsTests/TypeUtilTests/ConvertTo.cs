using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LeoZacche.Utils.UnitTests.TypeUtilTests
{
    [TestClass]
    public class ConvertTo
    {
        [TestMethod]
        public void Converter_string_123_para_int_retorna_123()
        {
            // Arrange
            var esperado = 123;

            // Act
            var resultado = TypeUtil.ConvertTo<int>("123");

            // Assert
            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Converter_string_1a1_para_int_gera_excecao()
        {
            // Arrange
            //var esperado = 123;

            // Act
            var resultado = TypeUtil.ConvertTo<int>("1a1");

            // Assert
            //Assert.AreEqual(esperado, resultado);
        }

    }
}
