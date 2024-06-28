using System.ComponentModel.DataAnnotations.Schema;

namespace ChatModelsLibrary.Models
{
    public class IndividualChat
    {
        public int Id { get; set; }

        public string? SenderId { get; set; }

        public string? ReciverId { get;}

        public string? Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }
    }
}
