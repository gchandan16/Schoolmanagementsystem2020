using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class DailyattendanceModel : BaseModel
    {
        public string DisplaymonthYear { get; set; }
        public int SelectClassId { get; set; }
        public int SelectSectionId { get; set; }
        public int Totalchkbox { get; set; }
        public DateTime Attendancedate { get; set; }
        public DateTime Toattendancedate { get; set; }
        public List<ClassMaster> ClassList { get; set; }
        public List<SectionMaster> ClassSectionList { get; set; }
        public List<Monthalyattendance> Monathalyattendancelist { get; set; }
        public List<StudentAttendanceMaster> StudentAttendanceList { get; set; }
        public List<Monthalyattendance> MonthalyAttendanceList { get; set; }
        public DataTable Attendancestulist { get; set; }
        public DataTable Attendancesheetlist { get; set; }
        public DataTable Monthalyattendancesheetlist { get; set; }
        public DataTable AttendanceSummarytbl { get; set; }

    }
}