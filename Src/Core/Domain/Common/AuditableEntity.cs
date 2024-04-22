using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public class AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }

        [StringLength(50)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
