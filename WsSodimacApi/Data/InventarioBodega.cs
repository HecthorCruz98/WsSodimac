using System.Data;
using System.Data.SqlClient;
using WsSodimacApi.Models;

namespace WsSodimacApi.Data
{
    public class InventarioBodega
    {
        private readonly string _connectionString;

        public InventarioBodega(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CadenaSQL");
        }

        public async Task<List<InventarioBodegaModel>> GetInventarioBodega(string Bodega,int?SKU)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_inventario_bodega", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@BODEGA", Bodega));
                    cmd.Parameters.Add(new SqlParameter("@SKU", SKU));

                    var response = new List<InventarioBodegaModel>();
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
        private InventarioBodegaModel MapToValue(SqlDataReader reader)
        {
            return new InventarioBodegaModel()
            {
                disponibilidadNeta = Convert.ToDecimal(reader["disponibilidadNeta"]),
                totalInventarioComprometido = Convert.ToDecimal(reader["totalInventarioComprometido"]),
            };
        }
    }
}
