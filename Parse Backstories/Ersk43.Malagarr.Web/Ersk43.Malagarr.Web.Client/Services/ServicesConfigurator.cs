using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;

namespace Ersk43.Malagarr.Web.Client.Services
{
    public static class ServicesConfigurator
    {
        public static void ConfigureCommonServices(this IServiceCollection services)
        {
            
            //FileExplorerService.LoadJSModule(
            //FileExplorerService implementationInstance
            services.AddSingleton<IFileExplorerService>(provider =>
            {
                IJSRuntime jsRuntime = provider.GetRequiredService<IJSRuntime>();
                FileExplorerService fileExplorerService = new FileExplorerService(jsRuntime);
                return fileExplorerService;
            });
        }
    }
}
