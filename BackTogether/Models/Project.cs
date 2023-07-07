using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackTogether.Models {
    public class Project {
        public string Id {
            get => Id; 
            set {
                Id = Guid.NewGuid().ToString();
            } 
        }
        public string Title { get; set; }
        public Enums.Categories? Category { get; set; }
        public string? Description{ get; set; }
        public string OwnerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated {
            get => DateCreated;
            set {
                DateCreated = DateTime.Now;
            }
        }
        public ICollection<ResourceURL> ImageURLS { get; set; } = null!;
        public ICollection<Reward> Rewards { get; set; } = null!;
        public ICollection<Backing> Backings { get; set; } = null!;
        public decimal CurrentFunding {
            get => CurrentFunding;
            set {
                CurrentFunding = 0.0m;
            }
        }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal FinalGoal { get; set; }
    }
}
