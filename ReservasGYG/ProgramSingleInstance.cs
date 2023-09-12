using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasGYG
{
    //internal class ProgramSingleInstance
    //{
    //}

    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public POINTAPI ptMinPosition;
        public POINTAPI ptMaxPosition;
        public RECT rcNormalPosition;
    }

    public struct POINTAPI
    {
        public int x;
        public int y;
    }

    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    internal static class ProgramSingleInstance
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [STAThread]
        static void MainProgramSingleInstance()
        {
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            bool processOpen = false;

            if (processes.Length > 1)
            {
                foreach (Process process in processes)
                {
                    IntPtr frameworkHandle = process.MainWindowHandle;

                    if (frameworkHandle != IntPtr.Zero)
                    {
                        processOpen = true;

                        WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                        //wp.showCmd = 3; // Maximizado
                        wp.showCmd = 0;

                        SetForegroundWindow(frameworkHandle); // A primer plano
                        SetWindowPlacement(frameworkHandle, ref wp);
                    }
                }
            }

            if (processOpen == false)
            {
                //Application.Run(new Form1());
                Application.Run(new FormAnalizaEmail());
            }
        }
    }

}
