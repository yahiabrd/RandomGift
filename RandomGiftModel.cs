namespace _2NET_PROJECT
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RandomGiftModel : DbContext
    {
        public RandomGiftModel()
            : base("name=RandomGiftModel")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<UsersRandomGift> Users { get; set; }
    }
}
