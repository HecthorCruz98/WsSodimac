namespace WebSodimac.Models
{
    public class InventarioBodegaModel
    {
        public decimal disponibilidadNeta { get; set; }
        public decimal totalInventarioComprometido { get; set; }
        public string Bodega { get; set; }
        public int SKU { get; set; }
    }
}
