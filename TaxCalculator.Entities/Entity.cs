namespace TaxCalculator.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDateTime { get; set; }
    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
