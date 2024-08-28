
using multi_tenant_ca.Application.Books.Queries.GetBookWithPagination;
using multi_tenant_ca.Application.Common.Models;

namespace multi_tenant_ca.Web.Endpoints;

public class Books : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .RequireAuthorization()
           .MapGet(GetBooksWithPagination);
    }

    public Task<PaginatedList<BookBriefDto>> GetBooksWithPagination(ISender sender, [AsParameters] GetBooksWithPaginationQuery query)
    {
        return sender.Send(query);
    }
}
