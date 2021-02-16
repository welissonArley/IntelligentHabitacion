using FluentValidation.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Application.SharedValidators
{
    public class EmergencyContactsValidator
    {
        public void IsValid(ICollection<RequestEmergencyContactJson> emergecyContacts, CustomContext context)
        {
            var index = 1;

            foreach (var emergecyContact in emergecyContacts)
            {
                if (string.IsNullOrWhiteSpace(emergecyContact.Name))
                    context.AddFailure(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, index));

                if (string.IsNullOrWhiteSpace(emergecyContact.Relationship))
                    context.AddFailure(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, index));

                if (string.IsNullOrWhiteSpace(emergecyContact.Phonenumber))
                    context.AddFailure(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, index));

                index++;
            }
        }
    }
}
