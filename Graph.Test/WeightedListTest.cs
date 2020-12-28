using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphs;
using System.Collections.Generic;

namespace Graph.Test
{
    [TestClass]
    public class WeightedListTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddArcRangeException()
        {
            WeightedGraphList<int> graph = new WeightedGraphList<int>(false);
            Assert.IsFalse(graph.Oriented);

            graph.AddPeak();
            Assert.AreEqual(1, graph.PeakCount);
            graph.AddPeak();
            Assert.AreEqual(2, graph.PeakCount);

            graph.AddArc(0, 2, 2);
        }

        [TestMethod, ExpectedException(typeof(KeyNotFoundException))]
        public void AddArcRangeExceptionOriented()
        {
            WeightedGraphList<int> graph = new WeightedGraphList<int>(true);
            Assert.IsTrue(graph.Oriented);

            graph.AddPeak();
            Assert.AreEqual(1, graph.PeakCount);
            graph.AddPeak();
            Assert.AreEqual(2, graph.PeakCount);

            graph.AddArc(0, 1, 2);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.AreEqual(2, graph.GetWeight(0, 1));
            Assert.AreEqual(20, graph.GetWeight(1, 0));
        }

        [TestMethod]
        public void TwoPeaksAddArc()
        {
            WeightedGraphList<int> graph = new WeightedGraphList<int>(false);
            Assert.IsFalse(graph.Oriented);

            graph.AddPeak();
            Assert.AreEqual(1, graph.PeakCount);
            graph.AddPeak();
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
        public void ThreePeaksAddArc()
        {
            WeightedGraphList<int> graph = new WeightedGraphList<int>(false);
            graph.AddPeak();
            graph.AddPeak();
            graph.AddPeak();
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
        public void ThreePeaksAddArcOriented()
        {
            WeightedGraphList<int> graph = new WeightedGraphList<int>(true);
            Assert.IsTrue(graph.Oriented);

            graph.AddPeak();
            graph.AddPeak();
            graph.AddPeak();
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


            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(2, 1));
        }


        // ********************* Matrix 

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
    }
}
