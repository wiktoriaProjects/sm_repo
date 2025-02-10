using BlazorWasmHttp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using sm_Blazor;
using sm_Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//test

builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// Register HTTP Clients for API calls

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7188/api/auth/") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7187/api/post/") });
//builder.Services.AddHttpClient("PostApi", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:5002/");
//});
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CustomAuthStateProvider>();

// Authentication & Authorization
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddAuthorizationCore();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
