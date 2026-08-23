using FarmaTech.BD.Datos.Entity;
using FarmaTech.Server.Autenticacion;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Empleada> userManager;
        private readonly SignInManager<Empleada> signInManager;
        private readonly ITokenService tokenService;

        public AuthController(
            UserManager<Empleada> userManager,
            SignInManager<Empleada> signInManager,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO login)
        {
            Empleada? empleada = await userManager.FindByNameAsync(login.Usuario);

            if (empleada is null)
            {
                return Unauthorized("Usuario o PIN incorrectos.");
            }

            Microsoft.AspNetCore.Identity.SignInResult result =
                await signInManager.CheckPasswordSignInAsync(
                empleada,
                login.Pin,
                lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return Unauthorized("Usuario o PIN incorrectos.");
            }

            IList<string> roles = await userManager.GetRolesAsync(empleada);
            return Ok(tokenService.CrearToken(empleada, roles.ToArray()));
        }
    }
}
