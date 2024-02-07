using FluentValidation;

namespace Contract.Command;

public record CreateProductCommand(string Name, bool IsAvailable, string ManufactureEmail, string ManufacturePhone,
    DateTime ProduceDate);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private const int MaxNameLength = 100;
    private const string PhoneNumberPattern = @"^\d{10}$";

    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .MaximumLength(MaxNameLength).WithMessage($"Product name cannot exceed {MaxNameLength} characters.");

        RuleFor(x => x.ManufactureEmail)
            .NotEmpty().WithMessage("Manufacturer email cannot be empty.")
            .EmailAddress().WithMessage("Manufacturer email must be a valid email address.");

        RuleFor(x => x.ManufacturePhone)
            .NotEmpty().WithMessage("Manufacturer phone number cannot be empty.")
            .Matches(PhoneNumberPattern).WithMessage("Manufacturer phone number must match the required format.");

        RuleFor(x => x.ProduceDate)
            .NotEmpty().WithMessage("Produce date cannot be empty.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Produce date cannot be in the future.");
    }
}