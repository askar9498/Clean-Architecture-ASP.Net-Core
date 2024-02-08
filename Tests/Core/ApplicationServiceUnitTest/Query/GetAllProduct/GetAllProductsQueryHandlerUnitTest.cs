using ApplicationService;
using ApplicationService.Query.GetAllProduct;
using AutoMapper;
using Contract.Query;
using Domain;
using Moq;

namespace ApplicationServiceUnitTest.Query.GetAllProduct;

public class GetAllProductsQueryHandlerUnitTest
{
    [Fact]
    public async Task Handle_ReturnsListOfProductDtos()
    {
        // Arrange
        List<Product> products = new()
        {
            new Product("Name",true,"Email","Phone",DateTime.Now),
            new Product("Name2",true,"Email2","Phone2",DateTime.Now)
        };

        Mock<IProductRepository> productRepositoryMock = new();
        productRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(products);

        Mock<IMapper> mapperMock = new();
        mapperMock.Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>()))
            .Returns((Product product) => new ProductDto(product.Name, product.ManufactureEmail, product.ManufacturePhone, product.IsAvailable, product.ProduceDate));

        GetAllProductsQueryHandler queryHandler = new(productRepositoryMock.Object, mapperMock.Object);
        GetAllProductsQuery query = new();

        // Act
        IReadOnlyCollection<ProductDto> result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IReadOnlyCollection<ProductDto>>(result);

        List<ProductDto> resultList = result.ToList();
        Assert.Equal(products.Count, resultList.Count);

        for (int i = 0; i < resultList.Count; i++)
        {
            Assert.Equal(products[i].Name, resultList[i].Name);
            Assert.Equal(products[i].IsAvailable, resultList[i].IsAvailable);
        }
    }
}