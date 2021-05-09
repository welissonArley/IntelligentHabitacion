using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Friends.RemoveFriend;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Api.Domain.Services;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.Email;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.Friends.RemoveFriend
{
    public class RequestCodeToRemoveFriendUseCaseTest
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly ICodeWriteOnlyRepository _repository;
        private readonly ISendEmail _emailHelper;
        private readonly IUnitOfWork _unitOfWork;

        public RequestCodeToRemoveFriendUseCaseTest()
        {
            var admin = UserBuilder.Instance().User_WithHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(admin).Build();
            _repository = CodeWriteOnlyRepositoryBuilder.Instance().Build();
            _emailHelper = SendEmailBuilder.Instance().Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var useCase = new RequestCodeToRemoveFriendUseCase(_loggedUser, _repository, _emailHelper, _unitOfWork, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeNull();
        }
    }
}
