using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
   public partial class RoleMaster
{
        
        public int ROLE_ID { get; set; }
        
        public string ROLENAME { get; set; }
        
        public string ROLEDESCRIPTION { get; set; }
        
        public int SchoolID { get; set; }
        
        public bool ISACTIVE { get; set; }
        
        public string CREATEDBY { get; set; }
        
        public Nullable<System.DateTime> CREATEDDATE { get; set; }

    }
}
