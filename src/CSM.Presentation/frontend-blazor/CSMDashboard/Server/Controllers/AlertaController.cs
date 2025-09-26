using CSMDashboard.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMDashboard.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Alerta> Get()
        {
            return new[]
            {
                new Alerta { Simbolo = "BTCUSDT", PrecoAlvo = 65000, NivelVolume = 100000, NivelMacd = 1.5m, NivelRsi = 70 }
            };
        }
    }
}
