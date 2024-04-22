using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        IEnumerable<Appointment> GetAvailableItemsAsEnumerable(Func<Appointment, bool> condition, CancellationToken cancellationToken);

        IQueryable<Appointment?> GetItemsAsQueryable(Expression<Func<Appointment, bool>> condition, CancellationToken cancellationToken);

        ICollection<Appointment> GetCollection(CancellationToken cancellationToken);

        IList<Appointment> GetList(CancellationToken cancellationToken);
        Task<bool> DeleteBySlotAsync(DateTime slot, CancellationToken cancellationToken);
    }
}
