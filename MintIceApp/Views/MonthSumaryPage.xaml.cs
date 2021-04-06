using MintIceApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MintIceApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthSumaryPage : ContentPage
    {
        MonthSummaryViewModel vm;
        public MonthSumaryPage()
        {
            InitializeComponent();
            vm = new MonthSummaryViewModel();
            BindingContext = vm;
        }

        
    }
}