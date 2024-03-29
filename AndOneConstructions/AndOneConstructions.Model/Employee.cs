namespace AndOneConstructions.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        public Employee()
        {
            Projects = new HashSet<Project>();
        }

        public int EmployeeId { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}