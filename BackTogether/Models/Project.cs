using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BackTogether.Models {
    public class Project {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Helpers.Enums.Categories? Category { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Backing>? Backings { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public decimal CurrentFunding { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal FinalGoal { get; set; }

        // Virtual to benefit from EF lazy loading functionality
        public virtual ICollection<ResourceURL>? ImageURLS { get; set; } = null!;
        public virtual ICollection<Reward>? Rewards { get; set; } = null!;
    }
}
