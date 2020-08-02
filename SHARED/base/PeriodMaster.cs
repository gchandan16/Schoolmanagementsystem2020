using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial class PeriodMaster
{
        public int Pmid { get; set; }
        public string PeriodName { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public int schoolid { get; set; }
        public string Perioddesc { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }

    }
}
