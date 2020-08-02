using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class StudentleaveModel:BaseModel
    {
        public int Slmid { get; set; }
        public int Smid { get; set; }
        public int CMid { get; set; }
        public int SecMid { get; set; }
        public List<Studentddl> Studentdetaillist { get; set; }
        public Nullable<DateTime> Leavestartdate { get; set; }
        public Nullable<DateTime> Leaveenddate { get; set; }
        public string LeaveRreason { get; set; }
        public string Leavedocument { get; set; }
        public string LeaveStatus { get; set; }
        public string Remarks { get; set; }
        public string ActionName { get; set; }
        public List<ClassMaster> ClassList { get; set; }
        public List<SectionMaster> SectionList { get; set; }
        public List<Studentleavedetail> StudentleaveList { get; set; }
        public DataTable StudentleaveListTbl { get; set; }

    }
}