using Application.DTOs.Profesor;
using FluentValidation;

namespace Application.Validators.Profesor
{
    public class ProfesorUpdateValidator : AbstractValidator<ProfesorUpdateDto>
    {
        public ProfesorUpdateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del profesor es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no debe superar los 100 caracteres.");
        }
    }
}
