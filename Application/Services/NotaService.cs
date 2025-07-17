using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class NotaService(INotaRepository repo) : INotaService
    {
        private readonly INotaRepository _repo = repo;

        public async Task<IEnumerable<Nota>> ListarAsync() =>
            await _repo.GetAllAsync();

        public async Task<Nota?> ObtenerAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task CrearAsync(Nota nota)
        {
            await _repo.AddAsync(nota);
            await _repo.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Nota nota)
        {
            _repo.Update(nota);
            await _repo.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var nota = await _repo.GetByIdAsync(id);
            if (nota is not null)
            {
                _repo.Delete(nota);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Nota>, int)> ListarPaginadoAsync(int page, int size)
        {
            return await _repo.GetPagedAsync(page, size);
        }
    }
}
