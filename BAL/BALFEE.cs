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
    public class BALFee
    {
        string ConStr = "";
        public BALFee(string ConnectionString)
        {
            ConStr = ConnectionString;
        }
        public List<EmployeeMaster> SearchEmployeeList(string searchVal, int schoolID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.SearchEmployeeList(searchVal, schoolID);
        }

        public string SaveFeeHead(FeeHead FH)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.SaveFeeHead(FH);
        }
        public List<FeeHead> GetFeeHeads(FeeHead FH)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetFeeHeads(FH);
        }
        public List<FeeHeadDescription> GetFeeHeadDescription(FeeHeadDescription FHD)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetFeeHeadDescription(FHD);
        }
        public string SaveFeeHeadDescription(FeeHeadDescription FHD)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.SaveFeeHeadDescription(FHD);
        }
        public string DeleteFeeHeadDescription(FeeHeadDescription FHD)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.DeleteFeeHeadDescription(FHD);
        }
        public string DeleteFeeHead(string FeeID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.DeleteFeeHead(FeeID);
        }
        public List<FeeGroup> GetFeeGroup(FeeGroup FG)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetFeeGroup(FG);
        }
        public string SaveFeeGroup(FeeGroup FG)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.SaveFeeGroup(FG);
        }
        public string DeleteFeeGroup(string FeeGroupID, string FeeGroupName)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.DeleteFeeGroup(FeeGroupID, FeeGroupName);
        }
        public List<selectList> GetDDList(string Module, int SchoolID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetDDList(Module, SchoolID);
        }
        public List<FeeHeadList> GetFeeHeadListForFeeGroup(string FeeGroupName, int SchoolID, string AdmissionNo,int FMid)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetFeeHeadListForFeeGroup(FeeGroupName, SchoolID, AdmissionNo, FMid);
        }
        public int UpdateStudentFeeGroupName(string FeeGroupName, int SectionId, int StudentID, int SchoolId)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.UpdateStudentFeeGroupName(FeeGroupName, SectionId, StudentID, SchoolId);
        }
        public List<StudentMaster> SearchStudentList(string searchVal, int schoolID, int FYID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.SearchStudentList(searchVal, schoolID, FYID);
        }
        public int FeeDeposit(FeeDeposit FeeDepositDetails)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.FeeDeposit(FeeDepositDetails);
        }
        public List<FeeDeposit> GetFeeDeposit(FeeDeposit FeeDepositDetails)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetFeeDeposit(FeeDepositDetails);
        }
        public FeeDeposit GetFeeDepositByReceiptNo(FeeDeposit FeeDepositDetails)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetFeeDepositByReceiptNo(FeeDepositDetails);
        }

        public List<ClassMaster> GetClassList(string SchoolID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetClassList(SchoolID);
        }

        public List<StudentMaster> GetStudentList(int SectionID, int StudentID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetStudentList(SectionID, StudentID);
        }
        public List<ClassSectionMaster> GetClassSection(int ClassID, int SchoolID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetClassSection(ClassID, SchoolID);
        }

        #region Ledger
        public List<Ledger> GerLedgerReport(Ledger obj)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GerLedgerReport(obj);
        }
        public bool InsertLedgerEntry(Ledger obj)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.InsertLedgerEntry(obj);
        }
        #endregion Ledger
        #region Salary
        public EmployeeAttendance GetEmployeeAttendance(EmployeeAttendance model, string EmployeeID = "")
        {
            EmployeeSalary _EmployeeSalaryModel = new EmployeeSalary();
            EmployeeSalary _EmployeeSalary = new EmployeeSalary();
            DALFee dal = new DALFee(ConStr);
            List<EmployeeMaster> _employeeList = new List<EmployeeMaster>();
            List<EmployeeAttendanceRecord> ObjEmployeeAttendanceRecordList = new List<EmployeeAttendanceRecord>();
            EmployeeAttendanceRecord objEmployeeAttendanceRecord = null;
            EmployeeMaster objEmployeeMaster = null;
            AttendanceDetails objAttendanceDetails = null;
            int Month = 0, Year = 0;
            try
            {
                #region SET properties to Get Employee Salary status
                _EmployeeSalaryModel.SchoolID = model.SchoolID;
                _EmployeeSalaryModel.FinancialYear = model.FinancialYear;
                _EmployeeSalaryModel.MonthYear = model.MonthYear;
                #endregion SET properties to Get Employee Salary status
                model.MonthYear = string.IsNullOrEmpty(model.MonthYear) ? DateTime.Now.ToString("MMM/yyyy") : model.MonthYear;
                Month = Convert.ToInt32(Convert.ToDateTime(model.MonthYear).ToString("MM"));
                Year = Convert.ToInt32(Convert.ToDateTime(model.MonthYear).ToString("yyyy"));
                _employeeList = !string.IsNullOrEmpty(EmployeeID) ? dal.GetEmployeeList(1, model.SchoolID).Where(x => x.ISACTIVE == true && Convert.ToString(x.EMP_ID) == EmployeeID).ToList() : dal.GetEmployeeList(1, model.SchoolID).Where(x => x.ISACTIVE == true).ToList();
                foreach (var emp in _employeeList)
                {
                    objEmployeeAttendanceRecord = new EmployeeAttendanceRecord();
                    objEmployeeMaster = new EmployeeMaster();
                    objAttendanceDetails = new AttendanceDetails();



                    objEmployeeMaster = emp;
                    var dtEmployeeAttendance = dal.GetEmployeeAttendance(model.SchoolID, model.FinancialYear, emp.EMP_ID, Month, Year);

                    var startDate = !string.IsNullOrEmpty(Convert.ToString(emp.JOININGDATE)) ? Convert.ToDateTime(emp.JOININGDATE) : new DateTime(Year, Month, 1);
                    var endDate = !string.IsNullOrEmpty(Convert.ToString(emp.LEAVINGDATE)) ? Convert.ToDateTime(emp.LEAVINGDATE) : new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
                    model.TotalDaysInMonth = DateTime.DaysInMonth(Year, Month);
                    var ListDates = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month)).Select(day => new DateTime(Year, Month, day)).ToList();
                    objAttendanceDetails.TotalWorkingDaysList = ListDates.Where(x => Convert.ToDateTime(x) >= startDate && Convert.ToDateTime(x) <= endDate).Select(x => x.ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.Attendance = dtEmployeeAttendance;
                    //if (dtEmployeeAttendance.Rows.Count > 0)
                    //{
                    _EmployeeSalary = dal.GetEmployeeSalaryList(_EmployeeSalaryModel, emp.EMPCODE);

                    objAttendanceDetails.AbsentList = dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<string>("SalaryAttendance") == "A" && dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.ActualTotalAbsent = objAttendanceDetails.AbsentList.Count();
                    objAttendanceDetails.ModifiedTotalAbsent = objAttendanceDetails.AbsentList.Count();

                    objAttendanceDetails.PaidLeaveList = dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<string>("SalaryAttendance") == "PL" && dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.ActualTotalPaidLeave = objAttendanceDetails.PaidLeaveList.Count();
                    objAttendanceDetails.ModifiedTotalPaidLeave = objAttendanceDetails.PaidLeaveList.Count();


                    objAttendanceDetails.UnPaidLeaveList = dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<string>("SalaryAttendance") == "UPL" && dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.ActualTotalUnPaidLeave = objAttendanceDetails.UnPaidLeaveList.Count();
                    objAttendanceDetails.ModifiedTotalUnPaidLeave = objAttendanceDetails.UnPaidLeaveList.Count();

                    objAttendanceDetails.HolidayList = dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<string>("SalaryAttendance") == "H" && dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.ActualTotalHoliday = objAttendanceDetails.HolidayList.Count();
                    objAttendanceDetails.ModifiedTotalHoliday = objAttendanceDetails.HolidayList.Count();

                    objAttendanceDetails.WOList = dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<string>("SalaryAttendance") == "WO" && dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.ActualTotalWO = objAttendanceDetails.WOList.Count();
                    objAttendanceDetails.ModifiedTotalWO = objAttendanceDetails.WOList.Count();

                    objAttendanceDetails.PresentList = dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<string>("SalaryAttendance") == "P" && dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList();
                    objAttendanceDetails.ActualTotalPresent = objAttendanceDetails.PresentList.Count();
                    objAttendanceDetails.ModifiedTotalPresent = objAttendanceDetails.PresentList.Count();

                    objAttendanceDetails.MissingAttendenceList = objAttendanceDetails.TotalWorkingDaysList.Where(y => !(dtEmployeeAttendance.Rows.OfType<DataRow>().Where(dr => dr.Field<DateTime>("Attendancedate") >= startDate && dr.Field<DateTime>("Attendancedate") <= endDate).Select(dr => dr.Field<DateTime>("Attendancedate").ToString("dd/MM/yyyy")).ToList()).Any(z => z == y)).ToList();
                    objAttendanceDetails.ActualTotalMissingAttendence = objAttendanceDetails.MissingAttendenceList.Count();
                    objAttendanceDetails.ModifiedTotalMissingAttendence = objAttendanceDetails.MissingAttendenceList.Count();
                    objEmployeeAttendanceRecord.PaidSalary = "N/G";
                    objEmployeeAttendanceRecord.PaymentStatus = false;
                    if (_EmployeeSalary != null)
                    {
                        if (_EmployeeSalary.SalaryDetailsList != null && _EmployeeSalary.SalaryDetailsList.Count > 0)
                        {
                            if (_EmployeeSalary.SalaryDetailsList.Where(x => x.EMP_ID == emp.EMP_ID && x.EMP_CODE == emp.EMPCODE).Any())
                            {
                                var empSal = _EmployeeSalary.SalaryDetailsList.Where(x => x.EMP_ID == emp.EMP_ID && x.EMP_CODE == emp.EMPCODE).FirstOrDefault();
                                objEmployeeAttendanceRecord.PaidSalary = string.IsNullOrEmpty(Convert.ToString(empSal.PaidSalary)) ? objEmployeeAttendanceRecord.PaidSalary : Convert.ToString(empSal.PaidSalary);
                                objEmployeeAttendanceRecord.PaymentStatus = empSal.PaymentStatus;
                            }
                        }
                    }

                    //}
                    //else
                    //{

                    //    objAttendanceDetails.AbsentList = new List<string>();
                    //    objAttendanceDetails.ActualTotalAbsent = 0;
                    //    objAttendanceDetails.ModifiedTotalAbsent = 0;

                    //    objAttendanceDetails.PaidLeaveList = new List<string>();
                    //    objAttendanceDetails.ActualTotalPaidLeave = 0;
                    //    objAttendanceDetails.ModifiedTotalPaidLeave = 0;

                    //    objAttendanceDetails.UnPaidLeaveList = new List<string>();
                    //    objAttendanceDetails.ActualTotalUnPaidLeave = 0;
                    //    objAttendanceDetails.ModifiedTotalUnPaidLeave = 0;

                    //    objAttendanceDetails.HolidayList = new List<string>();
                    //    objAttendanceDetails.ActualTotalHoliday = 0;
                    //    objAttendanceDetails.ModifiedTotalHoliday = 0;

                    //    objAttendanceDetails.WOList = new List<string>();
                    //    objAttendanceDetails.ActualTotalWO = 0;
                    //    objAttendanceDetails.ModifiedTotalWO = 0;

                    //    objAttendanceDetails.PresentList = new List<string>();
                    //    objAttendanceDetails.ActualTotalPresent = 0;
                    //    objAttendanceDetails.ModifiedTotalPresent = 0;

                    //    objAttendanceDetails.MissingAttendenceList = ListDates.Select(x => x.ToString("dd/MM/yyyy")).ToList();
                    //    objAttendanceDetails.ActualTotalMissingAttendence = objAttendanceDetails.MissingAttendenceList.Count();
                    //    objAttendanceDetails.ModifiedTotalMissingAttendence = objAttendanceDetails.MissingAttendenceList.Count();
                    //    objEmployeeAttendanceRecord.PaidSalary = "N/G";
                    //}
                    objEmployeeAttendanceRecord.AttendanceDetail = objAttendanceDetails;
                    objEmployeeAttendanceRecord.EmployeeDetails = objEmployeeMaster;

                    ObjEmployeeAttendanceRecordList.Add(objEmployeeAttendanceRecord);
                }
                model.EmployeeAttendanceList = ObjEmployeeAttendanceRecordList;
            }
            catch (Exception ex)
            {

            }

            return model;
        }
        public List<EmployeeMaster> GetEmployeeList(int IsActive, int SchoolID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetEmployeeList(IsActive, SchoolID);
        }
        public int InsertSalaryEntry(EmployeeSalary obj)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.InsertSalaryEntry(obj);
        }
        public EmployeeSalary GetEmployeeSalaryList(EmployeeSalary employeeSalary, string EmpCode)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetEmployeeSalaryList(employeeSalary, EmpCode);
        }
        public int UpdateSalary(EmployeeSalary employeeSalary)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.UpdateSalary(employeeSalary);
        }
        #endregion Salary

        #region FeeConcession
        public int AssignFeeConcession(FeeConcessionDetails feeConcessionDetails)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.AssignFeeConcession(feeConcessionDetails);
        }
        public FeeConcessionDetails GetAssignFeeConcession(FeeConcessionDetails model)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetAssignFeeConcession(model);
        }
        #endregion FeeConcession


        public List<StudentMaster> GetDueFeeList(int SchoolId, int FinancialYearID, int ClassID, int SectionID, int FromMonth, int ToMonth)
        {
            DALCommon obj = new DALCommon(ConStr);
            DALFee dal = new DALFee(ConStr);
            List<StudentMaster> Studentlist = new List<StudentMaster>();
            List<FeeHeadList> _FeeHeadList = new List<FeeHeadList>();
            try
            {
                Studentlist = obj.GetStudentList(SchoolId, FinancialYearID, ClassID, SectionID, 0, "");
                Studentlist.ToList().ForEach(p =>
                {
                    p.UnPaidMonth = getUnPaidMonth(p.PaidMonths, FromMonth, ToMonth);
                    _FeeHeadList = dal.GetFeeHeadListForFeeGroup(p.FeeGroup, SchoolId, p.Adminssionno, FinancialYearID);
                    p.PendingAmount = _FeeHeadList.Count() == 0 ? "N/A" : getTotalPaid(dal.GetFeeHeadListForFeeGroup(p.FeeGroup, SchoolId, p.Adminssionno, FinancialYearID), p.UnPaidMonth, FromMonth, ToMonth);
                });
            }
            catch (Exception ex)
            {

            }
            return Studentlist.Where(x => !string.IsNullOrEmpty(x.UnPaidMonth)).ToList();
        }
        private string getUnPaidMonth(string PaidMonth, int FromMonth, int ToMonth)
        {
            string month = "";
            try
            {
                for (int m = FromMonth; m <= ToMonth; m++)
                {
                    if (m == 1)
                    {
                        if (!PaidMonth.Contains("April"))
                            month += string.IsNullOrEmpty(month) ? "April" : ",April";
                    }
                    else if (m == 2)
                    {
                        if (!PaidMonth.Contains("May"))
                            month += string.IsNullOrEmpty(month) ? "May" : ",May";
                    }
                    else if (m == 3)
                    {
                        if (!PaidMonth.Contains("June"))
                            month += string.IsNullOrEmpty(month) ? "June" : ",June";
                    }
                    else if (m == 4)
                    {
                        if (!PaidMonth.Contains("July"))
                            month += string.IsNullOrEmpty(month) ? "July" : ",July";
                    }
                    else if (m == 5)
                    {
                        if (!PaidMonth.Contains("August"))
                            month += string.IsNullOrEmpty(month) ? "August" : ",August";
                    }
                    else if (m == 6)
                    {
                        if (!PaidMonth.Contains("September"))
                            month += string.IsNullOrEmpty(month) ? "September" : ",September";
                    }
                    else if (m == 7)
                    {
                        if (!PaidMonth.Contains("October"))
                            month += string.IsNullOrEmpty(month) ? "October" : ",October";
                    }
                    else if (m == 8)
                    {
                        if (!PaidMonth.Contains("November"))
                            month += string.IsNullOrEmpty(month) ? "November" : ",November";
                    }
                    else if (m == 9)
                    {
                        if (!PaidMonth.Contains("December"))
                            month += string.IsNullOrEmpty(month) ? "December" : ",December";
                    }
                    else if (m == 10)
                    {
                        if (!PaidMonth.Contains("January"))
                            month += string.IsNullOrEmpty(month) ? "January" : ",January";
                    }
                    else if (m == 11)
                    {
                        if (!PaidMonth.Contains("February"))
                            month += string.IsNullOrEmpty(month) ? "February" : ",February";
                    }
                    else if (m == 12)
                    {
                        if (!PaidMonth.Contains("March"))
                            month += string.IsNullOrEmpty(month) ? "March" : ",March";
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return month;
        }
        private string getTotalPaid(List<FeeHeadList> _FeeHeadList, string UnPaidMonth, int FromMonth, int ToMonth)
        {
            decimal TotalPendingAmount = 0;
            try
            {
                for (int m = FromMonth; m <= ToMonth; m++)
                {
                    if (m == 1)
                    {
                        if (UnPaidMonth.Contains("April"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Apr)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Apr));
                    }
                    else if (m == 2)
                    {
                        if (UnPaidMonth.Contains("May"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.May)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.May));
                    }
                    else if (m == 3)
                    {

                        if (UnPaidMonth.Contains("June"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Jun)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Jun));
                    }
                    else if (m == 4)
                    {
                        if (UnPaidMonth.Contains("July"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Jul)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Jul));
                    }
                    else if (m == 5)
                    {
                        if (UnPaidMonth.Contains("August"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Aug)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Aug));
                    }
                    else if (m == 6)
                    {
                        if (UnPaidMonth.Contains("September"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Sep)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Sep));
                    }
                    else if (m == 7)
                    {
                        if (UnPaidMonth.Contains("October"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Oct)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Oct));
                    }
                    else if (m == 8)
                    {
                        if (UnPaidMonth.Contains("November"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Nov)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Nov));
                    }
                    else if (m == 9)
                    {
                        if (UnPaidMonth.Contains("December"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Dec)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Dec));
                    }
                    else if (m == 10)
                    {
                        if (UnPaidMonth.Contains("January"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Jan)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Jan));
                    }
                    else if (m == 11)
                    {
                        if (UnPaidMonth.Contains("February"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Feb)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Feb));
                    }
                    else if (m == 12)
                    {
                        if (UnPaidMonth.Contains("March"))
                            TotalPendingAmount += _FeeHeadList.Where(x => x.Name != "Concession").Sum(x => Convert.ToDecimal(x.Mar)) - _FeeHeadList.Where(x => x.Name == "Concession").Sum(x => Convert.ToDecimal(x.Mar));
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return Convert.ToString(TotalPendingAmount);
        }

        public List<IndisciplineDetails> GetIndisciplineList(int SchoolID, int FinancialYearID)
        {

            DALFee dal = new DALFee(ConStr);
            return dal.GetIndisciplineList(SchoolID, FinancialYearID);
        }
        public string SaveIndiscipline(IndisciplineDetails ID)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.SaveIndiscipline(ID);
        }
        public IndisciplineDetails GetIndisciplineFeeDepositByReceiptNo(IndisciplineDetails FeeDepositDetails)
        {
            DALFee dal = new DALFee(ConStr);
            return dal.GetIndisciplineFeeDepositByReceiptNo(FeeDepositDetails);
        }
    }
}
