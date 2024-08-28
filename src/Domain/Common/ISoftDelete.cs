namespace multi_tenant_ca.Domain.Common;

public interface ISoftDelete
{
    /// <summary>
    /// Used to mark an Entity as 'Deleted'.
    /// </summary>
    bool IsDeleted { get; }
}
