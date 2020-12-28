using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class TriangleMatrix<T> : IMatrix<T>
    {
        public int Length { get; private set; }
        private T[] Matrix;
        public TriangleMatrix(int length)
        {
            Length = length;
            Matrix = new T[(Length - 1) * Length / 2];
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

            //((2*Length - x+1)*x/2)+y-1-x
            //return Length * x - x * (x + 3) / 2 + y - 1;
            //return x * (Length * 2 - x - 1) / 2 + y - x - 1;
        }

        public T this[int x, int y]
        {
            get => Matrix[MatixIndex(x, y)];

            set => Matrix[MatixIndex(x, y)] = value;
        }
    }
}
