﻿using System.ComponentModel;
using XLabs.Data;

namespace IntelligentHabitacion.App.Model
{
    public class HomeModel : ObservableObject
    {
        public HomeModel()
        {
            City = new CityModel();
            NetWork = new WifiNetworkModel();
        }
        public string ZipCode { get; set; }
        public CityModel City { get; set; }
        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Address"));
            }
        }
        public string Number { get; set; }
        public string Complement { get; set; }
        private string _neighborhood;
        public string Neighborhood
        {
            get
            {
                return _neighborhood;
            }
            set
            {
                _neighborhood = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Neighborhood"));
            }
        }
        public short? DeadlinePaymentRent { get; set; }
        public WifiNetworkModel NetWork { get; set; }
    }
}
