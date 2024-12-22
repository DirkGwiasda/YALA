using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Yala.Components;
using Yala;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddHttpContextAccessor()
    .AddRazorComponents()
        .AddInteractiveServerComponents();

var keycloakConfig = builder.Configuration.GetSection("Keycloak");
builder.Services.AddSecurityServices(keycloakConfig);

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

builder.Services.AddControllers();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "Bearer" }); // APIs explizit mit Bearer sichern

app.Run();
