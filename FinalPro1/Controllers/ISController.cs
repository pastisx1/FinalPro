using FinalPro1.Models;
using Microsoft.AspNetCore.Mvc;
using FinalPro1.Repository;


namespace FinalPro1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class InicioSesionController : ControllerBase
    {
        [HttpGet(Name = "IniciarSesion")]
        public Usuario IniciaSesion([FromHeader] string user, string psw)
        {
            return UsuarioHandler.ValidarUsuario(user, psw);
        }
    }
}
