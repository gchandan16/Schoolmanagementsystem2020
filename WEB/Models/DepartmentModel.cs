using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class DepartmentModel: BaseModel
    {
        public int Department_id { get; set; }
        [Required(ErrorMessage = "Please enter Department name")]
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public string Remarks { get; set; }
        public bool Isactive { get; set; }
        public string ActionName { get; set; }
        public List<DepartmentMaster> DepartmentList { get; set; }
    }
}