namespace Domain.Common;

public class Entity : Entity<Guid>;

public class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
}