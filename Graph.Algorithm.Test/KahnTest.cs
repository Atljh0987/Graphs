using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphs;
using Graphs.Algorithms;

namespace Graph.Algorithm.Test
{
    [TestClass]
    public class KahnTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InputData()
        {
            UnweightedList graph = new UnweightedList(false);
            Kahn.Run(graph);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullCheck()
        {
            Kahn.Run(null);
        }
    }
}
