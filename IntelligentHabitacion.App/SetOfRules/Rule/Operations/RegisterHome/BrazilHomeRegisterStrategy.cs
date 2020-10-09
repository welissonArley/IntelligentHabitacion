using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public class BrazilHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestHomeJson CreateRequestHomeJson(HomeModel model)
        {
            ValidateBase(model);
            ValidadeNeighborhood(model.Neighborhood);

            return RequestHomeJson(model);
        }

        private void ValidadeNeighborhood(string neighborhood)
        {
            if (string.IsNullOrWhiteSpace(neighborhood))
                throw new NeighborhoodEmptyException();
        }
    }
}
