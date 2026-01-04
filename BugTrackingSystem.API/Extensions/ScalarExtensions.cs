using Scalar.AspNetCore;
using Microsoft.OpenApi;

namespace BugTrackingSystem.API.Extensions
{
    public static class ScalarExtensions
    {
        public static IServiceCollection AddScalarConfiguration(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    // API Info
                    document.Info = new OpenApiInfo
                    {
                        Title = "Bug Tracking System API",
                        Version = "v1",
                        Description = "API for managing bug reports, assignments, and tracking"
                    };

                    // Ensure Components is initialized
                    if (document.Components == null)
                        document.Components = new OpenApiComponents();

                    // Ensure SecuritySchemes is initialized
                    if (document.Components.SecuritySchemes == null)
                        document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();

                    // Define the JWT security scheme
                    var bearerScheme = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "JWT Authorization header using the Bearer scheme."
                    };

                    // Add the scheme
                    document.Components.SecuritySchemes["Bearer"] = bearerScheme;

                    // **Do NOT touch document.Security** — Scalar handles it safely

                    return Task.CompletedTask;
                });
            });

            return services;
        }
    }
}
