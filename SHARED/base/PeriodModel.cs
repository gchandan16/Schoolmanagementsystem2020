using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class PeriodModel : BaseModel
    {
        public int Pmid { get; set; }
        [Required(ErrorMessage = "Enter the period Name")]
        public string PeriodName { get; set; }
        [Required(ErrorMessage = "Choose the Period Start Time")]
        public string Perioddesc { get; set; }
        public string PeriodStart { get; set; }
        [Required(ErrorMessage = "Choose the Period End Time")]
        public string PeriodEnd { get; set; }
        public string ActionName { get; set; }
        public List<PeriodMaster> PeriodmasterList { get; set; }
        public DataTable PeriodmasterTblList { get; set; }
    }
}