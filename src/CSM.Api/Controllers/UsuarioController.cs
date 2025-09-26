using CSM.Domain;
using CSM.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSM.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly CsmDbContext _context;

        public UsuarioController(CsmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar todos os usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("Listar")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.tbUsuario.ToListAsync();
            return Ok(usuarios);
        }
        /// <summary>
        /// Recupera usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ListarPorId/{id}")]
        public async Task<IActionResult> GetUsuario(Guid id)
        {
            var usuario = await _context.tbUsuario.FindAsync(id);

            if (usuario == null) { return NotFound(); }
            
            return Ok(usuario);
        }
        /// <summary>
        /// Atualiza um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarUsuario(Guid id, [FromBody] Usuario usuario)
        {
            var _usuario = await _context.tbUsuario.FindAsync(id);
            
            if (_usuario == null) { return NotFound(); }

            _usuario.Nome = usuario.Nome;
            _usuario.Email = usuario.Email;
            _usuario.TelegramChatId = usuario.TelegramChatId;
            _usuario.Perfil = usuario.Perfil;

            await _context.SaveChangesAsync();

            return Ok(_usuario);
        }
        
        /// <summary>
        /// Deletar um usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarUsuario(Guid id)
        {
            var usuario = await _context.tbUsuario.FindAsync(id);

            if (usuario == null) { return NotFound(); }

            _context.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
