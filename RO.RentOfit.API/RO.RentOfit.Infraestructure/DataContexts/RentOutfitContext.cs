
using RO.RentOfit.Domain.DTOs.Cliente;

namespace RO.RentOfit.Infraestructure.DataContexts
{
    public class RentOutfitContext : DbContext
    {
        public RentOutfitContext(DbContextOptions<RentOutfitContext> options) : base(options) 
        { 
        }

        #region Generic Dtos DB
        public DbSet<RespuestaDB> respuestaDB { get; set; }
        public DbSet<ClienteDto> clienteDto { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer("Server=MOISESHC\\SA; Database=RentOutfit; User Id=sa; Password=123456;Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=true; Encrypt=false;");
            }
        }

    }
}
