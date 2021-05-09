using AutoMapper;
using FluentAssertions;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases;
using IntelligentHabitacion.Api.Application.UseCases.Friends.GetMyFriends;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Communication.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.CreateResponseUseCase;
using Useful.ToTests.Builders.LoggedUser;
using Useful.ToTests.Builders.Mapper;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Entity;
using Xunit;

namespace UseCases.Test.Friends.GetMyFriends
{
    public class GetMyFriendsUseCaseTest
    {
        private readonly IMapper _mapper;
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IntelligentHabitacion.Api.Domain.Entity.User _admin;

        public GetMyFriendsUseCaseTest()
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
            var friends = CreateFriends();
            var repository = UserReadOnlyRepositoryBuilder.Instance().GetByHome(_admin.HomeAssociation.HomeId, friends).Build();
            var useCase = new GetMyFriendsUseCase(repository, _mapper, _loggedUser, _unitOfWork, _intelligentHabitacionUseCase);

            var validationResult = await useCase.Execute();

            validationResult.Should().BeOfType<ResponseOutput>();
            validationResult.Token.Should().NotBeNullOrWhiteSpace();
            validationResult.ResponseJson.Should().BeOfType<List<ResponseFriendJson>>();
        }

        private IList<IntelligentHabitacion.Api.Domain.Entity.User> CreateFriends()
        {
            return new List<IntelligentHabitacion.Api.Domain.Entity.User>
            {
                new IntelligentHabitacion.Api.Domain.Entity.User
                {
                    Id = 2,
                    Name = "Friend",
                    Active = true,
                    HomeAssociation = new IntelligentHabitacion.Api.Domain.Entity.HomeAssociation
                    {
                        JoinedOn = DateTime.UtcNow,
                        HomeId = _admin.HomeAssociation.Home.Id,
                        Home = new IntelligentHabitacion.Api.Domain.Entity.Home
                        {
                            AdministratorId = _admin.Id
                        }
                    }
                }
            };
        }
    }
}
