namespace Domain.Common;

public class AuditableSoftDeleteEntity : AuditableSoftDeleteEntity<Guid>;

public class AuditableSoftDeleteEntity<T> : Entity<T>, IAuditableSoftDeleteEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
}