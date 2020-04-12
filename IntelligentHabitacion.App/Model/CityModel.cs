using System.ComponentModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class CityModel : ObservableObject
    {
        public CityModel()
        {
            State = new StateModel();
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        public StateModel State { get; set; }
    }
}
