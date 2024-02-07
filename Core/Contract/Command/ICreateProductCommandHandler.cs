namespace Contract.Command;

public interface ICreateProductCommandHandler
{
    Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken);
}