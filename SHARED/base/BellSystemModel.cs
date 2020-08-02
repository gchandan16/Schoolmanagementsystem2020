using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class BellSystemModel : BaseModel
    {
        public int bmid { get; set; }
        [Required(ErrorMessage = "Please Select Period")]
        public int Pmid { get; set; }
        [Required(ErrorMessage = "Please Enter Bell Title")]
        public string Belltitle { get; set; }
        public string Bellsongpath { get; set; }

        public string ActionName { get; set; }
        public List<PeriodMaster> PeriodmasterList { get; set; }
        public DataTable BellSystemTblList { get; set; }

    }
}