using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Text.RegularExpressions;

namespace IntelligentHabitacion.App.UseCases.Home.RegisterHome.Strategy
{
    public class BrazilHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestRegisterHomeJson Mapper(HomeModel model)
        {
            return new RequestRegisterHomeJson
            {
                ZipCode = model.ZipCode,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Address = model.Address,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                City = model.City.Name,
                Country = (Communication.Enums.CountryEnum)model.City.Country.Id,
                StateProvince = model.City.StateProvinceName
            };
        }

        public override void Validate(HomeModel model)
        {
            ValidateBase(model);
            ValidateStateProvince(model.City.StateProvinceName);
            ValidadeNeighborhood(model.Neighborhood);
            ValidateZipCode(model.ZipCode);
        }

        private void ValidateStateProvince(string stateProvince)
        {
            if (string.IsNullOrWhiteSpace(stateProvince))
                throw new StateProvinceEmptyException();
        }
        private void ValidadeNeighborhood(string neighborhood)
        {
            if (string.IsNullOrWhiteSpace(neighborhood))
                throw new NeighborhoodEmptyException();
        }
        public void ValidateZipCode(string zipCode)
        {
            Regex regex = new Regex(RegexExpressions.CEP);
            if (!regex.Match(zipCode).Success)
                throw new ZipCodeInvalidException(ResourceTextException.ZIPCODE_INVALID_BRAZIL);
        }
    }
}
