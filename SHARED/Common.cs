using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace SHARED
{
    public class Common
    {
    }

    public class Common_UserArea
    {
        public int AREAID { get; set; }
        public string AREANAME { get; set; }
        public string ISACTIVE { get; set; }

    }
    public class MenuMaster
    {

        public List<PermissionMaster> PermissionList { get; set; }

        public string Permissions { get; set; }

        public List<string> PermissionNameList { get; set; }
        public int MENU_ID { get; set; }
        public string MENUCODE { get; set; }
        public string MENUNAME { get; set; }
        public string PAGETITLE { get; set; }
        public Nullable<int> PARENTMENUID { get; set; }
        public Nullable<int> DISPLAYORDER { get; set; }
        public string URL { get; set; }
        public string icon { get; set; }
        public string IMAGEPATH { get; set; }
        public Boolean ISACTIVE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDDATE { get; set; }
        public string ISDISPLAY { get; set; }
    }
    public class PermissionMaster
    {
        /*-------------Base Parameter of table---------------*/

        public int PERMISSION_ID { get; set; }

        public string PERMISSIONNAME { get; set; }

        public string PERMISSIONDESC { get; set; }

        public int ISACTIVE { get; set; }

        public string CREATEDBY { get; set; }

        public Nullable<System.DateTime> CREATEDON { get; set; }

        public string MODIFIEDBY { get; set; }

        public Nullable<System.DateTime> MODIFIEDON { get; set; }

        /*------------End Base Parameter of table-------------*/



        public List<PermissionMaster> PermissionList { get; set; }
    }
    public class MenuPermissionMapMaster
    {

        public int MENU_PERMISSION_ID { get; set; }

        public Nullable<int> MENU_ID { get; set; }

        public Nullable<int> PERMISSION_ID { get; set; }

        public string Menu_PermissionString { get; set; }

        public string LeafMenuString { get; set; }

    }
    public class RoleMenuPermissionMapping
    {

        public int ROLEMENUE_PERMISSION_ID { get; set; }

        public int ROLE_MENU_ID { get; set; }

        public int PERMISSION_ID { get; set; }
    }
    public class UserMasters
    {

        public List<MenuPermissionMapMaster> MenuPermissionList { get; set; }

        public int UserId { get; set; }

        public string FISRTNAME { get; set; }

        public string LASTNAME { get; set; }

        public string EMAILID { get; set; }

        public string USERNAME { get; set; }

        public string PASSWORD { get; set; }

        public string ADDRESS { get; set; }

        public Nullable<int> CITY_ID { get; set; }

        public Nullable<int> STATE_ID { get; set; }

        public Nullable<int> COUNTRY_ID { get; set; }

        public Nullable<int> ROLE_ID { get; set; }

        public bool ISACTIVE { get; set; }

        public string IMAGE { get; set; }

        public string CREATEDBY { get; set; }


        public Nullable<System.DateTime> CREATEDDATE { get; set; }

        public Nullable<System.DateTime> MODIFIEDDATE { get; set; }

        public string MODIFIEDBY { get; set; }

        public string PASSWORDRESETTOKEN { get; set; }

        public Nullable<System.DateTime> PASSWORDRESETEXPIRATION { get; set; }


        public string ISUSERLOGGED { get; set; }

        public int SchoolID { get; set; }


        public string Mobile { get; set; }


        public int Usertype { get; set; }


        public string Usertypename { get; set; }


        public int UsertypebaseId { get; set; }
        public string OTP { get; set; }

    }
    public class DashboardMaster
    {

        public Nullable<DateTime> FROMDATE { get; set; }

        public Nullable<DateTime> TODATE { get; set; }

        public int USER_ID { get; set; }

    }

    public class UserModel : BaseModel
    {
        public int User_Id { get; set; }
        public string UserCode { get; set; }
        [Required(ErrorMessage = "Please enter user code")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public int Role_Id { get; set; }
        public int UsertypebaseId { get; set; }
        public int Usertype { get; set; }
        public string ActionName { get; set; }
        public Nullable<int> Student_Id { get; set; }
        [Required(ErrorMessage = "Please enter Student")]

        public Nullable<int> Employee_Id { get; set; }
        // [Required(ErrorMessage = "Please enter Employee")]
        public List<MenuPermissionMapMaster> MenuPermissionList { get; set; }
        public List<RoleMaster> RoleList { get; set; }

        public List<UserMasters> UserList { get; set; }
        public List<EmployeeMaster> EmployeeList { get; set; }
        public List<StudentMaster> StudentList { get; set; }
        public List<UserTypeMaster> UserTypeList { get; set; }



    }
    public partial class RoleMaster
    {
        // Additional Parameter As per Requirement
        public string MenuName { get; set; }

        public string PermissionName { get; set; }

        public List<MenuPermissionMapMaster> MenuPermissionList { get; set; }


    }
    public class EmployeeMaster
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
        public string EMERGENCY_CONT_PRS { get; set; }
        public string EMERGENCY_CONT_NO { get; set; }
        public Nullable<int> REPORTING_MANAGER { get; set; }
        public string PANIMGPATH { get; set; }
        public string EMPIMGPATH { get; set; }
        public string SPOUSE { get; set; }
        public int SchoolID { get; set; }
        public int ParentID { get; set; }
        public string ModeOfPayment { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string FullName
        {
            get
            {
                return !string.IsNullOrEmpty(FIRSTNAME) ? "(" + EMPCODE + ") :" + FIRSTNAME + " " + LASTNAME : string.Empty;
            }
        }
        public bool IsPF { get; set; }
        public double PFEmployeeAmount { get; set; }
        public double PFEmployerAmount { get; set; }
        public bool IsESI { get; set; }
        public double ESIEmployeeAmount { get; set; }
        public double ESIEmployerAmount { get; set; }
        public bool IsTDS { get; set; }
        public string TaxCategory { get; set; }
        public int ShiftType { get; set; }
        public string ESIType { get; set; }
        public string PFType { get; set; }
    }
    public class DepartmentMaster
    {
        public int DEPARTMENT_ID { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string DEPARTMENTDESC { get; set; }
        public string REMARKS { get; set; }
        public bool ISACTIVE { get; set; }
        public string CREATEDBY { get; set; }
         public Nullable<System.DateTime> CREATEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public Nullable<System.DateTime> MODIFIEDDATE { get; set; }
        public int SchoolID { get; set; }

    }
    public class DesignationMaster
    {
        public int DESIGNATION_ID { get; set; }
        public string DESIGNATIONNAME { get; set; }
        public string DESIGNATIONDESC { get; set; }
        public string REMARKS { get; set; }
        public bool ISACTIVE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public Nullable<System.DateTime> MODIFIEDDATE { get; set; }
        public int SchoolID { get; set; }

    }
    public class PinMaster
    {
    }
    public class BaseModel
    {
        public List<string> PermissionNameList { get; set; }

        public UserInfo BaseUserInfo { get; set; }
        public string usermenu { get; set; }
        public SessionInfo BaseSessionInfo { get; set; }
        public OragnisationMaster OragnisationMaster {get;set;} //New Attributes Added 
    }
    public class SessionInfo
    {
        public string FinancialYear { get; set; }
        public string SchoolName { get; set; }
        public string SchoolLogo { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public int UserType { get; set; }
        public int UserTypeBaseID { get; set; }
    }
    public class Common_UserAreaModel
    {
        public List<Common_UserArea> C_CountryList { get; set; }
        public List<Common_UserArea> C_StateList { get; set; }
        public List<Common_UserArea> C_DistrictList { get; set; }
        public List<Common_UserArea> C_ZoneList { get; set; }
        public List<Common_UserArea> C_TerriotryList { get; set; }
        public List<Common_UserArea> C_CityList { get; set; }
        public List<Common_UserArea> C_PartyList { get; set; }
        public List<Common_UserArea> C_PartyGroupList { get; set; }

        public List<int> C_SelectedIds { get; set; }

        public string C_SelectedAreaType { get; set; }
        //public List<ListItem> C_AreaTypeList { get; set; }


        public List<Common_UserArea> C_SegmentList { get; set; }
        public List<Common_UserArea> C_ProductCategoryList { get; set; }
        public List<Common_UserArea> C_SubCategoryList { get; set; }
        public List<Common_UserArea> C_PartFamilyList { get; set; }
        public List<Common_UserArea> C_PartList { get; set; }
        public List<Common_UserArea> C_ExistingItemGroupList { get; set; }

        public string C_SelectedItemType { get; set; }
        public List<ListItem> C_ItemGroupTypeList { get; set; }

    }
    public class DashboardModel
    {
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public StringBuilder ComonDashboardData { get; set; }

    }
    public class UserInfo
    {
        public string CityName { get; set; }

        public string StateName { get; set; }

        public string COUNTRYNAME { get; set; }


        public string DesgnationName { get; set; }

        public string CurrentDate { get; set; }

        public string UserType_Name { get; set; }

        public int User_ID { get; set; }
        public string BIRTHDATE { get; set; }
        public string MOBILENO { get; set; }
        public string EMAIL { get; set; }
        public string USERCODE { get; set; }
        public string USERNAME { get; set; }
        public string EMPIMGPATH { get; set; }
        public string FULLNAME { get; set; }
    }
    public partial class ReligionMaster
    {
        public int Relmid { get; set; }
        public String ReligionName { get; set; }
        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
    public partial class CategoryMaster
    {
        public int Catmid { get; set; }
        public String CatName { get; set; }
        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }

    public partial class CastMaster
    {

        public int Castmid { get; set; }
        public String CastName { get; set; }
        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
    public partial class SectionMaster
    {
        public int Secmid { get; set; }
        public int CMid { get; set; }
        public String SectionName { get; set; }
        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }

    public partial class RouteMaster
    {

        public int Routemid { get; set; }

        public String RouteName { get; set; }


        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
    public partial class BusMaster
    {

        public int Busmid { get; set; }

        public String BusName { get; set; }


        public int SchoolId { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
    }
    public partial class ClassSectionMaster
    {
        public bool Check { get; set; }
        public int SectionID { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
    public class Constants
    {
        #region SystemRole
        public static string SUPERADMIN = "SUPERADMIN";
        #endregion
        #region BreadCrumbURL
        public const string DEMOBREADCRUMB = "";
        #endregion
        #region Status
        public const string ALLSTATUS = "";
        public const int NSTATUS = 0;
        public const int YSTATUS = 1;
        #endregion
        public static string STUDENT_IMAGE = "/Content/Student/";
        public static string Employee_IMAGEPAN = "/Content/Employee/";
        public static string Lecture_IMAGEPAN = "/Content/Lectureattachment/";
        public static string Alarm_Files = "/Content/Alarmattachment/";
        public class Session
        {
            public const string USER = "CurrentUser";
            public const string USERINFO = "CurrentUserInfo";
            public const string MENU = "CurrentMenu";
            public const string MENUPERMISSION = "MenuPermission";
            public const string ALLMENUPERMISSION = "AllMenuPermission";
        }
        public class MessageInfo
        {
            public const string SUCCESS = "success";
            public const string ERROR = "error";
            public const string INFORMATION = "info";
            public const string WARNING = "warning";
            public const string BLANK = "Blank";
        }

        #region ListStatus
        public static string ACTIVE = "Y";
        public static string INACTIVE = "N";

        #endregion
        #region UserLogin
        public static string USER_NOT_VALID = "Incorrect username or password.";
        public static string AREA_NOT_ASSIGNED = "Please assign area before login.";
        #endregion

        #region RoleManagement
        public static string ROLE_ADD_SUCCESS = "Role added successfully";
        public static string ROLE_UPDATE_SUCCESS = "Role updated successfully";
        public static string ROLE_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string ROLE_DELETE_SUCCESS = "Role deleted successfully";
        public static string ROLE_DELETE_FAIL = "Unable to delete Role";
        #endregion

        #region Orgnisation
        public static string EMPATTACHMENT = "/Content/Orgnisationatt/";
        public static string USERPICK = "/Content/Userpicture/";
        public static string Orgnisation_ADD_SUCCESS = "Registered successfully";
        #endregion

        #region ButtonName

        public static string BTN_CREATE = "create";
        public static string BTN_EDIT = "edit";
        public static string BTN_DISPLAY = "display";
        #endregion
        public class PermissionName
        {
            public const string CREATE = "Create";
            public const string EDIT = "Edit";
            public const string INACTIVE = "InActive";
            // public const string DETAIL = "Detail";
            public const string VIEW = "View";
            public const string ApproveValue = "Approve Value";

        }
        #region urlhash

        public const string HASH_ADMIN = "#admin";
        public const string HASH_MASTER = "#master";
        public const string HASH_REPORT = "#report";

        #endregion

        #region Create Management System
        public static string User_ADD_SUCCESS = "User added successfully";
        public static string User_AlreadyExist_SUCCESS = "User Already Exist";
        public static string User_UPDATE_SUCCESS = "User updated successfully";
        public static string User_UPDATE_FAILED = "Could not updated.";
        public static string User_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string User_DELETE_SUCCESS = "User deleted successfully";
        public static string User_DELETE_FAIL = "Unable to delete User";
        #endregion

        #region Create AddEnquiry System
        public static string Enquiry_Add_SUCCESS = "Enquiry added successfully";
        public static string Enquiry_AlreadyExist_SUCCESS = "Enquiry Already Exist";
        public static string Enquiry_UPDATE_SUCCESS = "Enquiry updated successfully";
        public static string Enquiry_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Enquiry_DELETE_SUCCESS = "Enquiry deleted successfully";
        public static string Enquiry_DELETE_FAIL = "Unable to delete Enquiry";
        #endregion

        #region Create AddStudent System
        public static string Student_Add_SUCCESS = "Student added successfully";
        public static string Student_AlreadyExist_SUCCESS = "Student Already Exist";
        public static string Student_UPDATE_SUCCESS = "Student updated successfully";
        public static string Student_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Student_DELETE_SUCCESS = "Student deleted successfully";
        public static string Student_DELETE_FAIL = "Unable to delete student";
        #endregion
        #region Create AddEmployee System
        public static string Employee_Add_SUCCESS = "Employee added successfully";
        public static string Employee_AlreadyExist_SUCCESS = "Employee Already Exist";
        public static string Employee_UPDATE_SUCCESS = "Employee updated successfully";
        public static string Employee_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Employee_DELETE_SUCCESS = "Employee deleted successfully";
        public static string Employee_DELETE_FAIL = "Unable to delete employee";
        #endregion

        #region Create AddClass System
        public static string Class_Add_SUCCESS = "Class added successfully";
        public static string Class_AlreadyExist_SUCCESS = "Class Already Exists.";
        public static string Class_UPDATE_SUCCESS = "Class updated successfully";
        public static string Class_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Class_DELETE_SUCCESS = "Class deleted successfully";
        public static string Class_DELETE_FAIL = "Unable to delete class";
        #endregion
        #region Create AddSection System
        public static string Section_Add_SUCCESS = "Section added successfully";
        public static string Section_AlreadyExist_SUCCESS = "Section Already Exists.";
        public static string Section_UPDATE_SUCCESS = "Section updated successfully";
        public static string Section_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Section_DELETE_SUCCESS = "Section deleted successfully";
        public static string Section_DELETE_FAIL = "Unable to delete section";
        #endregion
        #region Create AddRoute System
        public static string Route_Add_SUCCESS = "Route added successfully";
        public static string Route_AlreadyExist_SUCCESS = "Route Already Exist";
        public static string Route_UPDATE_SUCCESS = "Route updated successfully";
        public static string Route_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Route_DELETE_SUCCESS = "Route deleted successfully";
        public static string Route_DELETE_FAIL = "Unable to delete route";
        #endregion
        #region Create AddBus System
        public static string Bus_Add_SUCCESS = "Bus added successfully";
        public static string Bus_AlreadyExist_SUCCESS = "Bus Already Exist";
        public static string Bus_UPDATE_SUCCESS = "Bus updated successfully";
        public static string Bus_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Bus_DELETE_SUCCESS = "Bus deleted successfully";
        public static string Bus_DELETE_FAIL = "Unable to delete bus";
        #endregion
        #region Create AddCast System
        public static string Cast_Add_SUCCESS = "Cast added successfully";
        public static string Cast_AlreadyExist_SUCCESS = "Cast Already Exist";
        public static string Cast_UPDATE_SUCCESS = "Cast updated successfully";
        public static string Cast_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Cast_DELETE_SUCCESS = "Cast deleted successfully";
        public static string Cast_DELETE_FAIL = "Unable to delete cast";
        #endregion
        #region Create Category System
        public static string Category_Add_SUCCESS = "Category added successfully";
        public static string Category_AlreadyExist_SUCCESS = "Category Already Exist";
        public static string Category_UPDATE_SUCCESS = "Category updated successfully";
        public static string Category_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Category_DELETE_SUCCESS = "Category deleted successfully";
        public static string Category_DELETE_FAIL = "Unable to delete category";
        #endregion
        #region Blood Group 
        public static string BloodGroup_Add_SUCCESS = "BloodGroup added successfully";
        public static string BloodGroup_AlreadyExist_SUCCESS = "BloodGroup Already Exist";
        public static string BloodGroup_UPDATE_SUCCESS = "BloodGroup updated successfully";
        public static string BloodGroup_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string BloodGroup_DELETE_SUCCESS = "BloodGroup deleted successfully";
        public static string BloodGroup_DELETE_FAIL = "Unable to delete bloodGroup";
        #endregion
        #region Contact Title
        public static string ContactTitle_Add_SUCCESS = "Contact Title added successfully";
        public static string ContactTitle_AlreadyExist_SUCCESS = "Contact Title Already Exist";
        public static string ContactTitle_UPDATE_SUCCESS = "Contact Title updated successfully";
        public static string ContactTitle_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string ContactTitle_DELETE_SUCCESS = "Contact Title deleted successfully";
        public static string ContactTitle_DELETE_FAIL = "Unable to delete contact Title";
        #endregion
        #region Create MartialStatus
        public static string MartialStatus_Add_SUCCESS = "MartialStatus added successfully";
        public static string MartialStatus_AlreadyExist_SUCCESS = "MartialStatus Already Exist";
        public static string MartialStatus_UPDATE_SUCCESS = "MartialStatus updated successfully";
        public static string MartialStatus_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string MartialStatus_DELETE_SUCCESS = "MartialStatus deleted successfully";
        public static string MartialStatus_DELETE_FAIL = "Unable to delete MartialStatus";
        #endregion

        #region Create Gendar System
        public static string Gendar_Add_SUCCESS = "Gendar added successfully";
        public static string Gendar_AlreadyExist_SUCCESS = "Gendar Already Exist";
        public static string Gendar_UPDATE_SUCCESS = "Gendar updated successfully";
        public static string Gendar_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Gendar_DELETE_SUCCESS = "Gendar deleted successfully";
        public static string Gendar_DELETE_FAIL = "Unable to delete gendar";
        #endregion
        #region Create Religion System
        public static string Religion_Add_SUCCESS = "Religion added successfully";
        public static string Religion_AlreadyExist_SUCCESS = "Religion Already Exist";
        public static string Religion_UPDATE_SUCCESS = "Religion updated successfully";
        public static string Religion_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Religion_DELETE_SUCCESS = "Religion deleted successfully";
        public static string Religion_DELETE_FAIL = "Unable to delete religion";
        #endregion
        #region TDSSetting
        public static string TDSSetting_Add_SUCCESS = "TDS Setting added successfully";
        public static string TDSSetting_AlreadyExist_SUCCESS = "TDS Setting Already Exist";
        public static string TDSSetting_UPDATE_SUCCESS = "TDS Setting updated successfully";
        public static string TDSSetting_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string TDSSetting_DELETE_SUCCESS = "TDS Setting deleted successfully";
        public static string TDSSetting_DELETE_FAIL = "Unable to delete TDS Setting";
        #endregion
        #region Create Department System
        public static string Department_Add_SUCCESS = "Department added successfully";
        public static string Department_AlreadyExist_SUCCESS = "Department Already Exist";
        public static string Department_UPDATE_SUCCESS = "Department updated successfully";
        public static string Department_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Department_DELETE_SUCCESS = "Department deleted successfully";
        public static string Department_DELETE_FAIL = "Unable to delete department";
        #endregion

        #region Create Designation System
        public static string Designation_Add_SUCCESS = "Designation added successfully";
        public static string Designation_AlreadyExist_SUCCESS = "Designation Already Exist";
        public static string Designation_UPDATE_SUCCESS = "Designation updated successfully";
        public static string Designation_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Designation_DELETE_SUCCESS = "Designation deleted successfully";
        public static string Designation_DELETE_FAIL = "Unable to delete designation";
        #endregion
        #region Attendance System
        public static string Attendance_Add_SUCCESS = "Attendance Uploaded successfully";
        public static string Attendance_AlreadyExist_SUCCESS = "Designation Already Exist";
        public static string Attendance_UPDATE_SUCCESS = "Designation updated successfully";
        public static string Attendance_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Attendance_DELETE_SUCCESS = "Designation deleted successfully";
        public static string Attendance_DELETE_FAIL = "Unable to delete attendance";
        #endregion
        #region Create AddSubject System
        public static string Subject_Add_SUCCESS = "Subject added successfully";
        public static string Subject_AlreadyExist_SUCCESS = "Subject Already Exists.";
        public static string Subject_UPDATE_SUCCESS = "Subject updated successfully";
        public static string Subject_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Subject_DELETE_SUCCESS = "Subject deleted successfully";
        public static string Subject_DELETE_FAIL = "Unable to delete Subject";
        #endregion
        #region Create AddSubjectChapter System
        public static string SubjectChapter_Add_SUCCESS = "Chapter added successfully";
        public static string SubjectChapter_AlreadyExist_SUCCESS = "Chapter Already Exists.";
        public static string SubjectChapter_UPDATE_SUCCESS = "Chapter updated successfully";
        public static string SubjectChapter_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string SubjectChapter_DELETE_SUCCESS = "Chapter deleted successfully";
        public static string SubjectChapter_DELETE_FAIL = "Unable to delete Chapter";
        #endregion

        #region Create Period System
        public static string Period_Add_SUCCESS = "Period added successfully";
        public static string Period_AlreadyExist_SUCCESS = "Period Already Exists.";
        public static string Period_UPDATE_SUCCESS = "Period updated successfully";
        public static string Period_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Period_DELETE_SUCCESS = "Period deleted successfully";
        public static string Period_DELETE_FAIL = "Unable to delete Period";
        #endregion
        #region Shift Master
        public static string Shift_Add_SUCCESS = "Shift added successfully";
        public static string Shift_AlreadyExist_SUCCESS = "Shift Already Exists.";
        public static string Shift_UPDATE_SUCCESS = "Shift updated successfully";
        public static string Shift_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Shift_DELETE_SUCCESS = "Shift deleted successfully";
        public static string Shift_DELETE_FAIL = "Unable to delete Shift";
        #endregion

        #region Assign Period Teacher
        public static string AssignPeriod_Add_SUCCESS = "Assign Period added successfully";
        public static string AssignPeriod_AlreadyExist_SUCCESS = "Assign Period Already Exists.";
        public static string AssignPeriod_UPDATE_SUCCESS = "Assign Period updated successfully";
        public static string AssignPeriod_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string AssignPeriod_DELETE_SUCCESS = "Assign Period deleted successfully";
        public static string AssignPeriod_DELETE_FAIL = "Unable to delete Assign Period";
        #endregion

        #region Assign Lecture Content
        public static string Assignleccont_Add_SUCCESS = "Assign Lecture added successfully";
        public static string Assignleccont_AlreadyExist_SUCCESS = "Assign Lecture Already Exists.";
        public static string Assignleccont_UPDATE_SUCCESS = "Assign Lecture updated successfully";
        public static string Assignleccont_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Assignleccont_DELETE_SUCCESS = "Assign Lecture deleted successfully";
        public static string Assignleccont_DELETE_FAIL = "Unable to delete Assign Lecture";
        #endregion
        #region Bell Opration System
        public static string Bellsystem_Add_SUCCESS = "Bell added successfully";
        public static string Bellsystem_AlreadyExist_SUCCESS = "Bell Already Exists.";
        public static string Bellsystem_UPDATE_SUCCESS = "Bell updated successfully";
        public static string Bellsystem_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Bellsystem_DELETE_SUCCESS = "Bell deleted successfully";
        public static string Bellsystem_DELETE_FAIL = "Unable to delete Bell";
        #endregion
        #region Student Leave Apply
        public static string Studentleave_Add_SUCCESS = "Student Leave Apply  successfully";
        public static string Studentleave_AlreadyExist_SUCCESS = "Student Leave Already Exists.";
        public static string Studentleave_UPDATE_SUCCESS = "Student Leave updated successfully";
        public static string Studentleave_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Studentleave_DELETE_SUCCESS = "Student Leave deleted successfully";
        public static string Studentleave_DELETE_FAIL = "Unable to delete Student Leave";
        #endregion
        #region Student Leave Apply
        public static string Salaryhead_Add_SUCCESS = "Salary Head Added  successfully";
        public static string Salaryhead_AlreadyExist_SUCCESS = "Salary Head Already Exists.";
        public static string Salaryhead_UPDATE_SUCCESS = "Salary Head updated successfully";
        public static string Salaryhead_ADD_FAIL = "Unable to Process, Please try after sometime";
        public static string Salaryhead_DELETE_SUCCESS = "Salary Head deleted successfully";
        public static string Salaryhead_DELETE_FAIL = "Unable to delete Salary Head";
        #endregion
        #region Salary Head Assign
        public static string Salaryheadassign_Add_SUCCESS = "Salary Head  Added  successfully";
        public static string Salaryheadassign_AlreadyExist_SUCCESS = "Salary Head Already Exists.";
        public static string Salaryheadassign_UPDATE_SUCCESS = "Salary Head updated successfully";
        public static string Salaryheadassign_ADD_FAIL = "Unable to Process, Please try after sometime";
        public static string Salaryheadassign_DELETE_SUCCESS = "Salary Head deleted successfully";
        public static string Salaryheadassign_DELETE_FAIL = "Unable to delete Salary Head";
        #endregion

        #region Question Add With Option
        public static string QuestionAddwoption_Add_SUCCESS = "Question  Added  successfully";
        public static string QuestionAddwoption_AlreadyExist_SUCCESS = "Question Already Exists.";
        public static string QuestionAddwoption_UPDATE_SUCCESS = "Question updated successfully";
        public static string QuestionAddwoption_ADD_FAIL = "Unable to Process, Please try after sometime";
        public static string QuestionAddwoption_DELETE_SUCCESS = "Question deleted successfully";
        public static string QuestionAddwoption_DELETE_FAIL = "Unable to delete Question";
        #endregion
        public static string Quiz_Add_SUCCESS = "Quiz Exam added successfully";
        public static string Quiz_AlreadyExist_SUCCESS = "Quiz Exam Already Exist";
        public static string Quiz_UPDATE_SUCCESS = "Quiz Exam updated successfully";
        public static string Quiz_ADD_FAIL = "Unable to update details, Please try after sometime";
        public static string Quiz_DELETE_SUCCESS = "Quiz Exam deleted successfully";
        public static string Quiz_DELETE_FAIL = "Unable to delete religion";
    }

    #region Holiday Master
    public class HolidayMaster : BaseModel
    {
        public int Hid { get; set; }
        public string HolidayDate { get; set; }
        public bool Student { get; set; }
        public bool Staff { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string Status { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYear { get; set; }
        public string ActionName { get; set; }
        public List<HolidayMaster> HolidayMasterList { get; set; }
    }
    #endregion Holiday Master

    #region Leave
    public class Leave : BaseModel
    {
        public int Lid { get; set; }
        public int SenderID { get; set; }
        public string SenderName { get; set; }
        public string SenderType { get; set; }
        public int ApproverID { get; set; }
        public string ApproverName { get; set; }
        public string ApproverType { get; set; }
        public string LeaveType { get; set; }
        public List<selectList> LeaveTypeList { get; set; }
        public string LeaveFrom { get; set; }
        public string LeaveTo { get; set; }
        public int TotalLeaves { get; set; }
        public bool HalfDay { get; set; }
        public string LeaveStatus { get; set; }
        public string SenderRemark { get; set; }
        public string ApproverRemark { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYear { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Action { get; set; }
        public List<Leave> LeaveList { get; set; }
    }
    #endregion Leave
    public class MailDetails
    {
        public string SenderMailID { get; set; }
        public string ToMailIDs { get; set; }
        public string CCMailIDs { get; set; }
        public string BCCMailIDs { get; set; }
        public string SmtpClient { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string AttachmentFile { get; set; }
        public bool SSL { get; set; }
        public int Port { get; set; }
        public bool HTMLBody { get; set; }
    }
    public class FinancialYear
    {
        public int FID { get; set; }
        public string FYear { get; set; }
        public bool SelectedYear { get; set; }
    }
    public class DashboardDetails
    {
        public int TotalStudents { get; set; }
        public int TotalPresentStudents { get; set; }
        public int TotalAbsentStudents { get; set; }
        public int TotalLeaveStudents { get; set; }

        public int TotalStaff { get; set; }
        public int TotalPresentStaff { get; set; }
        public int TotalAbsentStaff { get; set; }
        public int TotalLeaveStaff { get; set; }

        public int NewAdmission { get; set; }

        public double TodayFeeCollection { get; set; }
        public double TotalFeePending { get; set; }

        public List<StudentDashboard> StudentList { get; set; }
        public List<ChartFields> ChartData { get; set; }
    }
    public class StudentDashboard
    {
        public string StudentName { get; set; }
        public string AdmissionDate { get; set; }
        public string StudentImage { get; set; }
        public int StudentID { get; set; }
    }
    public class ChartFields
    {
        public string Month { get; set; }
        public string MonthFee { get; set; }
        public string Year { get; set; }
    }
    public class BulkStudentImport : BaseModel
    {

    }

}
