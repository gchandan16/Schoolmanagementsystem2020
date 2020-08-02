using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{

    public partial class OragnisationMaster
    {
        
        public int OMID { get; set; }
        
        public string Oname { get; set; }
        
        public string BOAddress { get; set; }
        
        public string BOAddress2 { get; set; }
        
        public int  BOCity { get; set; }
        
        public string BOPincode { get; set; }
        
        public int LCountry { get; set; }
        
        public int LState { get; set; }
        
        public string LDistict { get; set; }
        
        public string LArea { get; set; }
        
        public string LEmailId { get; set; }
        
        public string LMobile { get; set; }
        
        public string LPhone { get; set; }
        
        public string LWebsite { get; set; }
        
        public string OAfficilate { get; set; }
        
        public string OlicNo { get; set; }
        
        public string OTaxNo { get; set; }
        
        public string OPanNo { get; set; }
        
        public string OContactNo { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime Createddate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime Modifieddate { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public string OrgImage { get; set; }



    }
}
