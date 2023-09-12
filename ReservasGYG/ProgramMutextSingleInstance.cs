//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ReservasGYG
//{
//    //internal class ProgramMutextSingleInstance
//    //{
//    //}
//}

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ReservasGYG;
class ProgramMutextSingleInstance
{
    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool IsIconic(IntPtr hWnd);

    const int SW_RESTORE = 9;

    // El valor en la solución de ReservasGYG "{0A6BA240-5FDE-405F-9DE8-1652981B9F6C}"
    //static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
    static Mutex mutex = new Mutex(true, "{0A6BA240-5FDE-405F-9DE8-1652981B9F7D}");

    [STAThread]
    static void Main()
    {
        if (mutex.WaitOne(TimeSpan.Zero, true))
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormAnalizaEmail());

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            mutex.ReleaseMutex();
        }
        else
        {
            // La aplicación ya está en ejecución. Activamos la instancia en ejecución.
            Process current = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    // Comprobamos si la ventana está minimizada
                    if (IsIconic(process.MainWindowHandle))
                    {
                        ShowWindowAsync(process.MainWindowHandle, SW_RESTORE);
                    }

                    // Activamos la ventana
                    SetForegroundWindow(process.MainWindowHandle);
                    break;
                }
            }
        }
    }
}
