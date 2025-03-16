using Entities.Templates;
using FluentValidation;

namespace RegistrationApi;

public class DtoValidatorMiddleware: AbstractValidator<RegisterDto>
{
    public DtoValidatorMiddleware()
    {
        Console.WriteLine("Validator initialized!");

        RuleFor(x => x.Login)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9_-]{3,32}$")
            .WithMessage("Invalid login format");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d).{8,}$")
            .WithMessage("Password must contain at least one letter and one number");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format");
    }
}