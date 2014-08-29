namespace AndOneConstructionsModelHbr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
