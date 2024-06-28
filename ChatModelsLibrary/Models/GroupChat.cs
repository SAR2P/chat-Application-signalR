using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatModelsLibrary.Models
{
    public class GroupChat
    {
        public int Id { get; set; }
        
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateTime { get; set; }
    }
}
