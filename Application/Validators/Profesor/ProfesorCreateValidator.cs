using Application.DTOs.Profesor;
using FluentValidation;

namespace Application.Validators.Profesor
{
    public class ProfesorCreateValidator : AbstractValidator<ProfesorCreateDto>
    {
        public ProfesorCreateValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del profesor es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.");
        }
    }
}
