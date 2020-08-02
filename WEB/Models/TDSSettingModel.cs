using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class TDSSettingModel : BaseModel
    {
        public int Tdsmid { get; set; }

        public string Taxcategory { get; set; }
        public int TYear { get; set; }
        public Nullable<DateTime> ApplicableFrom { get; set; }
        public double TaxRate { get; set; }
        public double Tminamount { get; set; }
        public double Tmaxamount { get; set; }
        public string ActionName { get; set; }
        public List<TDSSettingmaster> TDSSettinglist { get; set; }

        public DataTable TDSSettinglistTbl { get; set; }
    }
}