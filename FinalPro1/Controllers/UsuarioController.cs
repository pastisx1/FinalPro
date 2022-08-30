using FinalPro1.Models;
using Microsoft.AspNetCore.Mvc;
using FinalPro1.Repository;
using FinalPro1.Controllers.DTOS;

namespace FinalPro1.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "MostrarUsuarios")]
        public List<Usuario> GetUsuarios(string NombreUsuario)
        {

            return UsuarioHandler.GetUsuarios(NombreUsuario);

        }

        [HttpDelete(Name = "BorrarUsuario")]

        public bool DeleteUsuario([FromBody] int Id)
        {
            return UsuarioHandler.DeleteUsuario(Id);

        }

        [HttpPut(Name = "ModificarUsuario")]
        public void ModificarUsuario([FromBody] PutUsuario usuario)
        {

            return UsuarioHandler.ModificarUsuario(new Usuario);

        }

        [HttpPost(Name = "CrearUsuario")]

        public string CrearUsuario([FromBody] PostUsuario usuario)
        {
              


                return UsuarioHandler.CrearUsuario(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario

                });

           

        }
    }
}
