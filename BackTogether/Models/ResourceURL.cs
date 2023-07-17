using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackTogether.Models {
    public class ResourceURL {
        public int Id { get; set; }
        public string Url { get; set; }
        public Helpers.Enums.Resource Type { get; set; }
    }
}