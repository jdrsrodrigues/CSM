using MediatR;
using CSM.Domain;
using CSM.Persistence;
using CSM.Application.Commands;

namespace CSM.Application.Handlers
{
    public class RegisterUsuarioHandler : IRequestHandler<RegisterUsuarioCommand, Guid>
    {
        private readonly CsmDbContext _context;
        public RegisterUsuarioHandler(CsmDbContext context) => _context = context;
        public async Task<Guid> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                PasswordHash = hash,
                TelegramChatId = request.TelegramChatId,
                Perfil = "User" //Padrão
                
            };
            _context.tbUsuario.Add(usuario);
            await _context.SaveChangesAsync(cancellationToken);
            return usuario.Id;
        }
    }
}
