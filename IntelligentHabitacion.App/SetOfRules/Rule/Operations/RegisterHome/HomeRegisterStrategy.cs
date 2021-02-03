﻿using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public abstract class HomeRegisterStrategy
    {
        public abstract RequestHomeJson CreateRequestHomeJson(HomeModel model);

        protected RequestHomeJson RequestHomeJson(HomeModel model)
        {
            return new RequestHomeJson
            {
                Address = model.Address,
                City = new RequestRegisterCityJson
                {
                    Name = model.City.Name,
                    StateProvinceName = model.City.StateProvinceName,
                    Country = model.City.Country.Id
                },
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                ZipCode = model.ZipCode,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                DeadlinePaymentRent = model.DeadlinePaymentRent.Value,
                NetworksName = model.NetWork.Name,
                NetworksPassword = model.NetWork.Password,
            };
        }

        protected void ValidateBase(HomeModel model)
        {
            ValidadeAdress(model.Address);
            ValidadeCity(model.City.Name);
            ValidadeNetWorkInformation(model.NetWork.Name, model.NetWork.Password);
            ValidadeNumber(model.Number);
            ValidadeDeadlinePaymentRent(model.DeadlinePaymentRent);
        }
        private void ValidadeNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new NumberEmptyException();
        }
        private void ValidadeNetWorkInformation(string name, string password)
        {
            if ((string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(password)) || (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(password)))
                throw new NetworkInformationsInvalidException();
        }
        private void ValidadeAdress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new AddressEmptyException();
        }
        private void ValidadeCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new CityEmptyException();
        }
        private void ValidadeDeadlinePaymentRent(short? deadline)
        {
            if (!deadline.HasValue || !(deadline >= 1 && deadline <= 28))
                throw new DeadlinePaymentRentException();
        }
    }
}