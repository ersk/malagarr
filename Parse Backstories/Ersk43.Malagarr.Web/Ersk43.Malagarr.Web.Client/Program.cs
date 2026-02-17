using Ersk43.Malagarr.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Ersk43.Malagarr.Web.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.ConfigureCommonServices();

            await builder.Build().RunAsync();
        }
    }
}
