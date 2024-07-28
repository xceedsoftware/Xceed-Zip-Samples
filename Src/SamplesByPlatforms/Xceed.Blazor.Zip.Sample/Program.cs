using Xceed.Blazor.Zip.Sample.Components;
Xceed.Zip.Licenser.LicenseKey = "XXXXX-XXXXX-XXXXX-YYYY";

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if( !app.Environment.IsDevelopment() )
{
	app.UseExceptionHandler( "/Error", createScopeForErrors: true );
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
