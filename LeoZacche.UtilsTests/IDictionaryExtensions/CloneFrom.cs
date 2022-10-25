﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeoZacche.Utils.UnitTests.IDictionaryExtensions
{
    [TestClass]
    public class CloneFrom
    {
        [TestMethod]
        public void ClonarDicionarioNaoInstanciadoGeraException()
        {
            // Arrange
            var esperado = true;
            IDictionary<int, string> origem = null;
            IDictionary<int, string> destino = null;

            // Act
            destino.CloneFrom(origem);

            // Assert
            Assert.AreEqual(esperado, esperado);
        }

        [TestMethod]
        public void ClonarDicionarioVazioRetornaOutraInstanciaDicionarioVazio()
        {
            // Arrange
            var origem = new Dictionary<int, string>();
            var destino = new Dictionary<int, string>();

            // Act
            destino.CloneFrom(origem);

            // Assert
            Assert.AreEqual(origem.Count, destino.Count);
            foreach (var itemOrigem in origem)
            {
                CollectionAssert.Contains(destino.Keys, itemOrigem.Key);
                Assert.AreEqual(destino[itemOrigem.Key], itemOrigem.Value);
            }
        }

        [TestMethod]
        public void ClonarDicionarioComDoisElementosRetornaOutraInstanciaDicionarioComDoisElementos()
        {
            // Arrange
            var origem = new Dictionary<int, string>();
            var destino = new Dictionary<int, string>();
            origem.Add(1, "um");
            origem.Add(2, "dois");
            destino.Add(3, "três");
            destino.Add(4, "quatro");
            destino.Add(5, "cinco");

            // Act
            destino.CloneFrom(origem);

            // Assert
            Assert.AreEqual(origem.Count, destino.Count);
            foreach (var itemOrigem in origem)
            {
                CollectionAssert.Contains(destino.Keys, itemOrigem.Key);
                Assert.AreEqual(destino[itemOrigem.Key], itemOrigem.Value);
                Assert.AreNotSame(destino[itemOrigem.Key], itemOrigem.Value);
            }
        }
    }
}
