using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class UnweightedGraphMatrix : IUnweightedGraph
    {
        IMatrix<bool> matrix;

        public bool Oriented => matrix is BitsSquareMatrix;

        public int PeakCount => matrix.Length;

        public UnweightedGraphMatrix(bool oriented, int length)
        {
            if (oriented) matrix = new BitsSquareMatrix(length);
            else matrix = new BitsTriangleMatrix(length);
        }

        public void AddPeak()
        {
            IMatrix<bool> tmp;
            if (Oriented) tmp = new BitsSquareMatrix(PeakCount + 1);
            else tmp = new BitsTriangleMatrix(PeakCount + 1);

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
            return matrix[from, to];
        }

        private bool CheckPeaks(int from, int to) => (from != to)? true : throw new ArgumentException("to");  

        public bool DeleteArc(int from, int to)
        {
            CheckPeaks(from, to);
            bool result = matrix[from, to];
            if(result) matrix[from, to] = false;
            return result;
        }

        //private void Bypasser(bool oriented, int v)
        //{
        //    int iVal = 0;
        //    int jVal = 0;

        //    for (int i = 0; i < v; i++)
        //    {
        //        for (int j = 0; j < v; j++)
        //        {
        //            if (i != j) tmp[i, j] = matrix[i, j];
        //        }

        //        for (int j = v + 1; j < PeakCount; j++)
        //        {
        //            if (i != j - 1) tmp[i, j - 1] = matrix[i, j];
        //        }
        //    }
        //}

        public void RemovePeak(int v) //// 1!!!!!!!!!!!!!!!!!!!!!
        {
            if ((uint)v > (uint)PeakCount) throw new IndexOutOfRangeException("v");

            IMatrix<bool> tmp;
            if (Oriented) tmp = new SquareMatrix<bool>(PeakCount - 1);
            else tmp = new TriangleMatrix<bool>(PeakCount - 1);

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
                for(int i = 0; i < v; i++)
                {
                    for(int j = i + 1; j < v; j++)
                    {
                        tmp[i, j] = matrix[i, j];
                    }

                    for(int j = v + 1; j < PeakCount; j++)
                    {
                        tmp[i, j - 1] = matrix[i, j];
                    }
                }

                for(int i = v + 1; i < PeakCount; i++)
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

        public bool AddArc(int from, int to)
        {
            CheckPeaks(from, to);
            bool result = !matrix[from, to];
            if(result) matrix[from, to] = true;
            return result;
        }
        public int InArcsCount(int peak)
        {
            int count = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                if (i != peak && matrix[i, peak])
                    count++;
            }

            return count;
        }

        public IEnumerable<int> OutGoingArcs(int peak)
        {
            for(int i = 0; i < matrix.Length; i++)
            {
                if (i != peak && matrix[peak, i])
                    yield return i;
            }
        }

        public IEnumerable<int> InComingArcs(int peak)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                if (i != peak && matrix[i, peak])
                    yield return i;
            }
        }
    }
}
