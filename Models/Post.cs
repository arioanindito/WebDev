using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev.Models
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Images = new List<Image>();
        }
        [Key]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Comment is Required!")]
        public string Comment { get; set; }
        //[Required]
        //[ForeignKey("User")]
        //public int UserId { get; set; }
        //public User User { get; set; }
        public string UserName { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
