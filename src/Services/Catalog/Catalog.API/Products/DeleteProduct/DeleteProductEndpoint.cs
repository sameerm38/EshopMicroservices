
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct
{
   // public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsDeleted);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("Products/{Id}",
                async (Guid Id,ISender sender) =>
                {
                    //var command = request.Adapt<DeleteProductCommand>();
                    var result = await sender.Send(new DeleteProductCommand(Id));
                    var response = result.Adapt<DeleteProductResponse>();
                    return Results.Ok(response);

                })
                .WithName("DeleteProduct")
                 .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .ProducesProblem(StatusCodes.Status404NotFound)
                 .WithSummary("Delete Product")
                 .WithDescription("Delete Product");
        }
    }
}
