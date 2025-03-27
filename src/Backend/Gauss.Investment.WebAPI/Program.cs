using Gauss.Investment.Application;
using Gauss.Investment.Domain.Security.Tokens;
using Gauss.Investment.Infrastructure;
using Gauss.Investment.Infrastructure.Extensions;
using Gauss.Investment.Infrastructure.Migrations;
using Gauss.Investment.WebAPI.Converters;
using Gauss.Investment.WebAPI.Filters;
using Gauss.Investment.WebAPI.Middleware;
using Gauss.Investment.WebAPI.Token;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options => 
options.JsonSerializerOptions.Converters.Add(new StringConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "GAUSS Investment API",
        Description = "This API is responsible for managing user authentication, ensuring secure access to the system.",
        Contact = new OpenApiContact() { Name = "Jeferson Almeida", Email = "jeffreys.sharp@outlook.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authoirization header using the Bearer scheme.
        Enter 'Bearer' [space] and then your token in the text input below.
        Example: 'Bearer 12345abcder...,",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateteDatabase();

await app.RunAsync();


void MigrateteDatabase()
{
    if (builder.Configuration.IsUnitTestEnvironment())
        return;

    var databaseType = builder.Configuration.DatabaseType();
    var connectionString = builder.Configuration.ConnectionString();

    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DatabaseMigration.Migrate(databaseType, connectionString, serviceScope.ServiceProvider);
}


public partial class Program
{
    protected Program()
    {

    }
}