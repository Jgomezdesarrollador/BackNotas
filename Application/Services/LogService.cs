using Domain.Entities;
using Domain.Interfaces.Services;
using Infrastructure.Persistences.Context;

namespace Application.Services
{
    public class LogService(AppDbContext context) : ILogService
    {
        private readonly AppDbContext _context = context;

        public async Task RegistrarAsync(string nivel, string mensaje, string? detalles = null, string? origen = null)
        {
            var log = new LogError
            {
                Nivel = nivel,
                Mensaje = mensaje,
                Detalles = detalles,
                Origen = origen
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
