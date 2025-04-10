using DeudasAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
IServiceCollection serviceCollection = builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DATABASE_URL")));
Console.WriteLine(builder.Configuration.GetConnectionString("DATABASE_URL"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseRouting(); // Opcional en .NET 6+ si usas MapControllers directamente, pero no da�a
app.UseCors("AllowAll");
app.UseAuthorization(); // Si usas autorizaci�n

app.MapControllers(); // �Esencial! Esto mapea tus controladores basados en atributos



app.Run();


