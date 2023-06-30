using Microsoft.AspNetCore.Mvc;
using WsSodimacApi.Data;
using WsSodimacApi.Models;

namespace WsSodimacApi.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class InventarioBodegaController : ControllerBase
    {
        private readonly InventarioBodega _repository;

        public InventarioBodegaController(InventarioBodega repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        public async Task<ActionResult<List<InventarioBodegaModel>>> GetInventarioBogota(string Bodega, int SKU)
        {
            var response = await _repository.GetInventarioBodega(Bodega,SKU);
            if (response == null) { return NotFound(); }
            return response;
        }
    }
}
