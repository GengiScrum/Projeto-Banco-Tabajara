using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using Ws_BancoTabajara.Applications.Features.BankAccounts;
using Ws_BancoTabajara.Applications.Features.Clients;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;
using Ws_BancoTabajara.Infra.ORM.Base;
using Ws_BancoTabajara.Infra.ORM.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Features.Transactions;

namespace Ws_BancoTabajara.Api.IoC
{
    public static class SimpleInjectorContainer
    {
        public static void Initializer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterServices(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static void RegisterServices(Container container)
        {
            container.Register<IBankAccountRepository, BankAccountRepository>();
            container.Register<IBankAccountService, BankAccountService>();
            container.Register<IClientRepository, ClientRepository>();
            container.Register<IClientService, ClientService>();
            container.Register<ITransactionRepository, TransactionRepository>();
            container.Register<BancoTabajaraDbContext>(() => new BancoTabajaraDbContext(), Lifestyle.Scoped);
        }
    }
}