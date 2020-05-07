﻿using IntelligentHabitacion.Api.Filter;
using IntelligentHabitacion.Api.SetOfRules.Interface;
using IntelligentHabitacion.Communication.Request;
using IntelligentHabitacion.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentHabitacion.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HomeController : BaseController
    {
        private readonly IHomeRule _homeRule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="homeRule"></param>
        public HomeController(IHomeRule homeRule)
        {
            _homeRule = homeRule;
        }

        /// <summary>
        /// This function verify if the homes's informations is correct and save the informations on database
        /// </summary>
        /// <param name="registerHomeJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Route("Register")]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        public IActionResult Register(RequestHomeJson registerHomeJson)
        {
            try
            {
                VerifyParameters(registerHomeJson);

                _homeRule.Register(registerHomeJson);
                return Created(string.Empty, string.Empty);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will return the home's informations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Informations")]
        [ProducesResponseType(typeof(ResponseHomeInformationsJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserAttribute))]
        public IActionResult Informations()
        {
            try
            {
                var informations = _homeRule.GetInformations();
                return Ok(informations);
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }

        /// <summary>
        /// This function will update the Home's informations
        /// </summary>
        /// <param name="updateHomeJson"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceFilter(typeof(AuthenticationUserIsAdminAttribute))]
        public IActionResult Update(RequestHomeJson updateHomeJson)
        {
            try
            {
                VerifyParameters(updateHomeJson);
                _homeRule.Update(updateHomeJson);
                return Ok();
            }
            catch (System.Exception exception)
            {
                return HandleException(exception);
            }
        }
    }
}