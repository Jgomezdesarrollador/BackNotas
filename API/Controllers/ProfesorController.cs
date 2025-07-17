using Application.DTOs.Paginacion;
using Application.DTOs.Profesor;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorController(
        IProfesorService profesorService,
        ILogService logService,
        IMapper mapper,
        ILogger<ProfesorController> logger) : ControllerBase
    {
        private readonly IProfesorService _profesorService = profesorService;
        private readonly ILogService _logService = logService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ProfesorController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Listando profesores");

            try
            {
                var profesores = await _profesorService.ListarAsync();
                var dto = _mapper.Map<IEnumerable<ProfesorDto>>(profesores);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar profesores");

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al listar profesores",
                    detalles: ex.ToString(),
                    origen: nameof(GetAll));

                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Buscando profesor con ID {Id}", id);

            try
            {
                var profesor = await _profesorService.ObtenerAsync(id);

                if (profesor == null)
                {
                    string mensaje = $"Profesor con ID {id} no encontrado";
                    _logger.LogWarning(mensaje);

                    await _logService.RegistrarAsync(
                        nivel: "Warning",
                        mensaje: mensaje,
                        origen: nameof(GetById));

                    return NotFound(mensaje);
                }

                var dto = _mapper.Map<ProfesorDto>(profesor);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener profesor con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al obtener profesor con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(GetById));

                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProfesorCreateDto dto)
        {
            _logger.LogInformation("Creando nuevo profesor");

            try
            {
                var entity = _mapper.Map<Profesor>(dto);
                await _profesorService.CrearAsync(entity);

                var response = _mapper.Map<ProfesorDto>(entity);

                _logger.LogInformation("Profesor creado con ID {Id}", entity.Id);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear profesor");

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al crear profesor",
                    detalles: ex.ToString(),
                    origen: nameof(Create));

                return StatusCode(500, "Error al crear el profesor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProfesorUpdateDto dto)
        {
            _logger.LogInformation("Actualizando profesor con ID {Id}", id);

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
                var entity = _mapper.Map<Profesor>(dto);
                await _profesorService.ActualizarAsync(entity);

                _logger.LogInformation("Profesor con ID {Id} actualizado correctamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar profesor con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al actualizar profesor con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(Update));

                return StatusCode(500, "Error al actualizar el profesor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Eliminando profesor con ID {Id}", id);

            try
            {
                await _profesorService.EliminarAsync(id);

                _logger.LogInformation("Profesor con ID {Id} eliminado correctamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar profesor con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al eliminar profesor con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(Delete));

                return StatusCode(500, "Error al eliminar el profesor.");
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var (profesores, total) = await _profesorService.ListarPaginadoAsync(page, size);
                var dtos = _mapper.Map<IEnumerable<ProfesorDto>>(profesores);

                var result = new PagedResultDto<ProfesorDto>
                {
                    Items = dtos,
                    TotalCount = total
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar profesores paginados");
                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al listar profesores paginados",
                    detalles: ex.ToString(),
                    origen: nameof(Delete));
                return StatusCode(500, "Error interno");
            }
        }

    }
}
