using FluentValidation;

namespace GymAndYou___MinimalAPI___Project.Gyms.Validator;

    public class GymValidator: AbstractValidator<Gym>
    {
        public GymValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(70);
        }
    }

