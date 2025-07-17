using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistences.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NotaRepository(AppDbContext context) : INotaRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Nota>> GetAllAsync() =>
            await _context.Notas
                          .Include(n => n.Estudiante)
                          .Include(n => n.Profesor)
                          .ToListAsync();

        public async Task<Nota?> GetByIdAsync(int id) =>
            await _context.Notas
                          .Include(n => n.Estudiante)
                          .Include(n => n.Profesor)
                          .FirstOrDefaultAsync(n => n.Id == id);

        public async Task AddAsync(Nota nota) =>
            await _context.Notas.AddAsync(nota);

        public void Update(Nota nota) =>
            _context.Notas.Update(nota);

        public void Delete(Nota nota) =>
            _context.Notas.Remove(nota);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
