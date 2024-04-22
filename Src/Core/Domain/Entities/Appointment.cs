using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Appointment : AuditableEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = default!;  // Can't be NULL [Use Default! or NULL!]
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get{ return SlotStartTime.AddMinutes(30);} }
        public bool IsActive { get; set; }
    }    
}
