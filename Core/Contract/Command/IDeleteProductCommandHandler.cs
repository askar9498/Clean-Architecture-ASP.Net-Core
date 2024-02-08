using MediatR;

namespace Contract.Command;

public interface IDeleteProductCommandHandler
{
    Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken);
}