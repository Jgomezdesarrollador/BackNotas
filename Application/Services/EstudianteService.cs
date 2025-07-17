using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class EstudianteService(IEstudianteRepository repo) : IEstudianteService
    {
        private readonly IEstudianteRepository _repo = repo;

        public async Task<IEnumerable<Estudiante>> ListarAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Estudiante?> ObtenerAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CrearAsync(Estudiante estudiante)
        {
            await _repo.AddAsync(estudiante);
            await _repo.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Estudiante estudiante)
        {
            _repo.Update(estudiante);
            await _repo.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var estudiante = await _repo.GetByIdAsync(id);
            if (estudiante is not null)
            {
                _repo.Delete(estudiante);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Estudiante>, int)> ListarPaginadoAsync(int page, int size)
        {
            return await _repo.GetPagedAsync(page, size);
        }
    }
}
