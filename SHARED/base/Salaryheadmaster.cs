using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class Salaryheadmaster
{
        public int   Shmid { get; set; }
        public string Headname { get; set; }
        public string Alias { get; set; }
        public string Displayname { get; set; }
        public string Headtype { get; set; }
        public Double Defaultvalue { get; set; }
        public string Leavededucation { get; set; }
        public string Esiapplicable { get; set; }
        public string Epfapplicable { get; set; }
        public string Calculationtype { get; set; }
        public string Displaysequence { get; set; }
        public int SchoolId { get; set; }
        public int FinancialYear { get; set; }
        public Nullable<DateTime> Createdate { get; set; }
        public Nullable<DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
    }
}
