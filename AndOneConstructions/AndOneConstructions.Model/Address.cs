namespace AndOneConstructions.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        public Address()
        {
            ConstructionSites = new HashSet<ConstructionSite>();
        }

        public int AddressId { get; set; }

        [Required]
        [StringLength(100)]
        public string Details { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<ConstructionSite> ConstructionSites { get; set; }
    }
}
