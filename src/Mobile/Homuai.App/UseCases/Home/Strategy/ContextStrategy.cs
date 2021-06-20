using Homuai.App.Model;

namespace Homuai.App.UseCases.Home.Strategy
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
