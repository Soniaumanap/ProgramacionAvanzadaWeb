using SGC.BLL.Mapeos;
using SGC.BLL.Servicios;
using SGC.DAL.Repositorios;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));
//builder.Services.AddAutoMapper(typeof(MapeoClases));

builder.Services.AddHttpClient("SGCAPI", (sp, client) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(cfg["ApiBaseUrl"]!);
});
builder.Services.AddSession();


builder.Services.AddHttpClient<SGC.Web.Services.ApiClient>("SGCAPI", (sp, client) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(cfg["ApiBaseUrl"]!);
});



// DAL (in-memory)
builder.Services.AddSingleton<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddSingleton<IClientesRepositorio, ClientesRepositorio>();
builder.Services.AddSingleton<ISolicitudesRepositorio, SolicitudesRepositorio>();
builder.Services.AddSingleton<ITrackingsRepositorio, TrackingsRepositorio>();

// BLL
builder.Services.AddScoped<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddScoped<IClientesServicio, ClientesServicio>();
builder.Services.AddScoped<ISolicitudesServicio, SolicitudesServicio>();

builder.Services.AddSession();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(name: "default", pattern: "{controller=Auth}/{action=Login}/{id?}");
app.Run();
