using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Yala.Components;
using Yala;
using Net.Gwiasda.Yala;
using Net.Gwiasda.Yala.Infrastructure;

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

var mariaDbConfig = builder.Configuration.GetSection("MariaDb");
builder.Services.AddSingleton<ILogEntryRepository>(new MariaDbLogEntryRepository(mariaDbConfig["ConnectionString"]));
builder.Services.AddSingleton<ILogEntryManager, LogEntryManager>();

var mng = builder.Services.BuildServiceProvider().GetService<ILogEntryManager>();
mng.WriteLogEntryAsync(new LogEntry
{
    AppName = "Yala",
    SourceName = "Program",
    Message = "Application starting",
    LogType = LogType.Information,
    Timestamp = DateTime.Now
}).Wait();

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
