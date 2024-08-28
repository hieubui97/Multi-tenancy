using multi_tenant_ca.Domain.Entities;

namespace multi_tenant_ca.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
        }
    }
}
