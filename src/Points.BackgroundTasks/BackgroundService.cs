using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Points.BackgroundTasks
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Armazena a tarefa que estamos executando
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // Se a tarefa for concluída, devolva-a,
            // isso irá fazer um balão de cancelamento e falha para o chamador
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Caso contrário, está em execução
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Parar chamado sem iniciar
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Cancelamento do sinal para o método de execução
                _stoppingCts.Cancel();
            }
            finally
            {
                // Espere até que a tarefa seja concluída ou o token de parada seja acionado
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                                                              cancellationToken));
            }

        }
        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
