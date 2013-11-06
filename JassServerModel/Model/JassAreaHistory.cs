using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassAreaHistory
    {
        public int JassAreaHistoryID { get; set; }
        public int JassAreaKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<JassActivityHistory> Activities { get; set; }
        public DateTime TimeStamp  { get; set; }

    }
}