using DeudasAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration["DATABASE_URL"];

// --- DEBUG (A�ade esto para verificar) ---
Console.WriteLine($"Valor le�do de Configuration[\"DATABASE_URL\"]: '{connectionString}'");
// --- FIN DEBUG ---

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("�ERROR CR�TICO! No se pudo obtener una cadena de conexi�n v�lida desde Configuration[\"DATABASE_URL\"].");
    throw new InvalidOperationException("La variable de configuraci�n 'DATABASE_URL' no se encontr� o est� vac�a.");
}

// ... configura tu DbContext con connectionString
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

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


