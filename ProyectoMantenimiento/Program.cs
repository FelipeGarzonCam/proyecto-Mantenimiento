// Program.cs
using System;
using System.Data.Entity;                                        // Namespace de EF6
using Microsoft.AspNetCore.Builder;                              // Builder de Minimal APIs
using Microsoft.Extensions.Configuration;                        // Configuración
using Microsoft.Extensions.DependencyInjection;                  // DI
using Microsoft.Extensions.Hosting;                              // Hosting
using ProyectoMantenimiento.Persistencia;                        // Tu contexto EF6

var builder = WebApplication.CreateBuilder(args);

// 1. Leer cadena de conexión desde appsettings.json
var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

/// En Program.cs
builder.Services.AddScoped<AppDbContext>(provider => {
    return new AppDbContext(); // Usa el constructor sin parámetros
});

// 3. Añadir soporte a MVC (controladores y vistas)
builder.Services.AddControllersWithViews();

// Construir la app
var app = builder.Build();

// 4. Configurar middleware estándar
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 5. Rutas por defecto de MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// 6. Ejecutar la aplicación
app.Run();
