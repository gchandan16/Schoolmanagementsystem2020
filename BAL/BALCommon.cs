using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED;
using DAL;
using System.Data;

namespace BAL
{
    public class BALCommon
    {
        string ConStr = "";
        public BALCommon(string ConnectionString)
        {
            ConStr = ConnectionString;
        }

        public DataSet GetCommonDashboardData(DashboardMaster obj)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCommonDashboardData(obj);
        }

        public List<MenuMaster> GetAllMenuByStatus(int status, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAllMenuByStatus(status, SchoolID);
        }

        public List<MenuMaster> GetMenuListByUserId(int userId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetMenuListByUserId(userId);
        }

        public List<MenuMaster> GetAllMenuListByUserId(int userId, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAllMenuListByUserId(userId, SchoolID);
        }

        public List<PermissionMaster> GetPermissionByMenuid(int menuId, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetPermissionByMenuid(menuId, SchoolID);
        }

        public List<UserMasters> GetAllUser(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAllUser(SchoolID);
        }

        public void AddUser(UserMasters UserMasters)
        {
            DALCommon dal = new DALCommon(ConStr);
            dal.AddUser(UserMasters);
        }

        public int UpdateUser(UserMasters UserMasters)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.UpdateUser(UserMasters);
        }

        public bool IsUserNameAllow(string UserName, int userId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.IsUserNameAllow(UserName, userId);
        }

        public UserMasters GetByUserId(int userId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetByUserId(userId);
        }


        public UserMasters ValidateUser(string UserName, string Password)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.ValidateUser(UserName, Password);
        }

        public void Insert(UserMasters usermaster)
        {
            DALCommon dal = new DALCommon(ConStr);
            dal.Insert(usermaster);
        }

        public void Update(UserMasters usermaster)
        {
            DALCommon dal = new DALCommon(ConStr);
            dal.Update(usermaster);

        }
        public int DeleteUser(UserMasters UserMasters)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.DeleteUser(UserMasters);
        }
        public UserMasters getUserProfile(string UserName)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetUserProfile(UserName);
        }
        public UserInfo GetUserInfoByuserId(int userId, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetUserInfoByuserId(userId, SchoolID);
        }

        public List<RoleMaster> GetRoleList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetRoleList(SchoolID);
        }
        public List<CityMaster> GetCityList(int? STATE_ID, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCityList(STATE_ID, SchoolID);
        }

        public List<StateMaster> GetStateList(int? COUNTRY_ID, int SchoolID)
        {

            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStateList(COUNTRY_ID, SchoolID);
        }

        public List<CountryMaster> GetCountryList(int SchoolID)
        {

            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCountryList(SchoolID);
        }
        public int OragnasitionBasicopation(OragnisationMaster _OM)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.OragnasitionBasicopation(_OM);
        }
        public int Firstuserconfigure(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.Firstuserconfigure(SchoolID);
        }

        public OragnisationMaster GetOragnisationAlready(string LEmailId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetOragnisationAlready(LEmailId);
        }

        public List<UserTypeMaster> UsertypeMasterlist(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.UsertypeMasterlist(SchoolID);
        }
        #region EmployeeManagement

        public List<EmployeeMaster> GetEmployeeList(int IsActive, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetEmployeeList(IsActive, SchoolID);
        }
        public EmployeeMaster GetSingleEmployee(int EMP_ID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSingleEmployee(EMP_ID);
        }
        #endregion
        #region StudentManagementSystem
        public StudentMaster GetSingleStudent(int Smid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSingleStudent(Smid);
        }

        //public List<StudentMaster> GetStudentList(int IsActive, int SchoolID)
        //{
        //    DALCommon dal = new DALCommon(ConStr);
        //    return dal.GetStudentList(IsActive,SchoolID); 
        //}
        public DataTable GetStudentbaseonAdmissionno(string AdmissionFormno, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentbaseonAdmissionno(AdmissionFormno, SchoolID);
        }
        public DataSet GetStudentTbl(Nullable<int> classid, Nullable<int> sectionid, Nullable<DateTime> Startdate, Nullable<DateTime> Enddate, int IsActive, int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentTbl(classid, sectionid, Startdate, Enddate,IsActive, SchoolID, FinancialYear);
        }
        public DataSet GetStudentStrengthTbl(Nullable<int> classid, Nullable<int> sectionid, int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentStrengthTbl(classid, sectionid, SchoolID, FinancialYear);
        }
            #endregion

            public List<GendraMaster> GetGendarList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetGendarList(SchoolId);
        }
        public List<ContactTitleMaster> GetContactList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetContactList(SchoolId);
        }
        public List<BloodGroupMaster> GetBloodGroupList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetBloodGroupList(SchoolId);
        }
        public List<MartialStatusMaster> GetMaritalStatusList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetMaritalStatusList(SchoolId);
        }

        public List<Feegroupstudentfrm> GetFeegroupList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetFeegroupList(SchoolID);
        }

        public List<SectionMaster> GetSectionList(int SchoolID, int Classid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSectionList(SchoolID, Classid);
        }
        public DataTable GetSectionListTBL(int SchoolID, int CMid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSectionListTBL(SchoolID, CMid);
        }
        public List<ReligionMaster> GetReligionList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetReligionList(SchoolID);
        }
        public List<DepartmentMaster> GetDepartmentList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetDepartmentList(SchoolID);
        }
        public List<DesignationMaster> GetDesignationList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetDesignationList(SchoolID);
        }
        public List<CategoryMaster> GetCategoryMasterList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCategoryMasterList(SchoolID);
        }
        public List<MartialStatusMaster> GetMartialStatusList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetMartialStatusList(SchoolID);
        }

        public List<CastMaster> GetCastList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCastList(SchoolID);
        }

        public List<RouteMaster> GetRouteList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetRouteList(SchoolID);
        }

        public List<BusMaster> GetBuslist(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetBuslist(SchoolID);
        }
        public SectionMaster GetSectionBySecmid(int Secmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSectionBySecmid(Secmid);
        }
        public RouteMaster GetRouteByid(int Routemid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetRouteByid(Routemid);
        }
        public BusMaster GetBusByid(int Busmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetBusByid(Busmid);
        }
        public CastMaster GetCastByid(int Castmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCastByid(Castmid);
        }
        public CategoryMaster GetCategoryByid(int Catmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetCategoryByid(Catmid);
        }
        public MartialStatusMaster GetMartialStatusByid(int Mrmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetMartialStatusByid(Mrmid);
        }
        public ContactTitleMaster GetContacttitleByid(int CTmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetContacttitleByid(CTmid);
        }
        public BloodGroupMaster GetBloodGroupByid(int Bdmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetBloodGroupByid(Bdmid);
        }
        public GendraMaster GetGendarByid(int GMid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetGendarByid(GMid);
        }
        public ReligionMaster GetReligionByid(int Relmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetReligionByid(Relmid);
        }
        public DepartmentMaster GetDepartmentByid(int Department_id)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetDepartmentByid(Department_id);
        }
        public DesignationMaster GetDesignationByid(int DESIGNATION_ID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetDesignationByid(DESIGNATION_ID);
        }

        public int AddBus(BusMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddBus(_master, Otype);
        }
        public int AddCast(CastMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddCast(_master, Otype);
        }
        public int AddCategory(CategoryMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddCategory(_master, Otype);
        }
        public int AddMartialStatus(MartialStatusMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddMartialStatus(_master, Otype);
        }
        public int AddBloodgroup(BloodGroupMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddBloodgroup(_master, Otype);
        }
        public int AddContacttitle(ContactTitleMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddContacttitle(_master, Otype);
        }
        public int AddGendar(GendraMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddGendar(_master, Otype);
        }
        public int AddReligion(ReligionMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddReligion(_master, Otype);
        }
        public int AddDepartment(DepartmentMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddDepartment(_master, Otype);
        }
        public int AddDesignation(DesignationMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddDesignation(_master, Otype);
        }

        public int AddSection(SectionMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddSection(_master, Otype);
        }
        public ClassMaster GetClassByClassid(int CMid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetClassByClassid(CMid);
        }
        public int AddClass(ClassMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddClass(_master, Otype);
        }
        public int AddRout(RouteMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddRout(_master, Otype);
        }
        public List<ClassMaster> GetClassList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetClassList(SchoolId);
        }

        public int AddAdmissionEnquiry(AdmissionEnquiryMaster _addenqmasters)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddAdmissionEnquiry(_addenqmasters);
        }
        public int UPDAdmissionEnquiry(AdmissionEnquiryMaster _addenqmasters)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.UPDAdmissionEnquiry(_addenqmasters);
        }

        public void DeleteAdmissionEnquiry(AdmissionEnquiryMaster _addenqmasters)
        {
            DALCommon dal = new DALCommon(ConStr);
            dal.DeleteAdmissionEnquiry(_addenqmasters);
        }

        public AdmissionEnquiryMaster GetAdmissionEnquiry(int AMID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAdmissionEnquiry(AMID);
        }
        public List<AdmissionEnquiryMaster> GetAdmissionenquerylist(string multipleenqueryid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAdmissionenquerylist(multipleenqueryid);
        }
        public string GetEnquiryNO(int OMID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetEnquiryNO(OMID);
        }

        public string GetEmpCodeId(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetEmpCodeId(SchoolId);
        }
        public SessionInfo GetSessionDetails(string Username, Nullable<DateTime> Currentdate)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSessionDetails(Username, Currentdate);
        }
        public List<AdmissionEnquiryMaster> GetAdmissionEnquiryList(int FMID, int OMID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAdmissionEnquiryList(FMID, OMID);
        }
        public DataSet GetAdmissionEnquiryTblList(Nullable<int> classid, Nullable<DateTime> Startdate, Nullable<DateTime> Enddate, int FMID, int OMID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAdmissionEnquiryTblList(classid, Startdate, Enddate,FMID, OMID);
        }
        public string Applicationfprmano(int Schoolid, int Classid, int Section, int Financialyear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.Applicationfprmano(Schoolid, Classid, Section, Financialyear);
        }
        public int AddStudent(StudentMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddStudent(_master, Otype);
        }
        public int UpdateStudentDetails(StudentMaster _master)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.UpdateStudentDetails(_master);
        }
        public int AddEmployee(EmployeeMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddEmployee(_master, Otype);
        }
        public OragnisationMaster GetOragnisationDetails(int OMID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetOragnisationDetails(OMID);
        }
        #region Holiday Master
        public string SaveHolidayDetails(HolidayMaster obj)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.SaveHolidayDetails(obj);
        }
        public string UpdateHolidayDetails(HolidayMaster obj)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.UpdateHolidayDetails(obj);
        }
        public string DeleteHolidayDetails(int Hid, int _OrgnisationID, int _Financialyearid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.DeleteHolidayDetails(Hid, _OrgnisationID, _Financialyearid);
        }
        public List<HolidayMaster> GetHolidaylist(int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetHolidaylist(SchoolID, FinancialYear);
        }
        public HolidayMaster GetHolidayDetails(int Hid, int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetHolidayDetails(Hid, SchoolID, FinancialYear);
        }
        #endregion Holiday Master
        #region ResetPassword
        public bool SetOTP(string UserName, string OTP)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.SetOTP(UserName, OTP);
        }
        #endregion ResetPassword

        #region Leave
        public List<Leave> GetLeaveReport(Leave obj)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetLeaveReport(obj);
        }
        public bool InsertUpdateLeaveRequest(Leave obj)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.InsertUpdateLeaveRequest(obj);
        }

        #endregion Leave
        public int InsertUpdateSession(string UserName, int FinancialID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.InsertUpdateSession(UserName, FinancialID);
        }
        public List<FinancialYear> GetFinancialYearList()
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetFinancialYearList();
        }

        public GEO GetGeoDetails(string PinCode)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetGeoDetails(PinCode);
        }
        public DataSet GetAttendancestudentlst(int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAttendancestudentlst(CMid, SecMid, SchoolId, Fmid);
        }
        public DataSet GetmonthalyAttendancestudentlst(int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetmonthalyAttendancestudentlst(CMid, SecMid, SchoolId, Fmid);
        }

        public DataSet GetEmployeeTbl(int IsActive, int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetEmployeeTbl(IsActive, SchoolID, FinancialYear);
        }
        public string StudentAttendanceopration(List<StudentAttendanceMaster> StudentAttendancelist, string optype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.StudentAttendanceopration(StudentAttendancelist, optype);
        }
        public DataSet GetAttendancesheetlst(DateTime attendate, int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAttendancesheetlst(attendate, CMid, SecMid, SchoolId, Fmid);
        }
        public DataSet GetAttendancesummary(DateTime attendate, DateTime toattendate, int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAttendancesummary(attendate, toattendate, CMid, SecMid, SchoolId, Fmid);
        }
        public List<StudentMaster> GetStudentList(int SchoolID, int FinancialYearID, int ClassID, int SectionID, int RouteID, string AdmissionNo)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentList(SchoolID, FinancialYearID, ClassID, SectionID, RouteID, AdmissionNo);
        }
        public DashboardDetails GetDashboardDetails(int OrgnisationID, int Financialyearid, DateTime CurrentDate)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetDashboardDetails(OrgnisationID, Financialyearid, CurrentDate);
        }
        public bool ValidateEmailExist(string EmailID, string UserType, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.ValidateEmailExist(EmailID, UserType, SchoolID);
        }
        public DataSet GetMasterForBulkStudentRegistration(int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetMasterForBulkStudentRegistration(SchoolID, FinancialYear);
        }
        public DataTable StudentRegistration(DataTable DTStudent)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.StudentRegistration(DTStudent);
        }
        #region CourseManagementSystem
        public DataTable GetSubjectListTBL(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectListTBL(SchoolID);
        }
        public SubjectMaster GetSubjectBySumid(int Sumid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectBySumid(Sumid);
        }
        public int AddSubject(SubjectMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddSubject(_master, Otype);
        }
        public List<SubjectMaster> GetSubjectList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectList(SchoolId);
        }

        /*------Chapter Listing data-----------*/
        public DataTable GetSubjectChapterListTBL(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectChapterListTBL(SchoolID);
        }
        public SubjectchapterMaster GetSubjectChapterBySumid(int Sumid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectChapterBySumid(Sumid);
        }
        public int AddSubjectchapter(SubjectchapterMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddSubjectchapter(_master, Otype);
        }
        public List<SubjectchapterMaster> GetSubjectChapterList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectChapterList(SchoolId);
        }
        /*----------End Chapter listing-------------*/
        public DataTable GetPeriodmasterTblList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetPeriodmasterTblList(SchoolID);
        }
        public List<PeriodMaster> GetPeriodMasterList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetPeriodMasterList(SchoolId);
        }
        public PeriodMaster GetPeriodByPmid(int Pmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetPeriodByPmid(Pmid);
        }
        public int AddPeriod(PeriodMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddPeriod(_master, Otype);
        }
        /*---------End of Period Master opration---------*/
        /*---------Asssign Period teacher opration---------*/
        public List<EmployeeMaster> GetTeacherList(int IsActive, int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetTeacherList(IsActive, SchoolID);
        }
        public DataTable GetAssignperiodteacherListTBL(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAssignperiodteacherListTBL(SchoolID);
        }
        public Assignperiodteacher GetAssignperiodteacherByApttmid(int Apttmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAssignperiodteacherByApttmid(Apttmid);
        }
        public int AddAssignperiodteacher(Assignperiodteacher _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddAssignperiodteacher(_master, Otype);
        }
        public List<Assignperiodteacher> GetAssignperiodteacherList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAssignperiodteacherList(SchoolId);
        }
        /*---------End Assign Period Teacher opration-------*/
        /*---------Assign Lecture Content--------------------*/
        public DataTable AssigncontentinlectureTblList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AssigncontentinlectureTblList(SchoolID);
        }
        public Assigncontentinlecture GetAssigncontentinlectureByAcdtmid(int Acdtmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetAssigncontentinlectureByAcdtmid(Acdtmid);
        }
        public int AddAssigncontentinlecture(Assigncontentinlecture _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddAssigncontentinlecture(_master, Otype);
        }
        public List<SubjectMaster> GetSubjectbaseonlectureList(int Apttmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSubjectbaseonlectureList(Apttmid);
        }
        public List<SubjectchapterMaster> GetchapterbaseonsubjectList(int subjectId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetchapterbaseonsubjectList(subjectId);
        }
        /*--------End Assign Lecture----------------------------*/
        #endregion
        #region BellSystem
        public DataTable GetBellSystemListTBL(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetBellSystemListTBL(SchoolID);
        }

        public BellSystemMaster GetBellSystemBybmid(int bmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetBellSystemBybmid(bmid);
        }
        public int AddBellSystem(BellSystemMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddBellSystem(_master, Otype);
        }
        #endregion

        #region API User
        public int UserAvailablity(string Username, string Password)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.UserAvailablity(Username, Password);
        }
        public int InsertInDataPacket(DataTable objtbl)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.InsertInDataPacket(objtbl);
        }
        #endregion

        #region UserProfile Oprations
        public int AddUserprofile(UserMasters _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddUserprofile(_master, Otype);
        }
        #endregion

        public List<StudentMaster> GetMultipleStudentslist(string Smid_multiplevalue)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetMultipleStudentslist(Smid_multiplevalue);
        }

        public List<Studentddl> GetStudentddl(Boolean isActive, int Schoolid, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentddl(isActive, Schoolid, FinancialYear);
        }
        public Studentleavedetail GetStudentleavedetailBySlmid(int Slmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentleavedetailBySlmid(Slmid);
        }
        public int AddStudentleave(Studentleavedetail _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddStudentleave(_master, Otype);
        }
        public DataTable GetStudentleaveTbl(Studentleavedetail _objeleave,Nullable<int> CMid, Nullable<int> SecMid, int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetStudentleaveTbl(_objeleave, CMid, SecMid,SchoolID, FinancialYear);
        }

        #region HR Management System
        public DataTable GetSalaryheadTbl(int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSalaryheadTbl(SchoolID, FinancialYear);
        }
        public Salaryheadmaster GetSalaryheaddetailbyShmid(int Shmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSalaryheaddetailbyShmid(Shmid);
        }
        public int AddSalaryheaddetail(Salaryheadmaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddSalaryheaddetail(_master, Otype);
        }
        public List<Salaryheadmaster> GetSalaryheadlist(int SchoolID, int FinancialYear, int EMP_ID = 0)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSalaryheadlist(SchoolID, FinancialYear, EMP_ID);

        }
        public DataTable GetSalaryheadassignTbl(int SchoolID, int FinancialYear)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSalaryheadassignTbl(SchoolID, FinancialYear);
        }
        public int Assignsalaryheadtoemployee(DataTable Salaryheadtoemployee, Assignheadtoemployee _objassign, string optype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.Assignsalaryheadtoemployee(Salaryheadtoemployee, _objassign, optype);
        }

        public TDSSettingmaster GetTDSSettingByTdsmid(int Tdsmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetTDSSettingByTdsmid(Tdsmid);
        }
        public List<TDSSettingmaster> GetTDSSettingList(int SchoolID, int FinancialYearID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetTDSSettingList(SchoolID, FinancialYearID);
        }
        public DataTable GetTDSSettingListTbl(int SchoolID, int FinancialYearID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetTDSSettingListTbl(SchoolID, FinancialYearID);
        }
        public int AddTDSSetting(TDSSettingmaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddTDSSetting(_master, Otype);
        }
        #endregion
        public DataTable GetShiftmasterTblList(int SchoolID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetShiftmasterTblList(SchoolID);
        }
        public ShiftMaster GetShiftByShiftmid(int Shiftmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetShiftByShiftmid(Shiftmid);
        }
        public int AddShift(ShiftMaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddShift(_master, Otype);
        }
        public List<ShiftMaster> GetShiftmasterList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetShiftmasterList(SchoolId);
        }
        public DataTable GetQuizeexamListTBL(int SchoolID, int FinancialYearID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetQuizeexamListTBL(SchoolID, FinancialYearID);
        }
        public Quizeexammaster GetQuizeexamByid(int Qzmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetQuizeexamByid(Qzmid);
        }
        public int AddQuizeexam(Quizeexammaster _master, string Otype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.AddQuizeexam(_master, Otype);
        }
        public List<Questionmaster> Genratenoofquestion(int quenumber)
        {
            List<Questionmaster> Queslst = new List<Questionmaster>();
            for (int i = 0; i < quenumber; i++)
            {
                Questionmaster _singlequest = new Questionmaster()
                {
                    Qmid = i + 1,
                    Question = "",
                    Optionlist = new List<Questionoptions>()
                    {
                        new Questionoptions { Opmid =1,Optiontext ="Option 1" },
                        new Questionoptions { Opmid=2,Optiontext="Option 2"},
                        new Questionoptions { Opmid=3,Optiontext="Option 3"},
                        new Questionoptions { Opmid=4,Optiontext="Option 4"}
                    }
                };
                Queslst.Add(_singlequest);
            }
            return Queslst;
        }
        public int Addquestionans(DataTable dtquestionans, Quizeexammaster _quizrec, string optype)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.Addquestionans(dtquestionans, _quizrec, optype);
        }
        public int Quizrecording(DataTable dtQuizrecording, Quizeexammaster _userrec)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.Quizrecording(dtQuizrecording, _userrec);
        }

        public List<Questionmaster> startexam(int Qzmid)
        {
            DALCommon dal = new DALCommon(ConStr);
            List<Questionmaster> Queslst = new List<Questionmaster>();
            DataTable _dtquestion = new DataTable();
            _dtquestion = dal.GetQuizequestionList(Qzmid);
            DataTable _dtquestionoption = new DataTable();
            for (int i = 0; i < _dtquestion.Rows.Count; i++)
            {
                Questionmaster _singlequest = new Questionmaster();
                _singlequest.Qmid = Convert.ToInt32(_dtquestion.Rows[i]["Qmid"]);
                _singlequest.Question = Convert.ToString(_dtquestion.Rows[i]["Question"]);
                _dtquestionoption = dal.GetQuestionoptionList(_singlequest.Qmid);
                List<Questionoptions> _optionlst = new List<Questionoptions>();

                for (int k = 0; k < _dtquestionoption.Rows.Count; k++)
                {
                    Questionoptions _optionsingle = new Questionoptions();
                    _optionsingle.Opmid = Convert.ToInt32(_dtquestionoption.Rows[k]["Opmid"]);
                    _optionsingle.Optiontext = Convert.ToString(_dtquestionoption.Rows[k]["Optiontext"]);
                    _optionlst.Add(_optionsingle);
                }
                _singlequest.Optionlist = _optionlst;
                Queslst.Add(_singlequest);
            }
            return Queslst;
        }

        public int Assignstudent4online(Onlineexamstudent master, string studentlist)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.Assignstudent4online(master, studentlist);
        }


    }
}
