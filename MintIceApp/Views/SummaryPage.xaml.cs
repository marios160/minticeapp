using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MintIceApp.Models;
using MintIceApp.Views;
using MintIceApp.ViewModels;

namespace MintIceApp.Views
{
    public partial class SummaryPage : ContentPage
    {
        SummaryViewModel _viewModel;

        public SummaryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SummaryViewModel(summary);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}