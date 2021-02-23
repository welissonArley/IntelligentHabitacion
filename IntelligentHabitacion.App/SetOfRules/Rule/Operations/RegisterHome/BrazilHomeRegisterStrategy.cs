using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public class BrazilHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestRegisterHomeJson CreateRequestToRegisterHome(HomeModel model)
        {
            ValidateBase(model);
            ValidadeNeighborhood(model.Neighborhood);

            return RequestRegisterHomeJson(model);
        }

        public override RequestUpdateHomeJson CreateRequestToUpdateHome(HomeModel model)
        {
            ValidateBase(model);
            ValidadeNeighborhood(model.Neighborhood);

            return RequestUpdateHomeJson(model);
        }

        private void ValidadeNeighborhood(string neighborhood)
        {
            if (string.IsNullOrWhiteSpace(neighborhood))
                throw new NeighborhoodEmptyException();
        }
    }
}
