using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Points.API.Application.Queries
{
    public interface ILeaderboardQueries
    {
        Task<List<Leaderboard>> GetTopHundredAsync();
    }
}
