using Application.DTOs.Nota;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaController(
        INotaService notaService,
        ILogService logService,
        IMapper mapper,
        ILogger<NotaController> logger) : ControllerBase
    {
        private readonly INotaService _notaService = notaService;
        private readonly ILogService _logService = logService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<NotaController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Listando notas");

            try
            {
                var notas = await _notaService.ListarAsync();
                var dto = _mapper.Map<IEnumerable<NotaDto>>(notas);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar notas");

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al listar notas",
                    detalles: ex.ToString(),
                    origen: nameof(GetAll));

                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Buscando nota con ID {Id}", id);

            try
            {
                var nota = await _notaService.ObtenerAsync(id);

                if (nota == null)
                {
                    string mensaje = $"Nota con ID {id} no encontrada";
                    _logger.LogWarning(mensaje);

                    await _logService.RegistrarAsync(
                        nivel: "Warning",
                        mensaje: mensaje,
                        origen: nameof(GetById));

                    return NotFound(mensaje);
                }

                var dto = _mapper.Map<NotaDto>(nota);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener nota con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al obtener nota con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(GetById));

                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotaCreateDto dto)
        {
            _logger.LogInformation("Creando nueva nota");

            try
            {
                var entity = _mapper.Map<Nota>(dto);
                await _notaService.CrearAsync(entity);

                var response = _mapper.Map<NotaDto>(entity);

                _logger.LogInformation("Nota creada con ID {Id}", entity.Id);
                return CreatedAtAction(nameof(GetById), new { id = entity.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nota");

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: "Error al crear nota",
                    detalles: ex.ToString(),
                    origen: nameof(Create));

                return StatusCode(500, "Error al crear la nota.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NotaUpdateDto dto)
        {
            _logger.LogInformation("Actualizando nota con ID {Id}", id);

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
                var entity = _mapper.Map<Nota>(dto);
                await _notaService.ActualizarAsync(entity);

                _logger.LogInformation("Nota con ID {Id} actualizada correctamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar nota con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al actualizar nota con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(Update));

                return StatusCode(500, "Error al actualizar la nota.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Eliminando nota con ID {Id}", id);

            try
            {
                await _notaService.EliminarAsync(id);

                _logger.LogInformation("Nota con ID {Id} eliminada correctamente", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar nota con ID {Id}", id);

                await _logService.RegistrarAsync(
                    nivel: "Error",
                    mensaje: $"Error al eliminar nota con ID {id}",
                    detalles: ex.ToString(),
                    origen: nameof(Delete));

                return StatusCode(500, "Error al eliminar la nota.");
            }
        }
    }
}
