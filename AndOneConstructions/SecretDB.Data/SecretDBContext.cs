namespace SecretDB.Data
{
    using SecretDB.Models;
    using System.Data.Entity;

    public class SecretDBContext : DbContext
    {
        public SecretDBContext()
            : base("SecretDBConnectionString")
        {
        }

        public IDbSet<Bribe> Bribes { get; set; }
    }
}