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

            return null;
        }
    }

    unsafe class UnmanagedMemoryArray : IDisposable
    {
        public readonly int Length;
        protected readonly void* pointer; // указатель
        public UnmanagedMemoryArray(int length)
        {
            pointer = Marshal.AllocHGlobal(length).ToPointer(); // Marshal класс взаимодействия с неуправл кодом // AllocHGl место в неупр памяти длинны length и перевести в Pointer
            if (pointer != null) { 
                Length = length;
                GC.AddMemoryPressure(Length); // GC сборщик мусора // AddMemoryPressute информируем что выделен блок длинны length
            }
        }

        public void Dispose()
        {
            if(Length > 0)
            {
                Marshal.FreeHGlobal((IntPtr)pointer); // FreeHG освобождение памяти по указателю pointer
                GC.RemoveMemoryPressure(Length); // Сообщение СБ мусора что память освобожденаfffffffff
            }
        }
    }
}
