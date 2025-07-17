using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task AddAsync(Estudiante estudiante);
        void Update(Estudiante estudiante);
        void Delete(Estudiante estudiante);
        Task SaveChangesAsync();
    }
}
