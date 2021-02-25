using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ProcessFoodsNextToDueDate
{
    public class ProcessFoodsNextToDueDateUseCasse : IProcessFoodsNextToDueDate
    {
        private readonly IMyFoodsWriteOnlyRepository _myFoodWriteRepository;
        private readonly IMyFoodsReadOnlyRepository _myFoodReadOnlyRepository;
        private readonly IPushNotificationService _pushNotificationService;

        public ProcessFoodsNextToDueDateUseCasse(IMyFoodsWriteOnlyRepository myFoodWriteRepository,
            IMyFoodsReadOnlyRepository myFoodReadOnlyRepository,
            IPushNotificationService pushNotificationService)
        {
            _myFoodWriteRepository = myFoodWriteRepository;
            _myFoodReadOnlyRepository = myFoodReadOnlyRepository;
            _pushNotificationService = pushNotificationService;
        }

        public async Task Execute()
        {
            var query = await _myFoodReadOnlyRepository.GetExpiredOrCloseToDueDate();

            var group = query.GroupBy(c => c.UserId);

            var users = query.Select(c => c.User).Distinct();

            if (query.Count() > 0)
            {
                foreach (var result in group)
                {
                    var foodList = result.ToList();
                    ProcessFoodList(users.First(c => c.Id == result.Key), foodList);
                }
            }
        }

        private void ProcessFoodList(User user, List<MyFood> listFoods)
        {
            var today = DateTime.UtcNow.Date;
            Dictionary<string, string> titles;
            Dictionary<string, string> messages;

            foreach (var food in listFoods)
            {
                var totalDays = (food.DueDate.Value - today).TotalDays;
                switch (totalDays)
                {
                    case 7:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "7 days until product expiration ⌛" },
                                { "pt", "Faltam 7 dias para vencimento de produto ⌛" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product will expire next week." },
                                { "pt", $"Seu produto {food.Name} irá vencer na próxima semana." }
                            };
                        }
                        break;
                    case 3:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "3 days until product expiration ⌛" },
                                { "pt", "Faltam 3 dias para vencimento de produto ⌛" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product will expire in three days." },
                                { "pt", $"Seu produto {food.Name} irá vencer dentro de três dias." }
                            };
                        }
                        break;
                    case 1:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "✘ TOMORROW, has product with expiration date for TOMORROW" },
                                { "pt", "✘ AMANHÃ, tem produto com data de vencimento para AMANHÃ" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product has the expiration date for tomorrow." },
                                { "pt", $"Seu produto {food.Name} possui a data de vencimento para amanhã." }
                            };
                        }
                        break;
                    case 0:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "Has product with expiration date for TODAY ⚠" },
                                { "pt", "Tem produto com data de vencimento para HOJE ⚠" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product has the expiration date for today." },
                                { "pt", $"Seu produto {food.Name} possui a data de vencimento para hoje." }
                            };
                        }
                        break;
                    case -1:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "HEY, you have a product that expired yesterday ⌚" },
                                { "pt", "HEY, você tem um produto que venceu ontem ⌚" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product has the expiration date for yesterday." },
                                { "pt", $"Seu produto {food.Name} venceu ontem." }
                            };
                        }
                        break;
                    case -2:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "Two days passed and ⌚" },
                                { "pt", "Dois dias se passaram e ... ⌚" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product expiration two days ago." },
                                { "pt", $"Seu produto {food.Name} venceu há dois dias." }
                            };
                        }
                        break;
                    default:
                        {
                            titles = new Dictionary<string, string>
                            {
                                { "en", "Deleted product ♻" },
                                { "pt", "Produto excluido ♻" }
                            };
                            messages = new Dictionary<string, string>
                            {
                                { "en", $"Your {food.Name} product has been deleted: it has been expired for more than two days." },
                                { "pt", $"Seu produto {food.Name} foi excluido: estava vencido há mais de dois dias." }
                            };
                            _myFoodWriteRepository.Delete(food);
                        }
                        break;
                }
                
                SendNotification(user, titles, messages);
            }
        }

        private void SendNotification(User user, Dictionary<string, string> titles, Dictionary<string, string> messages)
        {
            var random = new Random();

            var today = DateTime.Today.Date;
            var ts = new TimeSpan(random.Next(7, 11), random.Next(0, 59), 0);

            _pushNotificationService.Send(titles, messages, new List<string> { user.PushNotificationId }, today + ts);
        }
    }
}
