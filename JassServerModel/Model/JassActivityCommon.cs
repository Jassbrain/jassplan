using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassActivityCommon
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int? EstimatedDuration { get; set; }
        public int? ActualDuration { get; set; }
        public bool TodoToday { get; set; }
        public bool DoneToday { get; set; }

        public DateTime LastUpdated { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}