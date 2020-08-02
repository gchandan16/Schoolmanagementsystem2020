using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class DesignationModel: BaseModel
    {
        public int Designation_id { get; set; }
        [Required(ErrorMessage = "Please enter Designation name")]
        public string DesignationName { get; set; }
        public string DesignationDesc { get; set; }
        public string Remarks { get; set; }
        public bool Isactive { get; set; }
        public string ActionName { get; set; }
        public List<DesignationMaster> DesignationList { get; set; }
    }
}