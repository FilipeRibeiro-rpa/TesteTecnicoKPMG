using Points.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Points.Domain.AggregatesModel.GameResultAggregate
{
    public interface IGameResultRepository : IRepository<GameResult>
    {
        void Add(List<GameResult> gameResult);
    }
}
