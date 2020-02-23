﻿using IntelligentHabitacion.App.SetOfRules.Interface;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Validators.Validator;

namespace IntelligentHabitacion.App.SetOfRules.Rule
{
    public class LoginRule : ILoginRule
    {
        public void ChangePasswordForgetPassword(string email, string code, string newPassword, string confirmationPassword)
        {
            ValidateEmail(email);

            if (string.IsNullOrWhiteSpace(code))
                throw new CodeEmptyException();

            ValidatePasswordAndPasswordConfirmation(newPassword, confirmationPassword);
        }

        public void Login(string email, string password)
        {
            ValidateEmail(email);

            if (string.IsNullOrWhiteSpace(password))
                throw new PasswordEmptyException();
        }

        public void RequestCode(string email)
        {
            ValidateEmail(email);
        }

        private void ValidateEmail(string email)
        {
            new EmailValidator().IsValid(email);
        }

        private void ValidatePasswordAndPasswordConfirmation(string newPassword, string confirmationPassword)
        {
            new PasswordValidator().IsValidaPasswordAndConfirmation(newPassword, confirmationPassword);
        }

        public void ChangePassword(string currentPassword, string newPassword, string confirmationPassword)
        {
            if (string.IsNullOrWhiteSpace(currentPassword))
                throw new CurrentPasswordEmptyException();

            ValidatePasswordAndPasswordConfirmation(newPassword, confirmationPassword);
        }
    }
}
