using ApplicationService;
using ApplicationService.Command.UpdateProduct;
using AutoMapper;
using Contract.Command;
using Domain;
using Domain.Exceptions;
using Moq;

namespace ApplicationServiceUnitTest.Command.Update
{
    public class UpdateProductCommandHandlerUnitTest
    {
        [Fact]
        public async Task Handle_ProductExistsAndUserIsAuthorized_UpdatesProduct()
        {
            // Arrange
            int productId = 123;
            int userId = 321;

            Mock<IProductRepository> mockProductRepository = new();
            Mock<IMapper> mockMapper = new();

            Product existingProduct = new(userId, "Name", true, "Email", "Phone", DateTime.Now);
            UpdateProductCommand updatedProductCommand = new(productId, userId, "Updated Product Name", true, "updated@example.com", "1234567890", DateTime.UtcNow);

            mockProductRepository.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            mockMapper.Setup(mapper => mapper.Map<Product>(updatedProductCommand))
                .Returns(new Product(userId, updatedProductCommand.Name, updatedProductCommand.IsAvailable, updatedProductCommand.ManufactureEmail, updatedProductCommand.ManufacturePhone, updatedProductCommand.ProduceDate));

            UpdateProductCommandHandler handler = new(mockProductRepository.Object, mockMapper.Object);
            CancellationToken cancellationToken = CancellationToken.None;

            // Act
            await handler.Handle(updatedProductCommand, cancellationToken);

            // Assert
            mockProductRepository.Verify(repo => repo.UpdateAsync(existingProduct), Times.Once);
            Assert.Equal(updatedProductCommand.Name, existingProduct.Name);
            Assert.Equal(updatedProductCommand.IsAvailable, existingProduct.IsAvailable);
            Assert.Equal(updatedProductCommand.ManufactureEmail, existingProduct.ManufactureEmail);
            Assert.Equal(updatedProductCommand.ManufacturePhone, existingProduct.ManufacturePhone);
            Assert.Equal(updatedProductCommand.ProduceDate, existingProduct.ProduceDate);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ThrowsNotFoundException()
        {
            // Arrange
            int productId = 123;
            int userId = 321;

            Mock<IProductRepository> mockProductRepository = new();
            Mock<IMapper> mockMapper = new();

            mockProductRepository.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product)null!);

            UpdateProductCommandHandler handler = new(mockProductRepository.Object, mockMapper.Object);
            UpdateProductCommand command = new(productId, userId, "Updated Product Name", true, "updated@example.com", "1234567890", DateTime.UtcNow);

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserIsNotAuthorized_ThrowsAuthorizationException()
        {
            // Arrange
            int productId = 123;
            int wrongUserId = 321123321;

            Mock<IProductRepository> mockProductRepository = new();
            Mock<IMapper> mockMapper = new();

            Product existingProduct = new(1, "Name", true, "Email", "Phone", DateTime.Now);
            UpdateProductCommand command = new(productId, wrongUserId, "Updated Product Name", true, "updated@example.com", "1234567890", DateTime.UtcNow);

            mockProductRepository.Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);

            UpdateProductCommandHandler handler = new(mockProductRepository.Object, mockMapper.Object);

            // Act + Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
