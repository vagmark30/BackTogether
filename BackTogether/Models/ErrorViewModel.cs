using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackTogether.Models {
    public class ErrorViewModel {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}