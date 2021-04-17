using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Index { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
