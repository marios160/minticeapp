using System.ComponentModel;
using Xamarin.Forms;
using MintIceApp.ViewModels;

namespace MintIceApp.Views
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