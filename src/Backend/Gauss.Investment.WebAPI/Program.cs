using Gauss.Investment.Application;
using Gauss.Investment.Infrastructure;
using Gauss.Investment.Infrastructure.Extensions;
using Gauss.Investment.Infrastructure.Migrations;
using Gauss.Investment.WebAPI.Filters;
using Gauss.Investment.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

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

app.Run();


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