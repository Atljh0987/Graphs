using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphs;
using Graphs.Algorithms;
using System.Collections.Generic;

namespace Graph.Algorithm.Test
{
    [TestClass]
    public class TarjanTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InputData()
        {
            UnweightedGraphList graph = new UnweightedGraphList(false);
            Tarjan.Run(graph);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullCheck()
        {
            Tarjan.Run(null);
        }

        [TestMethod]
        public void WithoutArcs()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 5);
            int[] result = Tarjan.Run(graph);

            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
            Assert.AreEqual(2, result[2]);
            Assert.AreEqual(3, result[3]);
            Assert.AreEqual(4, result[4]);
        }

        [TestMethod]
        public void ThreePeaks()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 3);
            graph.AddArc(1, 0, 2);
            graph.AddArc(1, 2, 3);

            graph.AddArc(0, 2, 4);

            Assert.AreEqual(1, Tarjan.Run(new UnweightedGraphWrapper<int>(graph))[0]);
            Assert.AreEqual(0, Tarjan.Run(new UnweightedGraphWrapper<int>(graph))[1]);
            Assert.AreEqual(2, Tarjan.Run(new UnweightedGraphWrapper<int>(graph))[2]);
        }

        [TestMethod]
        public void FivePeaks()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 5);
            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(0, 3);
            graph.AddArc(0, 4);

            graph.AddArc(1, 3);

            graph.AddArc(2, 3);
            graph.AddArc(2, 4);

            graph.AddArc(3, 4);

            Assert.AreEqual(0, Tarjan.Run(graph)[0]);
            Assert.AreEqual(1, Tarjan.Run(graph)[1]);
            Assert.AreEqual(2, Tarjan.Run(graph)[2]);
            Assert.AreEqual(3, Tarjan.Run(graph)[3]);
            Assert.AreEqual(4, Tarjan.Run(graph)[4]);
        }

        [TestMethod]
        public void FivePeaksSeveralScatter()
        {
            // как разобрался, не увидел сложности в таких тестрах, на каждом уровне вершины просто в порядке возрастания            
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 5); 
            graph.AddArc(0, 1);
            graph.AddArc(0, 2);

            graph.AddArc(1, 2);

            graph.AddArc(3, 1);

            graph.AddArc(4, 2);
            graph.AddArc(4, 1);

            Assert.AreEqual(0, Tarjan.Run(graph)[0]);
            Assert.AreEqual(3, Tarjan.Run(graph)[1]);
            Assert.AreEqual(4, Tarjan.Run(graph)[2]);
            Assert.AreEqual(1, Tarjan.Run(graph)[3]);
            Assert.AreEqual(2, Tarjan.Run(graph)[4]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void CycleSix()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 6);
            graph.AddArc(0, 3);
            graph.AddArc(0, 2);

            graph.AddArc(1, 3);

            graph.AddArc(3, 4);
            graph.AddArc(3, 5);
            
            graph.AddArc(4, 1);

            graph.AddArc(5, 1);

            Tarjan.Run(graph);
        }

        [TestMethod]
        public void KahnDifference()
        {       
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 6);
            graph.AddArc(0, 1);
            graph.AddArc(0, 2);

            graph.AddArc(1, 2);

            graph.AddArc(3, 1);

            graph.AddArc(4, 2);
            graph.AddArc(4, 1);

            Assert.AreEqual(0, Tarjan.Run(graph)[0]);
            Assert.AreEqual(3, Tarjan.Run(graph)[1]);
            Assert.AreEqual(4, Tarjan.Run(graph)[2]);
            Assert.AreEqual(1, Tarjan.Run(graph)[3]);
            Assert.AreEqual(2, Tarjan.Run(graph)[4]);
        }
    }
}
