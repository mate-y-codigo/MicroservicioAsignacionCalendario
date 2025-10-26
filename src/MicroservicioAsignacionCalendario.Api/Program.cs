using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.EventoCalendario;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using MicroservicioAsignacionCalendario.Application.Services;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MicroserviceCorsPolicy",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Set Services
builder.Services.AddScoped<IAlumnoPlanService, AlumnoPlanService>();
builder.Services.AddScoped<IEjercicioRegistroService, EjercicioRegistroService>();
builder.Services.AddScoped<IEventoCalendarioService, EventoCalendarioService>();
builder.Services.AddScoped<IRecordPersonalService, RecordPersonalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MicroserviceCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();