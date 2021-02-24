using IntelligentHabitacion.Api.Application.UseCases.ChangeQuantityOfOneProduct;
using IntelligentHabitacion.Api.Application.UseCases.GetMyFoods;
using IntelligentHabitacion.Api.Application.UseCases.RegisterMyFood;
using IntelligentHabitacion.Api.Binder;
using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ServiceFilter(typeof(AuthenticationUserIsPartOfHomeAttribute))]
    public class MyFoodController : BaseController
    {
        private readonly IMyFoodRule _myFoodRule;

        /// <summary>
        /// 
        /// </summary>
        public MyFoodController(IMyFoodRule myFoodRule)
        {
            _myFoodRule = myFoodRule;
        }

        /// <summary>
        /// This function will return the list of foods
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MyFoods")]
        [ProducesResponseType(typeof(List<ResponseMyFoodJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MyFoods([FromServices] IGetMyFoodsUseCase useCase)
        {
            try
            {
                var response = await useCase.Execute();
                WriteAutenticationHeader(response);

                if (!((List<ResponseMyFoodJson>)response.ResponseJson).Any())
                    return NoContent();

                return Ok(response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will add one food and return the Id
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="requestMyFood"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddFood")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddFood(
            [FromServices] IRegisterMyFoodUseCase useCase,
            [FromBody] RequestAddMyFoodJson requestMyFood)
        {
            try
            {
                VerifyParameters(requestMyFood);

                var response = await useCase.Execute(requestMyFood);
                WriteAutenticationHeader(response);

                return Created(string.Empty, response.ResponseJson);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will delete one user's food
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteFoods([FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
        {
            try
            {
                VerifyParameters(id);
                //_myFoodRule.Delete(id);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will change the quantity of one product. If the quantity is less or equals 0, the product will be deleted
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeQuantity/{id:hashids}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeQuantity(
            [FromServices] IChangeQuantityOfOneProductUseCase useCase,
            [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id,
            [FromBody] RequestChangeQuantityMyFoodJson request)
        {
            try
            {
                var response = await useCase.Execute(id, request.Amount);
                WriteAutenticationHeader(response);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editMyFood"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("EditFood")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult EditFood(RequestEditMyFoodJson editMyFood)
        {
            try
            {
                VerifyParameters(editMyFood);
                _myFoodRule.Edit(editMyFood);

                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}