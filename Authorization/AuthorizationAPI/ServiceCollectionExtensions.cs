using AquaFlaim.CommonAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AuthorizationAPI
{
    public static class ServiceCollectionExtensions
    {
        public const string POLICY_TOKEN_CREATE = "TOKEN:CREATE";
        public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection section = configuration.GetSection("CorsOrigins");
            string[] corsOrigins = section.GetChildren().Select<IConfigurationSection, string>(child => child.Value).ToArray();
            if (corsOrigins != null && corsOrigins.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                        builder.WithOrigins(corsOrigins);
                    });
                });
            }
            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("External", o =>
            {
                o.Authority = configuration["IdIssuer"];
                o.Audience = configuration["IdAudience"];
            })
            .AddJwtBearer("AquaFlaim", o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateTokenReplay = false,
                    RequireAudience = false,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidAudience = configuration["Issuer"],
                    ValidIssuer = configuration["Issuer"]//,
                    //IssuerSigningKey = Controllers.JwksController.GetSecurityKey(configuration["TknCsp"])
                };
                o.IncludeErrorDetails = true;
            })
            ;
            return services;
        }

        public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes("External", "AquaFlaim")
                .Build();
                o.AddPolicy(POLICY_TOKEN_CREATE,
                    configure =>
                    {
                        configure.AddRequirements(new AuthorizationRequirement(POLICY_TOKEN_CREATE, configuration["IdIssuer"]))
                        .AddAuthenticationSchemes("External")
                        .Build();
                    });
            });
            return services;
        }

    }
}
