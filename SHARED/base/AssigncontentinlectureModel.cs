using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class AssigncontentinlectureModel : BaseModel
    {
        public int Acdtmid { get; set; }
        [Required(ErrorMessage ="Select Lecture")]
        public int Apttmid { get; set; }
        [Required(ErrorMessage = "Select Subject")]
        public int Sumid { get; set; }
        [Required(ErrorMessage = "Select Chapter")]
        public int Scmid { get; set; }
        [Required(ErrorMessage = "Enter Topic")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "Enter Contents")]
        public string Contents { get; set; }
        public string Files { get; set; }
        public string Onlineurl { get; set; }
        [Required(ErrorMessage = "Choose Date")]
        public Nullable<System.DateTime> Dates { get; set; }
        public string ActionName { get; set; }
        public List<Assignperiodteacher> AssignperiodteacherList { get; set; }
        public List<SubjectMaster> SubjectList { get; set; }
        public List<SubjectchapterMaster> SubjectchapterList { get; set; }
        public List<Assigncontentinlecture> AssigncontentinlectureList { get; set; }
        public DataTable AssigncontentinlectureTblList { get; set; }
    }
}