using FluentValidation.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;
using System.Linq;

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

            if(emergecyContacts.All(c => !string.IsNullOrWhiteSpace(c.Phonenumber)) && emergecyContacts.Select(c => c.Phonenumber).Distinct().Count() != emergecyContacts.Count)
                context.AddFailure(ResourceTextException.EMERGENCY_CONTACT_SAME_PHONENUMBER);
        }
    }
}
