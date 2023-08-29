using System.ComponentModel;

using ReservsGYG_Movil.ViewModels;

using Xamarin.Forms;

namespace ReservsGYG_Movil.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}