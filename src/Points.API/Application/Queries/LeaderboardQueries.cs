using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Dapper;

namespace Points.API.Application.Queries
{
    public class LeaderboardQueries : ILeaderboardQueries
    {
        private string _connectionString = string.Empty;
        public LeaderboardQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<List<Leaderboard>> GetTopHundredAsync()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                   @"
SELECT TOP 100 
                        PlayerId, 
                        MAX(CreateDate) AS lastUpdateDate,
                        SUM(Win) as Balance 
                    FROM dbo.games WITH (NOLOCK) 
                    GROUP BY PlayerId
                    ORDER BY SUM(Win) DESC; "
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapLeaderboard(result);
            }
        }
        private List<Leaderboard> MapLeaderboard(dynamic result)
        {
            var leaderboaditems = new List<Leaderboard>();

            foreach (dynamic item in result)
            {
                var leaderboad = new Leaderboard
                {
                    PlayerId = item.PlayerId,
                    Balance = item.Balance,
                    lastUpdateDate = Convert.ToDateTime(item.lastUpdateDate)
                };
                leaderboaditems.Add(leaderboad);
            }
            return leaderboaditems;
        }
    }
}
