using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Friends.ChangeDateFriendJoinHome;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using System;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.Friends.ChangeDateFriendJoinHome
{
    public class ChangeDateFriendJoinHomeUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IntelligentHabitacion.Api.Domain.Entity.User _admin;

        public ChangeDateFriendJoinHomeUseCaseTest()
        {
            _admin = UserBuilder.Instance().User_WithHomeAssociation();

            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _mapper = MapperBuilder.Build();
            _intelligentHabitacionUseCase = IntelligentHabitacionUseCaseBuilder.Instance().Build();
            _loggedUser = LoggedUserBuilder.Instance().User(_admin).Build();
        }

        [Fact]
        public async Task Validade_Sucess()
        {
            var friend = CreateFriend();
            var repository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(friend).Build();
            var useCase = new ChangeDateFriendJoinHomeUseCase(repository, _mapper, _loggedUser, _unitOfWork, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute(friend.Id, new RequestDateJson
            {
                Date = DateTime.UtcNow.AddDays(-4)
            });

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<ResponseFriendJson>();
        }

        [Fact]
        public async Task Validade_FriendNotFound()
        {
            var repository = UserUpdateOnlyRepositoryBuilder.Instance().Build();
            var useCase = new ChangeDateFriendJoinHomeUseCase(repository, _mapper, _loggedUser, _unitOfWork, _intelligentHabitacionUseCase);

            Func<Task> act = async () =>
            {
                await useCase.Execute(0, new RequestDateJson
                {
                    Date = DateTime.UtcNow.AddDays(-4)
                });
            };

            await act.Should().ThrowAsync<FriendNotFoundException>().WithMessage(ResourceTextException.FRIEND_NOT_FOUND);
        }

        [Fact]
        public async Task Validade_FriendNotPartSameHome()
        {
            var friend = CreateFriend();
            friend.HomeAssociation.Home.AdministratorId = 0;
            var repository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(friend).Build();
            var useCase = new ChangeDateFriendJoinHomeUseCase(repository, _mapper, _loggedUser, _unitOfWork, _intelligentHabitacionUseCase);

            Func<Task> act = async () =>
            {
                await useCase.Execute(friend.Id, new RequestDateJson
                {
                    Date = DateTime.UtcNow.AddDays(-4)
                });
            };

            await act.Should().ThrowAsync<YouCannotPerformThisActionException>().WithMessage(ResourceTextException.YOU_CANNNOT_PERMORM_THIS_ACTION);
        }

        [Fact]
        public async Task Validade_InvalidDate()
        {
            var friend = CreateFriend();
            var repository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(friend).Build();
            var useCase = new ChangeDateFriendJoinHomeUseCase(repository, _mapper, _loggedUser, _unitOfWork, _intelligentHabitacionUseCase);

            Func<Task> act = async () =>
            {
                await useCase.Execute(friend.Id, new RequestDateJson
                {
                    Date = DateTime.UtcNow.AddDays(4)
                });
            };

            await act.Should().ThrowAsync<InvalidDateException>();
        }

        private IntelligentHabitacion.Api.Domain.Entity.User CreateFriend()
        {
            return new IntelligentHabitacion.Api.Domain.Entity.User
            {
                Id = 1,
                Name = "Friend",
                Active = true,
                HomeAssociation = new IntelligentHabitacion.Api.Domain.Entity.HomeAssociation
                {
                    JoinedOn = DateTime.UtcNow,
                    Home = new IntelligentHabitacion.Api.Domain.Entity.Home
                    {
                        AdministratorId = _admin.Id
                    }
                }
            };
        }
    }
}
