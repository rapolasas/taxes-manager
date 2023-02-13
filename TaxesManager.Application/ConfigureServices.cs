using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaxesManager.Application.Common.Behaviours;

namespace TaxesManager.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return services
                .AddMediatR(assembly)
                .AddAutoMapper(assembly)
                .AddValidatorsFromAssembly(assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}