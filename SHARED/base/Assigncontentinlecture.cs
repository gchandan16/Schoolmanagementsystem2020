using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial  class Assigncontentinlecture
{
        public int Acdtmid { get; set; }
        public int Apttmid { get; set; }
        public int Sumid { get; set; }
        public int Scmid { get; set; }
        public string Topic { get; set; }
        public string Contents { get; set; }
        public string Files { get; set; }
        public string Onlineurl { get; set; }
        public Nullable<System.DateTime> Dates { get; set; }
        public int schoolid { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
}
