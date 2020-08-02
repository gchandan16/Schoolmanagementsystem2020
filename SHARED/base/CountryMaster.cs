using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class CountryMaster
    {
        
        public int COUNTRY_ID { get; set; }
        
        public string COUNTRYNAME { get; set; }
        
        public string COUNTRYDESC { get; set; }
        
        public bool ISACTIVE { get; set; }
        
        public string CREATEDBY { get; set; }
        
        public DateTime CREATEDDATE { get; set; }
        
        public string MODIFIEDBY { get; set; }
        
        public DateTime MODIFIEDDATE { get; set; }
        
        public string COUNTRYCODE { get; set; }

    }
}
