using Newtonsoft.Json;
using WebSodimac.Models;

namespace WebSodimac.Services
{
    public class UnidadesPorUbicacion 
    {
        private static string _baseUrl;

        public UnidadesPorUbicacion()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }
        public async Task<List<UnidadesPorUbicacionModel>> ObtenerUnidades(string bodega, int sku)
        {

            List<UnidadesPorUbicacionModel> objeto = new List<UnidadesPorUbicacionModel>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            var response = await cliente.GetAsync($"api/v1/UnidadesPorUbicacion?Bodega=" + bodega + "&SKU" + sku);

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<UnidadesPorUbicacionModel>>(json_respuesta);
                objeto = resultado;
            }

            return objeto;
        }

    }
}
