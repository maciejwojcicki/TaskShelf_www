using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementations.Models
{
    public class CreateCommentModel
    {
        public int TaskCommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
