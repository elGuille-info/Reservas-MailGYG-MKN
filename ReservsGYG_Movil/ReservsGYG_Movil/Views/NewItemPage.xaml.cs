using System;
using System.Collections.Generic;
using System.ComponentModel;

using ReservsGYG_Movil.Models;
using ReservsGYG_Movil.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservsGYG_Movil.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}