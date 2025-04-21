using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Xceed.Blazor.Zip.Sample;

Xceed.Zip.Licenser.LicenseKey = "LICENSE_KEY_PLACEHOLDER";

var builder = WebAssemblyHostBuilder.CreateDefault( args );
builder.RootComponents.Add<App>( "#app" );
builder.RootComponents.Add<HeadOutlet>( "head::after" );

builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) } );

await builder.Build().RunAsync();
