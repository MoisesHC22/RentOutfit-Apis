/// Developer : Nombre desarrollador
/// Creation Date : 25/09/2024
/// Creation Description: Class for connection with the DB
/// Update Date : --
/// Update Description : --
///CopyRight: Nombre proyecto
namespace RO.RentOfit.Infraestructure.Repositories;
public class PersonaInfraestructure : IPersonaInfraestructure
{
    private readonly GestorInventariosContext _context;
    public PersonaInfraestructure(GestorInventariosContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Consulta un registro de la tabla GI_Persona
    /// </summary>
    /// <returns></returns>
    public async Task<PersonaDto> GetPersona()
    {
        try
        {
            var resultadoBD = new SqlParameter
            {
                ParameterName = "TipoError",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            var NumError = new SqlParameter
            {
                ParameterName = "Mensaje",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            };
            SqlParameter[] parameters =
            {               
                resultadoBD,
                NumError
            };
            string sqlQuery = "EXEC dbo.sp_persona_selecciona  @TipoError OUTPUT, @Mensaje OUTPUT";
            var dataSP = await _context.personaDto.FromSqlRaw(sqlQuery,parameters).ToListAsync();
            return dataSP.FirstOrDefault();
        }
        catch (SqlException ex)
        {
            throw;
        }
    }


    /// <summary>
    /// Agrega un registro de la tabla GI_Persona
    /// </summary>
    /// <returns></returns>
    public async Task<RespuestaDB> AddPersona(PersonaAggregate aggreget)
    {
        try
        {
            var resultadoBD = new SqlParameter
            {
                ParameterName = "TipoError",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            var NumError = new SqlParameter
            {
                ParameterName = "Mensaje",
                SqlDbType = SqlDbType.VarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            };
            SqlParameter[] parameters =
            {
                new SqlParameter("nombre",aggreget.Nombre),
                new SqlParameter("apellidoPaterno",aggreget.AperllidoPaterno),
                new SqlParameter("apellidoMaterno",aggreget.ApellidoMaterno),
                new SqlParameter("edad",aggreget.Edad),
                resultadoBD,
                NumError
            };
            string sqlQuery = "EXEC dbo.sp_persona_agrega  @nombre, @apellidoPaterno, @apellidoMaterno, @edad, @TipoError OUTPUT, @Mensaje OUTPUT";
            var dataSP = await _context.respuestaDB.FromSqlRaw(sqlQuery, parameters).ToListAsync();
            return dataSP.FirstOrDefault();
        }
        catch (SqlException ex)
        {
            throw;
        }
    }
}
