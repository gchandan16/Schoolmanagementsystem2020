using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class RouteModel: BaseModel
    {
        public int Routemid { get; set; }
        [Required(ErrorMessage = "Please enter Route name")]
        public string RouteName { get; set; }
        public string ActionName { get; set; }
        public List<RouteMaster> RouteList { get; set; }
    }
}