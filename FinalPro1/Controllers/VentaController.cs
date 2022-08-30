using FinalPro1.Models;
using Microsoft.AspNetCore.Mvc;
using FinalPro1.Repository;
using FinalPro1.Controllers.DTOS;



namespace FinalPro1.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class VentaController : ControllerBase
    {
            [HttpGet(Name = "MostrarVenta")]

            public List<Venta> ObtenerVentas([FromHeader] int IdUsuario)
            {
                return VentaHandler.FullVentas(IdUsuario);
            }

            [HttpPost(Name = "CreaVenta")]
            public List<PostVenta> CrearVentas([FromBody] List<PostVenta> ListVenta)
            {
                return VentaHandler.CrearVentas(ListVenta);
            }

            [HttpDelete(Name = "BorraVenta")]
            public string DeleteVenta([FromHeader] int Idventa)
            {
                return VentaHandler.DeleteVenta(Idventa);
            }

    }

    
}
