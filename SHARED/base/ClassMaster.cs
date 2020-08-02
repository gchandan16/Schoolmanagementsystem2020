using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
public partial  class ClassMaster
{
        public int CMid { get; set; }
        public string  ClassName { get; set; }
        public int SchoolID { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string  Createdby { get; set; }
        public string  Modifiedby { get; set; }
        public bool IsActive { get; set; }

    }
}
