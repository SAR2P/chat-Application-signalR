using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatModelsLibrary
{
    public class Chat
    {
        public int Id { get; set; }
        [Required]
        public string? Message { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateTime { get; set; }
    }
}
