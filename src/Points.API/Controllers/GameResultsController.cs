using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Points.API.Application.Commands;

namespace Points.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameResultsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GameResultsController> _logger;

        public GameResultsController(IMediator mediator,
            ILogger<GameResultsController> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult<GameResultDTO>> Post([FromBody] CreateGameResultCommand createGameResultCommand)
        {
            _logger.LogInformation("----- Creating GameResult - GameResult: {@GameResult}");

            var result = await _mediator.Send(createGameResultCommand);
            return Ok(result);
        }
    }
}
