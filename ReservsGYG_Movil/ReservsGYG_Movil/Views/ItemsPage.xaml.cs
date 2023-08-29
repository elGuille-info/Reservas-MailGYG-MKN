using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservsGYG_Movil.Models;
using ReservsGYG_Movil.ViewModels;
using ReservsGYG_Movil.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReservsGYG_Movil.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}