namespace AndOneConstructions.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
        }

        public int ClientId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}