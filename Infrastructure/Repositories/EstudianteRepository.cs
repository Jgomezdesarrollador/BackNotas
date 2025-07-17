using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistences.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EstudianteRepository(AppDbContext context) : IEstudianteRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Estudiante>> GetAllAsync() =>
            await _context.Estudiantes.ToListAsync();

        public async Task<Estudiante?> GetByIdAsync(int id) =>
            await _context.Estudiantes.FindAsync(id);

        public async Task AddAsync(Estudiante estudiante) =>
            await _context.Estudiantes.AddAsync(estudiante);

        public void Update(Estudiante estudiante) =>
            _context.Estudiantes.Update(estudiante);

        public void Delete(Estudiante estudiante) =>
            _context.Estudiantes.Remove(estudiante);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();

        public async Task<(IEnumerable<Estudiante>, int)> GetPagedAsync(int page, int size)
        {
            var query = _context.Estudiantes.AsQueryable();

            int total = await query.CountAsync();
            var items = await query
                .OrderBy(e => e.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (items, total);
        }
    }
}
