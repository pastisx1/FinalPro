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
        public List<Usuario> GetUsuarios()
        {

            return UsuarioHandler.GetUsuarios();

        }

        [HttpDelete(Name = "BorrarUsuario")]

        public void DeleteUsuario([FromBody] int Id)
        {
            return UsuarioHandler.DeleteUsuario(Id);

        }

        [HttpPut(Name = "ModificarUsuario")]
        public void ModificarUsuario([FromBody] PutUsuario usuario)
        {

            return UsuarioHandler.ModificarUsuario(usuario);

        }

        [HttpPost(Name = "CrearUsuario")]

        public void CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {


                return UsuarioHandler.CrearUsuario(new usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario

                });

            }
            catch (Exception ex)
            { 

                Console.WriteLine(ex.Message);
                return false;
            }                              


        }
    }
}
