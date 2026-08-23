using FarmaTech.BD.Datos.Entity;
using FarmaTech.Server.Autenticacion;
using FarmaTech.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmaTech.Server.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Administrador)]
    [Route("api/empleada")]
    public class EmpleadaController : ControllerBase
    {
        private readonly UserManager<Empleada> userManager;

        public EmpleadaController(UserManager<Empleada> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadaResponseDTO>>> Get()
        {
            List<Empleada> empleadas = await userManager.Users
                .AsNoTracking()
                .ToListAsync();
            var response = new List<EmpleadaResponseDTO>(empleadas.Count);

            foreach (Empleada empleada in empleadas)
            {
                response.Add(await ToResponseAsync(empleada));
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpleadaResponseDTO>> GetById(int id)
        {
            Empleada? empleada = await userManager.FindByIdAsync(id.ToString());

            if (empleada is null)
            {
                return NotFound($"No se encontro la empleada con id: {id}");
            }

            return Ok(await ToResponseAsync(empleada));
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadaResponseDTO>> Post(EmpleadaDTO dto)
        {
            var empleada = new Empleada
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                UserName = dto.Usuario
            };

            IdentityResult createResult = await userManager.CreateAsync(empleada, dto.Pin);

            if (!createResult.Succeeded)
            {
                return IdentityErrors(createResult);
            }

            IdentityResult roleResult = await userManager.AddToRoleAsync(empleada, Roles.Empleada);

            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(empleada);
                return IdentityErrors(roleResult);
            }

            EmpleadaResponseDTO response = await ToResponseAsync(empleada);
            return CreatedAtAction(nameof(GetById), new { id = empleada.Id }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<EmpleadaResponseDTO>> Put(int id, EmpleadaDTO dto)
        {
            Empleada? empleada = await userManager.FindByIdAsync(id.ToString());

            if (empleada is null)
            {
                return NotFound($"No existe la empleada con id: {id}");
            }

            empleada.Nombre = dto.Nombre;
            empleada.Apellido = dto.Apellido;
            empleada.UserName = dto.Usuario;

            string resetToken = await userManager.GeneratePasswordResetTokenAsync(empleada);
            IdentityResult result = await userManager.ResetPasswordAsync(
                empleada,
                resetToken,
                dto.Pin);

            if (!result.Succeeded)
            {
                return IdentityErrors(result);
            }

            return Ok(await ToResponseAsync(empleada));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Empleada? empleada = await userManager.FindByIdAsync(id.ToString());

            if (empleada is null)
            {
                return NotFound($"No existe la empleada con id: {id}");
            }

            IdentityResult result = await userManager.DeleteAsync(empleada);

            if (!result.Succeeded)
            {
                return IdentityErrors(result);
            }

            return NoContent();
        }

        private async Task<EmpleadaResponseDTO> ToResponseAsync(Empleada empleada)
        {
            IList<string> roles = await userManager.GetRolesAsync(empleada);

            return new EmpleadaResponseDTO
            {
                Id = empleada.Id,
                Nombre = empleada.Nombre,
                Apellido = empleada.Apellido,
                Usuario = empleada.UserName ?? string.Empty,
                Rol = roles.FirstOrDefault() ?? string.Empty
            };
        }

        private ActionResult IdentityErrors(IdentityResult result)
        {
            return BadRequest(new
            {
                Errors = result.Errors.Select(error => error.Description)
            });
        }
    }
}
