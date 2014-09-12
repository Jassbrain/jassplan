using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using JassServerModel.Model;

namespace Jassplan.Model
{
    public class JassActivityReview: IUserOwned
    {
        [NotMapped]
        public List<JassActivityHistory> ActivityHistories { get; set; }

        public int JassActivityReviewID { get; set; }
        public string UserName { get; set; }
        public int ReviewYear { get; set; }
        public int ReviewMonth { get; set; }
        public int ReviewDay { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}