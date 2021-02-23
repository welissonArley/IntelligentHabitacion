using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Linq;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public abstract class HomeRegisterStrategy
    {
        public abstract RequestRegisterHomeJson CreateRequestToRegisterHome(HomeModel model);
        public abstract RequestUpdateHomeJson CreateRequestToUpdateHome(HomeModel model);

        protected RequestRegisterHomeJson RequestRegisterHomeJson(HomeModel model)
        {
            return new RequestRegisterHomeJson
            {
                Address = model.Address,
                City = model.City.Name,
                StateProvince = model.City.StateProvinceName,
                Country = (Communication.Enums.CountryEnum)model.City.Country.Id,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                ZipCode = model.ZipCode,
                Neighborhood = model.Neighborhood,
                Number = model.Number
            };
        }

        protected RequestUpdateHomeJson RequestUpdateHomeJson(HomeModel model)
        {
            return new RequestUpdateHomeJson
            {
                Address = model.Address,
                City = model.City.Name,
                StateProvince = model.City.StateProvinceName,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                ZipCode = model.ZipCode,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                NetworksName = model.NetWork.Name,
                NetworksPassword = model.NetWork.Password,
                Rooms = model.Rooms.Select(c => c.Room).ToList()
            };
        }

        protected void ValidateBase(HomeModel model)
        {
            ValidadeAdress(model.Address);
            ValidadeCity(model.City.Name);
            ValidadeNumber(model.Number);
        }
        private void ValidadeNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new NumberEmptyException();
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
    }
}
