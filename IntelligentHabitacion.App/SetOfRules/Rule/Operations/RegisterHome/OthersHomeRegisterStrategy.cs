using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public class OthersHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestRegisterHomeJson CreateRequestToRegisterHome(HomeModel model)
        {
            ValidateBase(model);
            return RequestRegisterHomeJson(model);
        }

        public override RequestUpdateHomeJson CreateRequestToUpdateHome(HomeModel model)
        {
            ValidateBase(model);
            return RequestUpdateHomeJson(model);
        }
    }
}
