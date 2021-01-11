using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph
    {
        void AddPeak();
        void RemovePeak(int v);
        bool Oriented { get; }
        int PeakCount { get; }
        bool DeleteArc(int from, int to);
        bool ContainsArc(int from, int to);
    }

    public interface IWeightedGraph<T> : IGraph where T : IConvertible
    {
        bool AddArc(int from, int to, T weight);
        T GetWeight(int from, int to);
        int InArcsCount(int peak);
        IEnumerable<Tuple<int, T>> OutGoingArcs(int peak);
        IEnumerable<Tuple<int, T>> InComingArcs(int peak);
    }

    public interface IUnweightedGraph : IGraph
    {
        bool AddArc(int from, int to);
        int InArcsCount(int peak);
        IEnumerable<int> OutGoingArcs(int peak);
        IEnumerable<int> InComingArcs(int peak);
    }
}
