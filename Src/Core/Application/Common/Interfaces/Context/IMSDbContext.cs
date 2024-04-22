using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IMSDbContext
    {
        DbSet<Appointment> Appointments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
