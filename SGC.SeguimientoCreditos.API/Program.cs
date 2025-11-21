using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SGC.SeguimientoCreditos.DAL.Contexto;
using SGC.SeguimientoCreditos.DAL.Interfaces;
using SGC.SeguimientoCreditos.DAL.Repositorios;
using SGC.SeguimientoCreditos.BLL.Interfaces;
using SGC.SeguimientoCreditos.BLL.Servicios;
using SGC.SeguimientoCreditos.BLL.Mapeos;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISolicitudCreditoRepositorio, SolicitudCreditoRepositorio>();
builder.Services.AddScoped<ITrackingGestionRepositorio, TrackingGestionRepositorio>();

// Servicios
builder.Services.AddScoped<IClientesServicio, ClientesServicio>();
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddScoped<ISolicitudesServicio, SolicitudesServicio>();
builder.Services.AddScoped<ITrackingsServicio, TrackingsServicio>();

// AutoMapper MANUAL (sin Extensions)
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
