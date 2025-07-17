using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Entities;
using Application.DTOs.Estudiante;
using Application.DTOs.Paginacion;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController(
        IEstudianteService estudianteService,
        ILogService logService,
        IMapper mapper,
        ILogger<EstudianteController> logger) : ControllerBase
    {
        private readonly IEstudianteService _estudianteService = estudianteService;
        private readonly ILogService _logService = logService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<EstudianteController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Listando estudiantes");

            try
            {
                //throw new Exception("Error de prueba generado manualmente.");
                var estudiantes = await _estudianteService.ListarAsync();
                var dtos = _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar estudiantes");

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al listar estudiantes",
                    detalles: ex.ToString(),
                    origen: nameof(GetAll));

                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Buscando estudiante con ID {Id}", id);

            try
            {
                var estudiante = await _estudianteService.ObtenerAsync(id);

                if (estudiante == null)
                {
                    string mensaje = $"Estudiante con ID {id} no encontrado";
                    _logger.LogWarning(mensaje);

                    await _logService.RegistrarAsync(
                        nivel: "Warning",
                        mensaje: mensaje,
                        origen: nameof(GetById));

                    return NotFound(mensaje);
                }

                var dto = _mapper.Map<EstudianteDto>(estudiante);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                string mensaje = $"Error al obtener estudiante con ID {id}";
                _logger.LogError(ex, mensaje);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: mensaje,
                    detalles: ex.ToString(),
                    origen: nameof(GetById));

                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstudianteCreateDto dto)
        {
            _logger.LogInformation("Creando nuevo estudiante");

            try
            {
                var entity = _mapper.Map<Estudiante>(dto);
                await _estudianteService.CrearAsync(entity);

                var response = _mapper.Map<EstudianteDto>(entity);

                _logger.LogInformation("Estudiante creado con ID {Id}", entity.Id);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear estudiante");

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al crear estudiante",
                    detalles: ex.ToString(),
                    origen: nameof(Create));

                return StatusCode(500, "Error al crear el estudiante.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EstudianteUpdateDto dto)
        {
            _logger.LogInformation("Actualizando estudiante con ID {Id}", id);

            if (id != dto.Id)
            {
                string msg = "El ID del cuerpo no coincide con el ID de la URL.";
                _logger.LogWarning(msg);

                await _logService.RegistrarAsync(
                    nivel: "Warning",
                    mensaje: msg,
                    origen: nameof(Update));

                return BadRequest(msg);
            }

            try
            {
                var entity = _mapper.Map<Estudiante>(dto);
                await _estudianteService.ActualizarAsync(entity);

                _logger.LogInformation("Estudiante con ID {Id} actualizado correctamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estudiante con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al actualizar estudiante con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(Update));

                return StatusCode(500, "Error al actualizar el estudiante.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Eliminando estudiante con ID {Id}", id);

            try
            {
                await _estudianteService.EliminarAsync(id);

                _logger.LogInformation("Estudiante con ID {Id} eliminado correctamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar estudiante con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al eliminar estudiante con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(Delete));

                return StatusCode(500, "Error al eliminar el estudiante.");
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            _logger.LogInformation("Listando estudiantes paginados");

            try
            {
                var (estudiantes, total) = await _estudianteService.ListarPaginadoAsync(page, size);
                var dtos = _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);

                var resultado = new PagedResultDto<EstudianteDto>
                {
                    Items = dtos,
                    TotalCount = total
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar estudiantes paginados");

                await _logService.RegistrarAsync("Error", "Error en paginación", ex.ToString(), nameof(GetPaged));
                return StatusCode(500, "Error interno");
            }
        }
    }
}
