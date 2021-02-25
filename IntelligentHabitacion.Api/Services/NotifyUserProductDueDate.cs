using IntelligentHabitacion.Api.Application.UseCases.ProcessFoodsNextToDueDate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IntelligentHabitacion.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class NotifyUserProductDueDate : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private IServiceProvider _serviceProvider;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public NotifyUserProductDueDate(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var delay = DateTime.UtcNow.Date.AddDays(1) - DateTime.UtcNow;
            _timer = new System.Timers.Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (sender, args) =>
            {
                _timer.Dispose();
                _timer = null;

                if (!cancellationToken.IsCancellationRequested)
                {
                    await Execute();
                    await ScheduleJob(cancellationToken);
                }

            };
            _timer.Start();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task Execute()
        {
            using var scope = _serviceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<IProcessFoodsNextToDueDate>();
            await useCase.Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
