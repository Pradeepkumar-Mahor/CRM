using CMR.DataAccess.Repositories;
using MGMTApp.DataAccess.Context;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDataAccessService(this IServiceCollection services)
        {
            #region PersonPro

            services.AddTransient<IProductRepository, ProductRepository>();

            #endregion PersonPro

            #region GenericEntityRepos

            _ = services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //services.AddTransient<IEmailSender, MailKit>();

            #endregion GenericEntityRepos

            return services;
        }
    }
}