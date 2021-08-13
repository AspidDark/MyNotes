using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.DataAccess.Services.Core;
using MyNotes.Domain.Contracts.Core;

namespace MyNotes.DataAccess
{
    public static class Entry
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("MyNotes.DataAccess"));
            });

            services.AddScoped<ITopicService, TopicService>();

            return services;
        }
    }
}
