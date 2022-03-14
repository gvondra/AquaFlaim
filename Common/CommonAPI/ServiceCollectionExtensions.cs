using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaFlaim.CommonAPI
{
    public static class ServiceCollectionExtensions
    {
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

        public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(Constants.AUTH_SCHEMA_EXTERNAL, Constants.AUTH_SCHEMA_AQUA_FLAIM)
                .Build();
                o.AddPolicy(Constants.POLICY_TOKEN_CREATE,
                    configure =>
                    {
                        configure.AddRequirements(new AuthorizationRequirement(Constants.POLICY_TOKEN_CREATE, configuration["ExternalIdIssuer"]))
                        .AddAuthenticationSchemes(Constants.AUTH_SCHEMA_EXTERNAL)
                        .Build();
                    });
                AddPolicy(o, Constants.POLICY_CLIENT_EDIT, Constants.AUTH_SCHEMA_AQUA_FLAIM, configuration["InternalIdIssuer"]);
                AddPolicy(o, Constants.POLICY_CLIENT_READ, Constants.AUTH_SCHEMA_AQUA_FLAIM, configuration["InternalIdIssuer"]);
                AddPolicy(o, Constants.POLICY_ROLE_EDIT, Constants.AUTH_SCHEMA_AQUA_FLAIM, configuration["InternalIdIssuer"]);
                AddPolicy(o, Constants.POLICY_USER_EDIT, Constants.AUTH_SCHEMA_AQUA_FLAIM, configuration["InternalIdIssuer"]);
                AddPolicy(o, Constants.POLICY_USER_READ, Constants.AUTH_SCHEMA_AQUA_FLAIM, configuration["InternalIdIssuer"]);
            });
            return services;
        }

        private static void AddPolicy(AuthorizationOptions authorizationOptions, string policyName, string schema, string issuer)
        {
            authorizationOptions.AddPolicy(policyName,
                    configure =>
                    {
                        configure.AddRequirements(new AuthorizationRequirement(policyName, issuer, policyName))
                        .AddAuthenticationSchemes(schema)
                        .Build();
                    });
        }
    }
}
