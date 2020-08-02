using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    class Fee
    {
    }

    public class FeeHead : BaseModel
    {

        public string FeeID { get; set; }

        public string FeeHeadName { get; set; }

        public string Refundable { get; set; }

        public string Frequency { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string DueDate { get; set; }

        public bool Active { get; set; }

        public int SchoolID { get; set; }

        public string CreatedBy { get; set; }

        public string Action { get; set; }

        public List<FeeHead> lstFeeHead { get; set; }

        public Months months { get; set; }

        public string ActionName { get; set; }

        public List<selectList> FrequecyLst { get; set; }

        public List<selectList> RefundableLst { get; set; }

    }
    public class FeeHeadDescription : BaseModel
    {


        public string FeeDescID { get; set; }

        public string FeeID { get; set; }

        public string FeeDescName { get; set; }

        public string Months { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string DueDate { get; set; }

        public bool Active { get; set; }

        public string CreatedBy { get; set; }

        public string Action { get; set; }

        public List<FeeHeadDescription> lstFeeHeadDesc { get; set; }
    }
    public class Months
    {

        public bool January { get; set; }

        public bool February { get; set; }

        public bool March { get; set; }

        public bool April { get; set; }

        public bool May { get; set; }

        public bool June { get; set; }

        public bool July { get; set; }

        public bool August { get; set; }

        public bool September { get; set; }

        public bool October { get; set; }

        public bool November { get; set; }

        public bool December { get; set; }

    }
    public class FeeGroup : BaseModel
    {

        public string FeeGroupID { get; set; }

        public string FeeGroupName { get; set; }

        public string Frequency { get; set; }

        public string FeeHeadName { get; set; }

        public string FeeHeadID { get; set; }

        public string Amount { get; set; }

        public FeeHeadList feeHead { get; set; }

        public bool Active { get; set; }

        public int SchoolID { get; set; }

        public string CreatedBy { get; set; }

        public string Action { get; set; }

        public string ActionName { get; set; }

        public List<FeeHeadList> feeHeadList { get; set; }

        public List<FeeGroup> lstFeeGroup { get; set; }

        public List<selectList> FrequecyLst { get; set; }
    }
    public class FeeHeadList
    {

        public bool Check { get; set; }

        public string Name { get; set; }

        public string ID { get; set; }

        public string Amount { get; set; }

        public string Apr { get; set; }

        public string May { get; set; }

        public string Jun { get; set; }

        public string Jul { get; set; }

        public string Aug { get; set; }

        public string Sep { get; set; }

        public string Oct { get; set; }

        public string Nov { get; set; }

        public string Dec { get; set; }

        public string Jan { get; set; }

        public string Feb { get; set; }

        public string Mar { get; set; }

        public Months months { get; set; }
    }
    public class selectList
    {

        public string Text { get; set; }

        public string Value { get; set; }
    }

    public class FeeGroupAllocation : BaseModel
    {
        public List<selectList> FeeForList { get; set; }
        public List<FeeGroup> lstFeeGroup { get; set; }

        public string FeeFor { get; set; }
        public string FeeGroupName { get; set; }
        public List<Section> SectionList { get; set; }

        public int SectionID { get; set; }
        public List<StudentMaster> StudentList { get; set; }
    }
    public class Section
    {
        public bool Check { get; set; }
        public int SectionID { get; set; }
    }
    public class FeeCollection : BaseModel
    {
        public StudentMaster Student { get; set; }
        public Months months { get; set; }
        public string Fine { get; set; }
        public string Concession { get; set; }
        public bool WaiveOff { get; set; }
        public string TotalFees { get; set; }
        public string Payment { get; set; }
        public string Balance { get; set; }
        public string Remarks { get; set; }
        public string MOP { get; set; }
        public string MOPRemark { get; set; }
        public string Arrears { get; set; }
        public string TransportCost { get; set; }


    }
    public class FeeDeposit
    {
        public int StudentID { get; set; }
        public string AdmissionNo { get; set; }
        public string StudentName { get; set; }
        public string months { get; set; }
        public string Fine { get; set; }
        public string Concession { get; set; }
        public bool WaiveOff { get; set; }
        public string TotalFees { get; set; }
        public string Payment { get; set; }
        public string Balance { get; set; }
        public string Remarks { get; set; }
        public string MOP { get; set; }
        public string MOPRemark { get; set; }
        public string TotalFeesBreakUp { get; set; }
        public string Action { get; set; }
        public string FeeGroup { get; set; }
        public string DepositDate { get; set; }
        public string ReceiptNo { get; set; }
        public int SchoolId { get; set; }
        public string Arrears { get; set; }
        public string TransportCost { get; set; }
        public int FinancialYear { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FeeReciept : BaseModel
    {
        public OragnisationMaster OragnisationDetails { get; set; }
        public StudentMaster StudentDetails { get; set; }
        public FeeDeposit FeeDetails { get; set; }
    }
    #region Ledger
    public class Ledger : BaseModel
    {
        public int LDid { get; set; }
        public string Description { get; set; }
        public string DescriptionType { get; set; }
        [DefaultValue("0")]
        public decimal Amount { get; set; }
        [DefaultValue("0")]
        public decimal Debit { get; set; }
        [DefaultValue("0")]
        public decimal Credit { get; set; }
        public string EffectiveDate { get; set; }
        public int SchoolId { get; set; }
        public int FinancialYear { get; set; }
        public string CreatedBy { get; set; }
        public string FromDate { get; set; }
        public string EndDate { get; set; }
        public string CreatedDate { get; set; }
        public List<Ledger> LedgerList { get; set; }
    }


    #endregion Ledger

    #region Salary

    public class EmployeeAttendance : BaseModel, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public int SchoolID { get; set; }
        public int FinancialYear { get; set; }
        public string MonthYear { get; set; }
        public int TotalDaysInMonth { get; set; }
        public List<EmployeeAttendanceRecord> EmployeeAttendanceList { get; set; }
    }
    public class EmployeeAttendanceRecord
    {
        public bool Check { get; set; }
        public bool LeaveInCash { get; set; }
        public EmployeeMaster EmployeeDetails { get; set; }
        public AttendanceDetails AttendanceDetail { get; set; }
        public string PaidSalary { get; set; }
        public string DaySalary { get; set; }
        public bool PaymentStatus { get; set; }
        public double MonthSalary { get; set; }
        public double TotalEarning { get; set; }
        public double TotalDeduction { get; set; }
        public double EmployeeESIAmount { get; set; }
        public double EmployerESIAmount { get; set; }
        public double EmployeePFAmount { get; set; }
        public double EmployerPFAmount { get; set; }
        public double EmployeeTDS { get; set; }
    }
   
    public class AttendanceDetails
    {
        public List<string> TotalWorkingDaysList { get; set; }
        public List<string> AbsentList { get; set; }
        public int ActualTotalAbsent { get; set; }
        public int ModifiedTotalAbsent { get; set; }
        public List<string> PaidLeaveList { get; set; }
        public decimal ActualTotalPaidLeave { get; set; }
        public decimal ModifiedTotalPaidLeave { get; set; }
        public List<string> UnPaidLeaveList { get; set; }
        public decimal ActualTotalUnPaidLeave { get; set; }
        public decimal ModifiedTotalUnPaidLeave { get; set; }
        public List<string> HolidayList { get; set; }
        public int ActualTotalHoliday { get; set; }
        public int ModifiedTotalHoliday { get; set; }
        public List<string> WOList { get; set; }
        public int ActualTotalWO { get; set; }
        public int ModifiedTotalWO { get; set; }
        public List<string> PresentList { get; set; }
        public int ActualTotalPresent { get; set; }
        public int ModifiedTotalPresent { get; set; }
        public List<string> MissingAttendenceList { get; set; }
        public int ActualTotalMissingAttendence { get; set; }
        public int ModifiedTotalMissingAttendence { get; set; }
        public DataTable Attendance { get; set; }
    }
    public class EmployeeSalary : BaseModel
    {
        public int SchoolID { get; set; }
        public int FinancialYear { get; set; }
        public string MonthYear { get; set; }
        public int TotalDays { get; set; }
        public List<EmployeeSalaryDetails> SalaryDetailsList { get; set; }
    }
    public class EmployeeSalaryDetails
    {
        public int Counter { get; set; }
        public int EMP_ID { get; set; }
        public string EMP_CODE { get; set; }
        public string EmployeeName { get; set; }
        public int WorkingDays { get; set; }
        public decimal Present { get; set; }
        public int Absent { get; set; }
        public int Holiday { get; set; }
        public decimal PaidLeave { get; set; }
        public decimal UnPaidLeave { get; set; }
        public int WeekOff { get; set; }
        public string PresentDays { get; set; }
        public string AbsentDays { get; set; }
        public string HolidayDays { get; set; }
        public string PaidLeaveDays { get; set; }
        public string UnPaidLeaveDays { get; set; }
        public string WeekOffDays { get; set; }
        public decimal MonthSalary { get; set; }
        public decimal PaidSalary { get; set; }

        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal EmployeePFAmount { get; set; }
        public decimal EmployerPFAmount { get; set; }
        public decimal EmployeeESIAmount { get; set; }
        public decimal EmployerESIAmount { get; set; }
        public decimal EmployeeTDS { get; set; }

        public string ModeOfPayment { get; set; }
        public string PaymentRemark { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string PaymentBy { get; set; }
        public decimal DaySalary { get; set; }
        public bool Active { get; set; }
        public string User { get; set; }
        public bool PaymentStatus { get; set; }
    }
    //public class BankDetails
    //{
    //    public string BankName { get; set; }
    //    public string AccountHolderName { get; set; }
    //    public string AccountNo { get; set; }
    //    public string Branch { get; set; }
    //    public string Location { get; set; }
    //    public string IFSCCode { get; set; }
    //}
    #endregion Salary
    #region Fee Concession
    public class FeeConcession : BaseModel
    {

    }
    public class FeeConcessionDetails
    {
        public int FeeConID { get; set; }
        public int StudentID { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public decimal Amount { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public string Action { get; set; }
    }
    #endregion  Fee Concession

    #region DueFeeList
    public class DueFeeList : BaseModel
    {
        public List<ClassMaster> ClassList { get; set; }
        public int ClassID { get; set; }

        public List<SectionMaster> SectionList { get; set; }
        public int SectionID { get; set; }

        public string FromMonth { get; set; }
        public string ToMonth { get; set; }

    }
    #endregion DueFeeList
    public class DueFeeListDetail : BaseModel
    {
        public OragnisationMaster OragnisationDetails { get; set; }
        public StudentMaster StudentDetails { get; set; }
        public FeeHeadList DueFeeList { get; set; }
    }

    public class IndisciplineDetails : BaseModel
    {
        public int Counter { get; set; }
        public string Status { get; set; }
        public string StudentName { get; set; }
        public string AdmissionNo { get; set; }
        public string FatherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public decimal FineAmount { get; set; }
        public string Remark { get; set; }
        public int SchoolId { get; set; }
        public int FinancialYearID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
    public class Indiscipline : BaseModel
    {
        public List<IndisciplineDetails> IndisciplineDetailList { get; set; }
        public StudentMaster Student { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public decimal FineAmount { get; set; }
    }
    public class IndisciplineFeeReciept : BaseModel
    {
        public OragnisationMaster OragnisationDetails { get; set; }
        public StudentMaster StudentDetails { get; set; }
        public IndisciplineDetails FeeDetails { get; set; }
    }
    public class UnallocatedFeeGroup : BaseModel
    {
        public List<ClassMaster> ClassList { get; set; }
        public List<FeeGroup> lstFeeGroup { get; set; }
        public string FeeGroupName { get; set; }
        public string ClassName { get; set; }
        public List<StudentMaster> StudentList { get; set; }
    }

    public class ExtraFee : BaseModel
    {
        public StudentMaster Student { get; set; }
        public string Fine { get; set; }
        public string Remarks { get; set; }
        public string MOP { get; set; }
        public string Amount { get; set; }
        public string FeeHead { get; set; }
    }
    #region DueFeeListMonthWise
    public class DueFeesMonthWise : BaseModel
    {
        public List<ClassMaster> ClassList { get; set; }
        public int ClassID { get; set; }

        public List<SectionMaster> SectionList { get; set; }
        public int SectionID { get; set; }

        public string TillMonth { get; set; }
        public string Session { get; set; }
        public List<FinancialYear> SessionList { get; set; }

    }
    #endregion DueFeeList
}
