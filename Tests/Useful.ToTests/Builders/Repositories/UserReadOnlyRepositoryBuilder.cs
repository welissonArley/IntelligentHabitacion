﻿using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository.User;
using Moq;

namespace Useful.ToTests.Builders.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private static UserReadOnlyRepositoryBuilder _instance;
        private readonly Mock<IUserReadOnlyRepository> _repository;

        private UserReadOnlyRepositoryBuilder()
        {
            if (_repository == null)
            {
                _repository = new Mock<IUserReadOnlyRepository>();
            }
        }

        public static UserReadOnlyRepositoryBuilder Instance()
        {
            _instance = new UserReadOnlyRepositoryBuilder();
            return _instance;
        }

        public UserReadOnlyRepositoryBuilder GetByEmail(string email, User user)
        {
            _repository.Setup(x => x.GetByEmail(email)).ReturnsAsync(user);
            return this;
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}