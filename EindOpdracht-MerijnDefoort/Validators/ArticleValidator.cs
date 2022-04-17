namespace Store.API.Validators;

public class ArticleValidator : AbstractValidator<Article>
{
    public ArticleValidator()
    {
        RuleFor(s => s.Name).NotEmpty().WithMessage("An article needs a name");
        RuleFor(s => s.Price).GreaterThanOrEqualTo(0).WithMessage("The price of Articles can't be lower than or equal to 0");


    }
}

