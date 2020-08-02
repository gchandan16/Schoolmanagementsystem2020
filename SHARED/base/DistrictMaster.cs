using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class DistrictMaster
    {
        public int DID { get; set; }
        public string StateCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictDesc { get; set; }
        public string DistrictCode { get; set; }
        public Boolean Active { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDDATE { get; set; }
    }
}
