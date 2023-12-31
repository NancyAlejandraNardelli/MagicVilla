using MagicVilla_api;
using MagicVilla_api.Datos;
using MagicVilla_api.Repositorio;
using MagicVilla_api.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();
builder.Services.AddScoped<INumeroVillaRepositorio, NumeroVillaRepositorio>();
//builder.Services.AddDbContext<ApplicationDbContex>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection"));
//});
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer("name=DefaultConection");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
