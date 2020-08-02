using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class ReligionModel: BaseModel
    {
        public int Relmid { get; set; }
        [Required(ErrorMessage = "Please enter Religion name")]
        public string ReligionName { get; set; }
        public string ActionName { get; set; }
        public List<ReligionMaster> ReligionList { get; set; }
    }
}