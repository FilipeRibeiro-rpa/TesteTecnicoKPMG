using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Points.Domain.AggregatesModel.GameResultAggregate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Points.API.Application.Commands
{
    public class CreateGameResultCommandHandler : IRequestHandler<CreateGameResultCommand, GameResultDTO>
    {

        private readonly IMediator _mediator;
        
        private readonly ILogger<CreateGameResultCommandHandler> _logger;

        // Using DI to inject infrastructure persistence Repositories
        public CreateGameResultCommandHandler(IMediator mediator,
            ILogger<CreateGameResultCommandHandler> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<GameResultDTO> Handle(CreateGameResultCommand request, CancellationToken cancellationToken)
        {
            //Instancia DTO
            GameResultDTO gameResult = new GameResultDTO { IdentityGuid = Guid.NewGuid().ToString() };
            //Instancia Objeto Aggregate
            GameResult game = new GameResult(Guid.NewGuid().ToString(), request.PlayerId, request.GameId, request.Win, request.TimeStamp, DateTime.UtcNow);

            using (StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + @"\BD\" + gameResult.IdentityGuid + ".txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, game);
            }

            return Task.FromResult(gameResult);
        }
    }
}
