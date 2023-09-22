// No cargar Shell, cargar directamente la página.          (01/sep/23 17.42)

using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservasGYG_Movil
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            MainPage = new MainPage();
        }

        // La descripción para las copias de seguridad. (21/Oct/21)
        // Se usará como descripción lo que esté entre comillas dobles (incluidos los espacios).
        // Ya que se comprueba que empiece con: [COPIAR]AppDescripcionCopia = y unas comillas dobles.

        // Intentar no pasar de estas marcas: 60 caracteres. 2         3         4         5         6
        //                                ---------|---------|---------|---------|---------|---------|
        //[COPIAR]AppDescripcionCopia = " margen iOS phone"

        /// <summary>
        /// La versión de la aplicación.
        /// </summary>
        public static string AppVersion { get; } = "1.0.83";

        /// <summary>
        /// La versión del fichero (la revisión)
        /// </summary>
        public static string AppFileVersion { get; } = "1.0.83.0";

        /// <summary>
        /// La fecha de última actualización
        /// </summary>
        public static string AppFechaVersion { get; } = "22-sep-2023";

        /// <summary>
        /// El nombre de la aplicación.
        /// </summary>
        public static string AppName { get; } = "ReservasGYG Movil";


        /// <summary>
        /// Hacer una pequeña pausa para refrescar.
        /// </summary>
        /// <param name="intervalo">El tiempo que hay que esperar (en milisegundos).</param>
        public static async Task Refrescar(int intervalo = 300)
        {
            await Task.Delay(intervalo);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
