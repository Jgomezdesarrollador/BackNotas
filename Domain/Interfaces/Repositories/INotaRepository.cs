using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> GetAllAsync();
        Task<Nota?> GetByIdAsync(int id);
        Task AddAsync(Nota nota);
        void Update(Nota nota);
        void Delete(Nota nota);
        Task SaveChangesAsync();
        Task<(IEnumerable<Nota>, int)> GetPagedAsync(int page, int size);
    }
}
