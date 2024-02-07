using Contract.Command;
using Domain;
using FluentValidation;

namespace ApplicationService.Command.CreateProduct;

public class CreateProductCommandHandler : ICreateProductCommandHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CreateProductCommand> _validator;
    public CreateProductCommandHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator)
    {
        _productRepository = productRepository;
        _validator = validator;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate the command
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Product product = new(request.Name, request.IsAvailable, request.ManufactureEmail, request.ManufacturePhone, request.ProduceDate);
        return await _productRepository.AddAsync(product);
    }
}