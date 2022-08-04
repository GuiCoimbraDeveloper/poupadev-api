using Microsoft.EntityFrameworkCore;
using Poupadev.API.Persistence;

namespace Poupadev.API.Jobs
{
    public class RendimentoAutomaticoJob : IHostedService
    {
        private Timer _timer;
        public IServiceProvider Serviceprovider { get; set; }
        public RendimentoAutomaticoJob(IServiceProvider serviceProvider)
        {
            Serviceprovider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RenderSaldo, null, 0, 10000);

            return Task.CompletedTask;
        }
        public void RenderSaldo(object? state)
        {
            using (var scope = Serviceprovider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PoupaDevContext>();

                var objetivos = context.Objetivos.Include(x => x.Operacoes);

                foreach (var objetivo in objetivos)
                {
                    objetivo.Render();
                }
                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
