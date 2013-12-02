using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassAreaHistory: JassAreaCommon
    {
        public int JassAreaHistoryID { get; set; }
        public int JassAreaKey { get; set; } //I use the word Key as opposed to ID to avoid messing up EF

        public virtual List<JassActivityHistory> ActivityHistories { get; set; }

        public DateTime TimeStamp  { get; set; }

    }
}