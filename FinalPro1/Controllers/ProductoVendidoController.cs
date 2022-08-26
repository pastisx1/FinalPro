using FinalPro1.Models;
using Microsoft.AspNetCore.Mvc;
using FinalPro1.Repository;
using FinalPro1.Controllers.DTOS;


namespace FinalPro1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "MostrarProductoVendido")]
        public List<ProductoVendido> ObtenerProductoVendido([FromHeader] int IdUsuario)
        {
            return ProductoVendidoHandler.MostrarProductosVendidos(IdUsuario);
        }
    }
}
