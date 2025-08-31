
namespace Basket.API.Basket.DeleteBasket
{
    //public record DeletBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsDeleted);
    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}",async (string UserName,ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(UserName));
                var response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteBasket")
                .Produces(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");
        }
    }
}
