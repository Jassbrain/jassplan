using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassActivity:JassActivityCommon
    {
        public int JassActivityID { get; set; }
        public int JassAreaID { get; set; }
        public virtual JassArea JassArea { get; set; }
    }
}