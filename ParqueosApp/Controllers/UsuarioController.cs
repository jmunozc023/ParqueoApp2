using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParqueosApp.Client;
using System.Diagnostics.Eventing.Reader;

namespace ParqueosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInDTO login)
        { //Aca deberia acceder a la base de datos para verificar si el usuario existe por ahora solo se valida si es admin
            SessionDTO sessionDTO = new SessionDTO();
            if (login.Correo == "admin@gmail.com" && login.Contrasena == "admin")
            {
                sessionDTO.Nombre = "Admin";
                sessionDTO.Correo = login.Correo;
                sessionDTO.Rol = "Admin";

            }
            else
            {
                sessionDTO.Nombre = "empleado";
                sessionDTO.Correo = login.Correo;
                sessionDTO.Rol = "Empleado";
            }
            return StatusCode(StatusCodes.Status200OK, sessionDTO);
        }
    }
}
