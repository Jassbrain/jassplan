using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Globalization;
using JassServerModel.Model;

namespace Jassplan.Model
{
    public class JassActivity:JassActivityCommon, IUserOwned
    {
        public int JassActivityID { get; set; }

        [NotMapped]
        public int id
        {
            get { return JassActivityID; }
        }

        [NotMapped]
        public string originalJson
        {
            get;
            set;
        }
    }
}