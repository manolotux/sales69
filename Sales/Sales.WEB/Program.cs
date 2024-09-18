using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sales.WEB;
using Sales.WEB.Auth;
using Sales.WEB.Repositorios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7092") });
builder.Services.AddScoped<IRepositorio, Repositorio>();

builder.Services.AddSweetAlert2();

//Autorizacion
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>(x=> x.GetRequiredService<AuthProvider>());
builder.Services.AddScoped<ILoginService, AuthProvider>(x => x.GetRequiredService<AuthProvider>());

await builder.Build().RunAsync();
