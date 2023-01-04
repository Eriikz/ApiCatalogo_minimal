using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApiCatalago.AppServicesExtensions
{
    public static class ApplicationBuilderExtensions
    {
        //metodos de extensão

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            return app;
        }

        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        {
            app.UseCors(p =>
                {
                    p.AllowAnyOrigin();
                    p.WithMethods("GET");
                    p.AllowAnyHeader();
                });
            return app;

        }

        public static IApplicationBuilder UseSwaggerMiddleweare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { });
            return app;

        }

        public static WebApplicationBuilder AddAutenticationJwt(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication
    (JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });

            builder.Services.AddAuthorization();

            return builder;
        }
    }
}
