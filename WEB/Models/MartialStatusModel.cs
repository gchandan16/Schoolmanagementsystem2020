using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class MartialStatusModel:BaseModel
    {
        public int Mrmid { get; set; }
        [Required(ErrorMessage = "Please enter MartialStatus name")]
        public string StatusName { get; set; }
        public string ActionName { get; set; }
        public List<MartialStatusMaster> MartialStatusList { get; set; }
    }
}