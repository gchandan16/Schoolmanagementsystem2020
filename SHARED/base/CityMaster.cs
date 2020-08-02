using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class CityMaster
    {
        
        public int CITY_ID { get; set; }
        
        public string CITYCODE { get; set; }
        
        public string CITYNAME { get; set; }
        
        public string CITYDESC { get; set; }
        
        public int STATE_ID { get; set; }
        
        public bool ISACTIVE { get; set; }
        
        public string CREATEDBY { get; set; }
        
        public DateTime CREATEDDATE { get; set; }
        
        public string MODIFIEDBY { get; set; }
        
        public DateTime MODIFIEDDATE { get; set; }

    }
}
