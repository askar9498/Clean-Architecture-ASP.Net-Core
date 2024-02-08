using MediatR;

namespace Contract.Command;

public class UpdateProductCommand : IRequest
{
    public UpdateProductCommand(int productId, int userId, string name, bool isAvailable, string manufactureEmail, string manufacturePhone, DateTime produceDate)
    {
        ProductId = productId;
        UserId = userId;
        Name = name;
        IsAvailable = isAvailable;
        ManufactureEmail = manufactureEmail;
        ManufacturePhone = manufacturePhone;
        ProduceDate = produceDate;
    }
    public int ProductId { get; private set; }
    public int UserId { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }
    public string ManufactureEmail { get; private set; }
    public string ManufacturePhone { get; private set; }
    public DateTime ProduceDate { get; private set; }
}