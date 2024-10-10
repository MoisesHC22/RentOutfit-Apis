using RO.RentOfit.Domain.DTOs;

namespace RO.RentOfit.Infraestructure.DataContexts;
public class GestorInventariosContext : DbContext
{
    public GestorInventariosContext(DbContextOptions<GestorInventariosContext> options) : base(options)
    {
    }
    #region Generic Dtos DB
    public DbSet<RespuestaDB>respuestaDB { get; set; }
    #endregion
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            optionsBuilder.UseSqlServer("");
        }
    }
}
