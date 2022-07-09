using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApp.Data.Models
{
    public class Tweet
    {
        [Key]
        public long Id { get; set; }

        public string? AuthorId { get; set; }

        public string? Text { get; set; }
    }
}
