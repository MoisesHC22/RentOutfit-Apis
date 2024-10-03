namespace RO.RentOfit.Infraestructure;
public class UnitRepository:BaseDisposable, IUnitRepository
{
    private readonly GestorInventariosContext _context;
    private readonly IConfiguration _configuration;

    public UnitRepository(GestorInventariosContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    protected override void DisposeManagedResource()
    {
        try
        {
            _context.Dispose();


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
    public IPersonaInfraestructure personaInfraestructure => new PersonaInfraestructure(_context);


    public async ValueTask<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }



    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

}
