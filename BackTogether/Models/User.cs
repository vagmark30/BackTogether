using System.ComponentModel.DataAnnotations;

namespace BackTogether.Models {
    public class User {
        public string Id {
            get => Id;
            set {
                Id = Guid.NewGuid().ToString();
            }
        }
        public string Username { get; set; }
        public string ImageURLId { get; set; }
        public ResourceURL? ImageURL { get; set; }
        public ICollection<Project> OwnedProjects { get; set; } = null!;
        public ICollection<Project> BackedProjects { get; set; } = null!;

    }
}
