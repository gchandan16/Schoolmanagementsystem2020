using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class StudentAttendanceMaster
    {
        public int Samid { get; set; }
        public int Smid { get; set; }
        public Nullable<System.DateTime> Sattendancedate { get; set; }
        public Nullable<System.DateTime> SIndatetime { get; set; }
        public Nullable<System.DateTime> SOutdatetime { get; set; }
        public string SAtstatus { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string Rollno { get; set; }
        public string RFID { get; set; }
        public string MachineSecialno { get; set; }
        public string Ipaddress { get; set; }
        public int FMid { get; set; }
        public int SchoolId { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> Createddate { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Modifiedby { get; set; }

    }
}
