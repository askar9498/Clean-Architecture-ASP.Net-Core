using Contract.Command;
using FluentValidation.TestHelper;

namespace ApplicationServiceUnitTest.Command.CreateProduct;

public class CreateProductCommandValidatorTest
{
    private readonly CreateProductCommandValidator _validator;

    public CreateProductCommandValidatorTest()
    {
        _validator = new CreateProductCommandValidator();
    }

    [Theory]
    [InlineData(1,"", true, "test@example.com", "1234567890", "2023-01-01", "Product name cannot be empty.")]
    [InlineData(1, "Test Product", true, "", "1234567890", "2023-01-01", "Manufacturer email cannot be empty.")]
    [InlineData(1, "Test Product", true, "test@example.com", "123", "2023-01-01", "Manufacturer phone number must match the required format.")]
    [InlineData(1, "Test Product", true, "test@example.com", "1234567890", "2025-01-01", "Produce date cannot be in the future.")]
    public void Validate_InvalidInput_ShouldHaveValidationError(int userId, string name, bool isAvailable, string manufactureEmail, string manufacturePhone, string produceDate, string expectedErrorMessage)
    {
        // Arrange
        CreateProductCommand command = new(userId,name, isAvailable, manufactureEmail, manufacturePhone, DateTime.Parse(produceDate));

        // Act
        TestValidationResult<CreateProductCommand> result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveAnyValidationError()
            .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public void Validate_ValidInput_ShouldNotHaveValidationError()
    {
        // Arrange
        CreateProductCommand command = new(1,"Test Product", true, "test@example.com", "1234567890", DateTime.Now.AddDays(-1));

        // Act
        TestValidationResult<CreateProductCommand> result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
        result.ShouldNotHaveValidationErrorFor(x => x.ManufactureEmail);
        result.ShouldNotHaveValidationErrorFor(x => x.ManufacturePhone);
        result.ShouldNotHaveValidationErrorFor(x => x.ProduceDate);
    }
}