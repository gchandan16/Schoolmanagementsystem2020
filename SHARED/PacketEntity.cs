using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    [DataContract]
  public  class PacketEntity
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string student_biometricid { get; set; }
        [DataMember]
        public string student_RFId { get; set; }
        [DataMember]
        public Nullable<System.DateTime> Punchdatetime { get; set; }

        public string student_cardnumber { get; set; }
        [DataMember]
        public string student_paycode { get; set; }
        [DataMember]
        public string Customercode { get; set; }
        [DataMember]
        public string Macaddress { get; set; }
        [DataMember]
        public string Ipaddress { get; set; }

    }
}
