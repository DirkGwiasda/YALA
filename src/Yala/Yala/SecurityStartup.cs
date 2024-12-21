using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Yala
{
    public static class SecurityStartup
    {
        internal static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfigurationSection keycloakConfig)
        {
            services
                .AddAuthentication(options =>
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
                        ValidAudience = keycloakConfig["ClientId"],
                        ValidateIssuer = true,
                        ValidIssuer = keycloakConfig["Authority"]
                    };
                });

                services.AddAuthorization(options =>
                {
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                });

            return services;
        }
    }
}