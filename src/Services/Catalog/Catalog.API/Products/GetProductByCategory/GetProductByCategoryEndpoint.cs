
using Catalog.API.Products.GetProductById;
using System.Threading.Tasks;

namespace Catalog.API.Products.GetProductByCategory
{
    //public record GetProductByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/Products/category/{Category}",
               async (string Category,ISender sender)=>{
                   var result = await sender.Send(new GetProductByCategoryQuery(Category));
                   var response = result.Adapt<GetProductByCategoryResponse>();
                   return Results.Ok(response);
            })
                .WithName("GetProductByCategory")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Category")
                .WithDescription("Get Product By Category");
        }

       
       
    }
}
