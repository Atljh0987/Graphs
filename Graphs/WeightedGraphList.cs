using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WeightedGraphList<T> : IWeightedGraph<T> where T : IConvertible
    {
        private readonly List<Dictionary<int, T>> forward = new List<Dictionary<int, T>>();
        private readonly List<Dictionary<int, T>> reverse;
        public bool Oriented => reverse != null;

        public int PeakCount => forward.Count;

        public WeightedGraphList(bool oriented)
        {
            if(oriented) reverse = new List<Dictionary<int, T>>();
        }

        public bool AddArc(int from, int to, T weight)
        {
            if (to < 0 || to >= PeakCount)    // забыл почему from не может выйти за границы? List проверяет правильность from
                throw new ArgumentOutOfRangeException("to");
            CheckPeaks(from, to);
            bool result = !forward[from].ContainsKey(to);
            forward[from][to] = weight;
            if (Oriented) reverse[to][from] = weight;
            else forward[to][from] = weight;
            return result;
        }

        public void AddPeak()
        {
            forward.Add(new Dictionary<int, T>());
            if (reverse != null) reverse.Add(new Dictionary<int, T>());
        }

        private bool CheckPeaks(int from, int to) => (from != to) ? true : throw new ArgumentException("to");

        public bool ContainsArc(int from, int to)
        {
            if ((uint)to >= (uint)PeakCount)
                throw new ArgumentOutOfRangeException("to");
            CheckPeaks(from, to);
            return forward[from].ContainsKey(to);
        }

        public bool DeleteArc(int from, int to)
        {
            if ((uint) to >= (uint)PeakCount)
                throw new ArgumentOutOfRangeException("to");
            CheckPeaks(from, to);
            bool result = forward[from].Remove(to);
            if (reverse != null) reverse[to].Remove(from);
            else forward[to].Remove(from);
            return result;
        }

        public T GetWeight(int from, int to)
        {
            CheckPeaks(from, to);
            return forward[from][to];
        }

        public int InArcsCount(int peak)
        {
            return (reverse ?? forward)[peak].Count;
        }

        public IEnumerable<Tuple<int, T>> InComingArcs(int peak)
        {
            if (Oriented)
                return WrapSet(reverse[peak]);
            else
                return WrapSet(forward[peak]);
        }

        private IEnumerable<Tuple<int, T>> WrapSet(Dictionary<int, T> dictionaries)
        {
            foreach (var el in dictionaries)
                yield return new Tuple<int, T>(el.Key, el.Value);
        }

        public IEnumerable<Tuple<int, T>> OutGoingArcs(int peak)
        {
            return WrapSet(forward[peak]);
        }

        public void RemovePeak(int v)
        {
            RemovePeak(forward, v);
            if (Oriented) RemovePeak(reverse, v);
        }

        private void RemovePeak(List<Dictionary<int, T>> list, int v)
        {
            list.RemoveAt(v);
            List<int> remake = v < list.Count ? new List<int>() : null;

            foreach (var el in list)
            {
                el.Remove(v);

                if (remake == null)
                    continue;

                remake.AddRange(el.Keys.Where(e => e > v).OrderBy(e => e));

                foreach (var elRemake in remake)
                {
                    T weight = el[elRemake];
                    el.Remove(elRemake);
                    el.Add(elRemake - 1, weight);
                }

                remake.Clear();
            }
        }
    }
}
