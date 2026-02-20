using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SisPrestamos;
using SisPrestamos.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Registro de servicios
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<LineaService>();
builder.Services.AddScoped<CreditoService>();
builder.Services.AddScoped<PagoService>();
builder.Services.AddScoped<ExcelExportService>();
builder.Services.AddScoped<ExcelImportService>();

await builder.Build().RunAsync();
