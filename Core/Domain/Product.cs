using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Product
{
    [Range(0, int.MaxValue, ErrorMessage = "Id must be greater than  0.")]
    public int Id { get; private set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = "UserId must be greater than  0.")]
    public int UserId { get; private set; } = 0;

    [Required(ErrorMessage = "Availability status is required.")]
    public bool IsAvailable { get; private set; }

    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string ManufactureEmail { get; private set; }

    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string ManufacturePhone { get; private set; }

    [Required(ErrorMessage = "Produce Date is required.")]
    public DateTime ProduceDate { get; private set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed  100 characters.")]
    public string Name { get; private set; }

    public Product(int userId, string name, bool isAvailable, string manufactureEmail, string manufacturePhone, DateTime produceDate)
    {
        Name = name;
        IsAvailable = isAvailable;
        ManufactureEmail = manufactureEmail;
        ManufacturePhone = manufacturePhone;
        ProduceDate = produceDate;
        UserId = userId;
        Validate(this);
    }

    public void Update(Product newProduct)
    {
        Validate(newProduct);

        Name = newProduct.Name;
        IsAvailable = newProduct.IsAvailable;
        ManufactureEmail = newProduct.ManufactureEmail;
        ManufacturePhone = newProduct.ManufacturePhone;
        ProduceDate = newProduct.ProduceDate;
    }

    private static void Validate(Product product)
    {
        var context = new ValidationContext(product);
        Validator.ValidateObject(product, context, validateAllProperties: true);
    }
}