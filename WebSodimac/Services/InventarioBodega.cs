using Newtonsoft.Json;
using WebSodimac.Models;

namespace WebSodimac.Services
{
    public class InventarioBodega 
    {
        private static string _baseUrl;

        public InventarioBodega()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        public async Task<List<InventarioBodegaModel>> ObtenerInventario(string bodega, int sku)
        {
            List<InventarioBodegaModel> objeto = new List<InventarioBodegaModel>();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            // cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/v1/InventarioBodega?Bodega=" + bodega + "&SKU" + sku);

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<InventarioBodegaModel>>(json_respuesta);

                objeto = resultado;
            }

            return objeto;
        }

        
    }
}
