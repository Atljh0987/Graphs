﻿using System;
using Graphs;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Graph.Test
{
    [TestClass]
    public class UnweightedMatrixTest
    {
        class PeakCodes : List<char>
        {
            public int this[char code] => IndexOf(code);
        }

        // -- Simple

        #region Graph Creation
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

        #region Oriented

        [TestMethod]
        public void Oriented()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 1);
            Assert.IsFalse(graph.Oriented);
        }

        [TestMethod]
        public void OrientedOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 1);
            Assert.IsTrue(graph.Oriented);
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

        #region OutGoingArcs

        [TestMethod]
        public void OutGoingArcs()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            int i = 0;
            int[] zero = { 1, 2 };
            int[] one = { 0, 2 };
            int[] two = { 0, 1 };

            foreach (var e in graph.OutGoingArcs(0))
            {
                Assert.IsTrue(graph.OutGoingArcs(0).Count() == zero.Length);
                Assert.IsTrue(zero[i] == e);
                i++;
            }

            i = 0;

            foreach (var e in graph.OutGoingArcs(1))
            {
                Assert.IsTrue(graph.OutGoingArcs(1).Count() == one.Length);
                Assert.IsTrue(one[i] == e);
                i++;
            }

            i = 0;

            foreach (var e in graph.OutGoingArcs(2))
            {
                Assert.IsTrue(graph.OutGoingArcs(2).Count() == two.Length);
                Assert.IsTrue(two[i] == e);
                i++;
            }
        }

        [TestMethod]
        public void OutGoingArcsOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(1, 0);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            int i = 0;
            int[] zero = { 1, 2 };
            int[] one = { 0, 2 };
            int[] two = { 0 };

            foreach (var e in graph.OutGoingArcs(0))
            {
                Assert.IsTrue(graph.OutGoingArcs(0).Count() == zero.Length);
                Assert.IsTrue(zero[i] == e);
                i++;
            }

            i = 0;

            foreach (var e in graph.OutGoingArcs(1))
            {
                Assert.IsTrue(graph.OutGoingArcs(1).Count() == one.Length);
                Assert.IsTrue(one[i] == e);
                i++;
            }

            i = 0;

            foreach (var e in graph.OutGoingArcs(2))
            {
                Assert.IsTrue(graph.OutGoingArcs(2).Count() == two.Length);
                Assert.IsTrue(two[i] == e);
                i++;
            }
        }

        [TestMethod]
        public void OutGoingArcsEmptyPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);

            Assert.IsTrue(graph.OutGoingArcs(2).Count() == 0);
            foreach (var e in graph.OutGoingArcs(2))
            {
                Assert.AreEqual(0, graph.OutGoingArcs(2));
                Assert.AreEqual(0, e);
            }
        }

        [TestMethod]
        public void OutGoingArcsOrEmptyPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);

            Assert.IsTrue(graph.OutGoingArcs(2).Count() == 0);
            foreach (var e in graph.OutGoingArcs(2))
            {
                Assert.AreEqual(0, graph.OutGoingArcs(2));
                Assert.AreEqual(0, e);
            }
        }

        #endregion

        #region InComingArcs

        [TestMethod]
        public void InComingArcs()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            int i = 0;
            int[] zero = { 1, 2 };
            int[] one = { 0, 2 };
            int[] two = { 0, 1 };

            Assert.IsTrue(graph.InComingArcs(0).Count() == zero.Length);
            foreach (var e in graph.InComingArcs(0))
            {
                Assert.IsTrue(zero[i] == e);
                i++;
            }

            i = 0;

            Assert.IsTrue(graph.InComingArcs(1).Count() == one.Length);
            foreach (var e in graph.InComingArcs(1))
            {
                Assert.IsTrue(one[i] == e);
                i++;
            }

            i = 0;

            Assert.IsTrue(graph.InComingArcs(2).Count() == two.Length);
            foreach (var e in graph.InComingArcs(2))
            {
                Assert.IsTrue(two[i] == e);
                i++;
            }
        }

        [TestMethod]
        public void InComingArcsOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);
            graph.AddArc(0, 2);
            graph.AddArc(1, 0);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            int i = 0;
            int[] zero = { 1, 2 };
            int[] one = { 0 };
            int[] two = { 0, 1 };

            Assert.IsTrue(graph.InComingArcs(0).Count() == zero.Length);
            foreach (var e in graph.InComingArcs(0))
            {
                Assert.IsTrue(zero[i] == e);
                i++;
            }

            i = 0;

            Assert.IsTrue(graph.InComingArcs(1).Count() == one.Length);
            foreach (var e in graph.InComingArcs(1))
            {
                Assert.IsTrue(one[i] == e);
                i++;
            }

            i = 0;

            Assert.IsTrue(graph.InComingArcs(2).Count() == two.Length);
            foreach (var e in graph.InComingArcs(2))
            {
                Assert.IsTrue(two[i] == e);
                i++;
            }
        }

        [TestMethod]
        public void InComingArcsEmptyPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);

            Assert.IsTrue(graph.InComingArcs(2).Count() == 0);
            foreach (var e in graph.InComingArcs(2))
            {
                Assert.AreEqual(0, graph.InComingArcs(2));
                Assert.AreEqual(0, e);
            }
        }

        [TestMethod]
        public void InComingArcsOrEmptyPeak()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 3);
            graph.AddArc(0, 1);

            Assert.IsTrue(graph.InComingArcs(2).Count() == 0);
            foreach (var e in graph.InComingArcs(2))
            {
                Assert.AreEqual(0, graph.InComingArcs(2));
                Assert.AreEqual(0, e);
            }
        }
        #endregion

        // -- Combine

        #region Combine

        [TestMethod]
        public void Combine()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 2);
            Assert.IsFalse(graph.Oriented);
            Assert.AreEqual(2, graph.PeakCount);

            // AddArc ==================

            graph.AddArc(0, 1);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));

            Assert.AreEqual(1, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));

            Assert.IsTrue(graph.OutGoingArcs(0).Count() == 1);
            foreach (var e in graph.OutGoingArcs(0))
                Assert.AreEqual(1, e);

            Assert.IsTrue(graph.OutGoingArcs(1).Count() == 1);
            foreach (var e in graph.OutGoingArcs(1))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.InComingArcs(0).Count() == 1);
            foreach (var e in graph.InComingArcs(0))
                Assert.AreEqual(1, e);

            Assert.IsTrue(graph.InComingArcs(1).Count() == 1);
            foreach (var e in graph.InComingArcs(1))
                Assert.AreEqual(0, e);

            // AddPeak ==================

            graph.AddPeak();
            Assert.AreEqual(3, graph.PeakCount);

            graph.AddArc(0, 2);
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.AreEqual(2, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(2));

            int[] zeroIn = { 1, 2 };
            int zeroInInd = 0;
            Assert.IsTrue(graph.InComingArcs(0).Count() == 2);
            foreach (var e in graph.InComingArcs(0))
            {
                Assert.AreEqual(zeroIn[zeroInInd], e);
                zeroInInd++;
            }

            Assert.IsTrue(graph.InComingArcs(2).Count() == 1);
            foreach (var e in graph.InComingArcs(2))
                Assert.AreEqual(0, e);

            int[] zeroOut = { 1, 2 };
            int zeroOutInd = 0;
            Assert.IsTrue(graph.OutGoingArcs(0).Count() == 2);
            foreach (var e in graph.OutGoingArcs(0))
            {
                Assert.AreEqual(zeroOut[zeroOutInd], e);
                zeroOutInd++;
            }

            Assert.IsTrue(graph.OutGoingArcs(2).Count() == 1);
            foreach (var e in graph.OutGoingArcs(2))
                Assert.AreEqual(0, e);

            graph.AddPeak();
            graph.AddArc(3, 2);
            graph.DeleteArc(2, 0);
            graph.RemovePeak(1);

            // Проверка ребер

            Assert.IsFalse(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));

            // Проверка вершин

            // Нулевая

            Assert.AreEqual(0, graph.InArcsCount(0));

            Assert.IsTrue(graph.OutGoingArcs(0).Count() == 0);
            foreach (var e in graph.OutGoingArcs(0))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.InComingArcs(0).Count() == 0);
            foreach (var e in graph.InComingArcs(0))
                Assert.AreEqual(0, e);

            // Первая

            Assert.AreEqual(1, graph.InArcsCount(1));

            Assert.IsTrue(graph.OutGoingArcs(1).Count() == 1);
            foreach (var e in graph.OutGoingArcs(1))
                Assert.AreEqual(2, e);

            Assert.IsTrue(graph.InComingArcs(1).Count() == 1);
            foreach (var e in graph.InComingArcs(1))
                Assert.AreEqual(2, e);

            // Вторая

            Assert.AreEqual(1, graph.InArcsCount(2));

            Assert.IsTrue(graph.OutGoingArcs(2).Count() == 1);
            foreach (var e in graph.OutGoingArcs(2))
                Assert.AreEqual(1, e);

            Assert.IsTrue(graph.InComingArcs(2).Count() == 1);
            foreach (var e in graph.InComingArcs(2))
                Assert.AreEqual(1, e);


            // Завершение

            Assert.IsFalse(graph.Oriented);
            Assert.AreEqual(3, graph.PeakCount);
        }

        [TestMethod]
        public void CombineOr()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 2);
            Assert.IsTrue(graph.Oriented);
            Assert.AreEqual(2, graph.PeakCount);

            // AddArc ==================

            graph.AddArc(0, 1);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));

            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(1));

            Assert.IsTrue(graph.OutGoingArcs(0).Count() == 1);
            foreach (var e in graph.OutGoingArcs(0))
                Assert.AreEqual(1, e);            

            Assert.IsTrue(graph.InComingArcs(0).Count() == 0);
            foreach (var e in graph.InComingArcs(0))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.OutGoingArcs(1).Count() == 0);
            foreach (var e in graph.OutGoingArcs(1))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.InComingArcs(1).Count() == 1);
            foreach (var e in graph.InComingArcs(1))
                Assert.AreEqual(0, e);

            // AddPeak ==================

            graph.AddPeak();
            Assert.AreEqual(3, graph.PeakCount);

            graph.AddArc(0, 2);
            Assert.IsTrue(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));
            Assert.AreEqual(0, graph.InArcsCount(0));
            Assert.AreEqual(1, graph.InArcsCount(2));


            Assert.IsTrue(graph.InComingArcs(0).Count() == 0);
            foreach (var e in graph.InComingArcs(0))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.InComingArcs(2).Count() == 1);
            foreach (var e in graph.InComingArcs(2))
                Assert.AreEqual(0, e);

            int[] zeroOut = { 1, 2 };
            int zeroOutInd = 0;
            Assert.IsTrue(graph.OutGoingArcs(0).Count() == 2);
            foreach (var e in graph.OutGoingArcs(0))
            {
                Assert.AreEqual(zeroOut[zeroOutInd], e);
                zeroOutInd++;
            }

            Assert.IsTrue(graph.OutGoingArcs(2).Count() == 0);
            foreach (var e in graph.OutGoingArcs(2))
                Assert.AreEqual(0, e);

            graph.AddPeak();
            graph.AddArc(3, 2);

            Assert.AreEqual(2, graph.InArcsCount(2));

            graph.DeleteArc(0, 2);
            graph.RemovePeak(1);

            // Проверка ребер

            Assert.IsFalse(graph.ContainsArc(0, 1));
            Assert.IsFalse(graph.ContainsArc(1, 0));
            Assert.IsFalse(graph.ContainsArc(0, 2));
            Assert.IsFalse(graph.ContainsArc(2, 0));
            Assert.IsFalse(graph.ContainsArc(1, 2));
            Assert.IsTrue(graph.ContainsArc(2, 1));

            // Проверка вершин

            // Нулевая

            Assert.AreEqual(0, graph.InArcsCount(0));

            Assert.IsTrue(graph.OutGoingArcs(0).Count() == 0);
            foreach (var e in graph.OutGoingArcs(0))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.InComingArcs(0).Count() == 0);
            foreach (var e in graph.InComingArcs(0))
                Assert.AreEqual(0, e);

            // Первая

            Assert.AreEqual(1, graph.InArcsCount(1));

            Assert.IsTrue(graph.OutGoingArcs(1).Count() == 0);
            foreach (var e in graph.OutGoingArcs(1))
                Assert.AreEqual(0, e);

            Assert.IsTrue(graph.InComingArcs(1).Count() == 1);
            foreach (var e in graph.InComingArcs(1))
                Assert.AreEqual(2, e);

            // Вторая

            Assert.AreEqual(0, graph.InArcsCount(2));

            Assert.IsTrue(graph.OutGoingArcs(2).Count() == 1);
            foreach (var e in graph.OutGoingArcs(2))
                Assert.AreEqual(1, e);

            Assert.IsTrue(graph.InComingArcs(2).Count() == 0);
            foreach (var e in graph.InComingArcs(2))
                Assert.AreEqual(0, e);

            // Завершение

            Assert.IsTrue(graph.Oriented);
            Assert.AreEqual(3, graph.PeakCount);
        }
        #endregion

    }
}
