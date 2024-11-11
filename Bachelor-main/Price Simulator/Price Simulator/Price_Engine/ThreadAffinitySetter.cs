using System.Runtime.InteropServices;

namespace Price_Simulator.Price_Engine;

public class ThreadAffinitySetter
{
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetCurrentThread();
    [DllImport("kernel32.dll")]
    private static extern IntPtr SetThreadAffinityMask(IntPtr hThread, IntPtr dwThreadAffinityMask);

    public static void SetAffinity(int processorNumber)
    {
        IntPtr ptrThread = GetCurrentThread();
        // Create a bitmask for the processor you want to use. Processor numbers start from 0.
        IntPtr affinityMask = (IntPtr)(1 << processorNumber);
        SetThreadAffinityMask(ptrThread, affinityMask);
    }
}