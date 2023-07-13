using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackTogether.Models {
    public class Project {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }
        public Helpers.Enums.Categories? Category { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }  
        public User User { get; set; }

        public decimal CurrentFunding { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal FinalGoal { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        // Virtual to benefit from EF lazy loading functionality
        public virtual ICollection<ResourceURL> ImageURLS { get; set; } = null!;
        public virtual ICollection<Reward> Rewards { get; set; } = null!;
    }
}
