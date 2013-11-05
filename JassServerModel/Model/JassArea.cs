using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Jassplan.Model
{
    public class JassArea
    {
        public int JassAreaID { get; set; }
        public string Name { get; set; }
        public virtual List<JassActivity> Activities { get; set; }



    }
}