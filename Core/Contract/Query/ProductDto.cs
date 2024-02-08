namespace Contract.Query;

public record ProductDto(string Name, string ManufactureEmail, string ManufacturePhone, bool IsAvailable, DateTime ProduceDate);