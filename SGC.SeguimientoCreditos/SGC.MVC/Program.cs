using Microsoft.EntityFrameworkCore;
using SGC.DAL;
using SGC.DAL.Repositorios;
using SGC.BLL.Servicios;
using AutoMapper;
using SGC.BLL.Mapeos;
using SGC.DAL.Repositorios.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ConnectionString
builder.Services.AddDbContext<SgcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
//builder.Services.AddAutoMapper(typeof(MapeoClases));
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

// Repositorios DAL
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<ISolicitudesRepositorio, SolicitudesRepositorio>();
builder.Services.AddScoped<ITrackingsRepositorio, TrackingsRepositorio>();

// Servicios BLL
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddScoped<IClientesServicio, ClientesServicio>();
builder.Services.AddScoped<ISolicitudesServicio, SolicitudesServicio>();

// MVC
builder.Services.AddControllersWithViews();

// Sesiones
builder.Services.AddSession();

// HttpContextAccessor (para leer la sesión en el Layout)
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapDefaultControllerRoute();

app.Run();
