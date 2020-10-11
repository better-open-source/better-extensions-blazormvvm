using System;
using System.Net.Http;
using System.Threading.Tasks;
using BetterExtensions.BlazorMVVM.Demo.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BetterExtensions.BlazorMVVM.Demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddTransient<CounterViewModel>()
                .AddScoped<FetchDataViewModel>();

            await builder.Build().RunAsync();
        }
    }
}
