using ApplicationService.Command.CreateProduct;
using Contract.Command;
using Domain;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ApplicationServiceUnitTest.Command.CreateProduct;

public class CreateProductCommandHandlerUnitTest
{
    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnProductId()
    {
        // Arrange
        Mock<IProductRepository> productRepositoryMock = new();
        Mock<IValidator<CreateProductCommand>> validatorMock = new();

        CreateProductCommand command = new(
            "Test Product",
            true,
            "test@example.com",
            "1234567890",
            DateTime.UtcNow
        );

        validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None))
                     .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        CreateProductCommandHandler handler = new(productRepositoryMock.Object, validatorMock.Object);

        // Act
        int productId = await handler.Handle(command, CancellationToken.None);

        // Assert
        productId.Should().Be(0);
        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        // Arrange
        Mock<IProductRepository> productRepositoryMock = new();
        Mock<IValidator<CreateProductCommand>> validatorMock = new();

        CreateProductCommand command = new("Name", false, "InvalidData", "Phone", DateTime.Now);

        validatorMock.Setup(v => v.ValidateAsync(command, CancellationToken.None))
                     .ReturnsAsync(InValidResponse);

        CreateProductCommandHandler handler = new(productRepositoryMock.Object, validatorMock.Object);

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        productRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
    }

    private FluentValidation.Results.ValidationResult InValidResponse()
    {
        return new FluentValidation.Results.ValidationResult(new[] { new ValidationFailure("Name", "Name is required") });
    }
}