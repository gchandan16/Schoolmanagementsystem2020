using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
public partial  class Studentleavedetail
    {
        
        public int Slmid { get; set; }
        
        public int Smid { get; set; }
        
        public string AdmissionNo { get; set; }
        
        public Nullable<DateTime> Leavestartdate { get; set; }
        
        public Nullable<DateTime> Leaveenddate { get; set; }
        
        public string LeaveRreason { get; set; }
        
        public string Leavedocument { get; set; }
        
        public int SchoolId { get; set; }
        public Nullable<DateTime> Createddate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public Nullable<DateTime> Modifieddate { get; set; }
        
        public string Modifiedby { get; set; }
        
        public int FMid { get; set; }
        public string LeaveStatus { get; set; }
        public string Remarks { get; set; }

    }
}
