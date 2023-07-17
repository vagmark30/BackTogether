using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackTogether.Models {
    public class Reward {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public decimal UnlockAmount { get; set; }
    }
}
