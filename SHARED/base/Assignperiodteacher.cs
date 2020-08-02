using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial class Assignperiodteacher
{
        public int Apttmid { get; set; }
        public int EMP_ID { get; set; }
        public int Pmid { get; set; }
        public int CMid { get; set; }
        public int Secmid { get; set; }
        public int schoolid { get; set; }
        public string Perioddescription { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
}
