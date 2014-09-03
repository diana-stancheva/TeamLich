namespace SecretDB.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public class Bribe
    {
        [Key]
        public long BribeId { get; set; }

        public int Amount { get; set; }

        public string ProjectName { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}