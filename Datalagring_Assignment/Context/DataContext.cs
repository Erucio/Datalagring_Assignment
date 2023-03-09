using Microsoft.EntityFrameworkCore;
using Datalagring_Assignment.Models.Entities;
using System.Diagnostics.Metrics;

namespace Datalagring_Assignment.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\erick\source\repos\Datalagring_Assignment\Datalagring_Assignment\Context\Assignment_DB.mdf;Integrated Security=True;Connect Timeout=30";
        #region constructors
        public DataContext()
        { 
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        #endregion

        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        #endregion

        #region entities

        public DbSet<CommentEntity> Comments { get; set; } = null!;
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<ErrandEntity> Errands { get; set; } = null!;
        public DbSet<StatusEntity> Status { get; set; } = null!;


        #endregion
    }
}
