using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ProfesorService(IProfesorRepository repo) : IProfesorService
    {
        private readonly IProfesorRepository _repo = repo;

        public async Task<IEnumerable<Profesor>> ListarAsync() =>
            await _repo.GetAllAsync();

        public async Task<Profesor?> ObtenerAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task CrearAsync(Profesor profesor)
        {
            await _repo.AddAsync(profesor);
            await _repo.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Profesor profesor)
        {
            _repo.Update(profesor);
            await _repo.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var profesor = await _repo.GetByIdAsync(id);
            if (profesor is not null)
            {
                _repo.Delete(profesor);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
