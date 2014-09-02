namespace SecretDB.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    
    public class Bribe
    {
        [Key]
        public int BribeId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}