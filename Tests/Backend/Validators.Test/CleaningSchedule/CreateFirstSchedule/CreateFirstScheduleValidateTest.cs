using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.CleaningSchedule.CreateFirstSchedule;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace Validators.Test.CleaningSchedule.CreateFirstSchedule
{
    public class CreateFirstScheduleValidateTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var homeId = 1;
            var cleaningScheduleRepository = CleaningScheduleReadOnlyRepositoryBuilder.Instance();

            var validator = new CreateFirstScheduleValidate(cleaningScheduleRepository.Build(), homeId);
            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1A",
                    Rooms = new List<string>{ "Kitchen", "Living room" }
                },
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1B",
                    Rooms = new List<string>{ "Dining room", "Bathroom" }
                }
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_HomeHasCleaningScheduleCreated()
        {
            var homeId = 1;
            
            var cleaningScheduleRepository = CleaningScheduleReadOnlyRepositoryBuilder.Instance().HomeHasCleaningScheduleCreated(homeId);

            var validator = new CreateFirstScheduleValidate(cleaningScheduleRepository.Build(), homeId);
            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1A",
                    Rooms = new List<string>{ "Kitchen", "Living room" }
                },
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1B",
                    Rooms = new List<string>{ "Dining room", "Bathroom" }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(c => c.ErrorMessage.Equals(ResourceTextException.CLEANING_SCHEDULE_ALREADY_CREATED));
        }

        [Fact]
        public async Task Validade_DuplicatedUsers()
        {
            var homeId = 1;

            var cleaningScheduleRepository = CleaningScheduleReadOnlyRepositoryBuilder.Instance();

            var validator = new CreateFirstScheduleValidate(cleaningScheduleRepository.Build(), homeId);
            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1A",
                    Rooms = new List<string>{ "Kitchen", "Living room" }
                },
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1A",
                    Rooms = new List<string>{ "Dining room", "Bathroom" }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(c => c.ErrorMessage.Equals(ResourceTextException.THERE_ARE_DUPLICATE_USERS_REQUEST));
        }

        [Fact]
        public async Task Validade_DuplicatedTasks()
        {
            var homeId = 1;

            var cleaningScheduleRepository = CleaningScheduleReadOnlyRepositoryBuilder.Instance();

            var validator = new CreateFirstScheduleValidate(cleaningScheduleRepository.Build(), homeId);
            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1A",
                    Rooms = new List<string>{ "Kitchen", "Kitchen" }
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(c => c.ErrorMessage.Equals(ResourceTextException.THERE_ARE_USERS_DUPLICATE_TASKS_REQUEST));
        }

        [Fact]
        public async Task Validade_AllUsersWithoutTasks()
        {
            var homeId = 1;

            var cleaningScheduleRepository = CleaningScheduleReadOnlyRepositoryBuilder.Instance();

            var validator = new CreateFirstScheduleValidate(cleaningScheduleRepository.Build(), homeId);
            var validationResult = await validator.ValidateAsync(new List<RequestUpdateCleaningScheduleJson>
            {
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1A"
                },
                new RequestUpdateCleaningScheduleJson
                {
                    UserId = "1B"
                }
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(c => c.ErrorMessage.Equals(ResourceTextException.ALL_USER_WITHOUT_CLEANING_TASKS));
        }
    }
}
