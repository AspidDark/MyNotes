﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Services
{
    public static class Entry
    {
        public static IServiceCollection AddServiceLogic(this IServiceCollection services, 
            IConfiguration configuration)
        {

            return services;
        }
    }
}
