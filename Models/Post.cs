using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev.Models
{
    public class Post
    {
        public Post()
        {
            Images = new List<Image>();
        }
        [Key]
        public int PostId { get; set; }
        [DisplayName("Post Name")]
        [Required(ErrorMessage = "Post Name is Required!")]
        [StringLength(50)]
        public string PostName { get; set; }
        [Required(ErrorMessage = "Comment is Required!")]
        [StringLength(50)]
        public string Comment { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
