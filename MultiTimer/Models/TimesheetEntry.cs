using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTimer
{
    internal class TimesheetEntry
    {
        public string CommentText { get; set; }
        public bool IsBug { get; set; }
        public int ProjectId { get; set; }
        public int DevOpsTaskId { get; set; }
        public string TicketReference { get; set; }
        public float Time { get; set; }
    }
}
