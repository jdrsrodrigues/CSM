namespace CSM.Domain
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? TelegramChatId { get; set; }
        public string? Perfil { get; set; } = "User";
        public IEnumerable<Alerta>? Alertas { get; set; } = new List<Alerta>();
    }
}
