using Gemstone.HomeLibrary.Ui.Blazor;
using Gemstone.HomeLibrary.Ui.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IHomeLibraryApiService, HomeLibraryApiService>();

builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();
