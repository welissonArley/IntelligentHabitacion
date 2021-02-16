﻿using AutoMapper;
using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Application.UseCases.EmailAlreadyBeenRegistered;
using IntelligentHabitacion.Api.Domain.Entity;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.User;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using System.Linq;

namespace IntelligentHabitacion.Api.Application.UseCases.UpdateUserInformations
{
    public class UpdateUserInformationsUseCase : IUpdateUserInformationsUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailAlreadyBeenRegisteredUseCase _registeredUseCase;
        private readonly IMapper _mapper;
        private readonly IUserUpdateOnlyRepository _repository;

        public UpdateUserInformationsUseCase(ILoggedUser loggedUser, IMapper mapper,
            IUserUpdateOnlyRepository repository, IUnitOfWork unitOfWork,
            IEmailAlreadyBeenRegisteredUseCase registeredUseCase, IntelligentHabitacionUseCase intelligentHabitacionUseCase)
        {
            _loggedUser = loggedUser;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
            _registeredUseCase = registeredUseCase;
        }

        public ResponseOutput Execute(RequestUpdateUserJson updateUserJson)
        {
            var loggedUser = _loggedUser.User();

            Validate(updateUserJson, loggedUser);

            var userToUpdate = _repository.GetById_Update(loggedUser.Id);

            userToUpdate.Name = updateUserJson.Name;
            userToUpdate.Email = updateUserJson.Email;
            userToUpdate.Phonenumbers.Clear();
            userToUpdate.EmergencyContacts.Clear();

            userToUpdate.Phonenumbers = updateUserJson.Phonenumbers.Select(c => _mapper.Map<Phonenumber>(c)).ToList();
            userToUpdate.EmergencyContacts = updateUserJson.EmergencyContacts.Select(c => _mapper.Map<EmergencyContact>(c)).ToList();

            _repository.Update(userToUpdate);
            var response = _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            _unitOfWork.Commit();

            return response;
        }

        private void Validate(RequestUpdateUserJson updateUserJson, User userDataNow)
        {
            var validation = new UpdateUserInformationsValidation().Validate(updateUserJson);

            if (!userDataNow.Email.Equals(updateUserJson.Email) && _registeredUseCase.Execute(updateUserJson.Email).Value)
                validation.Errors.Add(new FluentValidation.Results.ValidationFailure("", ResourceTextException.EMAIL_ALREADYBEENREGISTERED));

            if (!validation.IsValid)
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }
    }
}