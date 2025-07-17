using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistences.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProfesorRepository(AppDbContext context) : IProfesorRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Profesor>> GetAllAsync() =>
            await _context.Profesores.ToListAsync();

        public async Task<Profesor?> GetByIdAsync(int id) =>
            await _context.Profesores.FindAsync(id);

        public async Task AddAsync(Profesor profesor) =>
            await _context.Profesores.AddAsync(profesor);

        public void Update(Profesor profesor) =>
            _context.Profesores.Update(profesor);

        public void Delete(Profesor profesor) =>
            _context.Profesores.Remove(profesor);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
