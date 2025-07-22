using Microsoft.EntityFrameworkCore;

namespace CommonApi.infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Configure your entities here
			// For example:
			// modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
			// modelBuilder.Entity<YourEntity>().HasKey(e => e.Id);
			// modelBuilder.Entity<YourEntity>().Property(e => e.Name).IsRequired();
		}
	}
}
