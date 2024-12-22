using Yala;
using Yala.Components;

var builder = WebApplication.CreateBuilder(args);

var keycloakConfig = builder.Configuration.GetSection("Keycloak");

// Add services to the container.
builder.Services
    .AddHttpContextAccessor()
    .AddRazorComponents()
        .AddInteractiveServerComponents();

builder.Services.AddSecurityServices(keycloakConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
// any