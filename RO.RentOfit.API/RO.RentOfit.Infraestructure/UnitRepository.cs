namespace RO.RentOfit.Infraestructure;
public class UnitRepository:BaseDisposable, IUnitRepository
{
    private readonly RentOutfitContext _outfitContext;
    private readonly IConfiguration _configuration;

    public UnitRepository( IConfiguration configuration, RentOutfitContext outfitContext)
    {

        _configuration = configuration;
        _outfitContext = outfitContext;
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
    public IClienteInfraestructure clienteInfraestructure => new ClienteInfraestructure(_outfitContext);

    public async ValueTask<bool> Complete()
    {
        return await _outfitContext.SaveChangesAsync() > 0;
    }



    public bool HasChanges()
    {
        return _outfitContext.ChangeTracker.HasChanges();
    }

}
