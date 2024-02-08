using MediatR;

namespace Contract.Command
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(int productId, int userId)
        {
            ProductId = productId;
            UserId = userId;
        }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
