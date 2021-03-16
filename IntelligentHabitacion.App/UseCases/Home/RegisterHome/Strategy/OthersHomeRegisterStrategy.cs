using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.App.UseCases.Home.RegisterHome.Strategy
{
    public class OthersHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestRegisterHomeJson Mapper(HomeModel model)
        {
            return new RequestRegisterHomeJson
            {
                ZipCode = model.ZipCode,
                AdditionalAddressInfo = model.AdditionalAddressInfo,
                Address = model.Address,
                Number = model.Number,
                City = model.City.Name,
                Country = (Communication.Enums.CountryEnum)model.City.Country.Id,
                StateProvince = model.City.StateProvinceName
            };
        }

        public override void Validate(HomeModel model)
        {
            ValidateBase(model);
        }
    }
}
