using IntelligentHabitacion.Api.Repository.Interface;
using IntelligentHabitacion.Api.Repository.Model;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Api.SetOfRules.LoggedUser;
using IntelligentHabitacion.Api.Validators;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using IntelligentHabitacion.Exception;
using IntelligentHabitacion.Exception.ExceptionsBase;
using IntelligentHabitacion.Useful;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentHabitacion.Api.SetOfRules.Rule
{
    public class MyFoodRule : IMyFoodRule
    {
        private readonly IMyFoodRepository _myFoodRepository;
        private readonly ILoggedUser _loggedUser;

        public MyFoodRule(ILoggedUser loggedUser, IMyFoodRepository myFoodRepository)
        {
            _loggedUser = loggedUser;
            _myFoodRepository = myFoodRepository;
        }

        public void ChangeQuantity(RequestChangeQuantityMyFoodJson changeQuantity)
        {
            var decriptedId = new MyFood().DecryptedId(changeQuantity.Id);
            var model = _myFoodRepository.GetMyFood(decriptedId, _loggedUser.User().Id);
            if (model is null)
                throw new ProductNotFoundException();

            if (changeQuantity.Quantity <= 0)
                _myFoodRepository.DeleteOnDatabase(model);
            else
            {
                model.Quantity = changeQuantity.Quantity;
                _myFoodRepository.Update(model);
            }
        }

        public string Create(RequestAddMyFoodJson requestMyFood)
        {
            var model = new Mapper.Mapper().MapperJsonToModel(requestMyFood);
            model.UserId = _loggedUser.User().Id;

            var validation = new MyFoodValidator().Validate(model);

            if (validation.IsValid)
                _myFoodRepository.Create(model);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());

            return model.EncryptedId();
        }

        public void Delete(string encryptedId)
        {
            var decriptedId = new MyFood().DecryptedId(encryptedId);
            var model = _myFoodRepository.GetMyFood(decriptedId, _loggedUser.User().Id);
            if (model is null)
                throw new ProductNotFoundException();

            _myFoodRepository.DeleteOnDatabase(model);
        }

        public void Edit(RequestEditMyFoodJson editMyFood)
        {
            var decriptedId = new MyFood().DecryptedId(editMyFood.Id);
            var model = _myFoodRepository.GetMyFood(decriptedId, _loggedUser.User().Id);
            if (model is null)
                throw new ProductNotFoundException();

            model.Quantity = editMyFood.Quantity;
            model.DueDate = editMyFood.DueDate;
            model.Manufacturer = editMyFood.Manufacturer;
            model.Name = editMyFood.Name;
            model.Type = (Repository.Model.Type)editMyFood.Type;
            model.UpdateDate = DateTimeController.DateTimeNow();

            var validation = new MyFoodValidator().Validate(model);

            if (validation.IsValid)
                _myFoodRepository.Update(model);
            else
                throw new ErrorOnValidationException(validation.Errors.Select(c => c.ErrorMessage).ToList());
        }

        public List<ResponseMyFoodJson> GetMyFoods()
        {
            var list = _myFoodRepository.GetMyFoods(_loggedUser.User().Id);
            var mapper = new Mapper.Mapper();
            return list.Select(c => mapper.MapperModelToJson(c)).ToList();
        }
    }
}
