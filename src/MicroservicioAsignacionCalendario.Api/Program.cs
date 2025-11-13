using Application.Interfaces.Command;
using Application.Interfaces.EventoCalendario;
using Application.Interfaces.Query;
using Application.Services;
using Infrastructure.Commands;
using Infrastructure.Queries;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using MicroservicioAsignacionCalendario.Application.Services;
using MicroservicioAsignacionCalendario.Infrastructure.Clients;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using MicroservicioAsignacionCalendario.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using MicroservicioAsignacionCalendario.Application.Mapper;
using AutoMapper;
using MicroservicioAsignacionCalendario.Application.Interfaces.SesionRealizada;
using MicroservicioAsignacionCalendario.Infrastructure.Persistence.Commands;

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
builder.Services.AddScoped<ISesionRealizadaService, SesionRealizadaService>();

// Set HttpClients
builder.Services.AddHttpClient<IPlanEntrenamientoClient, PlanEntrenamientoClient>();
builder.Services.AddHttpClient<IUsuariosClient, UsuariosClient>();

// Set CQRS Handlers
builder.Services.AddScoped<IAlumnoPlanCommand, AlumnoPlanCommand>();
builder.Services.AddScoped<IAlumnoPlanQuery, AlumnoPlanQuery>();
builder.Services.AddScoped<IEjercicioRegistroQuery, EjercicioRegistroQuery>();
builder.Services.AddScoped<ISesionRealizadaCommand, SesionRealizadaCommand>();
builder.Services.AddScoped<ISesionRealizadaQuery, SesionesRealizadasQuery>();
builder.Services.AddScoped<IEventoCalendarioCommand, EventoCalendarioCommand>();
builder.Services.AddScoped<IRecordPersonalQuery, RecordPersonalQuery>();

// Set Mapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
builder.Services.AddAuthorization();
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