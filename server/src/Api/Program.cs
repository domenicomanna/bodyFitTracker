using System;
using System.Text;
using Api.Middleware;
using Api.Database;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Api.Common.Attributes;
using System.Reflection;
using FluentValidation;

DotNetEnv.Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
    });

builder.Services.AddValidatorsFromAssemblyContaining<Program>().AddFluentValidationAutoValidation();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<BodyFitTrackerContext>(options =>
{
    string[] connectionStringParts =
    {
        $"Host={DotNetEnv.Env.GetString("POSTGRES_HOST")}",
        $"Username={DotNetEnv.Env.GetString("POSTGRES_USER")}",
        $"Password={DotNetEnv.Env.GetString("POSTGRES_PASSWORD")}",
        $"Database={DotNetEnv.Env.GetString("POSTGRES_DB")}",
    };
    options.UseNpgsql(string.Join(";", connectionStringParts));
});

builder.Services.AddAutoMapper(Assembly.GetCallingAssembly());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        string jwtSecret = DotNetEnv.Env.GetString("JWTSecret");
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.Scan(
    scan =>
        scan.FromCallingAssembly()
            .AddClasses(c => c.WithAttribute<Inject>())
            .AsSelf()
            .WithScopedLifetime()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime()
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<BodyFitTrackerContext>();
    dataContext.Database.Migrate();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
