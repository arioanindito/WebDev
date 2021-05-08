using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDev.Models
{
    public class PostIndexModel
    {
        public int PostId { get; set; }
        [Display(Name = "Post Name")]
        public string Comment { get; set; }
        [Display(Name = "Post Creator")]
        public string UserName { get; set; }
    }
}
