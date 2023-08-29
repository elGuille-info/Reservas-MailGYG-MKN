using System;
using System.Collections.Generic;

using ReservasGYG_Movil.ViewModels;
using ReservasGYG_Movil.Views;

using Xamarin.Forms;

namespace ReservasGYG_Movil
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
