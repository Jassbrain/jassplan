using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassActivityHistory:JassActivityCommon
    {
        public int JassActivityHistoryID { get; set; }
        public int JassActivityKey { get; set; } //I use the word Key as opposed to ID to avoid messing up EF

        public int JassAreaHistoryID { get; set; }
        public virtual JassAreaHistory JassAreaHistory { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}