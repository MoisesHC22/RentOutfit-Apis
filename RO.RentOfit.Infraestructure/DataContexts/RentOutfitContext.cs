﻿
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
        public DbSet<IniciarSesionDto> iniciarSesionDto { get; set; }
        public DbSet<GeneroDto> generoDto { get; set; }
        public DbSet<EstadosDto> estadosDto { get; set; }
        public DbSet<MunicipiosDto> municipiosDto { get; set; }
        public DbSet<TallasDto> tallasDto { get; set; }
        public DbSet<EstilosDto> estilosDto { get; set; }
        public DbSet<RecuperarContrasenaDto> recuperarContrasenaDto { get; set; }
        public DbSet<InformacionVestimentaDto> informacionVestimentaDto  { get; set; }
        public DbSet<ListaVestimentasDto> listaVestimentasDto { get; set; }
        public DbSet<EstablecimientosCercanosDto> establecimientosCercanosDto { get; set; }
        public DbSet<RetornoEstablecimientoDto> retornoEstablecimientoDto { get; set; }
        public DbSet<InformacionEstablecimientoDto> informacionEstablecimientoDto { get; set; }
        public DbSet<VestimentasEstablecimientosDto> vestimentasEstablecimientosDto { get; set; }
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