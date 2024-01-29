namespace Domain.Common;

public class SoftDeleteEntity : SoftDeleteEntity<Guid>;

public class SoftDeleteEntity<T> : Entity<T>, ISoftDeleteEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
}