using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class BitsSquareMatrix : IMatrix<bool>
    {
        public bool this[int from, int to] { get => matrix[MatixIndex(from, to)]; set => matrix[MatixIndex(from, to)] = value; }

        public int Length { get => length; }

        int length;

        BitArray matrix;

        public BitsSquareMatrix(int length)
        {
            this.length = length;
            matrix = new BitArray(length*length);
        }

        private int MatixIndex(int x, int y)
        {
            if ((uint)y >= (uint)Length)
                throw new IndexOutOfRangeException("y");
            if ((uint)x >= (uint)Length)
                throw new IndexOutOfRangeException("x");

            return x * length + y;
        }
    }
}
