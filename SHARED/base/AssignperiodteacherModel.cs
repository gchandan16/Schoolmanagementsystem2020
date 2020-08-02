using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class AssignperiodteacherModel : BaseModel
    {
        public int Apttmid { get; set; }
        [Required(ErrorMessage ="Select Teacher")]
        public int EMP_ID { get; set; }
        [Required(ErrorMessage = "Select Period")]
        public int Pmid { get; set; }
        [Required(ErrorMessage = "Select Class")]
        public int CMid { get; set; }
        [Required(ErrorMessage = "Select Section")]
        public int Secmid { get; set; }
        [Required(ErrorMessage = "Enter the period remarks")]
        public string Perioddescription { get; set; }
        public string ActionName { get; set; }
        public List<EmployeeMaster> TeacherList { get; set; }
        public List<PeriodMaster> PeriodmasterList { get; set; }
        public List<ClassMaster> ClassmasterList { get; set; }
        public List<SectionMaster> SectionmasterList { get; set; }
        public List<Assignperiodteacher> AssignperiodteacherList { get; set; }
        public DataTable AssignperiodteacherTblList { get; set; }
    }
}