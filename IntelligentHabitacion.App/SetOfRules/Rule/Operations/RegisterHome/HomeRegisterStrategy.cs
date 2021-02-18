using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public abstract class HomeRegisterStrategy
    {
        public abstract RequestRegisterHomeJson CreateRequest(HomeModel model);

        protected RequestRegisterHomeJson RequestHomeJson(HomeModel model)
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

        protected void ValidateBase(HomeModel model)
        {
            ValidadeAdress(model.Address);
            ValidadeCity(model.City.Name);
            ValidadeNetWorkInformation(model.NetWork.Name, model.NetWork.Password);
            ValidadeNumber(model.Number);
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
    }
}
