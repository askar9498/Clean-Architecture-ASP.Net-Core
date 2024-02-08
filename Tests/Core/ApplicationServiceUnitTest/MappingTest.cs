using ApplicationService.Query;
using AutoMapper;
using Contract.Query;
using Domain;
using FluentAssertions;

namespace ApplicationServiceUnitTest;

public class MappingTest
{
    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Arrange
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        // Act + Assert
        configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void Mapping_Product_To_ProductDto_IsCorrect()
    {
        // Arrange
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        Mapper mapper = new(configuration);

        Product product = new("Name", true, "Email", "Phone", DateTime.Now);

        // Act
        ProductDto productDto = mapper.Map<ProductDto>(product);

        // Assert
        productDto.Should().NotBeNull();
        productDto.Name.Should().Be(product.Name);
        productDto.IsAvailable.Should().Be(product.IsAvailable);
        productDto.ManufactureEmail.Should().Be(product.ManufactureEmail);
        productDto.ManufacturePhone.Should().Be(product.ManufacturePhone);
        productDto.ProduceDate.Should().Be(product.ProduceDate);
    }
}