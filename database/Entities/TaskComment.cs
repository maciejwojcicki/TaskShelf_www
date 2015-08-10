using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Entities
{
    public class TaskComment
    {
        public int TaskCommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public virtual Task Task { get; set; }
    }
}
