using Microsoft.Extensions.DependencyInjection;

namespace GenericRepository.SQL
{
    public static class Setup
    {
        public static IServiceCollection AddSQLDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }
    }
}
