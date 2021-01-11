using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class UnweightedGraphWrapper<T> : IUnweightedGraph where T : IConvertible
    {
        private readonly IWeightedGraph<T>  weightedGraph;

        public UnweightedGraphWrapper(IWeightedGraph<T> weightedGraph)
        {
            this.weightedGraph = weightedGraph ?? throw new ArgumentNullException("WeightedGraph");
        }
        public bool Oriented => weightedGraph.Oriented;

        public int PeakCount => weightedGraph.PeakCount;

        public bool AddArc(int from, int to)
        {
            throw new NotImplementedException();
        }

        public void AddPeak()
        {
            weightedGraph.AddPeak();
        }

        public bool ContainsArc(int from, int to)
        {
            return weightedGraph.ContainsArc(from, to);
        }

        public bool DeleteArc(int from, int to)
        {
            return weightedGraph.DeleteArc(from, to);
        }

        public int InArcsCount(int peak)
        {
            return weightedGraph.InArcsCount(peak);
        }

        public IEnumerable<int> InComingArcs(int peak)
        {
            return weightedGraph.InComingArcs(peak).Select(el => el.Item1);
        }

        public IEnumerable<int> OutGoingArcs(int peak)
        {
            return weightedGraph.OutGoingArcs(peak).Select(el => el.Item1);
        }

        public void RemovePeak(int v)
        {
            weightedGraph.RemovePeak(v);
        }
    }
}
