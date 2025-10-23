
using Microsoft.EntityFrameworkCore;
using Sidh_Api.Controllers;
using Sidh_Api.Database;
using Sidh_Api.Repository;


var builder = WebApplication.CreateBuilder(args);
ConfigurationManager Configuration = builder.Configuration;
builder.Services.AddScoped<PostgreSqlTcp>();
var pgService = builder.Services.BuildServiceProvider().GetRequiredService<PostgreSqlTcp>();

builder.Services.AddDbContext<MoprContext>(options =>
    options.UseNpgsql(pgService.NewPostgreSqlTCPConnectionString().ConnectionString, npgsqlOptions =>
    {
        npgsqlOptions.CommandTimeout(400); //  timeout 

    }
    ));

// Add services to the container
builder.Services.AddScoped<SidhLogin>();
builder.Services.AddScoped<SchemeListing>();
builder.Services.AddScoped<BatchListing>();
builder.Services.AddScoped<CandidateListing>();
//builder.Services.AddHttpClient<SchemeListing>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

