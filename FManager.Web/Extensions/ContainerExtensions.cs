namespace FManager.Web
{
    using SimpleInjector;
    using System.Collections.Generic;
    using FManager.Core;
    using FManager.Core.Services;
    using FManager.Data.Contexts;
    using FManager.Impl;
    using FManager.Impl.Services;
    using AutoMapper;
    using FManager.Core.Helpers;
    using FManager.Web.Helpers;
    using FManager.Core.Providers;
    using FManager.Impl.Providers;

    public static class ContainerExtensions
    {
        public static Container Container;

        public static Container RegisterInjections(this Container container)
        {
            Container = container;

            container.Register<IEnumerable<IDbContext>>(() => new List<IDbContext> { new FManagerEntities() {} }, Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.RegisterSingleton(() => Mapper.Instance);
            container.Register<IEmailsProvider, EmailsProvider>(Lifestyle.Scoped);
            container.Register<ISessionHelper, SessionHelper>(Lifestyle.Scoped);
            container.Register<IUsersService, UsersService>(Lifestyle.Scoped);
            container.Register<IPaymentsProvider, PagSeguroPaymentsProvider>(Lifestyle.Scoped);
            container.Register<ICriptographyProvider, MD5Provider>(Lifestyle.Scoped);
            container.Register<IAccountsService, AccountsService>(Lifestyle.Scoped);
            container.Register<ISectionsService, SectionsService>(Lifestyle.Scoped);
            container.Register<IEntriesService, EntriesService>(Lifestyle.Scoped);
            container.Register<IParitiesService, ParitiesService>(Lifestyle.Scoped);
            container.Register<ICurrenciesService, CurrenciesService>(Lifestyle.Scoped);
            container.Register<IDayBooksService, DayBooksService>(Lifestyle.Scoped);
            container.Register<IDayBookItemsService, DayBookItemsService>(Lifestyle.Scoped);
            container.Register<IPaymentStatusService, PaymentStatusService>(Lifestyle.Scoped);
            container.Register<IPaymentPlansService, PaymentPlansService>(Lifestyle.Scoped);
            
            RegisterFactories(container);

            return container;
        }

        private static void RegisterFactories(Container container)
        {
        }
    }
}