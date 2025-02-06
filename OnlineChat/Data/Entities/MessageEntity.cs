using System.ComponentModel.DataAnnotations;

namespace OnlineChat.Data.Models
{
    public class MessageEntity
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public int message_num { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        [MaxLength(128)]
        public string content { get; set; }
    }
}
