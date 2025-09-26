namespace CSM.Application.Dtos
{
    public class UsuarioRegisterDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string TelegramChatId { get; set; }
        public string TelegramChatToken { get; set; }
    }
}
