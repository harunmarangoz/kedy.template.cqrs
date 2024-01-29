namespace Domain.Common;

public interface ISoftDeleteEntity
{
    public string DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    bool IsDeleted { get; set; }
}