using Microsoft.AspNetCore.Mvc;

namespace IntelligentHabitacion.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parametro"></param>
        protected void VerificarParametro(object parametro)
        {
            var parametroEString = parametro is string;
            if (parametroEString)
            {
                if (string.IsNullOrEmpty((string)parametro))
                    throw new System.Exception();
            }
            else if (parametro == null)
            {
                throw new System.Exception();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        protected ObjectResult TratarException(System.Exception exception)
        {
            return StatusCode(500, string.Empty);
        }
    }
}
