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
using System.Windows.Input;
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
        public string CivInfo { get; set; }
        public string CivImage { get; set; }
        public bool IsError { get; set; }
        public bool IsLoaded { get; set; }


        //Commands
        public INavigation Navigation { get; internal set; }
        public ICommand Clear { get; set; }
        private ObservableCollection<Civilization> civList;


        public MainPageModel()
        {
            ShowCivs();
            Clear = new Command(ClearCivs);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Methods
        private void ShowCivs()
        {
            var response = AoE2CivsAPI.GetCivs();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Root result = JsonConvert.DeserializeObject<Root>(response.Content);
                //Created a new list of collection from Civilization
                var NewCivList = new ObservableCollection<Civilization>();
                Civilizations = new ObservableCollection<Civilization>(result.civilizations);
                //Created a foreach to check every var in Civilizations object that calls the imagePath inside
                //Civilization class and assigned the value to AssignedPicture with properties of type string
                //that came in the list of name then called the new collection and added the civ parameter to it
                foreach(var civ in Civilizations)
                {
                    civ.imagePath = AssignedPicture(civ.name);
                    NewCivList.Add(civ);
                }
                //Assigned NewCivList value to Civilizations
                Civilizations = NewCivList;
                civList = new ObservableCollection<Civilization>(result.civilizations);
                IsLoaded = true;
                IsError = false;
            }
            else
            {
                Message = $"Network Problems\nStatus Code: {response.StatusCode}";
                IsError = true;
                IsLoaded = false;
                ClearCivs();
            }
            OnPropertyChanged(nameof(Civilizations));
        }
        public void FilterCivs()
        {
            var filteredCivs = new List<Civilization>();
            if (!string.IsNullOrEmpty(Search))
            {
                filteredCivs = Civilizations.Where(u => u.name.ToLower().Contains(Search.ToLower())).ToList(); ;
                Civilizations = new ObservableCollection<Civilization>(filteredCivs);
            }
            else
                Civilizations = civList;
            OnPropertyChanged(nameof(Civilizations));
        }
        private void ClearCivs()
        {
            if (Civilizations != null)
                Civilizations.Clear(); 
            if (civList != null)
                civList.Clear();
        }
        //Made a function with the AssignedPicture and added a parameter called civName of type string
        //created a variable called CivImage which will have the .png value to pass it on to the xaml with a switch statement
        public string AssignedPicture(string civName)
        {
            var CivImage = "";
            switch(civName)
            {//generizicing with sqlite
                case "Aztecs":
                    CivImage = "Aztecs.png";
                    break;
                case "Britons":
                    CivImage = "Britons.png";
                   break;
                case "Byzantines":
                    CivImage = "Byzantines.png";
                    break;
                case "Celts":
                    CivImage = "Celts.png";
                    break;
                case "Chinese":
                    CivImage = "Chinese.png";
                    break;
                case "Franks":
                    CivImage = "Franks.png";
                    break;
                case "Goths":
                    CivImage = "Goths.png";
                    break;
                case "Huns":
                    CivImage = "Huns.png";
                    break;
                case "Japanese":
                    CivImage = "Japanese.png";
                    break;
                case "Koreans":
                    CivImage = "Koreans.png";
                    break;
                case "Mayans":
                    CivImage = "Mayans.png";
                    break;
                case "Mongols":
                    CivImage = "Mongols.png";
                    break;
                case "Persians":
                    CivImage = "Persians.png";
                    break;
                case "Saracens":
                    CivImage = "Saracens.png";
                    break;
                case "Spanish":
                    CivImage = "Spanish.png";
                    break;
                case "Teutons":
                    CivImage = "Teutons.png";
                    break;
                case "Turks":
                    CivImage = "Turks.png";
                    break;
                case "Vikings":
                    CivImage = "Vikings.png";
                    break;
                case "Berbers":
                    CivImage = "Berbers.png";
                    break;
                case "Burmese":
                    CivImage = "Burmese.png";
                    break;
                case "Ethiopians":
                    CivImage = "Ethiopians.png";
                    break;
                case "Incas":
                    CivImage = "Incas.png";
                    break;
                case "Indians":
                    CivImage = "Indians.png";
                    break;
                case "Italians":
                    CivImage = "Italians.png";
                    break;
                case "Khmer":
                    CivImage = "Khmer.png";
                    break;
                case "Magyars":
                    CivImage = "Magyars.png";
                    break;
                case "Malians":
                    CivImage = "Malians.png";
                    break;
                case "Portuguese":
                    CivImage = "Portuguese.png";
                    break;
                case "Slavs":
                    CivImage = "Slavs.png";
                    break;
                case "Vietnamese":
                    CivImage = "Vietnamese.png";
                    break;
            }
            return CivImage;
        } 
        public async Task TapCommand(Civilization itemTapped)
        {
            User = itemTapped;
            await Navigation.PushAsync(new InfoCivPage(this));
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
