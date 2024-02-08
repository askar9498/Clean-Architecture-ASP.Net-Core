using ApplicationService;
using ApplicationService.Command.DeleteProduct;
using Contract.Command;
using Domain;
using Domain.Exceptions;
using Moq;

namespace ApplicationServiceUnitTest.Command.Delete;

public class DeleteProductCommandHandlerUnitTests
{
    [Fact]
    public async Task Handle_ProductExistsAndUserIsAuthorized_DeletesProduct()
    {
        // Arrange
        int productId = 123;
        int userId = 321;

        Mock<IProductRepository> mockProductRepository = new();
        mockProductRepository
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(new Product(userId, "Name", true, "Email", "Phone", DateTime.Now));

        DeleteProductCommandHandler handler = new(mockProductRepository.Object);
        DeleteProductCommand command = new(productId, userId);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockProductRepository.Verify(repo => repo.DeleteAsync(productId), Times.Once);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ThrowsNotFoundException()
    {
        // Arrange
        int productId = 123;
        int userId = 321;

        Mock<IProductRepository> mockProductRepository = new();
        mockProductRepository
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync((Product)null!);

        DeleteProductCommandHandler handler = new(mockProductRepository.Object);
        DeleteProductCommand command = new(productId, userId);

        // Act + Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserIsNotAuthorized_ThrowsAuthorizationException()
    {
        // Arrange
        int productId = 123;
        int wrongUserId = 321111;

        Mock<IProductRepository> mockProductRepository = new();
        mockProductRepository
            .Setup(repo => repo.GetByIdAsync(productId))
            .ReturnsAsync(new Product(1, "Name", true, "Email", "Phone", DateTime.Now));

        DeleteProductCommandHandler handler = new(mockProductRepository.Object);
        DeleteProductCommand command = new(productId, wrongUserId);

        // Act + Assert
        await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(command, CancellationToken.None));
    }
}