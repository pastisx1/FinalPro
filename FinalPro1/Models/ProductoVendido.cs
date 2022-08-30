namespace FinalPro1.Models
{
    public class ProductoVendido
    {

        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
        public string? Descripciones { get; internal set; }
        public double PrecioVenta { get; internal set; }
        public int IdUsuario { get; internal set; }
    }
}
