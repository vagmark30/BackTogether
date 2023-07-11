using System.ComponentModel.DataAnnotations;

namespace BackTogether.Models {
    public class Reward {
        [Key]
        public string Id {
            get => Id;
            set {
                Id = Guid.NewGuid().ToString();
            }
        }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public decimal UnlockAmount { get; set; }
    }
}
