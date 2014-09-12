using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using JassServerModel.Model;

namespace Jassplan.Model
{
    public class JassActivity:JassActivityCommon, IUserOwned
    {
        public int JassActivityID { get; set; }
    }
}