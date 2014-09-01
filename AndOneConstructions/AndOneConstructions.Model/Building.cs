namespace AndOneConstructions.Model
{
    using System.ComponentModel.DataAnnotations;

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