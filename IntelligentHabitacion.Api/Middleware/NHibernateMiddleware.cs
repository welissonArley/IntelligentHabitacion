using IntelligentHabitacion.Api.Repository.WorkUnit;
using Microsoft.AspNetCore.Http;
using NHibernate;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class NHibernateMiddleware
    {
        readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public NHibernateMiddleware(RequestDelegate next)
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
