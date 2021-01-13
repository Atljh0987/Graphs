using System;
using Graphs;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graph.Test
{
    [TestClass]
    public class UnweightedMatrixTest
    {
        [TestMethod]
        public void OutComingArcsOriented()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 2);            
            graph.AddArc(0, 1);
            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.Oriented);
        }

        [TestMethod]
        public void OutComingArcsUnoriented()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 2);
            graph.AddArc(0, 1);
            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.Oriented);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void IncorrectPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 1);
            graph.AddArc(0, 1);
        }

        [TestMethod]
        public void IncomingArcsRemovePeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 2);
            graph.AddArc(1, 0);
            graph.AddArc(1, 2);

            Assert.IsTrue(graph.ContainsArc(0, 2));

            int[] ans = new int[] { 0, 1 };
            int ind = 0;

            foreach (var el in graph.InComingArcs(2))
            {
                Assert.IsTrue(el == ans[ind]);
                ind++;
            }

            ind = 0;

            graph.RemovePeak(2);

            foreach (var el in graph.InComingArcs(0))
                Assert.AreEqual(1, el);

            foreach (var el in graph.InComingArcs(1))
                Assert.AreEqual(0, el);
        }

        [TestMethod]
        public void OutgoingArcsRemovePeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 2);
            graph.AddArc(1, 0);
            graph.AddArc(1, 2);

            Assert.IsTrue(graph.ContainsArc(0, 2));

            int[] ans = new int[] { 0, 2 };
            int ind = 0;

            foreach (var el in graph.OutGoingArcs(1))
            {
                Assert.IsTrue(el == ans[ind]);
                ind++;
            }

            ind = 0;

            Assert.IsTrue(graph.ContainsArc(0, 2));

            graph.RemovePeak(2);

            foreach (var el in graph.OutGoingArcs(1))
                Assert.AreEqual(0, el);

            foreach (var el in graph.OutGoingArcs(0))
                Assert.AreEqual(0, el);
        }

        [TestMethod]
        public void IncomingArcsCount()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 5);
            graph.AddArc(0, 1);
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(0, graph.InArcsCount(0));

            graph.AddArc(1, 2);
            Assert.AreEqual(1, graph.InArcsCount(2));

            graph.AddArc(3, 2);
            Assert.AreEqual(2, graph.InArcsCount(2));

            graph.AddArc(4, 2);
            Assert.AreEqual(3, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(4));

            graph.AddArc(0, 2);
            Assert.AreEqual(4, graph.InArcsCount(2));
        }
    }
}
