using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace CSM.Infrastructure.Services
{
    public class TelegramService
    {
        public readonly ITelegramBotClient _botClient;

        public TelegramService(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task EnviarMensagemAsync(string chatId, string mensagem) 
        { 
            if (string.IsNullOrEmpty(chatId)) { return; }

            await _botClient.SendMessage(chatId, mensagem, ParseMode.Markdown);
        }
    }
}
