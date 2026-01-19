using ClearWork.API.Middlewares;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ClearWork.API.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                var relativePath = apiDesc.RelativePath?.ToLower();

                if (relativePath == null || !relativePath.StartsWith("api/identity"))
                    return true;
                
                string[] allowedEndpoints = ["register", "login", "refresh"];
                
                return allowedEndpoints.Any(x => relativePath.EndsWith(x));
            });

            const string securityDefinitionName = "bearerAuth";
            const string securitySchemeName = "Bearer";
            
            options.AddSecurityDefinition(securityDefinitionName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = securitySchemeName
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = securityDefinitionName
                        }
                    },
                    []
                }
            });
        });
        
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
    }
}