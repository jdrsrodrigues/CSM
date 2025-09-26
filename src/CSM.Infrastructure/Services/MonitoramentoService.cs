using CSM.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSM.Infrastructure.Services
{
    public class MonitoramentoService
    {
        private readonly CsmDbContext _context;
        private readonly TelegramService _telegram;
        private readonly HttpClient _http;
        public MonitoramentoService(CsmDbContext context, TelegramService telegram, HttpClient http)
        {
            _context = context;
            _telegram = telegram;
            _http = http;
        }
        public async Task VerificarAlertasAsync()
        {
            var alertas = await _context.tbAlerta.Include(u => u.Usuario).ToListAsync();

            foreach (var alerta in alertas)
            {
                var precoAtual = await BuscarPrecoByBit(alerta.Simbolo);

                if (precoAtual == null) continue;

                if (precoAtual >= alerta.PrecoAlvo)
                { 
                    var mensagem = $"🚨 Alerta disparado!\n\n" +
                               $"Ativo: {alerta.Simbolo}\n" +
                               $"Preço atual: {precoAtual}\n" +
                               $"Preço alvo: {alerta.PrecoAlvo}";

                    await _telegram.EnviarMensagemAsync(alerta.Usuario.TelegramChatId, mensagem);
                }
            }
        }
        public async Task<decimal?> BuscarPrecoByBit(string simbolo)
        {
            var response = await _http.GetAsync($"https://api.bybit.com/v5/market/tickers?category=linear");

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonDocument.Parse(json);

            var ticker = data.RootElement
                .GetProperty("result")
                .GetProperty("list")
                .EnumerateArray()
                .FirstOrDefault(t => t.GetProperty("symbol").GetString() == simbolo);

            if (ticker.ValueKind == JsonValueKind.Undefined) return null;

            return decimal.Parse(ticker.GetProperty("lastPrice").GetString());
        }
    }
}
