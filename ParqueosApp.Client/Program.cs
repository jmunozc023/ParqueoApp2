using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ParqueosApp.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ParqueosApp.Client.Extensiones;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

