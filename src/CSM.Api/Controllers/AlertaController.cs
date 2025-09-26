using CSM.Application.Dtos;
using CSM.Domain;
using CSM.Infrastructure.Services;
using CSM.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CSM.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Alerta")]
    public class AlertaController : Controller
    {
        private readonly CsmDbContext _context;

        public AlertaController(CsmDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recupera o ID do usuário autenticado a partir dos claims do token JWT.
        /// </summary>
        /// <returns></returns>
        private Guid GetUsuarioId()
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier)
                            ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.Parse(idUsuario);
        }

        /// <summary>
        /// Recuperar todos os alertas do usuário autenticado
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListarPorUsuarioId")]
        public async Task<IActionResult> GetAlertaByUsuarioId()
        {
            var idUsuario = GetUsuarioId();

            var alertas = await _context.tbAlerta
                .Where(a => a.IdUsuario == idUsuario)
                .ToListAsync();

            return Ok(alertas);
        }

        /// <summary>
        /// Atualizar alerta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="alertaAtualizado"></param>
        /// <returns></returns>
        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarAlerta(Guid id, [FromBody] Alerta alertaAtualizado)
        {
            var idUsuario = GetUsuarioId();
            var alerta = await _context.tbAlerta.FirstOrDefaultAsync(a => a.Id == id && a.IdUsuario == idUsuario);

            if (alerta == null)
                return NotFound("Alerta não encontrado.");

            alerta.Simbolo = alertaAtualizado.Simbolo;
            alerta.PrecoAlvo = alertaAtualizado.PrecoAlvo;
            alerta.NivelVolume = alertaAtualizado.NivelVolume;
            alerta.NivelMacd = alertaAtualizado.NivelMacd;
            alerta.NivelRsi = alertaAtualizado.NivelRsi;

            await _context.SaveChangesAsync();
            return Ok(alerta);
        }

        /// <summary>
        /// Excluir alerta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> DeletarAlerta(Guid id)
        {
            var idUsuario = GetUsuarioId();
            var alerta = await _context.tbAlerta.FirstOrDefaultAsync(a => a.Id == id && a.IdUsuario == idUsuario);

            if (alerta == null)
                return NotFound("Alerta não encontrado.");

            _context.tbAlerta.Remove(alerta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Cria um novo alerta
        /// </summary>
        /// <param name="alerta"></param>
        /// <param name="telegramService"></param>
        /// <returns></returns>
        [HttpPost("Criar")]
        public async Task<IActionResult> CriarAlerta([FromBody] Alerta alerta, [FromServices] TelegramService telegramService) 
        {
            alerta.Id = new Guid();
            alerta.IdUsuario = GetUsuarioId();

            _context.tbAlerta.Add(alerta); 
            await _context.SaveChangesAsync();

            var usuario = await _context.tbUsuario.FindAsync(alerta.IdUsuario);
            var _alerta = await _context.tbAlerta
                .Where(u => u.Id == alerta.Id)
                .Select(a => new AlertaDto
                {
                    Id = a.Id,
                    IdUsuario = alerta.IdUsuario,
                    NivelMacd = alerta.NivelMacd,
                    NivelRsi = alerta.NivelRsi,
                    NivelVolume = alerta.NivelVolume,
                    PrecoAlvo = alerta.PrecoAlvo,
                    Simbolo = alerta.Simbolo
                }).ToListAsync();
                

            if (usuario != null && !string.IsNullOrEmpty(usuario.TelegramChatId))
            {
                var mensagem = $"📈 Novo alerta criado!\n\n"+
                    $"Criptomoeda: {alerta.Simbolo}\n" +
                    $"Preço alvo: {alerta.PrecoAlvo}\n" +
                    $"Volume mínimo: {alerta.NivelVolume}\n" +
                    $"MACD: {alerta.NivelMacd}\n" +
                    $"RSI: {alerta.NivelRsi}";

                await telegramService.EnviarMensagemAsync(usuario.TelegramChatId, mensagem);
            }

            return Ok(_alerta);
        }
    }
}
