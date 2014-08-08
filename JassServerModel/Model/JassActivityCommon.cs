using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassActivityCommon
    {
        public int? ParentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string title { get; set; }
        public string narrative { get; set; }

        public string Status { get; set; }   //sleep, star, done... for now
        public string Flag { get; set; }   //red, yellow, red, blue



        public DateTime dateCreated { get; set; }

        public int? EstimatedDuration { get; set; }
        public int? ActualDuration { get; set; }
        public bool TodoToday { get; set; }
        public bool DoneToday { get; set; }

        public DateTime LastUpdated { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}