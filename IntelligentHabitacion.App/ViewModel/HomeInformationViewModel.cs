using IntelligentHabitacion.App.Model;
using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.App.SQLite.Interface;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.ViewModel
{
    public class HomeInformationViewModel : BaseViewModel
    {
        private readonly IHomeRule _homeRule;

        public bool IsAdministrator { get; set; }

        public HomeModel Model { get; set; }

        public HomeInformationViewModel(IHomeRule homeRule, ISqliteDatabase database)
        {
            IsAdministrator = database.Get().IsAdministrator;
            _homeRule = homeRule;
            Model = Task.Run(async () => await homeRule.GetInformations()).Result;
        }
    }
}
