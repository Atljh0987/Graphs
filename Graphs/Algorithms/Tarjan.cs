using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Algorithms
{
    public static class Tarjan
    {
        enum State : byte
        {
            NotCheck,
            Checking,
            Checked
        }

        private static void Passer(IUnweightedGraph graph, int v, State[] states, UnmanagedNumbersArray sorted, ref int sortedCount)
        {
            if (states[v] == State.Checked) return;
            if (states[v] == State.Checking) throw new ArgumentException("Cycle in graph");

            states[v] = State.Checking;

            foreach(var arc in graph.InComingArcs(v))
            {
                Passer(graph, arc, states, sorted, ref sortedCount);
            }

            sorted[sortedCount++] = v;
            states[v] = State.Checked;
        }

        public static int[] Run(IUnweightedGraph graph)
        {
            if (graph == null) throw new ArgumentNullException("graph");
            if (!graph.Oriented) throw new ArgumentException("Graph must be oriented");

            using (UnmanagedNumbersArray SortedElements = new UnmanagedNumbersArray(graph.PeakCount))
            {
                State[] states = new State[graph.PeakCount];
                int sortedCount = 0;

                for(int i = 0; i < graph.PeakCount; i++)
                {
                    Passer(graph, i, states, SortedElements, ref sortedCount); // переизучить что тако ref 
                }                

                return SortedElements.ToArray();   
            }
        }
    }
}
