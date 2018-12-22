using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using IB.Data.EF;
using IB.Repository.Interface;
using IB.Services;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Validation;
using InternetBanking.Filters;
using InternetBanking.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace InternetBanking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.Configure<UnitOfWorkOptions>(Configuration.GetSection("UnitOfWork"));
            services.Configure<SecurityOptions>(Configuration.GetSection("Security"));

            services.AddCors(options => 
                options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            services.AddMvc()
                .AddMvcOptions(x => x.Filters.AddService(typeof(ExceptionFilter)))
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddMemoryCache();
            services.AddAuthorization(options => options.AddPermissionPolicies() );

            var securityOptions = Configuration.GetSection("Security").Get<SecurityOptions>();
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityOptions.SecretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = securityOptions.JwtIssuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = false,
                ValidAudience = securityOptions.JwtAudience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IValidatorFactory, ValidatorFactory>();
            services.AddSingleton<ExceptionFilter>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Please insert JWT with Bearer into field. Example: ",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }*/

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            app.UseMvc();
        }
    }
}
