using Microsoft.EntityFrameworkCore;
using Infraestructura.DAL;
using Dominio.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Siniestros Viales",
        Version = "v1",
        Description = "API REST para registro y consulta de siniestros viales",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "OPITECH",
            Email = "contacto@opitech.com"
        }
    });
});

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=opitech.db"));

// Registrar servicios personalizados
builder.Services.AddScoped<ITransaccionalidad, Transaccionalidad>();
builder.Services.AddScoped<ISiniestro, SiniestroDAL>();
builder.Services.AddScoped<ICiudad, CiudadDAL>();
builder.Services.AddScoped<IDepartamento, DepartamentoDAL>();
builder.Services.AddScoped<ITipoSiniestro, TipoSiniestroDAL>();

// MediatR
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(typeof(Aplicacion.Comandos.RegistrarSiniestro).Assembly);
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Siniestros Viales v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();