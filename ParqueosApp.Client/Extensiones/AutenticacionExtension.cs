using ParqueosApp.Client;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ParqueosApp.Client.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _sininformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExtension(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;

        }
        // Metodo para guardar la autenticacion del usuario
        public async Task ActualizarEstadoAutenticacion(SessionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;
            if (sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Email, sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role, sesionUsuario.Rol)
                },"JwAuth"));
                await _sessionStorage.GuardarStorage("sesioUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sininformacion;
                await _sessionStorage.RemoveItemAsync("sesioUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        // Metodo para obtener la autenticacion del usuario
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<SessionDTO>("sesioUsuario");
            if (sesionUsuario ==null)
                return await Task.FromResult(new AuthenticationState(_sininformacion));
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, sesionUsuario.Nombre),
                new Claim(ClaimTypes.Email, sesionUsuario.Correo),
                new Claim(ClaimTypes.Role, sesionUsuario.Rol)
            }, "JwAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
