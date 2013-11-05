using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassActivity
    {
        public int JassActivityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int JassAreaID { get; set; }
        public virtual JassArea JassArea { get; set; }

        public int? EstimatedDuration { get; set; }
        public int? ActualDuration { get; set; }
        public bool TodoToday { get; set; }
        public bool DoneToday { get; set; }

        public DateTime LastUpdated { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DoneDateTime { get; set; }
    }
}