using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial   class GendraMaster
{
        
        public int GMid { get; set; }
        
        public string  GName { get; set; }
        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
}
