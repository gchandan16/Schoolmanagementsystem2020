using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class ContactTitleModel:BaseModel
    {
        public int CTmid { get; set; }
        [Required(ErrorMessage = "Please enter Contact Title")]
        public string ContactName { get; set; }
        public string ActionName { get; set; }
        public List<ContactTitleMaster> ContactTitleMasterList { get; set; }
    }
}