using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class SubjectModel : BaseModel
    {
        public int Sumid { get; set; }
        [Required(ErrorMessage = "Please enter Section name")]
        public string Subjectname { get; set; }
        public string Subjectdescription { get; set; }
        public string ActionName { get; set; }
        public List<ClassMaster> ClassList { get; set; }
        public List<SubjectMaster> SubjectList { get; set; }
        public DataTable SubjectTblList { get; set; }
        
    }
}