using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackTogether.Models {
    public class User {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public virtual ICollection<Project> Projects { get; } = new List<Project>();
        public virtual ICollection<Backing> Backings { get; } = new List<Backing>();
        public int? ImageURLId { get; set; }
        public virtual ResourceURL? ImageURL { get; set; }
        public bool HasAdminPrivileges { get; set; }
    }
}
