using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Points.API.Application.Queries
{
    public class Leaderboard
    {
        public long PlayerId { get; set; }
        public long Balance { get; set; }
        public DateTime lastUpdateDate { get; set; }
    }
}
