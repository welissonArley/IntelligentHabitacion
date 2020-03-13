using IntelligentHabitacion.Api.Repository.WorkUnit;
using Microsoft.AspNetCore.Http;
using NHibernate;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class IntelligentHabitacionMiddleware
    {
        readonly RequestDelegate _next;

        private readonly List<string> _idioms = new List<string> { "EN", "PT", "PT-BR", "EN-US" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public IntelligentHabitacionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionFactory"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ISessionFactory sessionFactory)
        {
            if (!context.Request.Path.Value.Contains("swagger") && WorkUnitNHibernate.WorkUnitNHibernateActive == null)
            {
                var culture = new CultureInfo("pt-BR");
                if (context.Request.Headers["Accept-Language"].Count > 0 && _idioms.Contains(context.Request.Headers["Accept-Language"][0].ToUpper()))
                    culture = new CultureInfo(context.Request.Headers["Accept-Language"][0]);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

                WorkUnitNHibernate.WorkUnitNHibernateActive = new WorkUnitNHibernate(sessionFactory);

                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status200OK || context.Response.StatusCode == StatusCodes.Status201Created)
                    WorkUnitNHibernate.WorkUnitNHibernateActive.Commit();
                else
                    WorkUnitNHibernate.WorkUnitNHibernateActive.Rollback();

                WorkUnitNHibernate.WorkUnitNHibernateActive.Close();
            }
            else
                await _next(context);
        }
    }
}
