using System;
using Ninject;
using Ninject.Web.WebApi;
using Ninject.Web.Common;
using System.Web.Http;
using Pedidos.DAL.Repositorio;
using Pedidos.DAL.Entities;
using Pedidos.DAL.Core;
using Pedidos.DAL.UnidadDeTrabajo;
using Pedidos.BL.Interfaces;
using Pedidos.BL.Implementacion;

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
            kernel.Bind<IRepositorioGenerico<Producto>>().To<RepositorioGenerico<Producto>>().InSingletonScope();
            kernel.Bind<IRepositorioGenerico<Pedido>>().To<RepositorioGenerico<Pedido>>().InSingletonScope();
            kernel.Bind<IRepositorioGenerico<LineaPedido>>().To<RepositorioGenerico<LineaPedido>>().InSingletonScope();
            // Contexto
            kernel.Bind<ComprasContext>().To<ComprasContext>().InSingletonScope();
            // Unidad de trabajo
            kernel.Bind<UnidadDeTrabajo>().To<UnidadDeTrabajo>().InSingletonScope();
            // Lógica de ngocio
            kernel.Bind<IProductoServicio>().To<ProductoServicio>().InRequestScope();

        }
    }
}