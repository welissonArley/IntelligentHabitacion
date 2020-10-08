using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.Communication.Request;

namespace IntelligentHabitacion.App.SetOfRules.Rule.Operations.RegisterHome
{
    public class OthersHomeRegisterStrategy : HomeRegisterStrategy
    {
        public override RequestHomeJson CreateRequestHomeJson(HomeModel model)
        {
            ValidateBase(model);
            return RequestHomeJson(model);
        }
    }
}
