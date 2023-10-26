using Example_App.ApplicationApp.Interface_Controller;
using Example_App.ApplicationApp.Service;
using Example_App.Infraestructura.Model;
using Microsoft.EntityFrameworkCore;
using Example_App.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Aca agregamos nuestra independencia de inyeccion
builder.Services.AddAplicationServices(builder.Configuration); 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolity", app =>
    {
        app.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var connectString = builder.Configuration.GetConnectionString("DefaultConnect");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectString,ServerVersion.AutoDetect(connectString)));
 
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

app.UseCors("NewPolity");

app.Run();
