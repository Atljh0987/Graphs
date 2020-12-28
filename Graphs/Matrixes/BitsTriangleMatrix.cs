using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class BitsTriangleMatrix : IMatrix<bool>
    {
        public bool this[int from, int to] { get => matrix[MatixIndex(from, to)]; set => matrix[MatixIndex(from, to)] = value; }

        public int Length => length;

        int length;

        BitArray matrix;

        public BitsTriangleMatrix(int length)
        {
            this.length = length;
            matrix = new BitArray((length - 1) * length / 2);
        }

        private int MatixIndex(int x, int y)
        {
            if ((uint)y >= (uint)Length)
                throw new IndexOutOfRangeException("y");
            if ((uint)x >= (uint)Length)
                throw new IndexOutOfRangeException("x");

            if (x > y)
                return y * Length + x - 1 - y * (y + 3) / 2;
            else
                return x * Length + y - 1 - x * (x + 3) / 2;
        }
    }
}
