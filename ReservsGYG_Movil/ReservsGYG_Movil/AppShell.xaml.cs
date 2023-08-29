using System;
using System.Collections.Generic;

using ReservsGYG_Movil.ViewModels;
using ReservsGYG_Movil.Views;

using Xamarin.Forms;

namespace ReservsGYG_Movil
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
