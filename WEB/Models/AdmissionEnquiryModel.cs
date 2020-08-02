using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class AdmissionEnquiryModel: BaseModel
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

        public string APFatherdob { get; set; }
        public string APFatheroccuption { get; set; }
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
        public string ActionName { get; set; }
        public List<ClassMaster> GetClassList { get; set; }
        public List<GendraMaster> GetGendarList { get; set; }
        public List<AdmissionEnquiryMaster> GetAddmissionEnqlist { get; set; }
        public DataSet GetAddmissionEnqTbllist { get; set; }
        // new column added
        public Nullable<DateTime> Startdate { get; set; }
        public Nullable<DateTime> Enddate { get; set; }

    }
}