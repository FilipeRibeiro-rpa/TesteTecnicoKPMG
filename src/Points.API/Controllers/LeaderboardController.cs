using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Points.API.Application.Queries;


namespace Points.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardQueries _leaderboardQueries;

        public LeaderboardController(ILeaderboardQueries leaderboardQueries)
        {
            _leaderboardQueries = leaderboardQueries;
        }
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Leaderboard>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var balance = await _leaderboardQueries.GetTopHundredAsync();

            return Ok(balance);
        }
    }
}
