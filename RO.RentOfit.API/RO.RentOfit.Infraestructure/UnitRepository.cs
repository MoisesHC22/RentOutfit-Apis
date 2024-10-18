using RO.RentOfit.API.Services;

namespace RO.RentOfit.Infraestructure;
public class UnitRepository : BaseDisposable, IUnitRepository
{
    private readonly RentOutfitContext _outfitContext;
    private readonly IConfiguration _configuration;
    private readonly StorageFirebaseConfig _storageFirebaseConfig;
    private readonly EmailService _emailService;

    // Agregar EmailService al constructor
    public UnitRepository(IConfiguration configuration, RentOutfitContext outfitContext, StorageFirebaseConfig storageFirebaseConfig, EmailService emailService)
    {
        _configuration = configuration;
        _outfitContext = outfitContext;
        _storageFirebaseConfig = storageFirebaseConfig;
        _emailService = emailService;  // Asignar el emailService inyectado
    }

    protected override void DisposeManagedResource()
    {
        try
        {
            _outfitContext.Dispose();
        }
        finally
        {
            base.DisposeManagedResource();
        }
    }

    // Crear las instancias de las infraestructuras
    public IClienteInfraestructure clienteInfraestructure => new ClienteInfraestructure(_outfitContext, _storageFirebaseConfig);
    public IListasInfraestructure listasInfraestructure => new ListasInfraestructure(_outfitContext);
    public IVendedorInfraestructure vendedorInfraestructure => new VendedorInfraestructure(_outfitContext, _storageFirebaseConfig);
    public IRecuperarContrasenaInfraestructure recuperarContrasenaInfraestructure => new RecuperarContrasenaInfraestructure(_outfitContext, _emailService);

    public async ValueTask<bool> Complete()
    {
        return await _outfitContext.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _outfitContext.ChangeTracker.HasChanges();
    }
}
