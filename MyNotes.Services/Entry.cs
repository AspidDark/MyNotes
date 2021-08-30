using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNotes.Services.Helpers;
using MyNotes.Services.Options;
using MyNotes.Services.ServiceContracts;
using MyNotes.Services.Services;

namespace MyNotes.Services
{
    public static class Entry
    {
        public static IServiceCollection AddServiceLogic(this IServiceCollection services, 
            IConfiguration configuration)
        {
            FilePathOptions filePathOptions = new();
            configuration.Bind(nameof(FilePathOptions), filePathOptions);
            services.AddSingleton(filePathOptions);

            services.AddScoped<IFileHelper, FileHelper>();

            services.AddScoped<IAccessToEntity, AccessToEntity>();
            services.AddScoped<ITopicLogic, TopicLogic>();
            services.AddScoped<IParagraphLogic, ParagraphLogic>();
            services.AddScoped<INoteLogic, NoteLogic>();
            return services;
        }
    }
}
