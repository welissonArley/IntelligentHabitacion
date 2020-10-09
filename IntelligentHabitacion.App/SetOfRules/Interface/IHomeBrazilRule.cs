using IntelligentHabitacion.App.Model;
using System.Threading.Tasks;

namespace IntelligentHabitacion.App.SetOfRules.Interface
{
    public interface IHomeBrazilRule : IHomeRule
    {
        Task<HomeModel> GetLocationByZipCode(string zipCode);
    }
}
