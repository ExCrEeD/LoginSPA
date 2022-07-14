using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LoginApp.Servicios
{
    public class ServicioAutomatico : BackgroundService
    {
        private const int hourDelay = 1 * 3600 * 1000; // 1 hora 
        //private const int generalDelay = 1 * 10 * 1000; // 10 segundos 
        private readonly IServiceProvider serviceProvider;
        public ServicioAutomatico(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(hourDelay, stoppingToken);
                await ActualizarRefreshTokenCuentas();
            }
        }

        private  Task ActualizarRefreshTokenCuentas() {

            var horaActual = DateTime.Now.TimeOfDay;
            if (horaActual.TotalMilliseconds <= hourDelay)
            {
                using var scope = serviceProvider.CreateScope();
                var servicioToken = scope.ServiceProvider.GetService<IServicioToken>();
                servicioToken.RefrescarTokenProcesoAutomatico();
            }
            return Task.FromResult("Done");
        }

    }
}
