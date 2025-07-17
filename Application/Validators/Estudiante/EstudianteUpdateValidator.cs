using Application.DTOs.Estudiante;
using FluentValidation;

namespace Application.Validators.Estudiante
{
    public class EstudianteUpdateValidator : AbstractValidator<EstudianteUpdateDto>
    {
        public EstudianteUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del estudiante es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.");
        }
    }
}
