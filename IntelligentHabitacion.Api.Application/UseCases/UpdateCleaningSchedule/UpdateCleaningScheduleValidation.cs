using FluentValidation;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateCleaningSchedule
{
    public class UpdateCleaningScheduleValidation : AbstractValidator<List<RequestUpdateCleaningScheduleJson>>
    {
        public UpdateCleaningScheduleValidation()
        {
            RuleFor(c => c).Must(c => c.Select(k => k.UserId).Distinct().Count() == c.Count()).WithMessage(ResourceTextException.THERE_ARE_DUPLICATE_USERS_REQUEST);
            RuleForEach(c => c).ChildRules(users =>
            {
                users.RuleFor(c => c.Rooms).Must(c => c.Distinct().Count() == c.Count()).WithMessage(ResourceTextException.THERE_ARE_USERS_DUPLICATE_TASKS_REQUEST);
            });
            RuleFor(c => c).Must(c => c.SelectMany(k => k.Rooms).Distinct().Count() == c.SelectMany(k => k.Rooms).Count()).WithMessage(ResourceTextException.THERE_AREA_TASKS_ASSOCIATED_SEVERAL_USERS);
            RuleFor(c => c).Must(c => c.SelectMany(k => k.Rooms).Any()).WithMessage(ResourceTextException.ALL_USER_WITHOUT_CLEANING_TASKS);
        }
    }
}
