namespace Domain.Common;

public interface IEntity : IEntity<Guid>;

public interface IEntity<T>
{
    T Id { get; set; }
}