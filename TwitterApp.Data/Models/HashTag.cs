using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterApp.Data.Models
{
    public class HashTag
    {
        [Key]
        public Guid Id { get; set; }
        public long TweetId { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength(int.MaxValue)]
        public string? Text { get; set; }
        public int Count { get; set; }
    }
}
