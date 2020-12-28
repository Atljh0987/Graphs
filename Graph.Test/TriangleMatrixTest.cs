using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Graphs;

namespace Graph.Test
{
    /// <summary>
    /// Сводное описание для TriangleMatrixTest
    /// </summary>
    [TestClass]
    public class TriangleMatrixTest
    {
        [TestMethod]
        public void TriangleMatrixLength()
        {
            var matrix = new TriangleMatrix<int>(5);
            Assert.AreEqual(5, matrix.Length);
        }

        [TestMethod]
        public void TrianglesMatrixValuesFive()
        {
            var matrix = new TriangleMatrix<int>(5);
            matrix[0, 1] = 1;
            matrix[0, 2] = 2;
            matrix[0, 3] = 3;
            matrix[0, 4] = 4;
            matrix[1, 2] = 5;
            matrix[1, 3] = 6;
            matrix[1, 4] = 7;
            matrix[2, 3] = 8;
            matrix[2, 4] = 9;
            matrix[3, 4] = 10;

            Assert.AreEqual(1, matrix[0, 1]);
            Assert.AreEqual(2, matrix[0, 2]);
            Assert.AreEqual(3, matrix[0, 3]);
            Assert.AreEqual(4, matrix[0, 4]);
            Assert.AreEqual(5, matrix[1, 2]);
            Assert.AreEqual(6, matrix[1, 3]);
            Assert.AreEqual(7, matrix[1, 4]);
            Assert.AreEqual(8, matrix[2, 3]);
            Assert.AreEqual(9, matrix[2, 4]);
            Assert.AreEqual(10, matrix[3, 4]);
        }

        [TestMethod]
        public void TrianglesMatrixValuesFour()
        {
            var matrix = new TriangleMatrix<int>(4);
            matrix[0, 1] = 1;
            matrix[0, 2] = 2;
            matrix[0, 3] = 3;
            matrix[1, 2] = 5;
            matrix[1, 3] = 6;
            matrix[2, 3] = 8;

            Assert.AreEqual(1, matrix[0, 1]);
            Assert.AreEqual(2, matrix[0, 2]);
            Assert.AreEqual(3, matrix[0, 3]);
            Assert.AreEqual(5, matrix[1, 2]);
            Assert.AreEqual(6, matrix[1, 3]);
            Assert.AreEqual(8, matrix[2, 3]);
        }

        [TestMethod]
        public void TrianglesMatrixValuesXYInvert()
        {
            var matrix = new TriangleMatrix<int>(5);
            matrix[1, 0] = 1;
            matrix[2, 0] = 2;
            matrix[3, 0] = 3;
            matrix[4, 0] = 4;
            matrix[2, 1] = 5;
            matrix[3, 1] = 6;
            matrix[4, 1] = 7;
            matrix[3, 2] = 8;
            matrix[4, 2] = 9;
            matrix[4, 3] = 10;

            Assert.AreEqual(1, matrix[0, 1]);
            Assert.AreEqual(2, matrix[0, 2]);
            Assert.AreEqual(3, matrix[0, 3]);
            Assert.AreEqual(4, matrix[0, 4]);
            Assert.AreEqual(5, matrix[1, 2]);
            Assert.AreEqual(6, matrix[1, 3]);
            Assert.AreEqual(7, matrix[1, 4]);
            Assert.AreEqual(8, matrix[2, 3]);
            Assert.AreEqual(9, matrix[2, 4]);
            Assert.AreEqual(10, matrix[3, 4]);
        }

        [TestMethod]
        public void TrianglesMatrixValuesAssertInvert()
        {
            var matrix = new TriangleMatrix<int>(5);
            matrix[0, 1] = 1;
            matrix[0, 2] = 2;
            matrix[0, 3] = 3;
            matrix[0, 4] = 4;
            matrix[1, 2] = 5;
            matrix[1, 3] = 6;
            matrix[1, 4] = 7;
            matrix[2, 3] = 8;
            matrix[2, 4] = 9;
            matrix[3, 4] = 10;

            Assert.AreEqual(1, matrix[1, 0]);
            Assert.AreEqual(2, matrix[2, 0]);
            Assert.AreEqual(3, matrix[3, 0]);
            Assert.AreEqual(4, matrix[4, 0]);
            Assert.AreEqual(5, matrix[2, 1]);
            Assert.AreEqual(6, matrix[3, 1]);
            Assert.AreEqual(7, matrix[4, 1]);
            Assert.AreEqual(8, matrix[3, 2]);
            Assert.AreEqual(9, matrix[4, 2]);
            Assert.AreEqual(10, matrix[4, 3]);
        }
    }
}
