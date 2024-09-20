using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR((config) => config.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly)).AddConsumers();
            return services;
        }

        private static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddHostedService<PaymentApprovedConsumer>();

            return services;
        }
    }
}
