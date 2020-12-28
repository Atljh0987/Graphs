using System;
using Graphs;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graph.Test
{
    [TestClass]
    public class UnweightedListTest
    {
        [TestMethod]
        public void OutComingArcsOriented()
        {
            UnweightedList graph = new UnweightedList(true);
            graph.AddPeak();
            graph.AddPeak();
            graph.AddArc(0, 1);
            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.Oriented);
        }

        [TestMethod]
        public void OutComingArcsUnoriented()
        {
            UnweightedList graph = new UnweightedList(false);
            graph.AddPeak();
            graph.AddPeak();
            graph.AddArc(0, 1);
            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.Oriented);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IncorrectPeak()
        {
            UnweightedList graph = new UnweightedList(false);
            graph.AddPeak();
            graph.AddArc(0, 1);            
        }
    }
}
