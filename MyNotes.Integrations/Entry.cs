using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Integrations
{
    //ConfigurationBinder
    //https://jakeydocs.readthedocs.io/en/latest/security/authentication/accconfirm.html
    // https://docs.microsoft.com/ru-ru/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=visual-studio
    //https://www.youtube.com/watch?v=Vj7iCb7wDs0
    //https://docs.microsoft.com/ru-ru/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=visual-studio
    public static class Entry
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            var mailKitOprions = configuration.GetSection("MailKitOptions").Get<MailKitOptions>();

            services.AddMailKit(config => config.UseMailKit(mailKitOprions));
            return services;
        }
    }
}
