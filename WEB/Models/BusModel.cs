using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class BusModel: BaseModel
    {
        public int Busmid { get; set; }
        [Required(ErrorMessage = "Please enter Bus name")]
        public string BusName { get; set; }
        public string ActionName { get; set; }
        public List<BusMaster> BusList { get; set; }
    }
}