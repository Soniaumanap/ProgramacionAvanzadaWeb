using Microsoft.EntityFrameworkCore;
using SGC.DAL;
using SGC.DAL.Repositorios;
using SGC.BLL.Servicios;
using AutoMapper;
using SGC.BLL.Mapeos;
using SGC.DAL.Repositorios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<SgcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
//builder.Services.AddAutoMapper(typeof(MapeoClases));
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

// DAL
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddScoped<ISolicitudesRepositorio, SolicitudesRepositorio>();
builder.Services.AddScoped<ITrackingsRepositorio, TrackingsRepositorio>();

// BLL
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddScoped<IClientesServicio, ClientesServicio>();
builder.Services.AddScoped<ISolicitudesServicio, SolicitudesServicio>();

// Controllers + Swagger
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
