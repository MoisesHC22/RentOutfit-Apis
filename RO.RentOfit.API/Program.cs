using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RO.RentOfit.API.Extensions;
using RO.RentOfit.Infraestructure.Repositories;
using RO.RentOfit.API.Services; // Servicio de Email
using RO.RentOfit.Infraestructure.Security; // Servicio de manejo de contrase�as
using RO.RentOfit.Domain.Interfaces.Services;
using RO.RentOfit.Domain.Interfaces.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Limpiar proveedores de logs predeterminados
builder.Logging.ClearProviders();

// Servicios de la aplicaci�n
builder.Services.AddControllers();
builder.Services.AddSwagger(builder);

// Registrar servicios que usar� la aplicaci�n
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<StorageFirebaseConfig>();

// Registrar servicios personalizados
builder.Services.AddScoped<PasswordService>(); // Servicio para manejo de contrase�as
builder.Services.AddScoped<EmailService>();    // Servicio para env�o de emails

// Registrar el servicio `IAdministradorPresenter`
builder.Services.AddScoped<IAdministradorPresenter, RO.RentOfit.Aplication.Presenters.AdministradorPresenter>();
builder.Services.AddScoped<IAdministradorInfraestructure, AdministradorInfraestructure>();

// Configurar la sesi�n
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de inactividad de la sesi�n
});

// Agregar servicios adicionales (e.g., Azure Key Vault) si es necesario
// builder.Configuration.AzureKeyVault(builder);

// Registrar las clases de dependencias adicionales
builder.Services.AddApplicationServices(builder.Configuration);

// Configurar autenticaci�n JWT
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

// Configuraci�n de explorador de API
builder.Services.AddEndpointsApiExplorer();

// Configuraci�n de CORS
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

// Construir la aplicaci�n
var app = builder.Build();

// Configuraci�n del entorno para Swagger, desarrollo, producci�n, etc.
if (Environment.GetEnvironmentVariable("ASPNETCORE_SWAGGER_UI_ACTIVE") == "On" || app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSession(); // Habilitar las sesiones en desarrollo
    app.UseDeveloperExceptionPage(); // P�gina de excepciones en desarrollo

    // Habilitar Swagger para documentaci�n API
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GI.GestorInventarios.API");
        c.DefaultModelsExpandDepth(-1); // Ocultar la expansi�n autom�tica de modelos en Swagger
        c.InjectStylesheet("./swagger/ui/custom.css"); // A�adir estilos personalizados
        c.DisplayRequestDuration(); // Mostrar la duraci�n de cada solicitud en Swagger
        c.RoutePrefix = string.Empty; // Establecer el prefijo de ruta como la ra�z
    });
}

// Aplicar middleware de CORS
app.UseCors("AllowSpecificOrigins");

// Forzar redirecci�n HTTPS
app.UseHttpsRedirection();

// Habilitar archivos est�ticos
app.UseStaticFiles();

// Configurar enrutamiento de solicitudes
app.UseRouting();

// Habilitar autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

// Habilitar sesiones
app.UseSession();

// Mapear los controladores a sus rutas
app.MapControllers();

// Ejecutar la aplicaci�n
app.Run();

// Clase parcial para permitir pruebas en integraci�n
public partial class Program { }
