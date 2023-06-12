using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Application.Contracts.Infrastructure;
using Site.Application.Models;
using Site.Infrastructure.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure
{
    public static class InfrastractureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastracturServices(this IServiceCollection services,
        IConfiguration configuration)
        {
           // services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
