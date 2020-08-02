using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial  class Assignheadtoemployee
{
        public int Ashmid { get; set; }
        [Required]
        public int EMP_ID { get; set; }
        public int Shmid { get; set; }
        public  Double Amount { get; set; }
        public Nullable<DateTime> Applyfrom { get; set; }
        public int SchoolId { get; set; }
        public int FinancialYear { get; set; }
        public string IsPFType { get; set; }
        public string IsESIType { get; set; }
        public string IsPF { get; set; }
        public Double PFEmployeeamt { get; set; }
        public Double PFEmployeramt { get; set; }
        public string IsESI { get; set; }
        public Double ESIEmployeeamt { get; set; }
        public Double ESIEmployeramt { get; set; }
        public string IsTDS { get; set; }
        public string Taxcategory { get; set; }
        public string Shfittype { get; set; }
        public Nullable<DateTime> Createdate { get; set; }
        public Nullable<DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }

    }
}
