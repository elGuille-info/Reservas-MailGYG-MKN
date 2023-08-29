using System;

using ReservsGYG_Movil.Services;
using ReservsGYG_Movil.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservsGYG_Movil
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
