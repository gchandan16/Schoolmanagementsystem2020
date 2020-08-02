using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class Onlineexamstudent
    {
        public int Somid { get; set; }
        public int Smid { get; set; }
        public int quazid { get; set; }
       
        public int SchoolId { get; set; }
        public int FinancialYear { get; set; }
        public Nullable<DateTime> Createdate { get; set; }
        public string Createdby { get; set; }
        public Nullable<DateTime> Modifieddate { get; set; }
        public string Modifiedby { get; set; }
    }
}
