using Ninject;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using Pedidos.BL.Implement;
using Pedidos.BL.Interfaces;
using Pedidos.DAL.Core;
using Pedidos.DAL.Entities;
using Pedidos.DAL.Repository;
using Pedidos.DAL.UnitWork;
using System;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(YourProjectName.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(YourProjectName.Web.App_Start.NinjectWebCommon), "Stop")]

namespace YourProjectName.Web.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static readonly IKernel _kernel = new StandardKernel();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// I added this method in order to get the kernel...
        /// </summary>
        public static IKernel GetKernel()
        {
            return _kernel;
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            //var kernel = new StandardKernel();
            try
            {
                _kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);

                RegisterServices(_kernel);

                // Install our Ninject-based IDependencyResolver into the Web API config
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(_kernel);

                return _kernel;
            }
            catch
            {
                _kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Repos
            kernel.Bind<IRepositoryGeneric<Product>>().To<RepositoryGeneric<Product>>().InSingletonScope();
            kernel.Bind<IRepositoryGeneric<Order>>().To<RepositoryGeneric<Order>>().InSingletonScope();
            kernel.Bind<IRepositoryGeneric<OrderLine>>().To<RepositoryGeneric<OrderLine>>().InSingletonScope();
            // Contexto
            kernel.Bind<ShoppingContext>().To<ShoppingContext>().InSingletonScope();
            // Unidad de trabajo
            kernel.Bind<UnitOfWork>().To<UnitOfWork>().InSingletonScope();
            // Lógica de ngocio
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();

        }
    }
}