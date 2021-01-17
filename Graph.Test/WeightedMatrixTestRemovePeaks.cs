using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Graphs;
using System;

namespace Graph.Test
{
    [TestClass]
    public class WeightedMatrixTestRemovePeaks
    {
        class PeakCodes : List<char>
        {
            public int this[char code] => IndexOf(code);
        }

        [TestMethod]
        public void CountAfter()
        {
            WeightedGraphMatrix<int> graph = new WeightedGraphMatrix<int>(false, 3);

            PeakCodes peakCodes = new PeakCodes()
            {
                'a', 'b', 'c'
            };

            graph.AddArc(peakCodes['a'], peakCodes['b'], 31);
            graph.AddArc(peakCodes['b'], peakCodes['c'], 42);
            graph.AddArc(peakCodes['c'], peakCodes['a'], 45);

            Assert.AreEqual(3, graph.PeakCount);
            Assert.AreEqual(42, graph.GetWeight(peakCodes['b'], peakCodes['c']));
            Assert.AreEqual(42, graph.GetWeight(peakCodes['c'], peakCodes['b']));
            Assert.AreEqual(31, graph.GetWeight(peakCodes['a'], peakCodes['b']));
            Assert.AreEqual(31, graph.GetWeight(peakCodes['b'], peakCodes['a']));
            Assert.AreEqual(45, graph.GetWeight(peakCodes['c'], peakCodes['a']));
            Assert.AreEqual(45, graph.GetWeight(peakCodes['a'], peakCodes['c']));

            graph.RemovePeak(peakCodes['c']);

            Assert.AreEqual(2, graph.PeakCount);
            Assert.AreEqual(31, graph.GetWeight(peakCodes['a'], peakCodes['b']));
            Assert.AreEqual(31, graph.GetWeight(peakCodes['b'], peakCodes['a']));
        }

        [TestMethod]
        public void MatrixFiveModern()
        {
            WeightedGraphMatrix<string> graph = new WeightedGraphMatrix<string>(true, 5);

            PeakCodes peakCodes = new PeakCodes()
            {
                'a', 'b', 'c', 'd', 'e'
            };

            graph.AddArc(peakCodes['a'], peakCodes['b'], "a");
            graph.AddArc(peakCodes['b'], peakCodes['c'], "b");
            graph.AddArc(peakCodes['c'], peakCodes['d'], "c");
            graph.AddArc(peakCodes['d'], peakCodes['e'], "d");
            graph.AddArc(peakCodes['e'], peakCodes['a'], "e");

            graph.RemovePeak(peakCodes['c']);
            peakCodes.Remove('c');

            Assert.AreEqual(4, graph.PeakCount);
            Assert.AreEqual("a", graph.GetWeight(peakCodes['a'], peakCodes['b']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['a']));

            Assert.AreEqual("d", graph.GetWeight(peakCodes['d'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['e'], peakCodes['d']));

            Assert.AreEqual("e", graph.GetWeight(peakCodes['e'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['e']));

            Assert.IsTrue(graph.ContainsArc(peakCodes['d'], peakCodes['e']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['e'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['d']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['d']));

            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['e'], peakCodes['d']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['b']));

            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['e'], peakCodes['b']));

            graph.RemovePeak(peakCodes['e']);
            peakCodes.Remove('e');

            Assert.AreEqual(3, graph.PeakCount);

            Assert.AreEqual("a", graph.GetWeight(peakCodes['a'], peakCodes['b']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['a']));

            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['b']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['d']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['d']));

            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['a'])); // ??? - ошибка
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['b']));
        }
    }
}
