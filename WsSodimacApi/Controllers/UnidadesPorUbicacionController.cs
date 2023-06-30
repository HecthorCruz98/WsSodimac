using Microsoft.AspNetCore.Mvc;
using WsSodimacApi.Data;
using WsSodimacApi.Models;

namespace WsSodimacApi.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UnidadesPorUbicacionController : ControllerBase
    {
        private readonly UnidadesPorUbicacion _repository;

        public UnidadesPorUbicacionController(UnidadesPorUbicacion repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        public async Task<ActionResult<List<UnidadesPorUbicacionModel>>> GetUnidadesPorUbicacion(string Bodega, int SKU)
        {
            var response = await _repository.GetUnidadesporUbicacion(Bodega, SKU);
            if (response == null) { return NotFound(); }
            return response;
        }
    }
}
