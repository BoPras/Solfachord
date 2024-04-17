using Microsoft.AspNetCore.Rewrite;
using WebApplicationMain.Middlewares;
using WebApplicationMain.Models;
using WebApplicationMain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MongoSettings>(
        builder.Configuration.GetSection("MongoDbConnection")
    );
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();
builder.Services.AddTransient<ILogServices, LogServices>();
builder.Services.AddScoped<IRoomServices,  RoomServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseRewriter(new RewriteOptions().Add(
    context =>
    {
        if (context.HttpContext.Request.Path.StartsWithSegments("/api/employee"))
        {
            context.HttpContext.Request.Path = "/api/error";
        }
    }));

app.UseAuthorization();

app.MapControllers();

app.Run();
