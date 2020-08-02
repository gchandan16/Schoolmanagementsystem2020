using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
public partial class ShiftMaster
{
        public int Shiftmid { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
        public int schoolid { get; set; }
        public string Shiftdesc { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
}
