using AoE2Civs.Model;
using AoE2Civs.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AoE2Civs.ViewModel
{
    public class MainPageModel : INotifyPropertyChanged
    {
        //Properties
        public ObservableCollection<Civilization> Civilizations { get; private set; }
        public Civilization User { get; set; }
        public string Search { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public bool IsLoaded { get; set; }


        //Commands
        public INavigation Navigation { get; internal set; }
        private ObservableCollection<Civilization> civList;


        public MainPageModel()
        {
            ShowCivs();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Methods
        private void ShowCivs()
        {
            var response = AoE2CivsAPI.GetCivs();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Root result = JsonConvert.DeserializeObject<Root>(response.Content);
                Civilizations = new ObservableCollection<Civilization>(result.civilizations);
                civList = new ObservableCollection<Civilization>(result.civilizations);
                IsLoaded = true;
                IsError = false;
            }
            else
            {
                Message = $"Network Problems\nStatus Code: {response.StatusCode}";
                IsError = true;
                IsLoaded = false;
            }
            OnPropertyChanged(nameof(Civilizations));
        }
        public void FilterCivs()
        {
            var filteredCivs = new List<Civilization>();
            if (!string.IsNullOrEmpty(Search))
            {
                filteredCivs = Civilizations.Where(u => u.name.ToLower().Contains(Search.ToLower())).ToList(); ;
                civList = new ObservableCollection<Civilization>(filteredCivs);
            }
            else
                Civilizations = civList;
            OnPropertyChanged(nameof(Civilizations));
        }

        public async Task TapCommand(Civilization itemTapped)
        {
            User = itemTapped;
            await Navigation.PushAsync(new InfoCivPage(this));
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
