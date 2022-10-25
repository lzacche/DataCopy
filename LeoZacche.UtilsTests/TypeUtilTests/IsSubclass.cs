using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LeoZacche.Utils.UnitTests.TypeUtilTests
{
    [TestClass]
    public class IsSubclass
    {
        [TestMethod]
        public void Testar_TypeUtil_subclass_de_object_retorna_true()
        {
            // Arrange
            var esperado = true;

            // Act
            var resultado = TypeUtil.IsSubclass(typeof(object), typeof(TypeUtil));

            // Assert
            Assert.AreEqual(esperado, resultado);
        }

    }
}
