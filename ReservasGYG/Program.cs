using System;
using System.Windows.Forms;

namespace ReservasGYG
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            
            // Volver a usar la de crear las reservas,      (25/ago/23 14.09)
            // que Ana se lía :-)
            Application.Run(new FormAnalizaEmail());
        }
    }
}