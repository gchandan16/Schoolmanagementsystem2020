using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class QuizeexammasterModel:BaseModel
    {
        public int Qzmid { get; set; }
        public string Examtitle { get; set; }
        public int Classid { get; set; }
        public int Subjectid { get; set; }
        public int Rightque { get; set; }
        public int Wrongque { get; set; }
        public int ExamTime { get; set; }
        public Nullable<DateTime> Examdate { get; set; }
        public string Description { get; set; }
        public int Totalquestion { get; set; }
        public string ActionName { get; set; }
        public List<Quizeexammaster> QuizeexammasterList { get; set; }
        public List<SubjectMaster> SubjectList { get; set; }
        public List<ClassMaster> ClassMasterList { get; set; }
        public DataTable QuizeexamlistTbl { get; set; }
    }
}