using RO.RentOfit.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

//Services
builder.Services.AddControllers();
builder.Services.AddSwagger(builder);
//builder.Services.AddCors();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Configure session -----------------------------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //options.Cookie.Name = "example";
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    //options.Cookie.IsEssential = true;
});
//-------------------------------------------------

//Configuration Azure Key Vault
//builder.Configuration.AzureKeyVault(builder);
// DependencyContainers classes, it's a run time dependency
builder.Services.AddApplicationServices(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Configure CORS
// Configure the HTTP request pipeline 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (Environment.GetEnvironmentVariable("ASPNETCORE_SWAGGER_UI_ACTIVE") == "On" || app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSession();
    app.UseDeveloperExceptionPage();
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GI.GestorInventarios.API");
        c.DefaultModelsExpandDepth(-1);
        c.InjectStylesheet("./swagger/ui/custom.css");
        c.DisplayRequestDuration();
        c.RoutePrefix = string.Empty;
    });
}
app.UseCors("AllowedOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
public partial class Program { }
