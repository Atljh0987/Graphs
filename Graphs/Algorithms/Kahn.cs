using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Graphs;

namespace Graphs.Algorithms
{
    public static class Kahn
    {
        public static int[] Run(IUnweightedGraph graph)
        {
            if (graph == null) throw new ArgumentNullException("graph");
            if (!graph.Oriented) throw new ArgumentException("Graph must be oriented");

            using(UnmanagedNumbersArray CountOutputArcs = new UnmanagedNumbersArray(graph.PeakCount), 
                                         SortedElements = new UnmanagedNumbersArray(graph.PeakCount))
            {
                int sortedElementsCount = 0;
                int previousPassageCount = 0;
                int test = 0;
                int correct = 0;

                for(int i = 0; i < CountOutputArcs.Length; i++)
                {
                    //CountOutputArcs[i] = graph.InComingArcs(i).Count(); // !!!!
                    CountOutputArcs[i] = graph.InArcsCount(i);
                    if (CountOutputArcs[i] == 0)
                        SortedElements[sortedElementsCount++] = i;
                }

                while (sortedElementsCount < CountOutputArcs.Length) // Пока есть несортированные вершины
                {
                    int sortedInThisPass = 0;

                    for (int i = previousPassageCount; i < sortedElementsCount; i++) // Проходим по всем отсортированным вершинам
                    {
                        //foreach (var el in graph.OutGoingArcs(i)) // Перебираем ребра идущие из отсортированных // ошибка
                        foreach (var el in graph.OutGoingArcs(SortedElements[i])) 
                            {
                            CountOutputArcs[el]--;
                            if (CountOutputArcs[el] == 0)
                                SortedElements[sortedElementsCount + sortedInThisPass++] = el;
                        }
                    }

                    if (sortedInThisPass == 0)
                        throw new ArgumentException();

                    previousPassageCount = sortedElementsCount;
                    sortedElementsCount += sortedInThisPass;
                }

                return SortedElements.ToArray();
            }
        }
    }   

    unsafe class UnmanagedMemoryArray : IDisposable
    {
        public readonly int Size;
        protected readonly void* pointer; // указатель
        public UnmanagedMemoryArray(int length)
        {
            pointer = Marshal.AllocHGlobal(length).ToPointer(); // Marshal класс взаимодействия с неуправл кодом // AllocHGl место в неупр памяти длинны length и перевести в Pointer
            if (pointer != null) {
                Size = length;
                GC.AddMemoryPressure(Size); // GC сборщик мусора // AddMemoryPressute информируем что выделен блок длинны length
            }
        }

        public void Dispose()
        {
            if(Size > 0)
            {
                Marshal.FreeHGlobal((IntPtr)pointer); // FreeHG освобождение памяти по указателю pointer
                GC.RemoveMemoryPressure(Size); // Сообщение СБ мусора что память освобождена
            }
        }
    }

    unsafe class UnmanagedNumbersArray : UnmanagedMemoryArray
    {
        public UnmanagedNumbersArray(int size) : base(size * sizeof(int)) { }

        public int Length => Size / sizeof(int);

        public int this[int index]
        {
            get => ((int*)pointer)[index];
            set => ((int*)pointer)[index] = value;
        }

        public int[] ToArray()
        {
            int[] result = new int[Length];

            for(int i = 0; i < result.Length; i++)
            {
                result[i] = this[i];
            }

            return result;
        }
    }
}
