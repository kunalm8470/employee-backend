namespace Employees.Domain.Models
{
    public abstract class BaseEntity<TKey> where TKey: notnull
    {
        public TKey Id { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
