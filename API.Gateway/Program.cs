using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Cargar configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// *** PASO 1: AÑADIR SERVICIO CORS ***
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Registrar Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// *** PASO 2: USAR CORS ***
// ¡ESTO DEBE IR ANTES DE OCELOT!
app.UseCors("AllowAll");

// Middleware de Ocelot
app.UseOcelot().Wait();

app.Run();