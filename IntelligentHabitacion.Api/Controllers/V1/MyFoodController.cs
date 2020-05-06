using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IntelligentHabitacion.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ServiceFilter(typeof(AuthenticationUserAttribute))]
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
        [ProducesResponseType(typeof(List<ResponseProductJson>), StatusCodes.Status200OK)]
        public IActionResult MyFoods()
        {
            try
            {
                var list = _myFoodRule.GetMyFoods();
                if (list.Count == 0)
                    return NoContent();

                return Ok(list);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will add one food and return the Id
        /// </summary>
        /// <param name="requestMyFood"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddFood")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        public IActionResult AddFood(RequestAddMyFoodJson requestMyFood)
        {
            try
            {
                VerifyParameters(requestMyFood);
                var id = _myFoodRule.Create(requestMyFood);

                return Created(string.Empty, id);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will delete one user's food
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteFoods(string id)
        {
            try
            {
                VerifyParameters(id);
                _myFoodRule.Delete(id);

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
        /// <param name="changeQuantity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangeQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ChangeQuantity(RequestChangeQuantityJson changeQuantity)
        {
            try
            {
                VerifyParameters(changeQuantity);
                _myFoodRule.ChangeQuantity(changeQuantity);

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