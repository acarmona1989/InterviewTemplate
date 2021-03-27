using Football.Domain.MainBoundleContext;
using Football.Domain.Persistence;
using Football.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FootballContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepository<Manager>, BaseRepository<Manager>>();
            services.AddTransient<IRepository<Player>, BaseRepository<Player>>();
            services.AddTransient<IRepository<PlayerMatch>, BaseRepository<PlayerMatch>>();
            services.AddTransient<IRepository<Referee>, BaseRepository<Referee>>();
            services.AddTransient<IRepository<Match>, BaseRepository<Match>>();
            return services;
        }
    }
}
