using Ninject;
using NoNameApp.LogicContracts;
using NoNameApp.Logic;
using NoNameApp.DAL.EF;
using System.Data.Entity;

namespace NoNameAppNinject
{
    public class NinjectConfig
    {
        
        public static void AddBindings(IKernel kernel)
        {
            kernel.Bind<ISerialService>().To<SerialService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IEpisodeService>().To<EpisodeService>(); 
            kernel.Bind<DbContext>().To<AppContext>().InSingletonScope().WithConstructorArgument("connectionString", "AppContext");

        }
    }
}
