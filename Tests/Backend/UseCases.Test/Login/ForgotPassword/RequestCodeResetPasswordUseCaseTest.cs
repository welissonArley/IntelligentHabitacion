using FluentAssertions;
using IntelligentHabitacion.Api.Application.UseCases.Login.ForgotPassword;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Services;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Email;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace UseCases.Test.Login.ForgotPassword
{
    public class RequestCodeResetPasswordUseCaseTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendEmail _sendEmail;
        private readonly ICodeWriteOnlyRepository _tokenWriteOnlyRepository;

        public RequestCodeResetPasswordUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _sendEmail = SendEmailBuilder.Instance().Build();
            _tokenWriteOnlyRepository = CodeWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var email = "user@email.com";

            var userRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(CreateUserModel(email)).Build();

            var useCase = new RequestCodeResetPasswordUseCase(userRepository, _tokenWriteOnlyRepository, _sendEmail, _unitOfWork);

            Func<Task> act = async () =>
            {
                await useCase.Execute(email);
            };

            await act.Should().NotThrowAsync();
        }

        private IntelligentHabitacion.Api.Domain.Entity.User CreateUserModel(string email)
        {
            return new IntelligentHabitacion.Api.Domain.Entity.User
            {
                Id = 1,
                Active = true,
                CreateDate = DateTime.UtcNow,
                Email = email
            };
        }
    }
}
