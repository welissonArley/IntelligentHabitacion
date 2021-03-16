using IntelligentHabitacion.App.Model;

namespace IntelligentHabitacion.App.UseCases.Home.RegisterHome.Strategy
{
    public class ContextStrategy
    {
        public HomeRegisterStrategy GetStrategy(CountryModel country)
        {
            switch (country.Id)
            {
                case Useful.CountryEnum.BRAZIL:
                    {
                        return new BrazilHomeRegisterStrategy();
                    }
                default:
                    {
                        return new OthersHomeRegisterStrategy();
                    }
            }
        }
    }
}
