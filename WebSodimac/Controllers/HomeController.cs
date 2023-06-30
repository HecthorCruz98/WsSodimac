using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSodimac.Models;
using WebSodimac.Services;

namespace WebSodimac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly InventarioBodega _inventarioRepository;
        private readonly UnidadesPorUbicacion _unidadesRepository;
        public HomeController(ILogger<HomeController> logger,InventarioBodega inventarioRepository, UnidadesPorUbicacion unidadesRepository)
        {
            this._inventarioRepository = inventarioRepository ?? throw new ArgumentNullException(nameof(inventarioRepository));
            this._unidadesRepository = unidadesRepository ?? throw new ArgumentNullException(nameof(unidadesRepository));
            this._logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            return View();

        }
        public async Task<IActionResult> Inventario(InventarioBodegaModel obj)
        {
            var response = await _inventarioRepository.ObtenerInventario("NA", 0);
            return View(response);

        }
        public async Task<IActionResult> ConsultaInventario(InventarioBodegaModel obj)
        {
            var response = await _inventarioRepository.ObtenerInventario(obj.Bodega, obj.SKU);
            if (response == null) { return NotFound(); }
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Unidades(UnidadesPorUbicacionModel obj)
        {
            var response = await _unidadesRepository.ObtenerUnidades("NA", 0);
            if (response == null) { return NotFound(); }
            return View(response);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}