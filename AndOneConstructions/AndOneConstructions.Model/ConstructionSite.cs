namespace AndOneConstructions.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ConstructionSite
    {
        public ConstructionSite()
        {
            Buildings = new HashSet<Building>();
        }

        public int ConstructionSiteId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int ProjectId { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }

        public virtual Project Project { get; set; }
    }
}