namespace Employees.Domain.Models
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; private set; }

        public Department(string name)
        {
            Name = name;
        }

        public Department(string name,
            DateTime createdAt,
            DateTime? updatedAt) : this(name)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
