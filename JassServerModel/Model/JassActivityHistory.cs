﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassActivityHistory
    {
        public int JassActivityHistoryID { get; set; }
        public int JassActivityKey { get; set; }

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
        public DateTime? Done { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}