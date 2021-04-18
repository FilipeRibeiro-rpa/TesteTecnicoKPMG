using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Points.API.Application.Queries;
using Points.BackgroundTasks;
using Points.Domain.AggregatesModel.GameResultAggregate;
using Points.Domain.Seedwork;
using Points.Infrastructure;
using Points.Infrastructure.Repositories;

namespace Points.API.Application.DI
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string conection, int checkUpdateTimeService)
        {
            services.AddDbContext<PointsContext>(options => options.UseSqlServer(conection), ServiceLifetime.Scoped);
            services.AddScoped(typeof(IRepository<GameResult>), typeof(GameResultRepository));
            services.AddScoped<IGameResultRepository, GameResultRepository>();
            services.AddScoped(typeof(GameResultRepository));
            services.AddMediatR(typeof(Startup));
            services.AddTransient<ILeaderboardQueries, LeaderboardQueries>().AddSingleton(conection);
            services.AddHostedService<PopularDataService>().AddSingleton(new BackgroundSettings { CheckUpdateTime = checkUpdateTimeService });


        }
    }
}
