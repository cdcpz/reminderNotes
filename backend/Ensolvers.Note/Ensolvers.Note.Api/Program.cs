using Ensolvers.Note.Application;
using Ensolvers.Note.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var dbSection = builder.Configuration.GetSection("DbConectionString");
var dbConnectionString = dbSection.Value.ToString();

// Add services to the container.
builder.Services.AddDbContext<NoteContext>(opt =>
{
    //opt.UseMySql(ServerVersion.AutoDetect(dbConnectionString));
    opt.UseMySql(
        dbConnectionString,
        //ServerVersion.Create(
        //    new Version(8, 8),
        //    Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql
        //)
        ServerVersion.AutoDetect(dbConnectionString)
    );
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
       builder =>
       {
           builder.WithOrigins("http://localhost:4200", "https://ensolvers-test-front.azurewebsites.net", "pipelinetest.ddns.net")
           .AllowAnyHeader()
           .AllowAnyMethod();
       });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
