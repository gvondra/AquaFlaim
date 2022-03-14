using AquaFlaim.CommonAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AuthorizationAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(Constants.AUTH_SCHEMA_EXTERNAL, o =>
            {
                o.Authority = configuration["ExternalIdIssuer"];
                o.Audience = configuration["ExternalIdAudience"];
            })
            .AddJwtBearer(Constants.AUTH_SCHEMA_AQUA_FLAIM, o =>
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
                    ValidAudience = configuration["InternalIdIssuer"],
                    ValidIssuer = configuration["InternalIdIssuer"],
                    IssuerSigningKey = RsaSecurityKeySerializer.GetSecurityKey(configuration["TknCsp"])
                };
                o.IncludeErrorDetails = true;
            })
            ;
            return services;
        }
    }
}
