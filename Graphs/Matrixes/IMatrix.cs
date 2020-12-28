using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    interface IMatrix<T>
    {
        int Length { get; }
        T this[int from, int to] { get; set; }
    }

    class SquareMatrix<T> : IMatrix<T>
    {
        public T this[int from, int to] { get => matrix[from, to]; set => matrix[from, to] = value; }

        public int Length => matrix.GetLength(0);
        private readonly T[,] matrix;

        public SquareMatrix(int length)
        {
            matrix = new T[length, length];
        }
    }
}
