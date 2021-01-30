using AoE2Civs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AoE2Civs.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoCivPage : ContentPage
    {
        MainPageModel viewModel = new MainPageModel();
        public InfoCivPage(MainPageModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }
    }
}