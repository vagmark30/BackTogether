using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackTogether.Models {
    public class Backing{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }= null!;

        [DataType(DataType.Date)]
        public DateTime DateBacked { get; set; }

        public decimal Amount { get; set; }

        // Virtual to benefit from EF lazy loading functionality
        public virtual ICollection<Reward> RewardsUnlocked { get; set; } = null!;
    }
}
