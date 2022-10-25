using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LeoZacche.Utils.UnitTests.TypeUtilTests
{
    [TestClass]
    public class GetDefaultValue
    {
        [TestMethod]
        public void Get_default_de_int_retorna_zero()
        {
            // Arrange
            var esperado = 0;

            // Act
            var resultado = TypeUtil.GetDefaultValue(typeof(int));

            // Assert
            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void Get_default_de_nullable_int_retorna_null()
        {
            // Arrange
            int? esperado = null;

            // Act
            var resultado = TypeUtil.GetDefaultValue(typeof(int?));

            // Assert
            Assert.AreEqual(esperado, resultado);
        }
    }
}
