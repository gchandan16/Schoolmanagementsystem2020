using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class CastModel: BaseModel
    {
        public int Castmid { get; set; }
        [Required(ErrorMessage = "Please enter Cast name")]
        public string CastName { get; set; }
        public string ActionName { get; set; }
        public List<CastMaster> CastList { get; set; }
    }
}