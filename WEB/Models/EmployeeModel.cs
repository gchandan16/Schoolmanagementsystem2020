using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WEB.Models
{
    public class EmployeeModel : BaseModel
    {
        public int EMP_ID { get; set; }
        public string EMPCODE { get; set; }
        public bool ISACTIVE { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int DESIGNATION_ID { get; set; }
        public int CONTACTTITLE { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string LASTNAME { get; set; }
        public string MOBILENUMBER { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string EMAIL { get; set; }
        public Nullable<System.DateTime> BIRTHDATE { get; set; }
        public string GENDER { get; set; }
        public string BLOODGROUP { get; set; }
        public string MARITALSTATUS { get; set; }
        public string QUALIFICATION1 { get; set; }
        public string QUALIFICATION2 { get; set; }
        public Nullable<System.DateTime> JOININGDATE { get; set; }
        public Nullable<System.DateTime> CONFIRMATIONDATE { get; set; }
        public Nullable<System.DateTime> LEAVINGDATE { get; set; }
        public decimal SALARY { get; set; }
        public string BANKACNO { get; set; }
        public string BANKNAME { get; set; }
        public string BANKBRANCH { get; set; }
        public string PFNO { get; set; }
        public string PANNO { get; set; }
        public string REMARKS { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public Nullable<System.DateTime> MODIFIEDDATE { get; set; }
        public string FATHER_SPOUSE { get; set; }
        public string PF_ESTD_CODE { get; set; }
        public string PF_UAN { get; set; }
        public Nullable<decimal> VPF_CONTB_RATE { get; set; }
        public string IFSC_CODE { get; set; }
        public string LEAVE_PL_ENTITLED { get; set; }
        public string LEAVE_CL_ENTITLED { get; set; }
        public string LEAVE_SL_ENTITLED { get; set; }
        public Nullable<decimal> GROSS_BASIC { get; set; }
        public Nullable<decimal> GROSS_HRA { get; set; }
        public Nullable<decimal> GROSS_CONVEYANCE { get; set; }
        public Nullable<decimal> GROSS_CHILDREN_EDUCATION { get; set; }
        public Nullable<decimal> GROSS_UNIFORM_MAINTENANCE { get; set; }
        public Nullable<decimal> GROSS_CAR_AMOUNT { get; set; }
        public Nullable<decimal> GROSS_SPECIAL_ALLOWANCE { get; set; }
        public Nullable<decimal> GROSS_SALARY { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid mobile number")]
        public string EMERGENCY_CONT_PRS { get; set; }
        public string EMERGENCY_CONT_NO { get; set; }
        public Nullable<int> SelectedRManagerId { get; set; }
        public string PANIMGPATH { get; set; }
        public string EMPIMGPATH { get; set; }
        public string SPOUSE { get; set; }
        public int SchoolID { get; set; }
        // public int ParentID { get; set; }
        public string DISPLAYEMPNAME { get; set; }
        public string FullName
        {
            get
            {
                return !string.IsNullOrEmpty(FIRSTNAME) ? "(" + EMPCODE + ") :" + FIRSTNAME + " " + LASTNAME : string.Empty;
            }
        }
        public string ActionName { get; set; }
        public DataSet EmployeeTBLList { get; set; }
        public List<ContactTitleMaster> ContactList { get; set; }
        public List<GendraMaster> GenderList { get; set; }
        public List<MartialStatusMaster> MaritalStatusList { get; set; }
        public List<BloodGroupMaster> BloodGroupList { get; set; }
        public List<DepartmentMaster> DepartmentList { get; set; }
        public List<DesignationMaster> DesignationList { get; set; }
        public List<EmployeeMaster> ReportingManagerList { get; set; }
    }
}