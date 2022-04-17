namespace Store.API.Validators;

public class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator()
    {

        RuleFor(u => u.Name).NotEmpty().WithMessage("An user needs a name");
        RuleFor(u => u.Password).NotEmpty().WithMessage("An user needs a password");
        RuleFor(u => u.Password).Length(6).WithMessage("Password must be longer then 6 characters");
    }
}

