
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IMSDbContext _context;
        public AppointmentRepository(IMSDbContext context)
        {
            _context = context;
        }
        public async Task<Appointment?> CreateAsync(Appointment entity, CancellationToken cancellationToken)
        {
            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<bool> DeleteBySlotAsync(DateTime slot, CancellationToken cancellationToken)
        {
            //TODO : Search is not efficent
            var entity = await _context.Appointments.FirstOrDefaultAsync(entity => entity.SlotStartTime == slot);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Appointment), slot);
            }
            // TODO:Work on entity State and tracking
            _context.Appointments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: Work on properper return impementation
            return true;
        }
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Appointment), id);
            }
            // TODO:Work on entity State and tracking
            _context.Appointments.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            // TODO: Work on properper return impementation
            return true;
        }

        public async Task<IList<Appointment>?> GetAsync(CancellationToken cancellationToken)
        {
            return await _context.Appointments.ToListAsync(cancellationToken);
        }

        public async Task<Appointment?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Appointment), id);
            }

            return entity;
        }

        public async Task<Appointment?> PatchAsync(Guid id, Appointment entity, CancellationToken cancellationToken)
        {
            // TODO Implementaton
            throw new NotImplementedException();
        }

        public async Task<Appointment?> UpdateAsync(Guid id, Appointment entity, CancellationToken cancellationToken)
        {
            // TODO Implementaton
            throw new NotImplementedException();
        }

        /// <summary>
        /// Predicate
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<Appointment> GetAvailableItemsAsEnumerable(Func<Appointment, bool> condition, CancellationToken cancellationToken)
        {
            IEnumerable<Appointment> ienumerableQuery = _context.Appointments;

            foreach (var Appointment in ienumerableQuery)
                if (condition(Appointment))
                    yield return Appointment;
        }

        public IQueryable<Appointment?> GetItemsAsQueryable(Expression<Func<Appointment, bool>> condition, CancellationToken cancellationToken)
        {
            return _context.Appointments.Where(condition);
        }

        public ICollection<Appointment> GetCollection(CancellationToken cancellationToken)
        {
            // TODO Implementaton
            throw new NotImplementedException();
        }

        public IList<Appointment> GetList(CancellationToken cancellationToken)
        {
            // TODO Implementaton
            throw new NotImplementedException();
        }

        public async Task<bool> IsExist(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Appointments.AnyAsync(p => p.Id == id, cancellationToken);
        }
    }
}
