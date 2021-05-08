using FluentAssertions;
using HashidsNet;
using IntelligentHabitacion.Api.Application.Services.Token;
using IntelligentHabitacion.Api.Application.UseCases.Friends.AddFriends;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.Code;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.API;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Hashids;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.TokenController;
using Xunit;

namespace UseCases.Test.Friends.AddFriends
{
    public class AddFriendUseCaseTest
    {
        private readonly IHashids _hashIds;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeWriteOnlyRepository _codeWriteRepository;
        private readonly TokenController _tokenController;

        public AddFriendUseCaseTest()
        {
            _unitOfWork = UnitOfWorkBuilder.Instance().Build();
            _hashIds = HashidsBuilder.Instance().Build();
            _tokenController = TokenControllerBuilder.Instance().Build();
            _codeWriteRepository = CodeWriteOnlyRepositoryBuilder.Instance().Build();
        }

        [Fact]
        public async Task ApproveFriend_Sucess()
        {
            var friend = CreateFriend();
            var admin = CreateAdmin();
            var userUpdateRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(friend).Build();
            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetById(admin).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, null, userReadRepository, userUpdateRepository, _tokenController);

            Func<Task> act = async () =>
            {
                await usecase.ApproveFriend(_hashIds.EncodeLong(admin.Id), _hashIds.EncodeLong(friend.Id), new RequestApproveAddFriendJson
                {
                    JoinedOn = DateTime.UtcNow.AddDays(-30),
                    MonthlyRent = 600
                });
            };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ApproveFriend_MonthlyRentInvalid()
        {
            var friend = CreateFriend();
            var admin = CreateAdmin();
            var userUpdateRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(friend).Build();
            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetById(admin).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, null, userReadRepository, userUpdateRepository, _tokenController);

            Func<Task> act = async () =>
            {
                await usecase.ApproveFriend(_hashIds.EncodeLong(admin.Id), _hashIds.EncodeLong(friend.Id), new RequestApproveAddFriendJson
                {
                    JoinedOn = DateTime.UtcNow.AddDays(-30),
                    MonthlyRent = -30
                });
            };

            await act.Should().ThrowAsync<MonthlyRentInvalidException>().WithMessage(ResourceTextException.MONTHLYRENT_INVALID);
        }

        [Fact]
        public async Task ApproveFriend_InvalidDate()
        {
            var friend = CreateFriend();
            var admin = CreateAdmin();
            var userUpdateRepository = UserUpdateOnlyRepositoryBuilder.Instance().GetById(friend).Build();
            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetById(admin).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, null, userReadRepository, userUpdateRepository, _tokenController);

            Func<Task> act = async () =>
            {
                await usecase.ApproveFriend(_hashIds.EncodeLong(admin.Id), _hashIds.EncodeLong(friend.Id), new RequestApproveAddFriendJson
                {
                    JoinedOn = DateTime.UtcNow.AddDays(30),
                    MonthlyRent = 600
                });
            };

            await act.Should().ThrowAsync<InvalidDateException>().WithMessage(string.Format(ResourceTextException.DATE_MUST_BE_LESS_THAN, DateTime.UtcNow.ToShortDateString()));
        }

        [Fact]
        public async Task CodeWasRead_Sucess()
        {
            var friend = CreateFriend();
            var admin = CreateAdmin();
            var code = CreateCode();

            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetById(admin).GetByEmail(friend).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByCode(code).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, codeReadOnlyRepository, userReadRepository, null, _tokenController);

            var response = await usecase.CodeWasRead(_tokenController.Generate(friend.Email), code.Value);

            response.Should().BeOfType<ResponseCodeWasReadJson>();
        }

        [Fact]
        public async Task CodeWasRead_FriendWithHomeAssociationId()
        {
            var friend = CreateFriend();
            friend.HomeAssociationId = 1;
            var admin = CreateAdmin();
            var code = CreateCode();

            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetById(admin).GetByEmail(friend).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByCode(code).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, codeReadOnlyRepository, userReadRepository, null, _tokenController);

            Func<Task> act = async () =>
            {
                await usecase.CodeWasRead(_tokenController.Generate(friend.Email), code.Value);
            };

            await act.Should().ThrowAsync<IntelligentHabitacionException>().WithMessage(ResourceTextException.USER_IS_PART_OF_A_HOME);
        }

        [Fact]
        public async Task CodeWasRead_CodeNotFound()
        {
            var friend = CreateFriend();
            var admin = CreateAdmin();

            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetById(admin).GetByEmail(friend).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, codeReadOnlyRepository, userReadRepository, null, _tokenController);

            Func<Task> act = async () =>
            {
                await usecase.CodeWasRead(_tokenController.Generate(friend.Email), "");
            };

            await act.Should().ThrowAsync<IntelligentHabitacionException>().WithMessage(ResourceTextException.CODE_INVALID);
        }

        [Fact]
        public async Task GetCodeToAddFriend_Sucess()
        {
            var admin = CreateAdmin();

            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(admin).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, null, userReadRepository, null, _tokenController);

            var response = await usecase.GetCodeToAddFriend(_tokenController.Generate(admin.Email));

            response.Should().BeOfType<ResponseCodeToAddFriendJson>();
            response.AdminId.Should().NotBeNullOrWhiteSpace();
            response.Code.Should().NotBeNullOrWhiteSpace();
        }
        
        [Fact]
        public async Task GetCodeToAddFriend_UserIsNotTheAdm()
        {
            var admin = CreateAdmin();
            admin.HomeAssociation.Home.AdministratorId = 0;

            var userReadRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(admin).Build();

            var usecase = new AddFriendUseCase(_hashIds, _unitOfWork, _codeWriteRepository, null, userReadRepository, null, _tokenController);

            Func<Task> act = async () =>
            {
                await usecase.GetCodeToAddFriend(_tokenController.Generate(admin.Email));
            };

            await act.Should().ThrowAsync<IntelligentHabitacionException>().WithMessage(ResourceTextException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
        }

        private IntelligentHabitacion.Api.Domain.Entity.User CreateFriend()
        {
            return new IntelligentHabitacion.Api.Domain.Entity.User
            {
                Id = 1,
                Email = "friend@email.com",
                Phonenumbers = new List<IntelligentHabitacion.Api.Domain.Entity.Phonenumber> { new IntelligentHabitacion.Api.Domain.Entity.Phonenumber { Number = "31 9 9999-9999" } },
                EmergencyContacts = new List<IntelligentHabitacion.Api.Domain.Entity.EmergencyContact>
                {
                    new IntelligentHabitacion.Api.Domain.Entity.EmergencyContact
                    {
                        Name = "Contact",
                        Relationship = "Mother",
                        Phonenumber = "31 9 8888-8888"
                    }
                }
            };
        }
        private IntelligentHabitacion.Api.Domain.Entity.User CreateAdmin()
        {
            return new IntelligentHabitacion.Api.Domain.Entity.User
            {
                Id = 1,
                Email = "admin@email.com",
                HomeAssociationId = 1,
                HomeAssociation = new IntelligentHabitacion.Api.Domain.Entity.HomeAssociation
                {
                    HomeId = 1,
                    Home = new IntelligentHabitacion.Api.Domain.Entity.Home
                    {
                        AdministratorId = 1
                    }
                }
            };
        }
        private IntelligentHabitacion.Api.Domain.Entity.Code CreateCode()
        {
            return new IntelligentHabitacion.Api.Domain.Entity.Code
            {
                Value = "ABC123",
                UserId = 1
            };
        }
    }
}
