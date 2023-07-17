using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackTogether.Models {
    public class User {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<Project>? Projects { get; } = new List<Project>();
        public ICollection<Backing>? Backings { get; } = new List<Backing>();
        public int? ImageURLId { get; set; }
        public ResourceURL? ImageURL { get; set; }
        public bool HasAdminPrivileges { get; set; }
    }
}
