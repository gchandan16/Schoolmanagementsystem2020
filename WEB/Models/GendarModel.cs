using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class GendarModel:BaseModel
    {
        public int GMid { get; set; }
        [Required(ErrorMessage = "Please enter Gendar name")]
        public string GName { get; set; }
        public string ActionName { get; set; }
        public List<GendraMaster> GendarList { get; set; }
    }
}