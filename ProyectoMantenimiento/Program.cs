using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoMantenimiento.Persistencia;             // tu AppDbContext EF 6
using ProyectoMantenimiento.Aplicacion.Servicios;     // IUsuarioServicio y UsuarioServicio
using System;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC
builder.Services.AddControllersWithViews();

// 2. Registra AppDbContext (EF 6) con lifetime por petición
builder.Services.AddScoped<AppDbContext>(_ =>
    new AppDbContext(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 3. Registra tu servicio de usuario (sin SignInManager ni UserManager)
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
// :contentReference[oaicite:5]{index=5}

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");  // default al Login
// :contentReference[oaicite:6]{index=6}

app.Run();
