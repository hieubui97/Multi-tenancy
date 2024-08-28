using Ardalis.SharedKernel;

namespace multi_tenant_ca.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, ISoftDelete, IAggregateRoot
{
    /// <inheritdoc />
    public virtual Guid? CreatedBy { get; set; }

    /// <inheritdoc />
    public virtual DateTimeOffset CreatedTime { get; set; }

    /// <inheritdoc />
    public virtual Guid? ModifiedBy { get; set; }

    /// <inheritdoc />
    public virtual DateTimeOffset ModifiedTime { get; set; }

    /// <inheritdoc />
    public virtual bool IsDeleted { get; set; }

    /// <inheritdoc />
    public virtual Guid? DeletedBy { get; set; }

    /// <inheritdoc />
    public virtual DateTimeOffset? DeletedTime { get; set; }
}
