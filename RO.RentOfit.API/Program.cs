using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RO.RentOfit.API.Extensions;
using RO.RentOfit.Infraestructure.Repositories;
using RO.RentOfit.API.Services; // Servicio de Email
using RO.RentOfit.Infraestructure.Security; // Servicio de manejo de contraseñas
using RO.RentOfit.Domain.Interfaces.Services;
using RO.RentOfit.Domain.Interfaces.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Limpiar proveedores de logs predeterminados
builder.Logging.ClearProviders();

// Servicios de la aplicación
builder.Services.AddControllers();
builder.Services.AddSwagger(builder);

// Registrar servicios que usará la aplicación
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<StorageFirebaseConfig>();

// Registrar servicios personalizados
builder.Services.AddScoped<PasswordService>(); // Servicio para manejo de contraseñas
builder.Services.AddScoped<EmailService>();    // Servicio para envío de emails

// Registrar el servicio `IAdministradorPresenter`
builder.Services.AddScoped<IAdministradorPresenter, RO.RentOfit.Aplication.Presenters.AdministradorPresenter>();
builder.Services.AddScoped<IAdministradorInfraestructure, AdministradorInfraestructure>();

// Configurar la sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de inactividad de la sesión
});

// Agregar servicios adicionales (e.g., Azure Key Vault) si es necesario
// builder.Configuration.AzureKeyVault(builder);

// Registrar las clases de dependencias adicionales
builder.Services.AddApplicationServices(builder.Configuration);

// Configurar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
         };
     });

// Configuración de explorador de API
builder.Services.AddEndpointsApiExplorer();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "https://tu-dominio-produccion.com")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

// Construir la aplicación
var app = builder.Build();

// Configuración del entorno para Swagger, desarrollo, producción, etc.
if (Environment.GetEnvironmentVariable("ASPNETCORE_SWAGGER_UI_ACTIVE") == "On" || app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSession(); // Habilitar las sesiones en desarrollo
    app.UseDeveloperExceptionPage(); // Página de excepciones en desarrollo

    // Habilitar Swagger para documentación API
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GI.GestorInventarios.API");
        c.DefaultModelsExpandDepth(-1); // Ocultar la expansión automática de modelos en Swagger
        c.InjectStylesheet("./swagger/ui/custom.css"); // Añadir estilos personalizados
        c.DisplayRequestDuration(); // Mostrar la duración de cada solicitud en Swagger
        c.RoutePrefix = string.Empty; // Establecer el prefijo de ruta como la raíz
    });
}

// Aplicar middleware de CORS
app.UseCors("AllowSpecificOrigins");

// Forzar redirección HTTPS
app.UseHttpsRedirection();

// Habilitar archivos estáticos
app.UseStaticFiles();

// Configurar enrutamiento de solicitudes
app.UseRouting();

// Habilitar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Habilitar sesiones
app.UseSession();

// Mapear los controladores a sus rutas
app.MapControllers();

// Ejecutar la aplicación
app.Run();

// Clase parcial para permitir pruebas en integración
public partial class Program { }
