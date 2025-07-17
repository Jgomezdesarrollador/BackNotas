using Application.DTOs.Estudiante;
using FluentValidation;

namespace Application.Validators.Estudiante
{
    public class EstudianteCreateValidator : AbstractValidator<EstudianteCreateDto>
    {
        public EstudianteCreateValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del estudiante es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.");
        }
    }
}
