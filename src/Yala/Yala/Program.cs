using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Yala.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddHttpContextAccessor()
    .AddRazorComponents()
        .AddInteractiveServerComponents();

var keycloakConfig = builder.Configuration.GetSection("Keycloak");
var auth = keycloakConfig["Authority"];
var clientId = keycloakConfig["ClientId"];
var clientSecret = keycloakConfig["ClientSecret"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = keycloakConfig["Authority"];
        options.ClientId = keycloakConfig["ClientId"];
        options.ClientSecret = keycloakConfig["ClientSecret"];
        options.RequireHttpsMetadata = false;
        options.ResponseType = "code";
        options.SaveTokens = false;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = keycloakConfig["Authority"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = clientId, // Dein ClientId
            ValidateIssuer = true,
            ValidIssuer = keycloakConfig["Authority"]
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});


builder.Services.AddControllers(); // Hinzufügen der Controller-Unterstützung

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
