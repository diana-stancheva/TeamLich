namespace AndOneConstructions.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Building
    {
        public int BuildingId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int ConstructionSiteId { get; set; }

        public virtual ConstructionSite ConstructionSite { get; set; }
    }
}
