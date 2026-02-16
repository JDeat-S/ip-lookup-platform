using IpLookup.Api.Data;
using IpLookup.Api.Interfaces;
using IpLookup.Api.Repositories;
using IpLookup.Api.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(0) // desactiva reintentos
    ));*/ //cambiar por la cadena de conexión que se destine, para pruebas locales se puede usar progresSQL o SQL Express
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") //darle acceso al frontend cambiar al puerto que destine el frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IIpRepository, IpRepository>();
builder.Services.AddScoped<IIpService, IpService>();
builder.Services.AddHttpClient();

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:" + Environment.GetEnvironmentVariable("PORT"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
