namespace Entities.Interface
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    
    }
}
