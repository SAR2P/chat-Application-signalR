using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModelsLibrary.DTOs
{
    public class GroupChatDTO
    {
        public int Id { get; set; }
       
        [Required]
        public string? SenderId { get; set; }
        [Required]
        public string? SenderName { get; set; }

        [Required]
        public string? Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateTime { get; set; } = DateTime.Now;

    }
}
