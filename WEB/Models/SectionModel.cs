using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class SectionModel: BaseModel
    {
        public int Secmid { get; set; }
        [Required(ErrorMessage = "Please enter Section name")]
        public string SectionName { get; set; }
        public int CMid { get; set; }
        public string ActionName { get; set; }
        public List<ClassMaster> ClassList { get; set; }
        public List<SectionMaster> SectionList { get; set; }
        public DataTable SectionTblList { get; set; }
        
    }
}