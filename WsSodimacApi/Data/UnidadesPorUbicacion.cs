using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using WsSodimacApi.Models;

namespace WsSodimacApi.Data
{
    public class UnidadesPorUbicacion
    {
        private readonly string _connectionString;

        public UnidadesPorUbicacion(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<List<UnidadesPorUbicacionModel>> GetUnidadesporUbicacion(string Bodega, int SKU)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_unidades_por_ubicacion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@BODEGA", Bodega));
                    cmd.Parameters.Add(new SqlParameter("@SKU", SKU));

                    var response = new List<UnidadesPorUbicacionModel>();

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }
        private UnidadesPorUbicacionModel MapToValue(SqlDataReader reader)
        {
            return new UnidadesPorUbicacionModel()
            {
                cantidadActivo = (int)reader["cantidadActivo"],
                cantidadReserva = (decimal)reader["cantidadReserva"],
                cantidadNoAlmacenada = (int)reader["cantidadNoAlmacenada"]

            };
        }
    }
}
