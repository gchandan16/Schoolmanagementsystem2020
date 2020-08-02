using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class BloodGroupModel : BaseModel
    {
        public int Bdmid { get; set; }
        [Required(ErrorMessage = "Please enter BloodGroup name")]
        public string BloodGroupName { get; set; }
        public string ActionName { get; set; }
        public List<BloodGroupMaster> BloodGroupList { get; set; }
    }
}