using AutoMapper;
using Contract.Command;
using Domain;
using Domain.Exceptions;
using MediatR;

namespace ApplicationService.Command.UpdateProduct;

public class UpdateProductCommandHandler : IUpdateProductCommandHandler
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? existProduct = await _productRepository.GetByIdAsync(request.ProductId);

        if (existProduct == null)
        {
            throw new NotFoundException($"Product with ID {request.ProductId} not found.");
        }

        if (existProduct.UserId != request.UserId)
        {
            throw new AuthorizationException("You are not authorized to update this product.");
        }

        Product newProduct = _mapper.Map<Product>(request);
        existProduct.Update(newProduct);

        await _productRepository.UpdateAsync(existProduct);
        return Unit.Value;
    }
}