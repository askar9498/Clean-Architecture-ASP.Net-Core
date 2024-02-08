using Contract.Command;
using Domain;
using Domain.Exceptions;
using MediatR;

namespace ApplicationService.Command.DeleteProduct;

public class DeleteProductCommandHandler : IDeleteProductCommandHandler
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId);

        if (product == null)
        {
            throw new NotFoundException($"Product with ID {request.ProductId} not found.");
        }

        if (product.UserId != request.UserId)
        {
            throw new AuthorizationException("You are not authorized to delete this product.");
        }

        await _productRepository.DeleteAsync(request.ProductId);
        return Unit.Value;
    }
}