using multi_tenant_ca.Domain.Entities;

namespace multi_tenant_ca.Application.Books.Queries.GetBookWithPagination;
public class BookBriefDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public float Price { get; set; }
    public Guid TenantId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookBriefDto>();
        }
    }
}
