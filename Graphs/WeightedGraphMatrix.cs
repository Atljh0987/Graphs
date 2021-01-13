using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WeightedGraphMatrix<T> : IWeightedGraph<T> where T : IConvertible
    {
        struct Arc
        {
            public bool Exist;
            public T Weight;
        }

        private IMatrix<Arc> matrix;

        public bool Oriented => matrix is SquareMatrix<Arc>;

        public int PeakCount => matrix.Length;

        public WeightedGraphMatrix(bool oriented, int peaks)
        {
            if (oriented) matrix = new SquareMatrix<Arc>(peaks);
            else matrix = new TriangleMatrix<Arc>(peaks);
        }

        public bool AddArc(int from, int to, T weight)
        {
            CheckPeaks(from, to);
            bool result = !matrix[from, to].Exist;
            matrix[from, to] = new Arc() { Exist = true, Weight = weight };
            return result;
        }

        public void AddPeak()
        {
            IMatrix<Arc> tmp;
            if (Oriented) tmp = new SquareMatrix<Arc>(PeakCount + 1);
            else tmp = new TriangleMatrix<Arc>(PeakCount + 1);
            
            if(Oriented)
            {
                for(int i = 0; i < PeakCount; i++)
                {
                    for(int j = 0; j < PeakCount; j++)
                    {
                        if(i != j) tmp[i, j] = matrix[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < PeakCount - 1; i++)
                {
                    for (int j = i + 1; j < PeakCount; j++)
                    {
                        tmp[i, j] = matrix[i, j];
                    }
                }
            }

            matrix = tmp;
        }

        public bool ContainsArc(int from, int to)
        {
            CheckPeaks(from, to);
            return matrix[from, to].Exist;
        }

        private bool CheckPeaks(int from, int to) => (from != to) ? true : throw new ArgumentException("to");

        public bool DeleteArc(int from, int to)
        {
            CheckPeaks(from, to);
            bool result = matrix[from, to].Exist;
            if (result) matrix[from, to] = new Arc { Exist = false, Weight = default(T) } ;
            return result;
        }

        public T GetWeight(int from, int to)
        {
            if (!matrix[from, to].Exist) throw new KeyNotFoundException();
            return matrix[from, to].Weight;
        }

        public int InArcsCount(int peak)
        {
            int count = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                if (i != peak && matrix[i, peak].Exist)
                    count++;
            }

            return count;
        }

        public IEnumerable<Tuple<int, T>> InComingArcs(int peak)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                if (i != peak && matrix[i, peak].Exist)
                    yield return new Tuple<int, T>(i, matrix[i, peak].Weight);
            }
        }

        public IEnumerable<Tuple<int, T>> OutGoingArcs(int peak)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                if (i != peak && matrix[peak, i].Exist)
                    yield return new Tuple<int, T>(i, matrix[peak, i].Weight);
            }
        }

        public void RemovePeak(int v) // !!!!
        {
            if ((uint)v > (uint)PeakCount) throw new IndexOutOfRangeException("v");

            IMatrix<Arc> tmp;
            if (Oriented) tmp = new SquareMatrix<Arc>(PeakCount - 1);
            else tmp = new TriangleMatrix<Arc>(PeakCount - 1);

            if (Oriented)
            {
                for (int i = 0; i < v; i++)
                {
                    for (int j = 0; j < v; j++)
                    {
                        if (i != j) tmp[i, j] = matrix[i, j];
                    }

                    for (int j = v + 1; j < PeakCount; j++)
                    {
                        if (i != j - 1) tmp[i, j - 1] = matrix[i, j];
                    }
                }

                for (int i = v + 1; i < PeakCount; i++)
                {
                    for (int j = 0; j < v; j++)
                    {
                        if (i - 1 != j) tmp[i - 1, j] = matrix[i, j];
                    }

                    for (int j = v + 1; j < PeakCount; j++)
                    {
                        if (i != j) tmp[i - 1, j - 1] = matrix[i, j];
                    }
                }
            }
            else
            {
                for (int i = 0; i < v; i++)
                {
                    for (int j = i + 1; j < v; j++)
                    {
                        tmp[i, j] = matrix[i, j];
                    }

                    for (int j = v + 1; j < PeakCount; j++)
                    {
                        tmp[i, j - 1] = matrix[i, j];
                    }
                }

                for (int i = v + 1; i < PeakCount; i++)
                {
                    for (int j = i + 1; j < v; j++)
                    {
                        tmp[i - 1, j] = matrix[i, j];
                    }

                    for (int j = v + 1; j < PeakCount; j++)
                    {
                        tmp[i - 1, j - 1] = matrix[i, j];
                    }
                }
            }

            matrix = tmp;
        }
    }
}
