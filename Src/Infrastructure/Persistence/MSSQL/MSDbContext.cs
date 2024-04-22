using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.MSSQL
{
    public class MSDbContext : DbContext, IMSDbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        public MSDbContext(DbContextOptions<MSDbContext> options)
            : base(options)
        {
        }

        public MSDbContext(
            DbContextOptions<MSDbContext> options,
            IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;           
        }
        
        public DbSet<Appointment> Appointments { get; set; }
      
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:                    
                        entry.Entity.Created = _dateTimeProvider.GetCurrentDate();
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTimeProvider.GetCurrentDate();
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }

}
