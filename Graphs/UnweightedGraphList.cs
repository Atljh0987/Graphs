using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class UnweightedList : IUnweightedGraph
    {
        private readonly List<HashSet<int>> forward = new List<HashSet<int>>();
        private readonly List<HashSet<int>> reverse;
        public bool Oriented => reverse != null;
        
        public int PeakCount => forward.Count;
        public UnweightedList(bool oriented)
        {
            if (oriented) reverse = new List<HashSet<int>>();
        }

        private bool CheckPeaks(int from, int to) => (from != to) ? true : throw new ArgumentException("to");

        public bool AddArc(int from, int to)
        {
            if ((uint) to >= (uint)PeakCount)    // забыл почему from не может выйти за границы? List проверяет правильность from
                throw new ArgumentOutOfRangeException("to");
            CheckPeaks(from, to);
            bool result = forward[from].Add(to);
            if (reverse != null) reverse[to].Add(from);
            else forward[to].Add(from);
            return result;
        }

        public void AddPeak()
        {
            forward.Add(new HashSet<int>());
            if (reverse != null) reverse.Add(new HashSet<int>());
        }

        public bool ContainsArc(int from, int to) // +++
        {
            if ((uint)to >= (uint)PeakCount)
                throw new ArgumentOutOfRangeException("to");
            CheckPeaks(from, to);
            return forward[from].Contains(to); 
        }

        public bool DeleteArc(int from, int to) // +++
        {
            if (to < 0 || to >= PeakCount)
                throw new ArgumentOutOfRangeException("to");
            CheckPeaks(from, to);
            bool result = forward[from].Remove(to);
            if (reverse != null) reverse[to].Remove(from);
            else forward[to].Remove(from);
            return result; 
        }

        private IEnumerable<int> WrapSet(HashSet<int> set)
        {
            foreach (var el in set)
                yield return el;
        }

        public IEnumerable<int> InComingArcs(int peak)
        {
            if (Oriented)
                return WrapSet(reverse[peak]);
            else
                return WrapSet(forward[peak]);
        }

        public IEnumerable<int> OutGoingArcs(int peak)
        {
            return WrapSet(forward[peak]);
        }

        private void RemovePeak(List<HashSet<int>> list, int v) // !!!!!!!!!!!!!
        {
            list.RemoveAt(v);
            List<int> remake = v < list.Count ? new List<int>() : null;

            if(remake == null)
            {
                foreach(var el in list)
                    el.Remove(v);
            }
            else
            {
                foreach (var el in list)
                {
                    el.Remove(v);

                    remake.AddRange(el.Where(e => e > v).OrderBy(e => e)); // смысл сортировки, потому что в дальнейшем будут повторяться 2 вершины

                    foreach (var elRemake in remake)
                    {
                        el.Remove(elRemake);
                        el.Add(elRemake - 1);
                    }

                    remake.Clear();
                }
            }
        }

        public void RemovePeak(int v)
        {
            RemovePeak(forward, v);
            if (Oriented) RemovePeak(reverse, v);
        }
    }
}
