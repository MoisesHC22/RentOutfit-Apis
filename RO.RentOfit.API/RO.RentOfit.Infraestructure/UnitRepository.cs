
namespace RO.RentOfit.Infraestructure;
public class UnitRepository:BaseDisposable, IUnitRepository
{
    private readonly RentOutfitContext _outfitContext;
    private readonly IConfiguration _configuration;
    private readonly StorageFirebaseConfig _storageFirebaseConfig;

    public UnitRepository( IConfiguration configuration, RentOutfitContext outfitContext, StorageFirebaseConfig storageFirebaseConfig)
    {

        _configuration = configuration;
        _outfitContext = outfitContext;
        _storageFirebaseConfig = storageFirebaseConfig;
    }

    protected override void DisposeManagedResource()
    {
        try
        {
            _outfitContext.Dispose();

            //if (_context.Database.GetDbConnection != null)
            //{
            //    System.Diagnostics.Debug.WriteLine(_context.Database.GetDbConnection().State);
            //    System.Diagnostics.Debug.WriteLine(_context.Database.GetDbConnection().ConnectionTimeout);
            //}
        }
        finally
        {
            base.DisposeManagedResource();
        }
    }
    //
    public IClienteInfraestructure clienteInfraestructure => new ClienteInfraestructure(_outfitContext, _storageFirebaseConfig);
    public IListasInfraestructure listasInfraestructure => new ListasInfraestructure(_outfitContext);
    public IVendedorInfraestructure vendedorInfraestructure => new VendedorInfraestructure(_outfitContext);
    public IRecuperarContrasenaInfraestructure recuperarContrasenaInfraestructure => new RecuperarContrasenaInfraestructure( _outfitContext);
    
    public async ValueTask<bool> Complete()
    {
        return await _outfitContext.SaveChangesAsync() > 0;
    }



    public bool HasChanges()
    {
        return _outfitContext.ChangeTracker.HasChanges();
    }

}
