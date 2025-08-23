namespace Catalog.API.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{id}", 
                async (Guid Id,ISender sender) =>
            {
               // var query = request.Adapt<GetProductByIdQuery>();
                var responce = await sender.Send(new GetProductByIdQuery(Id));
                var result = responce.Adapt<GetProductByIdResponse>();
                return Results.Ok(result);

            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");
        }
    }
}
