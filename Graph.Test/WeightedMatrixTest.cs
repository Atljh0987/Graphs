using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System;
using System.Collections.Generic;

namespace Graph.Test
{
    [TestClass]
    public class WeightedMatrixTest
    {
        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void AddArcRangeExceptionMatrix()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 2);
            Assert.IsFalse(graph.Oriented);

            Assert.AreEqual(2, graph.PeakCount);

            graph.AddArc(0, 2, 2);
        }

        [TestMethod, ExpectedException(typeof(KeyNotFoundException))]
        public void AddArcRangeExceptionOrientedMatrix()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 2);
            Assert.IsTrue(graph.Oriented);


            Assert.AreEqual(2, graph.PeakCount);

            graph.AddArc(0, 1, 2);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.AreEqual(2, graph.GetWeight(0, 1));
            Assert.AreEqual(2, graph.GetWeight(1, 0));
        }

        [TestMethod]
        public void TwoPeaksAddArcMatrix()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 2);
            Assert.IsFalse(graph.Oriented);

            Assert.AreEqual(2, graph.PeakCount);

            graph.AddArc(0, 1, 2);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.AreEqual(2, graph.GetWeight(0, 1));
            Assert.AreEqual(2, graph.GetWeight(1, 0));

            graph.AddArc(1, 0, 4);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.AreEqual(4, graph.GetWeight(0, 1));
            Assert.AreEqual(4, graph.GetWeight(1, 0));
        }

        [TestMethod]
        public void ThreePeaksAddArcMatrix()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 3);

            graph.AddArc(0, 1, 32);
            graph.AddArc(1, 0, -32);
            graph.AddArc(1, 2, -64);
            graph.AddArc(2, 1, 64);
            graph.AddArc(0, 2, 128);

            Assert.IsFalse(graph.Oriented);
            Assert.AreEqual(3, graph.PeakCount);

            Assert.AreEqual(-32, graph.GetWeight(1, 0));
            Assert.AreEqual(-32, graph.GetWeight(0, 1));
            Assert.AreEqual(64, graph.GetWeight(1, 2));
            Assert.AreEqual(64, graph.GetWeight(2, 1));
            Assert.AreEqual(128, graph.GetWeight(0, 2));
            Assert.AreEqual(128, graph.GetWeight(2, 0));

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(2, 1));
        }

        [TestMethod]
        public void ThreePeaksAddArcOrientedMatrix()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 3);
            Assert.IsTrue(graph.Oriented);

            graph.AddArc(0, 1, 32);
            graph.AddArc(1, 0, -32);
            graph.AddArc(1, 2, -64);
            graph.AddArc(2, 1, 64);
            graph.AddArc(0, 2, 128);
            graph.AddArc(2, 0, 256);

            Assert.AreEqual(3, graph.PeakCount);

            Assert.AreEqual(32, graph.GetWeight(0, 1));
            Assert.AreEqual(-32, graph.GetWeight(1, 0));
            Assert.AreEqual(-64, graph.GetWeight(1, 2));
            Assert.AreEqual(64, graph.GetWeight(2, 1));
            Assert.AreEqual(128, graph.GetWeight(0, 2));
            Assert.AreEqual(256, graph.GetWeight(2, 0));


            //Assert.IsFalse(graph.ContainsArc(0, 0));
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            //Assert.IsFalse(graph.ContainsArc(1, 1));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            //Assert.IsFalse(graph.ContainsArc(2, 2));
        }

        // Матричные тесты

        [TestMethod]
        public void OutComingArcsOriented()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 2);
            graph.AddArc(0, 1, 234);
            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.Oriented);
        }

        [TestMethod]
        public void OutComingArcsUnoriented()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 2);
            graph.AddArc(0, 1, 234);
            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.Oriented);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void IncorrectPeak()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 1);
            graph.AddArc(0, 1, 234);
        }

        [TestMethod]
        public void IncomingArcsRemovePeak()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 3);
            graph.AddArc(0, 2, 234);
            graph.AddArc(1, 0, 235);
            graph.AddArc(1, 2, 236);

            Assert.IsTrue(graph.ContainsArc(0, 2));

            int[] ans = new int[] { 234, 236 };
            int ind = 0;

            foreach (var el in graph.InComingArcs(2))
            {
                Assert.IsTrue(el.Item2 == ans[ind]);
                ind++;
            }

            ind = 0;

            graph.RemovePeak(2);

            foreach (var el in graph.InComingArcs(0))
                Assert.AreEqual(235, el.Item2);

            foreach (var el in graph.InComingArcs(1))
                Assert.AreEqual(235, el.Item2);
        }

        [TestMethod]
        public void OutgoingArcsRemovePeak()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 3);
            graph.AddArc(0, 2, 234);
            graph.AddArc(1, 0, 235);
            graph.AddArc(1, 2, 236);

            Assert.IsTrue(graph.ContainsArc(0, 2));

            int[] ans = new int[] { 235, 236 };
            int ind = 0;

            foreach (var el in graph.OutGoingArcs(1))
            {
                Assert.IsTrue(el.Item2 == ans[ind]);
                ind++;
            }

            ind = 0;

            Assert.IsTrue(graph.ContainsArc(0, 2));

            graph.RemovePeak(2);

            foreach (var el in graph.OutGoingArcs(1))
                Assert.AreEqual(235, el.Item2);

            foreach (var el in graph.OutGoingArcs(0))
                Assert.AreEqual(235, el.Item2);
        }

        [TestMethod]
        public void IncomingArcsCount()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 5);
            graph.AddArc(0, 1, 234);
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(0));

            graph.AddArc(1, 2, 234);
            Assert.AreEqual(2, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));

            graph.AddArc(3, 2, 234);
            Assert.AreEqual(2, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));

            graph.AddArc(4, 2, 234);
            Assert.AreEqual(3, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(4));

            graph.AddArc(0, 2, 234);
            Assert.AreEqual(4, graph.InArcsCount(2));
            Assert.AreEqual(2, graph.InArcsCount(0));
        }

        [TestMethod]
        public void IncomingArcsCountOriented()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(true, 5);
            graph.AddArc(0, 1, 234);
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(0, graph.InArcsCount(0));

            graph.AddArc(1, 2, 234);
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));

            graph.AddArc(3, 2, 234);
            Assert.AreEqual(2, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(3));

            graph.AddArc(4, 2, 234);
            Assert.AreEqual(3, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(4));

            graph.AddArc(0, 2, 234);
            Assert.AreEqual(4, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(0));
        }
    }
}
