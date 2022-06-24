﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTimer
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public bool IsBug { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Id}";
        }
    }
}
