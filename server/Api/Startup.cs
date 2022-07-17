using System.Text;
using Api.ApplicationLogic.Authentication.Handlers;
using Api.ApplicationLogic.BodyMeasurements.Handlers;
using Api.ApplicationLogic.Users.Handlers;
using Api.ApplicationLogic.Users.Requests;
using Api.Common.Interfaces;
using Api.Configurations;
using Api.Infrastructure.Emailing;
using Api.Infrastructure.Security;
using Api.Middleware;
using Api.Persistence;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Api
{

    public class Startup
    {
        readonly string _corsPolicyName = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DotNetEnv.Env.TraversePath().Load();
        
            services.AddControllers(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserRequestValidator>());
            
            string databaseConnectionString = DotNetEnv.Env.GetString("DbConnection");

            services.AddDbContext<BodyFitTrackerContext>(options =>
            {
                options.UseMySql(databaseConnectionString, ServerVersion.AutoDetect(databaseConnectionString));
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy(_corsPolicyName,
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            string secret = DotNetEnv.Env.GetString("JWTSecret");
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = securityKey,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };
                    });

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddScoped<GetAllBodyMeasurementsHandler>();
            services.AddScoped<GetBodyMeasurementHandler>();
            services.AddScoped<CreateOrEditBodyMeasurementHandler>();
            services.AddScoped<DeleteBodyMeasurementHandler>();

            services.AddScoped<GetUserHandler>();
            services.AddScoped<CreateUserHandler>();

            services.AddScoped<LoginHandler>();
            services.AddScoped<ChangePasswordHandler>();
            services.AddScoped<ResetPasswordStepOneHandler>();
            services.AddScoped<ResetPasswordStepTwoHandler>();
            services.AddScoped<ValidateResetPasswordTokenHandler>();
            services.AddScoped<ChangeProfileSettingsHandler>();

            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IPasswordResetTokenGenerator, PasswordResetTokenGenerator>();
            services.AddScoped<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            UpdateDatabase(app);

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseCors(_corsPolicyName);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<BodyFitTrackerContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }

}
