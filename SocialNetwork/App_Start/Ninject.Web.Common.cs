using System.Configuration;
using BLL.Abstraction;
using BLL.Services;
using BLL.Infrastructure;
using SocialNetwork.Tools;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SocialNetwork.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SocialNetwork.App_Start.NinjectWebCommon), "Stop")]

namespace SocialNetwork.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static readonly System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/DefaultApi");
        private static readonly System.Configuration.ConnectionStringSettings connString =
            rootWebConfig.ConnectionStrings.ConnectionStrings["SocialNetwork"];

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
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
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new ServiceModule(connString.Name), new AutoMapperModule());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IChatService>().To<ChatService>();
            kernel.Bind<IFriendRequestService>().To<FriendRequestService>();
            kernel.Bind<IFriendService>().To<FriendService>();
        }        
    }
}