using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.BLL.Mappings;
using SGC.SeguimientoCreditos.BLL.Servicios;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Repositorios.Implementaciones;
using SGC.SeguimientoCreditos.DAL.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ?? DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SgcDbContext>(options =>
    options.UseSqlServer(connectionString));

// ?? Repositorios DAL
builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<ISolicitudesRepositorio, SolicitudesRepositorio>();
builder.Services.AddScoped<ITrackingsRepositorio, TrackingsRepositorio>();

// ?? Servicios BLL
builder.Services.AddScoped<IClientesServicio, ClientesServicio>();
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddScoped<ISolicitudesServicio, SolicitudesServicio>();
builder.Services.AddScoped<ITrackingsServicio, TrackingsServicio>();

// ?? AutoMapper (usa el assembly de la BLL donde está MappingProfile)
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
