namespace Contract.Query.GetAllProduct;

public record ProductDto(string Name, string ManufactureEmail, string ManufacturePhone, bool IsAvailable, DateTime ProduceDate);