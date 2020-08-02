using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class TDSSettingmaster
{
        public int Tdsmid { get; set; }

        public string Taxcategory { get; set; }
        public int TYear { get; set; }
        public Nullable<DateTime> ApplicableFrom { get; set; }
        public double TaxRate { get; set; }
        public double Tminamount { get; set; }
        public double Tmaxamount { get; set; }
        public int Schoolid { get; set; }
        public int FinancialYear { get; set; }
        public Nullable<DateTime> Createdate { get; set; }
        public Nullable<DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }

    }
}
