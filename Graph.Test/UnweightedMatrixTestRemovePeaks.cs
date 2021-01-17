using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphs;
using System.Collections.Generic;

namespace Graph.Test
{
    [TestClass]
    public class UnweightedMatrixTestRemovePeak
    {
        class PeakCodes : List<char>
        {
            public int this[char code] => IndexOf(code);
        }

        


        [TestMethod]
        public void ThreeElFromEndCorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);
            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(2, 0));

            graph.RemovePeak(2);

            Assert.IsTrue(graph.ContainsArc(0, 1));
            Assert.IsTrue(graph.ContainsArc(1, 0));
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void ThreeElFromEndInorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            graph.RemovePeak(2);

            Assert.IsTrue(graph.ContainsArc(2, 1));
            Assert.IsTrue(graph.ContainsArc(1, 2));
        }

        [TestMethod]
        public void ThreeElFromMiddleCorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            graph.RemovePeak(1);

            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 1));
        }

        [TestMethod, ExpectedException(typeof(IndexOutOfRangeException))]
        public void ThreeElFromMiddleIncorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            graph.RemovePeak(1);

            Assert.IsTrue(graph.ContainsArc(2, 0));
            Assert.IsTrue(graph.ContainsArc(0, 2));
        }

        [TestMethod]
        public void FiveElFromMiddleCorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 5);

            Dictionary<char, int> peakCodes = new Dictionary<char, int>
            {
                ['a'] = 0,
                ['b'] = 1,
                ['c'] = 2,
                ['d'] = 3,
                ['e'] = 4
            };

            graph.AddArc(peakCodes['a'], peakCodes['b']);
            graph.AddArc(peakCodes['b'], peakCodes['c']);
            graph.AddArc(peakCodes['c'], peakCodes['d']);
            graph.AddArc(peakCodes['d'], peakCodes['e']);
            graph.AddArc(peakCodes['e'], peakCodes['a']);

            graph.RemovePeak(peakCodes['c']);
            peakCodes.Remove('c');
            peakCodes['d'] = 2;
            peakCodes['e'] = 3;

            Assert.AreEqual(4, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['b']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['d'], peakCodes['e']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['e'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['d']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['d']));

            Assert.IsTrue(graph.ContainsArc(peakCodes['b'], peakCodes['a']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['e'], peakCodes['d']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['b']));

            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['e'], peakCodes['b']));
        }

        [TestMethod]
        public void SevenElFromMiddleCorrect()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 7);

            PeakCodes peakCounts = new PeakCodes()
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g'
            };

            //Необходимость сортировки
            graph.AddArc(peakCounts['g'], peakCounts['f']);
            graph.AddArc(peakCounts['g'], peakCounts['e']);

            graph.AddArc(peakCounts['a'], peakCounts['b']);
            graph.AddArc(peakCounts['b'], peakCounts['c']);
            graph.AddArc(peakCounts['c'], peakCounts['b']);

            graph.AddArc(peakCounts['a'], peakCounts['d']);
            graph.AddArc(peakCounts['d'], peakCounts['b']);
            graph.AddArc(peakCounts['c'], peakCounts['d']);
            graph.AddArc(peakCounts['d'], peakCounts['c']);

            graph.AddArc(peakCounts['e'], peakCounts['f']);
            graph.AddArc(peakCounts['f'], peakCounts['e']);

            graph.AddArc(peakCounts['e'], peakCounts['d']);
            graph.AddArc(peakCounts['d'], peakCounts['e']);
            graph.AddArc(peakCounts['d'], peakCounts['f']);
            graph.AddArc(peakCounts['g'], peakCounts['d']);

            graph.AddArc(peakCounts['a'], peakCounts['e']);
            graph.AddArc(peakCounts['f'], peakCounts['b']);
            graph.AddArc(peakCounts['c'], peakCounts['g']);
            graph.AddArc(peakCounts['g'], peakCounts['c']);

            graph.RemovePeak(peakCounts['d']);
            peakCounts.Remove('d');


            Assert.IsTrue(graph.ContainsArc(peakCounts['a'], peakCounts['b']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['a'], peakCounts['c']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['a'], peakCounts['e']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['a'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['a'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['a']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['b'], peakCounts['c']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['e']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['c'], peakCounts['a']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['c'], peakCounts['b']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['c'], peakCounts['e']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['c'], peakCounts['f']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['c'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['a']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['c']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['b']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['e'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['f'], peakCounts['a']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['f'], peakCounts['c']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['f'], peakCounts['e']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['f'], peakCounts['b']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['f'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['g'], peakCounts['a']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['g'], peakCounts['c']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['g'], peakCounts['e']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['g'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['g'], peakCounts['b']));
        }

        [TestMethod]
        public void SquareMatrixSeven()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 7);

            PeakCodes peakCounts = new PeakCodes()
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g'
            };

            //Необходимость сортировки
            graph.AddArc(peakCounts['g'], peakCounts['f']);
            graph.AddArc(peakCounts['g'], peakCounts['e']);

            graph.AddArc(peakCounts['a'], peakCounts['b']);
            graph.AddArc(peakCounts['b'], peakCounts['c']);
            graph.AddArc(peakCounts['c'], peakCounts['b']);

            graph.AddArc(peakCounts['a'], peakCounts['d']);
            graph.AddArc(peakCounts['d'], peakCounts['b']);
            graph.AddArc(peakCounts['c'], peakCounts['d']);
            graph.AddArc(peakCounts['d'], peakCounts['c']);

            graph.AddArc(peakCounts['e'], peakCounts['f']);
            graph.AddArc(peakCounts['f'], peakCounts['e']);

            graph.AddArc(peakCounts['e'], peakCounts['d']);
            graph.AddArc(peakCounts['d'], peakCounts['e']);
            graph.AddArc(peakCounts['d'], peakCounts['f']);
            graph.AddArc(peakCounts['g'], peakCounts['d']);

            graph.AddArc(peakCounts['a'], peakCounts['e']);
            graph.AddArc(peakCounts['f'], peakCounts['b']);
            graph.AddArc(peakCounts['c'], peakCounts['g']);
            graph.AddArc(peakCounts['g'], peakCounts['c']);

            graph.RemovePeak(peakCounts['d']);
            peakCounts.Remove('d');

            Assert.IsTrue(graph.ContainsArc(peakCounts['a'], peakCounts['b']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['a'], peakCounts['c']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['a'], peakCounts['e']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['a'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['a'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['a']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['b'], peakCounts['c']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['e']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['b'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['c'], peakCounts['a']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['c'], peakCounts['b']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['c'], peakCounts['e']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['c'], peakCounts['f']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['c'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['a']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['c']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['b']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['e'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['e'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['f'], peakCounts['a']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['f'], peakCounts['c']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['f'], peakCounts['e']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['f'], peakCounts['b']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['f'], peakCounts['g']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['g'], peakCounts['a']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['g'], peakCounts['c']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['g'], peakCounts['e']));
            Assert.IsTrue(graph.ContainsArc(peakCounts['g'], peakCounts['f']));
            Assert.IsFalse(graph.ContainsArc(peakCounts['g'], peakCounts['b']));
        }

        [TestMethod]
        public void MatrixThree()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 3);
            graph.AddArc(0, 1);
            graph.AddArc(1, 2);
            graph.AddArc(2, 0);

            graph.RemovePeak(1);

            Assert.AreEqual(2, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(1, 0));
            Assert.IsTrue(graph.ContainsArc(0, 1));
        }

        [TestMethod]
        public void MatrixFive()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(false, 5);

            Dictionary<char, int> peakCodes = new Dictionary<char, int>
            {
                ['a'] = 0,
                ['b'] = 1,
                ['c'] = 2,
                ['d'] = 3,
                ['e'] = 4
            };

            graph.AddArc(peakCodes['a'], peakCodes['b']);
            graph.AddArc(peakCodes['b'], peakCodes['c']);
            graph.AddArc(peakCodes['c'], peakCodes['d']);
            graph.AddArc(peakCodes['d'], peakCodes['e']);
            graph.AddArc(peakCodes['e'], peakCodes['a']);

            graph.RemovePeak(peakCodes['c']);
            peakCodes.Remove('c');
            peakCodes['d'] = 2;
            peakCodes['e'] = 3;

            Assert.AreEqual(4, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['b']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['d'], peakCodes['e']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['e'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['d']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['d']));

            Assert.IsTrue(graph.ContainsArc(peakCodes['b'], peakCodes['a']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['e'], peakCodes['d']));
            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['b']));

            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['e']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['e'], peakCodes['b']));
        }

        [TestMethod]
        public void MatrixFiveModern()
        {
            UnweightedGraphMatrix graph = new UnweightedGraphMatrix(true, 5);

            Dictionary<char, int> peakCodes = new Dictionary<char, int>
            {
                ['a'] = 0,
                ['b'] = 1,
                ['c'] = 2,
                ['d'] = 3,
                ['e'] = 4
            };

            graph.AddArc(peakCodes['a'], peakCodes['b']);
            graph.AddArc(peakCodes['b'], peakCodes['c']);
            graph.AddArc(peakCodes['c'], peakCodes['d']);
            graph.AddArc(peakCodes['d'], peakCodes['e']);
            graph.AddArc(peakCodes['e'], peakCodes['a']);

            graph.RemovePeak(peakCodes['c']);
            peakCodes.Remove('c');
            peakCodes['d'] = 2;
            peakCodes['e'] = 3;

            Assert.AreEqual(4, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['b']));
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

            graph.RemovePeak(peakCodes['e']); // слетает Oriented // присваиваются не битовые матрицы
            peakCodes.Remove('e');

            Assert.AreEqual(3, graph.PeakCount);
            Assert.IsTrue(graph.ContainsArc(peakCodes['a'], peakCodes['b']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['a'], peakCodes['d']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['d']));

            Assert.IsFalse(graph.ContainsArc(peakCodes['b'], peakCodes['a'])); // ??? - ошибка
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['a']));
            Assert.IsFalse(graph.ContainsArc(peakCodes['d'], peakCodes['b']));
        }



        // Проверить при HashSet пустой
    }
}
