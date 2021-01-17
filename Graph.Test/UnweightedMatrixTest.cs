using System;
using Graphs;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graph.Test
{
    [TestClass]
    public class UnweightedMatrixTest
    {
        #region GraphInGeneral

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void GraphLessZeroPeaks()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, -3);
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void GraphOrLessZeroPeaks()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, -3);
        }

        #endregion

        #region AddArc, ContainsArc

        [TestMethod]
        public void AddArc()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(2, 1);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsFalse(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));

            graph.AddArc(2, 0);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
        }

        [TestMethod]
        public void AddArcOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);
            graph.AddArc(2, 1);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsFalse(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));

            graph.AddArc(2, 0);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsFalse(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddRepeatArcReverseEx()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 2);
            graph.AddArc(0, 1);
            graph.AddArc(1, 0);
        }

        [TestMethod]
        public void AddArcTwoDirOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 2);
            graph.AddArc(0, 1);
            graph.AddArc(1, 0);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddRepeatArc()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(0, 1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void AddRepeatArcOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);
            graph.AddArc(0, 1);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void AddArcNotExistPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(4, 1);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void AddArcNotExistPeakOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(4, 1);
        }

        #endregion

        #region PeakCount

        [TestMethod]
        public void PeakCount()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            Assert.AreEqual(3, graph.PeakCount);
        }

        [TestMethod]
        public void PeakCountOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            Assert.AreEqual(3, graph.PeakCount);
        }

        [TestMethod]
        public void PeakCountAfterAdd()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            Assert.AreEqual(3, graph.PeakCount);
            graph.AddPeak();
            Assert.AreEqual(4, graph.PeakCount);
        }

        [TestMethod]
        public void PeakCountAfterAddOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            Assert.AreEqual(3, graph.PeakCount);
            graph.AddPeak();
            Assert.AreEqual(4, graph.PeakCount);
        }

        [TestMethod]
        public void PeakCountAfterDel()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            Assert.AreEqual(3, graph.PeakCount);
            graph.RemovePeak(2);
            Assert.AreEqual(2, graph.PeakCount);
        }

        [TestMethod]
        public void PeakCountAfterDelOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            Assert.AreEqual(3, graph.PeakCount);
            graph.RemovePeak(2);
            Assert.AreEqual(2, graph.PeakCount);
        }

        #endregion

        #region DeleteArc

        [TestMethod]
        public void DeleteArc()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);

            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(1, 2);
            graph.AddArc(1, 3);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 3));
            Assert.IsTrue(graph.ContainsArc(3, 1));
            Assert.IsFalse(graph.ContainsArc(0, 3));
            Assert.IsFalse(graph.ContainsArc(3, 0));
            Assert.IsFalse(graph.ContainsArc(2, 3));
            Assert.IsFalse(graph.ContainsArc(3, 2));

            graph.DeleteArc(2, 1);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsFalse(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 3));
            Assert.IsTrue(graph.ContainsArc(3, 1));
            Assert.IsFalse(graph.ContainsArc(0, 3));
            Assert.IsFalse(graph.ContainsArc(3, 0));
            Assert.IsFalse(graph.ContainsArc(2, 3));
            Assert.IsFalse(graph.ContainsArc(3, 2));

            graph.DeleteArc(0, 1);

            Assert.IsFalse(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsFalse(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 3));
            Assert.IsTrue(graph.ContainsArc(3, 1));
            Assert.IsFalse(graph.ContainsArc(0, 3));
            Assert.IsFalse(graph.ContainsArc(3, 0));
            Assert.IsFalse(graph.ContainsArc(2, 3));
            Assert.IsFalse(graph.ContainsArc(3, 2));
        }

        [TestMethod]
        public void DeleteArcOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);

            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(1, 2);
            graph.AddArc(1, 3);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsFalse(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 3));
            Assert.IsFalse(graph.ContainsArc(3, 1));
            Assert.IsFalse(graph.ContainsArc(0, 3));
            Assert.IsFalse(graph.ContainsArc(3, 0));
            Assert.IsFalse(graph.ContainsArc(2, 3));
            Assert.IsFalse(graph.ContainsArc(3, 2));

            graph.DeleteArc(1, 2);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsFalse(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 3));
            Assert.IsFalse(graph.ContainsArc(3, 1));
            Assert.IsFalse(graph.ContainsArc(0, 3));
            Assert.IsFalse(graph.ContainsArc(3, 0));
            Assert.IsFalse(graph.ContainsArc(2, 3));
            Assert.IsFalse(graph.ContainsArc(3, 2));

            graph.DeleteArc(0, 1);

            Assert.IsFalse(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsFalse(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 3));
            Assert.IsFalse(graph.ContainsArc(3, 1));
            Assert.IsFalse(graph.ContainsArc(0, 3));
            Assert.IsFalse(graph.ContainsArc(3, 0));
            Assert.IsFalse(graph.ContainsArc(2, 3));
            Assert.IsFalse(graph.ContainsArc(3, 2));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteArcIncorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);

            graph.AddArc(0, 1);
            graph.DeleteArc(1, 2);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DeleteArcIncorrectOrInverse()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 2);

            graph.AddArc(0, 1);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));

            graph.DeleteArc(1, 0);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteArcNotExistPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.DeleteArc(4, 1);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteArcNotExistPeakOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.DeleteArc(4, 1);
        }

        #endregion

        #region AddPeak

        [TestMethod]
        public void AddPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            Assert.AreEqual(3, graph.PeakCount);
            graph.AddPeak();
            Assert.AreEqual(4, graph.PeakCount);
            graph.AddPeak();
            graph.AddPeak();
            Assert.AreEqual(6, graph.PeakCount);
        }

        [TestMethod]
        public void AddPeakOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            Assert.AreEqual(3, graph.PeakCount);
            graph.AddPeak();
            Assert.AreEqual(4, graph.PeakCount);
            graph.AddPeak();
            graph.AddPeak();
            Assert.AreEqual(6, graph.PeakCount);
        }

        #endregion

        #region RemovePeak

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemovePeakRangeExMore()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.RemovePeak(4);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemovePeakOrRangeExMore()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.RemovePeak(4);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemovePeakRangeExLess()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.RemovePeak(-1);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemovePeakOrRangeExLess()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.RemovePeak(-1);
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemovePeakOrRangeException()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.RemovePeak(4);
            graph.RemovePeak(-1);
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void RemovePeakLast()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 1);
            graph.RemovePeak(0);
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void RemovePeakOrLast()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 1);
            graph.RemovePeak(0);
        }

        [TestMethod]
        public void RemovePeakZero()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);
            graph.RemovePeak(0);
        }

        [TestMethod]
        public void RemovePeakOrZero()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);
            graph.RemovePeak(0);
        }

        [TestMethod]
        public void RemovePeakMiddle()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);
            graph.RemovePeak(2);
        }

        [TestMethod]
        public void RemovePeakOrMiddle()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);
            graph.RemovePeak(2);
        }

        [TestMethod]
        public void RemovePeakMax()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);
            graph.RemovePeak(3);
        }

        [TestMethod]
        public void RemovePeakOrMax()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);
            graph.RemovePeak(3);
        }

        [TestMethod]
        public void RemovePeakMany()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);
            graph.RemovePeak(3);
            Assert.AreEqual(3, graph.PeakCount);
            graph.RemovePeak(1);
            Assert.AreEqual(2, graph.PeakCount);
            graph.RemovePeak(0);
            Assert.AreEqual(1, graph.PeakCount);
        }

        [TestMethod]
        public void RemovePeakOrMany()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);
            graph.RemovePeak(3);
            Assert.AreEqual(3, graph.PeakCount);
            graph.RemovePeak(1);
            Assert.AreEqual(2, graph.PeakCount);
            graph.RemovePeak(0);
            Assert.AreEqual(1, graph.PeakCount);
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void RemovePeakAll()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);
            graph.RemovePeak(3);
            Assert.AreEqual(3, graph.PeakCount);
            graph.RemovePeak(1);
            Assert.AreEqual(2, graph.PeakCount);
            graph.RemovePeak(0);
            Assert.AreEqual(1, graph.PeakCount);
            graph.RemovePeak(0);
            Assert.AreEqual(0, graph.PeakCount);
        }

        [TestMethod, ExpectedException(typeof(OverflowException))]
        public void RemovePeakOrAll()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);
            graph.RemovePeak(3);
            Assert.AreEqual(3, graph.PeakCount);
            graph.RemovePeak(1);
            Assert.AreEqual(2, graph.PeakCount);
            graph.RemovePeak(0);
            Assert.AreEqual(1, graph.PeakCount);
            graph.RemovePeak(0);
            Assert.AreEqual(0, graph.PeakCount);
        }

        #endregion

        #region InArcsCount

        [TestMethod]
        public void InArcsCount()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 4);

            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(1, 2);
            graph.AddArc(1, 3);

            Assert.AreEqual(2, graph.InArcsCount(0));
            Assert.AreEqual(3, graph.InArcsCount(1));
            Assert.AreEqual(2, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));

            graph.DeleteArc(2, 1);

            Assert.AreEqual(2, graph.InArcsCount(0));
            Assert.AreEqual(2, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));

            graph.DeleteArc(0, 1);

            Assert.AreEqual(1, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));
        }

        [TestMethod]
        public void InArcsCountOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 4);

            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(1, 2);
            graph.AddArc(1, 3);

            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(2, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));

            graph.DeleteArc(1, 2);

            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));

            graph.DeleteArc(0, 1);

            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(0, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));
        }

        [TestMethod]
        public void InArcsCountDelPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);

            Assert.AreEqual(1, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(0, graph.InArcsCount(2));

            graph.AddArc(1, 2);
            graph.AddArc(0, 2);

            Assert.AreEqual(2, graph.InArcsCount(0));
            Assert.AreEqual(2, graph.InArcsCount(1));
            Assert.AreEqual(2, graph.InArcsCount(2));

            graph.RemovePeak(1);

            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(0));
        }

        [TestMethod]
        public void InArcsCountDelPeakOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 2);
            graph.AddArc(0, 1);
            graph.AddPeak();

            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(0, graph.InArcsCount(2));

            graph.AddArc(1, 2);
            graph.AddArc(0, 2);

            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(2, graph.InArcsCount(2));

            graph.RemovePeak(1);

            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(0, graph.InArcsCount(0));
        }

        #endregion

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
            Assert.AreEqual(1, graph.InArcsCount(0));

            graph.AddArc(1, 2);
            Assert.AreEqual(2, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));

            graph.AddArc(3, 2);
            Assert.AreEqual(2, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(3));

            graph.AddArc(4, 2);
            Assert.AreEqual(3, graph.InArcsCount(2));
            Assert.AreEqual(1, graph.InArcsCount(4));

            graph.AddArc(0, 2);
            Assert.AreEqual(4, graph.InArcsCount(2));
            Assert.AreEqual(2, graph.InArcsCount(0));
        }

        [TestMethod]
        public void IncomingArcsCountOriented()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 5);
            graph.AddArc(0, 1);
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(0, graph.InArcsCount(0));

            graph.AddArc(1, 2);
            Assert.AreEqual(1, graph.InArcsCount(1));
            Assert.AreEqual(1, graph.InArcsCount(2));

            graph.AddArc(3, 2);
            Assert.AreEqual(2, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(3));

            graph.AddArc(4, 2);
            Assert.AreEqual(3, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(4));

            graph.AddArc(0, 2);
            Assert.AreEqual(4, graph.InArcsCount(2));
            Assert.AreEqual(0, graph.InArcsCount(0));
        }
    }
}
