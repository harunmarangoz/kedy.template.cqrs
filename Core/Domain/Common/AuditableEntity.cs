namespace Domain.Common;

public class AuditableEntity : AuditableEntity<Guid>;

public class AuditableEntity<T> : Entity<T>, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }
}