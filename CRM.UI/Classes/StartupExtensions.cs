using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services)
        {
            #region PersonPro

            //services.AddTransient<IPersonRepository, PersonRepository>();

            #endregion PersonPro

            return services;
        }
    }
}