using System.ComponentModel.DataAnnotations;

namespace BackTogether.Models {
    public class Backing{
        public string Id {
            get => Id;
            set {
                Id = Guid.NewGuid().ToString();
            }
        }
        public string UserId { get; set; }
        public User User { get; set; } = null!;
        public string ProjectId { get; set; }
        public Project Project { get; set; }= null!;
        public decimal Amount { get; set; }
        public ICollection<Reward> RewardsUnlocked { get; set; } = null!;
    }
}
