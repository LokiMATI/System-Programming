using System.Runtime.InteropServices;

namespace ConsoleDllApp;

public class LibraryImport
{
    [DllImport("LibDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int is_simple(int val);
    [DllImport("LibDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int is_simple_array(int[] arr, int length);
    [DllImport("LibDll", CallingConvention = CallingConvention.Cdecl)]
    public static extern double calc_def(Point first, Point second);
}

public struct Point
{
    public double x;
    public double y;
}