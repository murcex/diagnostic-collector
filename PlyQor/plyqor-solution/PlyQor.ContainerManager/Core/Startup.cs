using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PlyQor.ContainerManager.Core;
using PlyQor.Storage.Interfaces;
using PlyQor.Storage.ProtoPylon;
using PlyQor.Storage.Model;

[assembly: FunctionsStartup(typeof(Startup))]

namespace PlyQor.ContainerManager.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IProtoPylon, ProtoPylon>();

            builder.Services.AddSingleton<IStorageManager, StorageManager>();

            builder.Services.AddSingleton<IContainerOperations, ContainerOperations>();

            builder.Services.AddSingleton<IContainerManager, Storage.Model.ContainerManager>();
        }
    }
}
