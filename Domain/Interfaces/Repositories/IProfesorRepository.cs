using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProfesorRepository
    {
        Task<IEnumerable<Profesor>> GetAllAsync();
        Task<Profesor?> GetByIdAsync(int id);
        Task AddAsync(Profesor profesor);
        void Update(Profesor profesor);
        void Delete(Profesor profesor);
        Task SaveChangesAsync();
    }
}
