using Domain;
using FluentAssertions;

namespace DomainUnitTest;

public class ProductUnitTests
{
    private static readonly int ExpectedUserId = 1;
    private static readonly string ExpectedProductName = "Name";
    private static readonly string ExpectedManufactureEmail = "Menu";
    private static readonly string ExpectedManufacturePhone = "Phone";
    private static readonly DateTime ExpectedProduceDate = DateTime.UtcNow;

    [Fact]
    public void GivenConstructorParameters_ProductPropertiesAreInitializedCorrectly()
    {
        // Arrange & Act
        Product product = new(
            ExpectedUserId ,
            ExpectedProductName,
            isAvailable: true,
            ExpectedManufactureEmail,
            ExpectedManufacturePhone,
            ExpectedProduceDate
        );

        // Assert
        product.Name.Should().Be(ExpectedProductName);
        product.IsAvailable.Should().BeTrue();
        product.ManufactureEmail.Should().Be(ExpectedManufactureEmail);
        product.ManufacturePhone.Should().Be(ExpectedManufacturePhone);
        product.ProduceDate.Should().Be(ExpectedProduceDate);
        product.UserId.Should().Be(ExpectedUserId);
    }
}

