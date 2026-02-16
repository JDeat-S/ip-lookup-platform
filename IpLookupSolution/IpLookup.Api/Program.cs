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
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IIpRepository, IpRepository>();
builder.Services.AddScoped<IIpService, IpService>();
builder.Services.AddHttpClient();
builder.WebHost.ConfigureKestrel(serverOptions =>//Eliminar o comentar si se hara en on premise la prueba
{//Eliminar o comentar si se hara en on premise la prueba
    serverOptions.ListenAnyIP(int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "8080"));//Eliminar o comentar si se hara en on premise la prueba
});//Eliminar o comentar si se hara en on premise la prueba

var app = builder.Build();
using (var scope = app.Services.CreateScope())//Eliminar o comentar si se hara en on premise la prueba
{//Eliminar o comentar si se hara en on premise la prueba
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();//Eliminar o comentar si se hara en on premise la prueba
    db.Database.Migrate();//Eliminar o comentar si se hara en on premise la prueba
}//Eliminar o comentar si se hara en on premise la prueba
app.Urls.Add("http://0.0.0.0:" + Environment.GetEnvironmentVariable("PORT"));//Eliminar o comentar si se hara en on premise la prueba

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
