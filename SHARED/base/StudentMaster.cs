using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial   class StudentMaster
{
        
        public bool Check { get; set; }
        
        public int Smid { get; set; }
        
        public string Adminssionno { get; set; }
        
        public string Enquiryno { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public int CMid { get; set; }
        
        public int SecMid { get; set; }
        
        public string RollNo { get; set; }
        
        public int RouteMid { get; set; }
        
        public int BusMid { get; set; }
        
        public int Castmid { get; set; }
        
        public int Categmid { get; set; }
        
        public int HouseMid { get; set; }
        
        public int Relmid { get; set; }
        
        public int GMid { get; set; }
        #region Basic Details

        

        
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
        #endregion
        #region Officail Details

        
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


        #endregion

        
        public string studentimage { get; set; }
        
        public string motherimage { get; set; }
        
        public string fatherimage { get; set; }
        
        public bool IsActive { get; set; }
        
        public int SchoolID { get; set; }
        
        public int CITY_ID { get; set; }
        
        public int STATE_ID { get; set; }
        
        public int COUNTRY_ID { get; set; }
         
        public Nullable<DateTime> Createdate { get; set; }
        
        public string Createdby { get; set; }
        
        public string Modifiedby { get; set; }
        
        public Nullable<DateTime> Modifieddate { get; set; }

        public string FeeGroup { get; set; }
        public int FinancialYear { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string CatName { get; set; }
        public string ReligionName { get; set; }
        public string GName { get; set; }
        public string DistrictName { get; set; }
        public string CITYNAME { get; set; }
        public string STATENAME { get; set; }
        public string COUNTRYNAME { get; set; }
        public string CastName { get; set; }
        public string FinancialYearName { get; set; }
        public string Oname { get; set; }
        public string PickDropPointName { get; set; }
        public string RouteName { get; set; }
        public string BloodGroupName { get; set; }
        public string VehicleNumber { get; set; }
        public int TPCostID { get; set; }
        public bool TPCost { get; set; }

        public int FeeConID { get; set; }
        public string TPFee { get; set; }
        public string StudentStatus { get; set; }

        public string PaidMonths { get; set; }
        public string UnPaidMonth { get; set; }
        public string PendingAmount { get; set; }
    }
}
