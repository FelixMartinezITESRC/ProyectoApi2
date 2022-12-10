using ApiAeropuerto.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<sistem21_equipo9AeropuertoContext>(x => x.UseMySql("server=sistemas19.com;user=sistem21_equipo9User;password=equipo9Avion;database=sistem21_equipo9Aeropuerto", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.29-mariadb")));

var app = builder.Build();

app.UseFileServer();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
