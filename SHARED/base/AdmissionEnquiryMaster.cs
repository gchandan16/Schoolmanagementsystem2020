using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
public partial  class AdmissionEnquiryMaster
{
        
        public int AMID { get; set; }
        
        public string AEnquiryNo { get; set; }
        
        public string AClassName { get; set; }
        
        public string AStudentName { get; set; }
        
        public string AGendar { get; set; }
        
        public string ACont_Address { get; set; }
        
        public string ACont_Address2 { get; set; }
        
        public string ACity { get; set; }
        
        public string AContactno { get; set; }
        
        public string ADOB { get; set; }
        
        public string APdfather { get; set; }
        
        public string APdFathermobile { get; set; }
        
        public string APQualification { get; set; }
        
        public string APFatheroccuption { get; set; }
        
        public string APFatherdob { get; set; }
        
        public string APMother { get; set; }
        
        public string Apmothermobile { get; set; }
        
        public string ApMotherqualifation { get; set; }
        
        public string Apmotheroccuption { get; set; }
        
        public string Apmotherdob { get; set; }
        
        public string Apdoanniversary { get; set; }
        
        public string Apemailid { get; set; }
        
        public string Fonameofschool { get; set; }
        
        public string Lastexamgiven { get; set; }
        
        public string FoYear { get; set; }
        
        public string Fostatus { get; set; }
        
        public string Marks { get; set; }
        
        public string BoardUniversity { get; set; }
        
        public string DateofEnquiry { get; set; }
        
        public string ProspectusNo { get; set; }
        
        public string Admissionformno { get; set; }
        
        public double Prospectusfees { get; set; }
        
        public double Registrationfees { get; set; }
        
        public string Remarks { get; set; }
        
        public Nullable<DateTime> Createddate { get; set; }
        
        public string CreatedBy { get; set; }
        
        public Nullable<DateTime> Modifieddate { get; set; }
        
        public string Modifiedby { get; set; }
        
        public int FMid { get; set; }
        
        public int OMID { get; set; }

        
        public bool IsActive { get; set; }

    }
}
