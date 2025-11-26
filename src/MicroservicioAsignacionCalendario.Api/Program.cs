using Application.Interfaces.Command;
using Application.Interfaces.EventoCalendario;
using Application.Interfaces.Query;
using Application.Services;
using AutoMapper;
using Infrastructure.Commands;
using Infrastructure.Queries;
using Interfaces.Query;
using MicroservicioAsignacionCalendario.Api.Auth;
using MicroservicioAsignacionCalendario.Application.Interfaces.AlumnoPlan;
using MicroservicioAsignacionCalendario.Application.Interfaces.Clients;
using MicroservicioAsignacionCalendario.Application.Interfaces.Query;
using MicroservicioAsignacionCalendario.Application.Interfaces.RecordPersonal;
using MicroservicioAsignacionCalendario.Application.Interfaces.RegistroEjercicio;
using MicroservicioAsignacionCalendario.Application.Interfaces.SesionRealizada;
using MicroservicioAsignacionCalendario.Application.Mapper;
using MicroservicioAsignacionCalendario.Application.Services;
using MicroservicioAsignacionCalendario.Infrastructure.Clients;
using MicroservicioAsignacionCalendario.Infrastructure.Commands;
using MicroservicioAsignacionCalendario.Infrastructure.Data;
using MicroservicioAsignacionCalendario.Infrastructure.Persistence.Commands;
using MicroservicioAsignacionCalendario.Infrastructure.Persistence.Queries;
using MicroservicioAsignacionCalendario.Infrastructure.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),

            ValidateLifetime = true,
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Set Services
builder.Services.AddScoped<IAlumnoPlanService, AlumnoPlanService>();
builder.Services.AddScoped<IEjercicioRegistroService, EjercicioRegistroService>();
builder.Services.AddScoped<IEventoCalendarioService, EventoCalendarioService>();
builder.Services.AddScoped<IRecordPersonalService, RecordPersonalService>();
builder.Services.AddScoped<ISesionRealizadaService, SesionRealizadaService>();

// To access HttpContext in services
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<TokenPropagationHandler>();

// Set HttpClients
builder.Services.AddHttpClient<IPlanEntrenamientoClient, PlanEntrenamientoClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:PlanEntrenamientoClientUrl"]);
}).AddHttpMessageHandler<TokenPropagationHandler>();

builder.Services.AddHttpClient<IUsuariosClient, UsuariosClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:UsuariosClientUrl"]);
}).AddHttpMessageHandler<TokenPropagationHandler>();

// Set CQRS Handlers
builder.Services.AddScoped<IAlumnoPlanCommand, AlumnoPlanCommand>();
builder.Services.AddScoped<IAlumnoPlanQuery, AlumnoPlanQuery>();
builder.Services.AddScoped<IEjercicioRegistroQuery, EjercicioRegistroQuery>();
builder.Services.AddScoped<ISesionRealizadaCommand, SesionRealizadaCommand>();
builder.Services.AddScoped<ISesionRealizadaQuery, SesionesRealizadasQuery>();
builder.Services.AddScoped<IEventoCalendarioCommand, EventoCalendarioCommand>();
builder.Services.AddScoped<IEventoCalendarioQuery, EventoCalendarioQuery>();
builder.Services.AddScoped<IRecordPersonalCommand, RecordPersonalCommand>();
builder.Services.AddScoped<IRecordPersonalQuery, RecordPersonalQuery>();

// Set Mapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());

builder.Services.AddAuthorization();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MicroserviceCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();