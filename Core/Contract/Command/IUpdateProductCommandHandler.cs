using MediatR;

namespace Contract.Command;

public interface IUpdateProductCommandHandler
{
    Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken);
}