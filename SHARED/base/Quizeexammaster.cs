using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
public partial class Quizeexammaster
{
        public int Qzmid { get; set; }
        public string Examtitle { get; set; }
        public int Classid { get; set; }
        public int Subjectid { get; set; }
        public int Rightque { get; set; }
        public int Wrongque { get; set; }
        public int ExamTime { get; set; }
        public string Description { get; set; }
        public int Totalquestion { get; set; }
        public int SchoolId { get; set; }
        public int FinancialYear { get; set; }
        public Nullable<DateTime> Examdate { get; set; }
        public Nullable<DateTime> Createdate { get; set; }
        public string Createdby { get; set; }
        public Nullable<DateTime> Modifieddate { get; set; }
        public string Modifiedby { get; set; }
    }
}
