using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReservasGYG
{
    //internal class ProgramMutex
    //{
    //}
    static class ProgramMutex
    {
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        [STAThread]
        static void MainProgramMutex()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                // Aquí puedes manejar el caso cuando ya existe una instancia de la aplicación.
                // Por ejemplo, puedes mostrar un mensaje al usuario.
            }
        }
    }
}
