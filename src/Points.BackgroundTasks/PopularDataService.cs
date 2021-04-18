using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Points.Domain.AggregatesModel.GameResultAggregate;
using Points.Infrastructure;
using Points.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Points.BackgroundTasks
{
    public class PopularDataService: BackgroundService
    {
        private readonly ILogger<PopularDataService> _logger;
        private readonly BackgroundSettings _settings;
        private readonly IServiceScopeFactory _scopeFactory;
        private GameResultRepository _gameResultRepository;
        public PopularDataService(BackgroundSettings settings, IServiceScopeFactory scopeFactory, ILogger<PopularDataService> logger)
        {
            _settings = settings;
            _scopeFactory = scopeFactory;
            _logger = logger;
            _gameResultRepository = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GameResultRepository>();
            
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"PopularDataService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($" PopularData background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"PopularDataService task doing background work.");

                // Método para para copiar dados do servidor e adicionar no banco
                PopulateDatabase();

                await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }

            _logger.LogDebug($"PopularDataService background task is stopping.");
        }
        /// <summary>
        /// Método para popular banco de dados
        /// </summary>
        public void PopulateDatabase()
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\BD\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // Obtem os arquivos no diretório
                DirectoryInfo Dir = new DirectoryInfo(path);
                // Busca automaticamente todos os arquivos
                FileInfo[] fileNames = Dir.GetFiles("*", SearchOption.AllDirectories);

                if (fileNames.Length > 0)
                {
                    //Criar lista de Game Result
                    List<GameResult> listGame = new List<GameResult>();
                    foreach (FileInfo fi in fileNames)
                    {
                        string text = File.ReadAllText(path + fi.Name);
                        listGame.Add(JsonConvert.DeserializeObject<GameResult>(text));
                        File.Delete(path + fi.Name);
                    }
                    //Adicionar Lista em Banco de dados
                    _gameResultRepository.Add(listGame);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
