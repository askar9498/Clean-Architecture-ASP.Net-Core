using AutoMapper;
using Contract.Command;
using Domain;
using FluentValidation;

namespace ApplicationService.Command.CreateProduct;

public class CreateProductCommandHandler : ICreateProductCommandHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IMapper _mapper;
    public CreateProductCommandHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator, IMapper mapper)
    {
        _productRepository = productRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Product product = _mapper.Map<Product>(request);
        return await _productRepository.AddAsync(product);
    }
}