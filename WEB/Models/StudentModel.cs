using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class StudentModel: BaseModel
    {
        public int Smid { get; set; }
        public string Adminssionno { get; set; }
        public string Enquiryno { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public int CMid { get; set; }
        
        public int SecMid { get; set; }
        
        public string RollNo { get; set; }

        public int RouteMid { get; set; } = 0;

        public int BusMid { get; set; } = 0;
        
        public int Castmid { get; set; }
        
        public int Categmid { get; set; }
        
        public int HouseMid { get; set; }

        public int Relmid { get; set; }
        public int GMid { get; set; }
        
        public string Bd_address1 { get; set; }
        
        public string Bd_address2 { get; set; }
        
        public string Bd_City { get; set; }
        
        public string Bd_contactno { get; set; }
        
        public string Bd_dob { get; set; }
        
        public string Bd_fathername { get; set; }
        
        public string Bd_fathermob { get; set; }
        
        public string Bd_qualification { get; set; }
        
        public string Bd_fatheroccuption { get; set; }
        
        public string Bd_fathdob { get; set; }
        
        public string Bd_mothername { get; set; }
        
        public string Bd_mothermob { get; set; }
        
        public string Bd_motherqualification { get; set; }
        
        public string Bd_Motheroccuption { get; set; }
        
        public string Bd_Mothredob { get; set; }
        
        public string Bd_dateofannversy { get; set; }
        
        public string Bd_Emailid { get; set; }
        
        public string Off_lastschool { get; set; }
        
        public string Off_remarks { get; set; }
        
        public string Off_Examgiven { get; set; }
        
        public string Off_Year { get; set; }
        
        public string Off_Status { get; set; }
        
        public string Off_marks { get; set; }
        
        public string Off_boardoruniversity { get; set; }
        
        public string Off_bloodgroup { get; set; }
        
        public string VisionLeft { get; set; }
        
        public string Off_grno { get; set; }
        
        public string Off_Visionright { get; set; }
        
        public string Off_heightweight { get; set; }
                
        public string Off_Dentailhygine { get; set; }
        
        public string Off_Hosalroomno { get; set; }
        
        public string Off_bedno { get; set; }
        
        public string Off_Scholarshipno { get; set; }
        
        public string Off_TC { get; set; }
        
        public string Off_CC { get; set; }
        
        public string Off_ReportC { get; set; }
        
        public string Off_Dobcertificate { get; set; }
        
        public string Off_admissionno { get; set; }
        
        public string Off_dateofadmission { get; set; }
        
        public string Off_Ledgerbalance { get; set; }
        
        public string Off_feesbalance { get; set; }
        
        public string Off_Comments { get; set; }
        
        public string Off_Aadharno { get; set; }
        
        public string Off_biometricno { get; set; }
        public string Off_nationality { get; set; }

        public string Off_childuniqueno { get; set; }
        
        public string Off_sessionno { get; set; }
        
        public string Off_family { get; set; }
        
        public string Off_stausinschool { get; set; }
        
        public string Off_discontinuethedate { get; set; }
        
        public string studentimage { get; set; }
        
        public string motherimage { get; set; }
        
        public string fatherimage { get; set; }

        public string FeeGroup { get; set; }

        public string ActionName { get; set; }
        public int CITY_ID { get; set; }
        public int STATE_ID { get; set; }
        public int COUNTRY_ID { get; set; }

        //for Search the data
        public Nullable<DateTime> Startdate { get; set; }
        public Nullable<DateTime> Enddate { get; set; }
        public List<MenuPermissionMapMaster> MenuPermissionList { get; set; }
        //Listing parameter
        #region Studentlistingpart
        public List<StudentMaster> StudentList { get; set; }
        public DataSet StudenttblList { get; set; }
        public DataSet StudentstrengthtblList { get; set; }
        
        public List<ClassMaster> ClassList { get; set; }
        public List<SectionMaster> SectionList { get; set; }
        public List<GendraMaster> GendraList { get; set; }
        public List<ReligionMaster> ReligionList { get; set; }
        public List<CastMaster> CastList { get; set; }
        public List<CategoryMaster> Categorylist { get; set; }
        public List<RouteMaster> Routelist { get; set; }
        public List<BusMaster> Buslist { get; set; }
        public List<CountryMaster> Countrylist { get; set; }
        public List<StateMaster> Statelist { get; set; }
        public List<CityMaster> Citylist { get; set; }
        public List<Feegroupstudentfrm> Feegrouplist { get; set; }
        public List<AdmissionEnquiryMaster> GetAddmissionEnqlist { get;set;}
        public List<BloodGroupMaster> BloodGroupList { get; set; }
        #endregion

        public OragnisationMaster OrganisationDetail { get; set; }

    }
}