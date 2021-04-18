using Points.Domain.AggregatesModel.GameResultAggregate;
using Points.Domain.Seedwork;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Points.Infrastructure.Repositories
{
    public class GameResultRepository : IGameResultRepository
    {
        private readonly PointsContext _context;
        public IUnitOfWork UnitOfWork 
        { 
            get
            {
                return _context;
            }
        }
        public GameResultRepository(PointsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(List<GameResult> gameResult)
        {
            try
            {
                gameResult.ForEach(x =>
                {
                    if (x.IsTransient())
                    {
                        _context
                       .Games
                       .Add(x);
                    }
                });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                
            }
           
        }
    }
}
