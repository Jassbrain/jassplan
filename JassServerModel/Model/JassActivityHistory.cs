using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using JassServerModel.Model;

namespace Jassplan.Model
{
    public class JassActivityHistory : JassActivityCommon, IUserOwned
    {
        public int JassActivityHistoryID { get; set; }
        public int JassActivityID { get; set; }
        public int? JassActivityReviewID { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}