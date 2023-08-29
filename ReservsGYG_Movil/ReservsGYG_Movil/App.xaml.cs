using System;

//using ReservasGYG_Movil.Services;
using ReservasGYG_Movil.Views;

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
            MainPage = new AppShell();
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
