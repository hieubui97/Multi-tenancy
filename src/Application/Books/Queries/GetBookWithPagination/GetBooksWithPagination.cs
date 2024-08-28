using Ardalis.SharedKernel;
using multi_tenant_ca.Application.Common.Mappings;
using multi_tenant_ca.Application.Common.Models;
using multi_tenant_ca.Domain.Entities;

namespace multi_tenant_ca.Application.Books.Queries.GetBookWithPagination;

public record GetBooksWithPaginationQuery : IRequest<PaginatedList<BookBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBooksWithPaginationQueryHandler : IRequestHandler<GetBooksWithPaginationQuery, PaginatedList<BookBriefDto>>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<Book> _bookRepository;

    public GetBooksWithPaginationQueryHandler(IMapper mapper, IReadRepository<Book> bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    public async Task<PaginatedList<BookBriefDto>> Handle(GetBooksWithPaginationQuery request, CancellationToken cancellationToken)
    {

        return await _bookRepository.GetQueryable()
         .OrderBy(x => x.Name)
         .ProjectTo<BookBriefDto>(_mapper.ConfigurationProvider)
         .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
