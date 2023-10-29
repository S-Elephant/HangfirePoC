using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HangfirePoC.Dal.DbContexts
{
    /// <summary>
    /// Represents the Entity Framework Core DbContext for the HangFire Proof of concept.
    /// </summary>
    public class HangfirePocDbContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public HangfirePocDbContext(DbContextOptions<HangfirePocDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If you add/move configurations to another assembly then change this line and/or add more lines that load other assemblies.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}