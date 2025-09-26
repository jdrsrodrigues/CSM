using MediatR;

namespace CSM.Application.Commands
{
    public class RegisterUsuarioCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string TelegramChatId { get; set; }
        public string TelegramChatToken { get; set; }
    }
}
