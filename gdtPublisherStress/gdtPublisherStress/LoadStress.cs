using System;
using System.Runtime.InteropServices;

namespace gdtPublisherStress
{
    public class LoadStress
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public const int WS_COPYDATA = 0x004A;

        [StructLayout(LayoutKind.Sequential)]

        public struct COPÝDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        public void Send(string message)
        {
            const int param = 4098;
            string json = message;
            IntPtr ptr = FindWindow(null, "gdtPublisher");
            SendString(param, 0, json, ptr);
        }

        public static void SendString(int param, int tag, string message, IntPtr hWnd)
        {
            message = message + '\0';
            COPÝDATASTRUCT data;
            data.dwData = new IntPtr(tag);
            data.lpData = Marshal.StringToHGlobalAnsi(message);
            data.cbData = message.Length;

            IntPtr ptrData = IntPtr.Zero;
            ptrData = Marshal.AllocCoTaskMem(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptrData, false);

            SendMessage(hWnd, WS_COPYDATA, new IntPtr(param), ptrData);
            if (data.lpData != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(data.lpData);
            }

            if (ptrData != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(ptrData);
            }

        }
    }
}
