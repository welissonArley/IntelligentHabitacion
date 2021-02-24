using IntelligentHabitacion.Api.Application.Services.LoggedUser;
using IntelligentHabitacion.Api.Domain.Repository;
using IntelligentHabitacion.Api.Domain.Repository.MyFoods;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Exception;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Application.UseCases.ChangeQuantityOfOneProduct
{
    public class ChangeQuantityOfOneProductUseCase : IChangeQuantityOfOneProductUseCase
    {
        private readonly IntelligentHabitacionUseCase _intelligentHabitacionUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;
        private readonly IMyFoodsReadOnlyRepository _repositoryReadOnly;
        private readonly IMyFoodsWriteOnlyRepository _repository;

        public ChangeQuantityOfOneProductUseCase(IMyFoodsReadOnlyRepository repositoryReadOnly,
            IMyFoodsWriteOnlyRepository repository, IUnitOfWork unitOfWork, IntelligentHabitacionUseCase intelligentHabitacionUseCase,
            ILoggedUser loggedUser)
        {
            _repositoryReadOnly = repositoryReadOnly;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _intelligentHabitacionUseCase = intelligentHabitacionUseCase;
            _loggedUser = loggedUser;
        }

        public async Task<ResponseOutput> Execute(long id, decimal amount)
        {
            var loggedUser = await _loggedUser.User();
            
            var model = await _repositoryReadOnly.GetById(id, loggedUser.Id);
            if (model is null)
                throw new ProductNotFoundException();

            if (amount <= 0)
                _repository.Delete(model);
            else
                await _repository.ChangeAmount(model.Id, amount);
            
            var response = await _intelligentHabitacionUseCase.CreateResponse(loggedUser.Email, loggedUser.Id);

            await _unitOfWork.Commit();

            return response;
        }
    }
}
