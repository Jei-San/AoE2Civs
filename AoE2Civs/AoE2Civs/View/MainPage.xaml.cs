using AoE2Civs.Model;
using AoE2Civs.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AoE2Civs
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //create instance of the view model
        MainPageModel viewModel = new MainPageModel();
        public MainPage()
        {
            InitializeComponent();

            viewModel.Navigation = Navigation;

            BindingContext = viewModel;
        }

        private async System.Threading.Tasks.Task UserList_ItemTappedAsync(ItemTappedEventArgs e)
        {
            //Assigns method to when the command is tapped
            await viewModel.TapCommand((Civilization)e.Item);
        }

        private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Call the method from viewmodel when text is changed
            viewModel.FilterCivs();
        }
    }
}
