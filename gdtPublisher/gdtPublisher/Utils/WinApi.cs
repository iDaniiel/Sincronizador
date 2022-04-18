using System;
using System.Runtime.InteropServices;

namespace gdtPublisher.Utils
{
    public static class WinApi
    {
        public const int WM_COPYDATA = 0x004A;
        public const int WM_QUERYENDSESSION = 0X11;

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [StructLayout(LayoutKind.Sequential)]

        public struct Copydata
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }
    }
}
