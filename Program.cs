using Microsoft.EntityFrameworkCore;
using HardReserve.Controllers;
using HardReserve.Models;
using HardReserve.Contexts;
using HardReserve.Interfaces;
using HardReserve.Repositories;
using HardReserve.Services;
using HardReserve.Repositores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuração do Banco de Dados
builder.Services.AddDbContext<HardReserveDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ============================================================================================
builder.Services.AddScoped<HardReserve.Interfaces.IUsuarioRepository, HardReserve.Repositories.UsuarioRepository>();
builder.Services.AddScoped<HardReserve.Interfaces.IUsuarioService, HardReserve.Services.UsuarioService>();
// ============================================================================================

// Configuração do contêiner da Sessão
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IHardwareRepository, HardwareRepository>();
builder.Services.AddScoped<IHardwareService, HardwareService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// ATENÇÃO: O UseSession() DEVE ficar obrigatoriamente DEPOIS de UseRouting() e ANTES de UseAuthorization()
app.UseSession(); 

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();