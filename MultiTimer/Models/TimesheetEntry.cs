using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTimer
{
    public class TimesheetEntry
    {
        public string CommentText { get; set; }
        public int ProjectId { get; set; }
        public int DevOpsTaskId { get; set; }
        public string TicketReference { get; set; }
        public int Time { get; set; }
    }
}
