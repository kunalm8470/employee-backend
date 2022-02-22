namespace Employees.Domain.Models
{
    public class Employee : BaseEntity<int>
    {
        public Guid Code { get; set; }

        public string Email { get; private set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public char GenderAbbreviation { get; set; }
        
        public decimal Salary { get; set; }

        public int? ManagerId { get; set; }

        public Employee? Manager { get; private set; }

        public int DepartmentId { get; set; }

        public Department Department { get; private set; }
    }
}
