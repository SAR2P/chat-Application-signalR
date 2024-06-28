

using System.ComponentModel.DataAnnotations.Schema;

namespace ChatModelsLibrary.DTOs
{
    public class IndividualChatDTO
    {
        public string? SenderId { get; set; }

        public string? SenderName { get; set; }

        public string? ReciverId { get; set; }
        public string? ReciverName { get; set; }
        public string? Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
