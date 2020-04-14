using IntelligentHabitacion.Api.SetOfRules.Rule;

namespace IntelligentHabitacion.Api.Test.FactoryFake
{
    public class FriendFactoryFake : BaseFactoryFake
    {
        public FriendRule GetRule()
        {
            return new FriendRule(GetLoggedUserWithouHome());
        }

        public FriendRule GetRuleLoggedUserAdministrator()
        {
            return new FriendRule(GetLoggedUserAdministrator());
        }
    }
}
