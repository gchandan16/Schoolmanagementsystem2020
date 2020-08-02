using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public partial class StateMaster
    {
        public int STATE_ID { get; set; }
        public string STATECODE { get; set; }
        public string STATENAME { get; set; }
        public string STATEDESC { get; set; }
        public Boolean ISACTIVE { get; set; }
        public int COUNTRY_ID { get; set; }
        public string CREATEDBY { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public DateTime MODIFIEDDATE { get; set; }

    }
}
