using Application.DTOs.Nota;
using FluentValidation;

namespace Application.Validators.Nota
{
    public class NotaCreateValidator : AbstractValidator<NotaCreateDto>
    {
        public NotaCreateValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la nota es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.");

            RuleFor(x => x.Valor)
                .InclusiveBetween(0, 5).WithMessage("El valor debe estar entre 0 y 5.");

            RuleFor(x => x.IdEstudiante)
                .GreaterThan(0).WithMessage("Debe seleccionar un estudiante.");

            RuleFor(x => x.IdProfesor)
                .GreaterThan(0).WithMessage("Debe seleccionar un profesor.");
        }
    }
}
