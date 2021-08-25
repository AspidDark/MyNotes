using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Services.ServiceContracts;
using MyNotes.Services.Services;
using AutoMapper;

namespace MyNotes.Services
{
    public static class Entry
    {
        public static IServiceCollection AddServiceLogic(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IAccessToEntity, AccessToEntity>();
            services.AddScoped<ITopicLogic, TopicLogic>();
            services.AddScoped<IParagraphLogic, ParagraphLogic>();
            return services;
        }
    }
}
