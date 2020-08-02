using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class SubjectChapterModel : BaseModel
    {
        public int Scmid { get; set; }
        [Required(ErrorMessage = "Please enter Chapter name")]
        public string Chaptername { get; set; }
        public string Chapterdes { get; set; }
        [Required(ErrorMessage = "Please Select Subject name")]
        public int Sumid { get; set; }
        [Required(ErrorMessage = "Please Select Class name")]
        public int CMid { get; set; }
        public string ActionName { get; set; }
        public List<SubjectMaster> SubjectList { get; set; }
        public List<ClassMaster> ClassList { get; set; }
        public List<SubjectchapterMaster> SubjectchapterList { get; set; }
        public DataTable SubjectChapterTblList { get; set; }
    }
}