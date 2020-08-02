using SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WEB.AppCode;
using WEB.Models;
using WebMatrix.WebData;
using BAL;
using WEB.Filters;
using ERROR;
using System.IO;
using System.Data;
using System.Collections;
using System.Dynamic;
using ClosedXML.Excel;


namespace WEB.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [PageActionFilter]
    public class AdminController : Controller
    {
        //ChannelFactory<ICommonSrv> CF = new ChannelFactory<ICommonSrv>("COMMON");
        //ICommonSrv CSvc;
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        StringBuilder str = new StringBuilder();
        List<MenuMaster> menulist;
        BALCommon CSvc;
        BALRole _Crole;
        int _Financialyearid = 0;
        int _OrgnisationID = 0;
        string _FinacialOrgid = "";
        public AdminController()
        {
            //CSvc = (ICommonSrv)CF.CreateChannel();
            CSvc = new BALCommon(ConStr);
            _Crole = new BALRole(ConStr);
            SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
            _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
            _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);

        }

        public ActionResult Index()
        {
            return View();
        }

        #region StudentManagementSystem
        public ActionResult StudentList()
        {
            StudentModel model = new StudentModel();

            model.StudenttblList = CSvc.GetStudentTbl(model.CMid,model.SecMid,model.Startdate,model.Enddate, 1, _OrgnisationID, _Financialyearid);

            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            return View(model);
        }

        
        [HttpPost]
        public ActionResult StudentList(StudentModel model,FormCollection collection)
        {
            DataSet _dsstudentlist = CSvc.GetStudentTbl(model.CMid, model.SecMid, model.Startdate, model.Enddate, 1, _OrgnisationID, _Financialyearid);

            string commandType = collection["rptbtn"];
            if (commandType == "Export")
            {
                // return RedirectToAction("Downloadexcel","Admin",new { exportdata= model.StudenttblList.Tables[1] , headingtitle = "Student Records", downloadfilename= "StudentList" });
                // Downloadexcel(model.StudenttblList.Tables[1], "Student Records", "StudentList");
                if (_dsstudentlist.Tables[1] != null)
                {
                    string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Files";
                    bool exists = System.IO.Directory.Exists(folderpath);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(folderpath);
                    string filepath = folderpath + "\\" + Guid.NewGuid() + "_StudentList.xls";

                    CommonExport.ExportToExcel(_dsstudentlist.Tables[1], "Student Records", filepath);
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    if (System.IO.File.Exists(filepath))
                        System.IO.File.Delete(filepath);
                    return File(file, "application/vnd.ms-excel", "Student Records.xls");
                }

            }
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, model.CMid);
            return View(model);
        }


        [HttpPost]
        public ActionResult DeleteStudent(StudentModel model)
        {
            Boolean Isstatus = false;
            if (model != null && model.Smid != default(int))
            {
                StudentMaster _smaster  = CSvc.GetSingleStudent(model.Smid);
                 Isstatus = _smaster.IsActive;
                if (_smaster != null)
                {
                    if (Isstatus == false)
                    {
                        _smaster.IsActive = true;
                    }else
                    {
                        _smaster.IsActive = false;
                    }
                
                    int ret = CSvc.AddStudent(_smaster,"DEL");
                    if (ret > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = "Student Deactivated Successfully !";
                    else
                        TempData[Constants.MessageInfo.WARNING] = "Some problem contact to support team";
                }
                else
                {
                    TempData[Constants.MessageInfo.ERROR] = "Some problem contact to support team";
                }
            }
            else
            {
                TempData[Constants.MessageInfo.ERROR] = "Some problem contact to support team";
            }
            if (Isstatus == false)
            {
                return RedirectToAction("inactivestudent");
            }
            else
            {
                return RedirectToAction("StudentList");
            }
            
        }

        //Below use in future for export excel
        public ActionResult Downloadexcel(DataTable exportdata, string headingtitle, string downloadfilename)
        {
            string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Files";
            bool exists = System.IO.Directory.Exists(folderpath);
            if (!exists)
                System.IO.Directory.CreateDirectory(folderpath);
            string filepath = folderpath + "\\" + Guid.NewGuid() + "_" + downloadfilename + ".xls";

            CommonExport.ExportToExcel(exportdata, headingtitle, filepath);
            byte[] file = System.IO.File.ReadAllBytes(filepath);
            if (System.IO.File.Exists(filepath))
                System.IO.File.Delete(filepath);
            return File(file, "application/vnd.ms-excel", downloadfilename + ".xls");
        }

        public ActionResult Attendance(Nullable<int> userattendanceid)
        {
            AttendanceModel model = new AttendanceModel();
            string _getres = Attendancestr(2020, 1);
            model.Attendancesection = _getres;
            return View(model);
        }
        public string Attendancestr(int datayear, int upid)
        {
            StringBuilder ggetcal = new StringBuilder();
            int divider = 0;
            for (int i = 0; i < 12; i++)
            {
                if (i % 3 == 0)
                {
                    ggetcal.Append("<div class='col-md-12 col-sm-12 col-xs-12 no-padding margin-bottom'><!-- Start of row_1 -->");
                }
                int datamonth = i + 1;
                DateTime _dtvalue = new DateTime(datayear, datamonth, 1);
                int currentmonthfirstday = (int)_dtvalue.DayOfWeek;
                int totaldaysinmonth = DateTime.DaysInMonth(datayear, datamonth);
                int totalDaysOfMonthDisplay = currentmonthfirstday == 7 ? totaldaysinmonth : (totaldaysinmonth + currentmonthfirstday);
                string getmonthyear = _dtvalue.ToString("MMMM yyyy");
                int boxDisplay = (totalDaysOfMonthDisplay <= 35) ? 35 : 42;
                int mnths = i + 1;
                ggetcal.Append("<div class='col-md-4 col-sm-4 col-xs-12'><!-- Start of col_1 -->");
                ggetcal.Append("<h4 class='text-center TxtGreenClr'><strong>" + getmonthyear + " </strong></h4>");
                ggetcal.Append("<div id='calender_section_top'><ul><li>Sun</li><li>Mon</li><li>Tue</li><li>Wed</li><li>Thu</li><li>Fri</li><li>Sat</li></ul></div>");
                ggetcal.Append("<div id='calender_section_bot'><ul>");
                int holydayslist = 0;
                int userattendancereclist = 0;
                int P_count = 0;
                int A_count = 0;
                int L_count = 0;
                int H_count = 0;
                int HF_count = 0;
                int dayCount = 1;
                DateTime currentDate;
                for (int cb = 1; cb <= boxDisplay; cb++)
                {
                    if ((cb >= currentmonthfirstday + 1 || currentmonthfirstday == 7) && cb <= (totalDaysOfMonthDisplay))
                    {

                        currentDate = new DateTime(datayear, datamonth, dayCount);
                        string _currentDate = currentDate.ToString("yyyy-MM-dd");
                        int eventNum = 0;
                        int prenotification = 0;
                        //case check datet is exist in holiday
                        bool getreturnhlyday = true;//currentDate will go in holidaylist
                        if (!getreturnhlyday)
                        {
                            prenotification = 1;
                            H_count++;
                            ggetcal.Append("<li date='" + _currentDate + "' class='date_cell Bg-gray'>");
                            ggetcal.Append("<span>");
                            ggetcal.Append(dayCount.ToString());
                            ggetcal.Append("</span>");
                        }
                        else if (Utility.IsCheckdayno(2, currentDate, System.DayOfWeek.Saturday))
                        {
                            //checksecondsaturday  2nd
                            prenotification = 1;
                            H_count++;
                            ggetcal.Append("<li date='" + _currentDate + "' class='date_cell Bg-gray'>");
                            ggetcal.Append("<span>");
                            ggetcal.Append(dayCount.ToString());
                            ggetcal.Append("</span>");
                        }
                        else if (Utility.IsCheckdayno(4, currentDate, System.DayOfWeek.Saturday))
                        {
                            //checksecondsaturday   4th
                            prenotification = 1;
                            H_count++;
                            ggetcal.Append("<li date='" + _currentDate + "' class='date_cell Bg-gray'>");
                            ggetcal.Append("<span>");
                            ggetcal.Append(dayCount.ToString());
                            ggetcal.Append("</span>");
                        }
                        else if (System.DayOfWeek.Sunday == currentDate.DayOfWeek)
                        {
                            //checksecondsaturday   sunday
                            prenotification = 1;
                            H_count++;
                            ggetcal.Append("<li date='" + _currentDate + "' class='date_cell Bg-gray'>");
                            ggetcal.Append("<span>");
                            ggetcal.Append(dayCount.ToString());
                            ggetcal.Append("</span>");
                        }
                        else
                        {
                            int leavesec = 0;
                            //this section for user attendance
                        }

                        if (prenotification == 0)
                        {
                            ggetcal.Append("<li date='" + _currentDate + "' class='date_cell'>");
                            ggetcal.Append("<span>");
                            ggetcal.Append(dayCount.ToString());
                            ggetcal.Append("</span>");
                        }
                        ggetcal.Append("</li></a>");
                        dayCount++;
                    }
                    else
                    {
                        ggetcal.Append("<li><span>&nbsp;</span></li>");
                    }

                }
                ggetcal.Append("</ul>");
                ggetcal.Append("</div>");
                ggetcal.Append("<div class='col-md-12 col-sm-12 col-xs-12 margin-top'><!-- Start of table_div -->");
                ggetcal.Append("<div id='responsive-mobile-table1' class='col-xs-12 col-md-12 col-sm-12 no-padding fixedtable'>");
                ggetcal.Append("<table class='table table-bordered no-padding '><!-- start of table-->");
                ggetcal.Append("<tbody>");
                ggetcal.Append("<tr class=''><!-- start of row1 -->");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>Present</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<span class='Square_box_green'></span>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>'" + P_count + "'</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("</tr><!-- end of row1 -->");
                ggetcal.Append("<tr class=''><!-- start of row2 -->");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>Absent</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<span class='Square_box_red'></span>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>'" + A_count + "'</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("</tr><!-- end of row2 -->");
                ggetcal.Append("<tr class=''><!-- start of row3 -->");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>Late</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<span class='Square_box_orange'></span>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>'" + L_count + "'</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("</tr><!-- end of row3 --> ");
                ggetcal.Append("<tr class=''><!-- start of row4-->");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>Holiday</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<span class='Square_box_gray'></span>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>'" + H_count + "'</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("</tr><!-- end of row4 -->     ");
                ggetcal.Append("<tr class=''><!-- start of row4-->");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>Half Day</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<span class='Square_box_purple'></span>");
                ggetcal.Append("</td>");
                ggetcal.Append("<td data-title='' class='TxtGrayClr'>");
                ggetcal.Append("<strong>'" + HF_count + "'</strong>");
                ggetcal.Append("</td>");
                ggetcal.Append("</tr><!-- end of row4 -->     ");
                ggetcal.Append("</tbody>");
                ggetcal.Append("</table><!-- End of table -->");
                ggetcal.Append("</div>");
                ggetcal.Append("</div><!-- end of table_div -->");
                ggetcal.Append("");
                ggetcal.Append("<div class='col-md-12 col-sm-12 col-xs-12 no-padding margin-md-bottom text-right'><!-- start of print_button -->");
                ggetcal.Append("<a class='btn btn- div text - uppercase' name='printdata' id='printdata' target='blank' href='.$baseurl.'><i class='fa fa - print' aria-hidden='true'></i> Print</a>");
                ggetcal.Append("</div>");
                ggetcal.Append("");
                ggetcal.Append("</div><!-- end of col_1 -->';");

                if ((i + 1) % 3 == 0 && i > 0)
                {
                    ggetcal.Append("</div><!-- end of row_1 -->");

                }
            }



            //ggetcal.Append("text");

            return ggetcal.ToString();
        }
        public JsonResult GetStudentList(string ClassID, string SectionID)
        {
            List<StudentMaster> StudentList = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, Convert.ToInt32(ClassID), Convert.ToInt32(SectionID), 0, "");
            return Json(StudentList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateStudent(Nullable<int> StudentId, string ActionName = "")
        {
            StudentModel model = new StudentModel();
            StudentMaster master = new StudentMaster();
            if (StudentId.HasValue)
            {
                master = CSvc.GetSingleStudent(StudentId.Value);
                model.Smid = master.Smid;
                model.Adminssionno = master.Adminssionno;
                model.Enquiryno = master.Enquiryno;
                model.Firstname = master.Firstname;
                model.CMid = master.CMid;
                model.SecMid = master.SecMid;
                model.RollNo = master.RollNo;
                model.RouteMid = master.RouteMid;
                model.BusMid = master.BusMid;
                model.Castmid = master.Castmid;
                model.Categmid = master.Categmid;
                model.HouseMid = master.HouseMid;
                model.Relmid = master.Relmid;
                model.GMid = master.GMid;
                model.CITY_ID = master.CITY_ID;
                model.STATE_ID = master.STATE_ID;
                model.COUNTRY_ID = master.COUNTRY_ID;
                model.Bd_address1 = master.Bd_address1;
                model.Bd_address2 = master.Bd_address2;
                model.Bd_City = master.Bd_City;
                model.Bd_contactno = master.Bd_contactno;
                model.Bd_dob = master.Bd_dob;
                model.Bd_fathername = master.Bd_fathername;
                model.Bd_fathermob = master.Bd_fathermob;
                model.Bd_qualification = master.Bd_qualification;
                model.Bd_fatheroccuption = master.Bd_fatheroccuption;
                model.Bd_fathdob = master.Bd_fathdob;
                model.Bd_mothername = master.Bd_mothername;
                model.Bd_mothermob = master.Bd_mothermob;
                model.Bd_motherqualification = master.Bd_motherqualification;
                model.Bd_Motheroccuption = master.Bd_Motheroccuption;
                model.Bd_Mothredob = master.Bd_Mothredob;
                model.Bd_dateofannversy = master.Bd_dateofannversy;
                model.Bd_Emailid = master.Bd_Emailid;
                model.Off_lastschool = master.Off_lastschool;
                model.Off_remarks = master.Off_remarks;
                model.Off_Examgiven = master.Off_Examgiven;
                model.Off_Year = master.Off_Year;
                model.Off_Status = master.Off_Status;
                model.Off_marks = master.Off_marks;
                model.Off_boardoruniversity = master.Off_boardoruniversity;
                model.Off_bloodgroup = master.Off_bloodgroup;
                model.VisionLeft = master.VisionLeft;
                model.Off_grno = master.Off_grno;
                model.Off_Visionright = master.Off_Visionright;
                model.Off_heightweight = master.Off_heightweight;
                model.Off_Dentailhygine = master.Off_Dentailhygine;
                model.Off_Hosalroomno = master.Off_Hosalroomno;
                model.Off_bedno = master.Off_bedno;
                model.Off_Scholarshipno = master.Off_Scholarshipno;
                model.Off_TC = master.Off_TC;
                model.Off_CC = master.Off_CC;
                model.Off_ReportC = master.Off_ReportC;
                model.Off_Dobcertificate = master.Off_Dobcertificate;
                model.Off_admissionno = master.Off_admissionno;
                model.Off_dateofadmission = master.Off_dateofadmission;
                model.Off_Ledgerbalance = master.Off_Ledgerbalance;
                model.Off_feesbalance = master.Off_feesbalance;
                model.Off_Comments = master.Off_Comments;
                model.Off_Aadharno = master.Off_Aadharno;
                model.Off_biometricno = master.Off_biometricno;
                model.Off_childuniqueno = master.Off_childuniqueno;
                model.Off_sessionno = master.Off_sessionno;
                model.Off_family = master.Off_family;
                model.Off_stausinschool = master.Off_stausinschool;
                model.Off_discontinuethedate = master.Off_discontinuethedate;
                model.studentimage = master.studentimage;
                model.motherimage = master.motherimage;
                model.fatherimage = master.fatherimage;
                model.FeeGroup = master.FeeGroup;

            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.Adminssionno = CSvc.Applicationfprmano(_OrgnisationID, 0, 0, _Financialyearid);
            model.GendraList = CSvc.GetGendarList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.ReligionList = CSvc.GetReligionList(_OrgnisationID);
            model.CastList = CSvc.GetCastList(_OrgnisationID);
            model.Categorylist = CSvc.GetCategoryMasterList(_OrgnisationID);
            // model.Routelist = CSvc.GetRouteList(_OrgnisationID);
            //model.Buslist = CSvc.GetBuslist(_OrgnisationID);
            model.Countrylist = CSvc.GetCountryList(_OrgnisationID);
            model.Statelist = CSvc.GetStateList(1, _OrgnisationID);
            model.Citylist = CSvc.GetCityList(1, _OrgnisationID);
            model.Feegrouplist = CSvc.GetFeegroupList(_OrgnisationID);
            model.BloodGroupList = CSvc.GetBloodGroupList(_OrgnisationID);
            model.GetAddmissionEnqlist = CSvc.GetAdmissionEnquiryList(_Financialyearid, _OrgnisationID);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateStudent(StudentModel model, FormCollection collection)
        {

            if (collection["btntype"] == "Create")
            {
                if (CSvc.GetStudentbaseonAdmissionno(model.Adminssionno, _OrgnisationID).Rows.Count > 0)
                {
                    TempData[Constants.MessageInfo.WARNING] = Constants.Student_AlreadyExist_SUCCESS;
                    return RedirectToAction("StudentList");
                }
            }
            StudentMaster master = new StudentMaster();
            if (model.Smid > 0)
            {
                master = CSvc.GetSingleStudent(model.Smid);
            }

            HttpPostedFileBase studentimage1 = Request.Files["studentimage"];
            HttpPostedFileBase fatherimage1 = Request.Files["fatherimage"];
            HttpPostedFileBase motherimage1 = Request.Files["motherimage"];
            HttpPostedFileBase studentimgaa = Request.Files["canvas"];
            if (studentimage1.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(studentimage1.FileName);
                string folderpath = Constants.STUDENT_IMAGE;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);

                studentimage1.SaveAs(filepath);
                master.studentimage = dbpath;

            }
            if (fatherimage1.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(fatherimage1.FileName);
                string folderpath = Constants.STUDENT_IMAGE;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                fatherimage1.SaveAs(filepath);
                master.fatherimage = dbpath;
            }
            if (motherimage1.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(motherimage1.FileName);
                string folderpath = Constants.STUDENT_IMAGE;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                motherimage1.SaveAs(filepath);
                master.motherimage = dbpath;
            }

            master.Smid = model.Smid;
            master.Adminssionno = model.Adminssionno;
            master.Enquiryno = model.Enquiryno;
            master.Firstname = model.Firstname;
            master.CMid = model.CMid;
            master.SecMid = model.SecMid;
            master.RollNo = model.RollNo;
            master.RouteMid = model.RouteMid;
            master.BusMid = model.BusMid;
            master.Castmid = model.Castmid;
            master.Categmid = model.Categmid;
            master.HouseMid = model.HouseMid;
            master.Relmid = model.Relmid;
            master.GMid = model.GMid;
            master.CITY_ID = model.CITY_ID;
            master.STATE_ID = model.STATE_ID;
            master.COUNTRY_ID = model.COUNTRY_ID;
            master.Bd_address1 = model.Bd_address1;
            master.Bd_address2 = model.Bd_address2;
            master.Bd_City = model.Bd_City;
            master.Bd_contactno = model.Bd_contactno;
            master.Bd_dob = model.Bd_dob;
            master.Bd_fathername = model.Bd_fathername;
            master.Bd_fathermob = model.Bd_fathermob;
            master.Bd_qualification = model.Bd_qualification;
            master.Bd_fatheroccuption = model.Bd_fatheroccuption;
            master.Bd_fathdob = model.Bd_fathdob;
            master.Bd_mothername = model.Bd_mothername;
            master.Bd_mothermob = model.Bd_mothermob;
            master.Bd_motherqualification = model.Bd_motherqualification;
            master.Bd_Motheroccuption = model.Bd_Motheroccuption;
            master.Bd_Mothredob = model.Bd_Mothredob;
            master.Bd_dateofannversy = model.Bd_dateofannversy;
            master.Bd_Emailid = model.Bd_Emailid;
            master.Off_lastschool = model.Off_lastschool;
            master.Off_remarks = model.Off_remarks;
            master.Off_Examgiven = model.Off_Examgiven;
            master.Off_Year = model.Off_Year;
            master.Off_Status = model.Off_Status;
            master.Off_marks = model.Off_marks;
            master.Off_boardoruniversity = model.Off_boardoruniversity;
            master.Off_bloodgroup = model.Off_bloodgroup;
            master.VisionLeft = model.VisionLeft;
            master.Off_grno = model.Off_grno;
            master.Off_Visionright = model.Off_Visionright;
            master.Off_heightweight = model.Off_heightweight;
            master.Off_Dentailhygine = model.Off_Dentailhygine;
            master.Off_Hosalroomno = model.Off_Hosalroomno;
            master.Off_bedno = model.Off_bedno;
            master.Off_Scholarshipno = model.Off_Scholarshipno;
            master.Off_TC = collection["chk_tc"] != null ? "Y" : "N";// model.Off_TC;
            master.Off_CC = collection["chk_cc"] != null ? "Y" : "N"; //model.Off_CC;
            master.Off_ReportC = collection["chk_tc"] != null ? "Y" : "N";// model.Off_ReportC;
            master.Off_Dobcertificate = collection["chk_dob_certificate"] != null ? "Y" : "N";
            master.Off_admissionno = model.Off_admissionno;
            master.Off_dateofadmission = model.Off_dateofadmission;
            master.Off_Ledgerbalance = model.Off_Ledgerbalance;
            master.Off_feesbalance = model.Off_feesbalance;
            master.Off_Comments = model.Off_Comments;
            master.Off_Aadharno = model.Off_Aadharno;
            master.Off_biometricno = model.Off_biometricno;
            master.Off_nationality = model.Off_nationality;
            master.Off_childuniqueno = model.Off_childuniqueno;
            master.Off_sessionno = model.Off_sessionno;
            master.Off_family = model.Off_family;
            master.Off_stausinschool = model.Off_stausinschool;
            master.Off_discontinuethedate = model.Off_discontinuethedate;
            master.FinancialYear = _Financialyearid;
            master.FeeGroup = model.FeeGroup;
            //master.studentimage = model.studentimage;
            // master.motherimage = model.motherimage;
            //master.fatherimage = model.fatherimage;

            if (collection["btntype"] == "Create")
            {
                master.IsActive = true;
                master.SchoolID = _OrgnisationID;
                master.Createdate = DateTime.Now;
                master.Createdby = WebSecurity.CurrentUserName;
                master.Modifiedby = WebSecurity.CurrentUserName;
                master.Modifieddate = DateTime.Now;
                int returntype = CSvc.AddStudent(master, "INS");
                if (returntype > 0)
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Student_Add_SUCCESS;
                }
                return RedirectToAction("StudentList");
            }
            else if (collection["btntype"] == "Update")
            {
                //  master.IsActive = true;
                //  master.SchoolID = _OrgnisationID;
                master.Modifiedby = WebSecurity.CurrentUserName;
                master.Modifieddate = DateTime.Now;
                int returntype = CSvc.AddStudent(master, "UPD");
                TempData[Constants.MessageInfo.SUCCESS] = Constants.Student_UPDATE_SUCCESS;
                return RedirectToAction("StudentList");
            }
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.GendraList = CSvc.GetGendarList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.ReligionList = CSvc.GetReligionList(_OrgnisationID);
            model.CastList = CSvc.GetCastList(_OrgnisationID);
            model.Categorylist = CSvc.GetCategoryMasterList(_OrgnisationID);
            model.Routelist = CSvc.GetRouteList(_OrgnisationID);
            model.Buslist = CSvc.GetBuslist(_OrgnisationID);
            model.Countrylist = CSvc.GetCountryList(_OrgnisationID);
            // model.Statelist = CSvc.GetStateList(model.COUNTRY_ID 1, _OrgnisationID);
            // model.Citylist = CSvc.GetCityList(1, _OrgnisationID);
            return View(model);
        }

        public JsonResult GetsectionbaseonclassId(int classId)
        {
            string _Applicationfrmno = CSvc.Applicationfprmano(_OrgnisationID, classId, 0, _Financialyearid);
            List<SectionMaster> SectionList = CSvc.GetSectionList(_OrgnisationID, classId);
            Hashtable _hstbl = new Hashtable();
            _hstbl.Add("frmno", _Applicationfrmno);
            _hstbl.Add("seclst", SectionList.Where(x => x.CMid == classId).ToList());
            return Json(_hstbl, JsonRequestBehavior.AllowGet);
            //_hstbl return Json(SectionList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getapplicationformno(int classId, int secid)
        {
            string _Applicationfrmno = CSvc.Applicationfprmano(_OrgnisationID, classId, secid, _Financialyearid);
            return Json(_Applicationfrmno, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region UserManagementSystem
        public ActionResult UserList()
        {
            UserModel model = new UserModel();
            model.UserList = CSvc.GetAllUser(_OrgnisationID);
            return View(model);
        }

        public ActionResult CreateUser(Nullable<int> UserId, string ActionName = "")
        {
            UserModel model = new UserModel();
            UserMasters _umaster = new UserMasters();
            model.RoleList = CSvc.GetRoleList(_OrgnisationID);
            model.UserTypeList = CSvc.UsertypeMasterlist(_OrgnisationID);
            model.EmployeeList = CSvc.GetEmployeeList(1, _OrgnisationID);
            model.StudentList = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, 0, 0, 0, "");
            if (UserId.HasValue)
            {
                _umaster = CSvc.GetByUserId(UserId.Value);
                model.UserName = _umaster.USERNAME;
                model.Password = _umaster.PASSWORD;
                model.Role_Id = _umaster.ROLE_ID.Value;
                model.IsActive = _umaster.ISACTIVE;
                model.User_Id = _umaster.UserId;
                model.UsertypebaseId = _umaster.UsertypebaseId;
                model.Usertype = _umaster.Usertype;
                // _umaster.UsertypebaseId = model.Student_Id.Value;
                if (model.UserTypeList.Where(x => x.UTMID == model.Usertype).Select(x => x.UName).FirstOrDefault().ToUpper() == "Student".ToUpper())
                {
                    model.Student_Id = model.UsertypebaseId;
                }
                else if (model.UserTypeList.Where(x => x.UTMID == model.Usertype).Select(x => x.UName).FirstOrDefault().ToUpper() == "Employee".ToUpper())
                {
                    model.Employee_Id = model.UsertypebaseId;
                }

            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;


            return View(model);
        }

        [HttpPost]
        public ActionResult CreateUser(UserModel model, FormCollection collection)
        {
            UserMasters _umaster = new UserMasters();
            if (model.Employee_Id != null && model.Employee_Id > 0)
            {
                EmployeeMaster _empsigle = CSvc.GetSingleEmployee(Convert.ToInt32(model.Employee_Id));
                _umaster.FISRTNAME = _empsigle.FIRSTNAME;
                _umaster.Mobile = _empsigle.MOBILENUMBER;
                _umaster.EMAILID = _empsigle.EMAIL;
                _umaster.ADDRESS = "--";
                _umaster.USERNAME = _empsigle.EMAIL;
                _umaster.UsertypebaseId = Convert.ToInt32(model.Employee_Id);
            }
            else if (model.Student_Id != null && model.Student_Id > 0)
            {
                StudentMaster _singstudent = CSvc.GetSingleStudent(Convert.ToInt32(model.Student_Id));
                _umaster.FISRTNAME = _singstudent.Firstname;
                _umaster.Mobile = _singstudent.Bd_contactno;
                _umaster.EMAILID = _singstudent.Bd_Emailid;
                _umaster.ADDRESS = "--";
                _umaster.USERNAME = _singstudent.Bd_Emailid;
                _umaster.UsertypebaseId = Convert.ToInt32(model.Student_Id);
            }
            _umaster.SchoolID = _OrgnisationID;
            _umaster.ROLE_ID = model.Role_Id;
            _umaster.ISACTIVE = true;
            if (collection["btntype"] == "Create")
            {
                if (!string.IsNullOrEmpty(_umaster.FISRTNAME) && !string.IsNullOrEmpty(_umaster.EMAILID) && !string.IsNullOrEmpty(_umaster.Mobile))
                {
                    UserMasters _umasterchk = CSvc.getUserProfile(_umaster.USERNAME);
                    if (string.IsNullOrEmpty(_umasterchk.USERNAME))
                    {
                        string Password = _umaster.Mobile.Length >= 10 ? Utility.Right(_umaster.Mobile, 10) : _umaster.Mobile;
                        WebSecurity.CreateUserAndAccount(_umaster.USERNAME, Password, new { Name = _umaster.FISRTNAME, UserType = model.Usertype, Mobile = _umaster.Mobile, EmailId = _umaster.EMAILID, Address = _umaster.ADDRESS, RoleId = model.Role_Id, CITY_ID = 1, STATE_ID = 1, COUNTRY_ID = 1, ISACTIVE = _umaster.ISACTIVE, Status = _umaster.ISACTIVE, UsertypebaseId = _umaster.UsertypebaseId, SchoolID = _OrgnisationID });

                        #region SendMail
                        MailDetails _MailDetails = new MailDetails();
                        _MailDetails.ToMailIDs = _umaster.USERNAME;
                        _MailDetails.HTMLBody = true;
                        _MailDetails.Subject = "User Registration";
                        _MailDetails.Body = BALMail.TemplateUserRegistration(_umaster, AESEncrytDecry.EncryptStringAES(Password));
                        if (BALMail.SendMail(_MailDetails))
                        {
                            TempData[Constants.MessageInfo.SUCCESS] = Constants.User_ADD_SUCCESS + "  |  User Name is: " + _umaster.USERNAME + ", Please check your mail inbox for more information.";
                        }
                        else
                        {
                            TempData[Constants.MessageInfo.SUCCESS] = Constants.User_ADD_SUCCESS + "  |  User Name is: " + _umaster.USERNAME;
                        }
                        #endregion SendMail
                        return RedirectToAction("Userlist");
                    }
                    else
                    {
                        TempData[Constants.MessageInfo.WARNING] = Constants.User_AlreadyExist_SUCCESS + "  |  User Name is: " + _umaster.USERNAME;
                        return RedirectToAction("Userlist");
                    }
                }
                else
                {
                    TempData[Constants.MessageInfo.WARNING] = "First Name ,EmailId and Mobile No are  Required for User ! ";
                    return RedirectToAction("Userlist");
                }
            }
            else
            {
                _umaster.UserId = model.User_Id;
                int ret = CSvc.UpdateUser(_umaster);
                if (ret > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.User_UPDATE_SUCCESS + "  |  User Name is: " + _umaster.USERNAME;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.User_UPDATE_FAILED + "  |  User Name is: " + _umaster.USERNAME;
                return RedirectToAction("Userlist");
            }

            model.RoleList = CSvc.GetRoleList(_OrgnisationID);
            model.UserTypeList = CSvc.UsertypeMasterlist(_OrgnisationID);
            model.EmployeeList = CSvc.GetEmployeeList(1, _OrgnisationID);
            model.StudentList = CSvc.GetStudentList(1, _OrgnisationID, 0, 0, 0, "");
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteUser(UserModel model)
        {
            if (model != null && model.User_Id != default(int))
            {
                UserMasters _umaster = CSvc.GetByUserId(model.User_Id);
                if (_umaster != null)
                {
                    _umaster.ISACTIVE = false;
                    int ret = CSvc.DeleteUser(_umaster);
                    if (ret > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.User_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.WARNING] = Constants.User_DELETE_FAIL;
                }
                else
                {
                    TempData[Constants.MessageInfo.ERROR] = Constants.User_DELETE_FAIL;
                }
            }
            else
            {
                TempData[Constants.MessageInfo.ERROR] = Constants.User_DELETE_FAIL;
            }
            return RedirectToAction("Userlist");
        }
        public JsonResult CheckIsUserCodeExist(string userCode)
        {
            UserMasters _umasters = CSvc.getUserProfile(userCode);
            bool IsExsist = _umasters.USERNAME != null ? true : false;
            return Json(IsExsist, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region EMPLOYEEMANAGEMENTSYSTEM
        public ActionResult EmployeeList()
        {
            EmployeeModel model = new EmployeeModel();
            model.EmployeeTBLList = CSvc.GetEmployeeTbl(1, _OrgnisationID, _Financialyearid);
            return View(model);
        }
        public ActionResult CreateEmployee(Nullable<int> EmpId, string ActionName = "")
        {
            EmployeeModel model = new EmployeeModel();
            EmployeeMaster master = new EmployeeMaster();
            if (EmpId.HasValue)
            {
                master = CSvc.GetSingleEmployee(Convert.ToInt32(EmpId));
                model.EMP_ID = master.EMP_ID;
                model.EMPCODE = master.EMPCODE;
                model.ISACTIVE = master.ISACTIVE;
                model.DEPARTMENT_ID = master.DEPARTMENT_ID;
                model.DESIGNATION_ID = master.DESIGNATION_ID;
                model.CONTACTTITLE = master.CONTACTTITLE;
                model.FIRSTNAME = master.FIRSTNAME;
                model.MIDDLENAME = master.MIDDLENAME;
                model.LASTNAME = master.LASTNAME;
                model.MOBILENUMBER = master.MOBILENUMBER;
                model.EMAIL = master.EMAIL;
                model.BIRTHDATE = master.BIRTHDATE;
                model.GENDER = master.GENDER;
                model.BLOODGROUP = master.BLOODGROUP;
                model.MARITALSTATUS = master.MARITALSTATUS;
                model.QUALIFICATION1 = master.QUALIFICATION1;
                model.QUALIFICATION2 = master.QUALIFICATION2;
                model.JOININGDATE = master.JOININGDATE;
                model.CONFIRMATIONDATE = master.CONFIRMATIONDATE;
                model.LEAVINGDATE = master.LEAVINGDATE;
                model.SALARY = master.SALARY;
                model.BANKACNO = master.BANKACNO;
                model.BANKNAME = master.BANKNAME;
                model.BANKBRANCH = master.BANKBRANCH;
                model.PFNO = master.PFNO;
                model.PANNO = master.PANNO;
                model.REMARKS = master.REMARKS;
                model.CREATEDBY = master.CREATEDBY;
                model.CREATEDDATE = master.CREATEDDATE;
                model.MODIFIEDBY = master.MODIFIEDBY;
                model.MODIFIEDDATE = master.MODIFIEDDATE;
                model.FATHER_SPOUSE = master.FATHER_SPOUSE;
                model.PF_ESTD_CODE = master.PF_ESTD_CODE;
                model.PF_UAN = master.PF_UAN;
                model.VPF_CONTB_RATE = master.VPF_CONTB_RATE;
                model.IFSC_CODE = master.IFSC_CODE;
                model.LEAVE_PL_ENTITLED = master.LEAVE_PL_ENTITLED;
                model.LEAVE_CL_ENTITLED = master.LEAVE_CL_ENTITLED;
                model.LEAVE_SL_ENTITLED = master.LEAVE_SL_ENTITLED;
                // model.TOTAL_LEAVES_ENTITLED = master.TOTAL_LEAVES_ENTITLED;
                model.GROSS_BASIC = master.GROSS_BASIC;
                model.GROSS_HRA = master.GROSS_HRA;
                model.GROSS_CONVEYANCE = master.GROSS_CONVEYANCE;
                model.GROSS_CHILDREN_EDUCATION = master.GROSS_CHILDREN_EDUCATION;
                model.GROSS_UNIFORM_MAINTENANCE = master.GROSS_UNIFORM_MAINTENANCE;
                model.GROSS_CAR_AMOUNT = master.GROSS_CAR_AMOUNT;
                model.GROSS_SPECIAL_ALLOWANCE = master.GROSS_SPECIAL_ALLOWANCE;
                model.GROSS_SALARY = master.GROSS_SALARY;
                model.EMERGENCY_CONT_PRS = master.EMERGENCY_CONT_PRS;
                model.EMERGENCY_CONT_NO = master.EMERGENCY_CONT_NO;
                model.SelectedRManagerId = master.ParentID;
                model.PANIMGPATH = master.PANIMGPATH;
                model.EMPIMGPATH = master.EMPIMGPATH;
                model.SPOUSE = master.SPOUSE;
                model.SchoolID = master.SchoolID;
            }
            else
            {
                model.EMPCODE = CSvc.GetEmpCodeId(_OrgnisationID);
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.DepartmentList = CSvc.GetDepartmentList(_OrgnisationID);
            model.DesignationList = CSvc.GetDesignationList(_OrgnisationID);
            model.ContactList = CSvc.GetContactList(_OrgnisationID);
            model.GenderList = CSvc.GetGendarList(_OrgnisationID); ;
            model.BloodGroupList = CSvc.GetBloodGroupList(_OrgnisationID);
            model.MaritalStatusList = CSvc.GetMaritalStatusList(_OrgnisationID);
            model.ReportingManagerList = CSvc.GetEmployeeList(1, _OrgnisationID);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateEmployee(EmployeeModel model, FormCollection collection)
        {
            EmployeeMaster master = new EmployeeMaster();
            if (collection["btntype"] == "Create")
            {
                //DataTable emp_dtlist = CSvc.GetEmployeeTbl(1, _OrgnisationID, _Financialyearid).Tables[0];
                ///*--------for Mobile No Already Exist-----------*/
                //var dtmobilechk = from row in emp_dtlist.AsEnumerable()
                //                  where row.Field<string>("Mobile Number").ToLower() == model.MOBILENUMBER.ToLower()
                //                  select row;
                //if (dtmobilechk.ToList().Count > 0)
                //{
                //    TempData[Constants.MessageInfo.WARNING] = "Mobile No is Already Exist !";
                //    return RedirectToAction("EmployeeList");
                //}
                ///*--------End for Mobile No Already Exist-----------*/
                ///*--------for EmailID No Already Exist-----------*/
                //var dtemailidchk = from row in emp_dtlist.AsEnumerable()
                //                   where row.Field<string>("Email Id").ToLower() == model.EMAIL.ToLower()
                //                   select row;
                //if (dtemailidchk.ToList().Count > 0)
                //{
                //    TempData[Constants.MessageInfo.WARNING] = "Email Id is Already Exist !";
                //    return RedirectToAction("EmployeeList");
                //}
                /*--------End for Mobile No Already Exist-----------*/
                if (CSvc.ValidateEmailExist(model.EMAIL.ToLower(), "Emp", _OrgnisationID))
                {
                    TempData[Constants.MessageInfo.WARNING] = "Email Id is Already Exist !";
                    return RedirectToAction("EmployeeList");
                }
            }
            if (model.EMP_ID > 0)
            {
                master = CSvc.GetSingleEmployee(Convert.ToInt32(model.EMP_ID));
            }
            HttpPostedFileBase employeephotoimge = Request.Files["employeephotoimge"];
            HttpPostedFileBase pancardimge = Request.Files["pancardimge"];
            if (employeephotoimge.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(employeephotoimge.FileName);
                string folderpath = Constants.Employee_IMAGEPAN;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                employeephotoimge.SaveAs(filepath);
                master.EMPIMGPATH = dbpath;
            }
            if (pancardimge.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(pancardimge.FileName);
                string folderpath = Constants.Employee_IMAGEPAN;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                pancardimge.SaveAs(filepath);
                master.PANIMGPATH = dbpath;
            }
            master.EMP_ID = model.EMP_ID;
            master.EMPCODE = model.EMPCODE;
            master.ISACTIVE = model.ISACTIVE;
            master.DEPARTMENT_ID = model.DEPARTMENT_ID;
            master.DESIGNATION_ID = model.DESIGNATION_ID;
            master.CONTACTTITLE = model.CONTACTTITLE;
            master.FIRSTNAME = model.FIRSTNAME;
            master.MIDDLENAME = model.MIDDLENAME;
            master.LASTNAME = model.LASTNAME;
            master.MOBILENUMBER = model.MOBILENUMBER;
            master.EMAIL = model.EMAIL;
            master.BIRTHDATE = model.BIRTHDATE;
            master.GENDER = model.GENDER;
            master.BLOODGROUP = model.BLOODGROUP;
            master.MARITALSTATUS = model.MARITALSTATUS;
            master.QUALIFICATION1 = model.QUALIFICATION1;
            master.QUALIFICATION2 = model.QUALIFICATION2;
            master.JOININGDATE = model.JOININGDATE;
            master.CONFIRMATIONDATE = model.CONFIRMATIONDATE;
            master.LEAVINGDATE = model.LEAVINGDATE;
            master.SALARY = model.SALARY;
            master.BANKACNO = model.BANKACNO;
            master.BANKNAME = model.BANKNAME;
            master.BANKBRANCH = model.BANKBRANCH;
            master.PFNO = model.PFNO;
            master.PANNO = model.PANNO;
            master.REMARKS = model.REMARKS;
            master.CREATEDBY = WebSecurity.CurrentUserName;
            master.CREATEDDATE = DateTime.Now;
            master.MODIFIEDBY = WebSecurity.CurrentUserName;
            master.MODIFIEDDATE = DateTime.Now;
            master.FATHER_SPOUSE = model.FATHER_SPOUSE;
            master.PF_ESTD_CODE = model.PF_ESTD_CODE;
            master.PF_UAN = model.PF_UAN;
            master.VPF_CONTB_RATE = model.VPF_CONTB_RATE;
            master.IFSC_CODE = model.IFSC_CODE;
            master.LEAVE_PL_ENTITLED = model.LEAVE_PL_ENTITLED;
            master.LEAVE_CL_ENTITLED = model.LEAVE_CL_ENTITLED;
            master.LEAVE_SL_ENTITLED = model.LEAVE_SL_ENTITLED;
            master.GROSS_BASIC = model.GROSS_BASIC;
            master.GROSS_HRA = model.GROSS_HRA;
            master.GROSS_CONVEYANCE = model.GROSS_CONVEYANCE;
            master.GROSS_CHILDREN_EDUCATION = model.GROSS_CHILDREN_EDUCATION;
            master.GROSS_UNIFORM_MAINTENANCE = model.GROSS_UNIFORM_MAINTENANCE;
            master.GROSS_CAR_AMOUNT = model.GROSS_CAR_AMOUNT;
            master.GROSS_SPECIAL_ALLOWANCE = model.GROSS_SPECIAL_ALLOWANCE;
            master.GROSS_SALARY = model.GROSS_SALARY;
            master.EMERGENCY_CONT_PRS = model.EMERGENCY_CONT_PRS;
            master.EMERGENCY_CONT_NO = model.EMERGENCY_CONT_NO;
            master.ParentID = Convert.ToInt32(model.SelectedRManagerId);
            master.SPOUSE = model.SPOUSE;
            master.SchoolID = _OrgnisationID;
            if (collection["btntype"] == "Create")
            {

                int returntype = CSvc.AddEmployee(master, "INS");
                if (returntype > 0)
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Employee_Add_SUCCESS;
                }
                else
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Employee_ADD_FAIL;
                }
                return RedirectToAction("EmployeeList");
            }
            else if (collection["btntype"] == "Update")
            {
                int returntype = CSvc.AddEmployee(master, "UPD");
                if (returntype == 0)
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Employee_UPDATE_SUCCESS;
                }
                else
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Employee_ADD_FAIL;
                }
                return RedirectToAction("EmployeeList");
            }

            return View(model);
        }
        #endregion

        #region RoleManagement
        public ActionResult RoleList()
        {

            RoleModel model = new RoleModel();
            model.RoleList = CSvc.GetRoleList(_OrgnisationID);
            return View(model);
        }

        public ActionResult CreateRole(Nullable<int> RoleId, string ActionName = "")
        {
            RoleModel model = new RoleModel();
            RoleMaster existingRole = new RoleMaster();
            if (RoleId.HasValue)
            {
                existingRole = _Crole.GetByRoleId(RoleId.Value, _OrgnisationID);
                model.role_id = existingRole.ROLE_ID;
                model.RoleName = existingRole.ROLENAME;
                model.RoleDescription = existingRole.ROLEDESCRIPTION;
                if (existingRole.ISACTIVE == true)
                {
                    model.IsActive = true;
                }
                else
                {
                    model.IsActive = false;
                }
                model.selectedMenuPermission = existingRole.MenuPermissionList;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            menulist = CSvc.GetAllMenuByStatus(Constants.YSTATUS, _OrgnisationID);
            str.Append("<ul>");
            foreach (var item in menulist.Where(x => x.PARENTMENUID == default(int)).ToList())
            {
                GetChildMenu(item);
            }
            str.Append("</ul>");
            model.MenuList = str.ToString();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRole(RoleModel model, FormCollection collection)
        {
            // BALRole CSvc = new BALRole(ConStr);
            RoleMaster rolemaster = new RoleMaster();
            UserMasters currentuser = null;
            List<MenuPermissionMapMaster> permissionmenulist = new List<MenuPermissionMapMaster>();
            if (collection["btntype"] == "Create" || collection["btntype"] == "Update")
            {
                var menuuperlist = collection.Get("hdnmenuper").ToString();
                var leafmenulist = collection.Get("hdnleafmenu").ToString();
                var serializer = new JavaScriptSerializer();
                var Menu_PermissionString = serializer.Deserialize<List<MenuPermissionMapMaster>>(menuuperlist);
                var Leaf_MenuString = serializer.Deserialize<List<MenuPermissionMapMaster>>(leafmenulist);
                foreach (var item in Menu_PermissionString)
                {
                    MenuPermissionMapMaster permissionmenu = new MenuPermissionMapMaster();
                    int menuid = 0;
                    int permissionid = 0;
                    int.TryParse(item.Menu_PermissionString.Split('_')[1].Split(',')[0], out menuid);
                    int.TryParse(item.Menu_PermissionString.Split('_')[1].Split(',')[1], out permissionid);
                    permissionmenu.MENU_ID = menuid;

                    permissionmenu.PERMISSION_ID = permissionid;
                    permissionmenulist.Add(permissionmenu);
                }
                foreach (var item in Leaf_MenuString)
                {
                    MenuPermissionMapMaster permissionmenu = new MenuPermissionMapMaster();
                    int menuid = 0;
                    int.TryParse(item.LeafMenuString.Split('_')[1], out menuid);
                    permissionmenu.MENU_ID = menuid;
                    permissionmenu.PERMISSION_ID = default(int);//for leaf node
                    permissionmenulist.Add(permissionmenu);
                }
            }
            if (collection["btntype"] == "Create")
            {
                if (ModelState.IsValid)
                {
                    rolemaster.ROLENAME = model.RoleName;
                    rolemaster.ROLEDESCRIPTION = model.RoleDescription;
                    rolemaster.ISACTIVE = true;
                    rolemaster.CREATEDBY = WebSecurity.CurrentUserName;
                    rolemaster.CREATEDDATE = DateTime.Now;
                    rolemaster.SchoolID = _OrgnisationID;
                    _Crole.AddRole(rolemaster, permissionmenulist);
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ROLE_ADD_SUCCESS + "  |  Role Name is: " + model.RoleName;
                    return RedirectToAction("RoleList");
                }
                else
                {
                    TempData[Constants.MessageInfo.ERROR] = Constants.ROLE_ADD_FAIL;
                    return RedirectToAction("CreateRole");
                }
            }
            else if (collection["btntype"] == "Update")
            {
                if (ModelState.IsValid)
                {
                    rolemaster.ROLE_ID = model.role_id;
                    rolemaster.ROLENAME = model.RoleName;
                    rolemaster.ROLEDESCRIPTION = model.RoleDescription;
                    rolemaster.ISACTIVE = true;
                    rolemaster.CREATEDBY = WebSecurity.CurrentUserName;
                    rolemaster.CREATEDDATE = DateTime.Now;
                    rolemaster.SchoolID = _OrgnisationID;
                    _Crole.UpdateRole(rolemaster, permissionmenulist);
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ROLE_ADD_SUCCESS + "  |  Role Name is: " + model.RoleName;
                    return RedirectToAction("RoleList");
                }
            }

            return View();
        }

        public JsonResult CheckIsRoleNameExist(string roleName, int? roleId)
        {
            //BALCommon CSvc = new BALCommon(ConStr);
            RoleModel model = new RoleModel();
            model.RoleList = CSvc.GetRoleList(_OrgnisationID);
            bool isrolenameallowed = model.RoleList.Select(m => m.ROLENAME == roleName).FirstOrDefault();
            return Json(isrolenameallowed, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidateEmpEmailExist(string EmailID)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            bool isEmailAllowed = CSvc.ValidateEmailExist(EmailID, "Emp", _OrgnisationID);
            return Json(isEmailAllowed, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteRole(RoleModel model)
        {
            //BALRole _role = new BALRole(ConStr);
            //  BALCommon CSvc = new BALCommon(ConStr);
            if (model != null && model.role_id != default(int))
            {
                RoleMaster rolemaster = _Crole.GetByRoleId(model.role_id, _OrgnisationID);
                List<MenuPermissionMapMaster> permissionmenulist = new List<MenuPermissionMapMaster>();
                if (rolemaster != null)
                {
                    rolemaster.ISACTIVE = false;
                    _Crole.UpdateRole(rolemaster, permissionmenulist);
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ROLE_DELETE_SUCCESS + "  |  Role Name is: " + model.RoleName;
                    return RedirectToAction("RoleList");
                }
            }
            return View();
        }


        #endregion
        #region AdmissionEnquiryForm
        public ActionResult AdmissionEnquiryList()
        {
            AdmissionEnquiryModel model = new AdmissionEnquiryModel();
            model.GetAddmissionEnqTbllist = CSvc.GetAdmissionEnquiryTblList(Convert.ToInt32(model.AClassName), model.Startdate, model.Enddate,_Financialyearid, _OrgnisationID);
            model.GetClassList = CSvc.GetClassList(_OrgnisationID);
            return View(model);
        }
        [HttpPost]
        public ActionResult AdmissionEnquiryList(AdmissionEnquiryModel model,FormCollection collection)
        {
            model.GetClassList = CSvc.GetClassList(_OrgnisationID);
            model.GetAddmissionEnqTbllist = CSvc.GetAdmissionEnquiryTblList(Convert.ToInt32(model.AClassName),model.Startdate,model.Enddate, _Financialyearid, _OrgnisationID);
            string commandType = collection["rptbtn"];
            if (commandType == "Export")
            {
                if (model.GetAddmissionEnqTbllist.Tables[1] != null)
                {
                    string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Files";
                    bool exists = System.IO.Directory.Exists(folderpath);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(folderpath);
                    string filepath = folderpath + "\\" + Guid.NewGuid() + "_EnquiryList.xls";
                    CommonExport.ExportToExcel(model.GetAddmissionEnqTbllist.Tables[1], "Enquiry Records", filepath);
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    if (System.IO.File.Exists(filepath))
                        System.IO.File.Delete(filepath);
                    return File(file, "application/vnd.ms-excel", "Enquiry Records.xls");
                }

            }
            return View(model);
        }

        public ActionResult AdmissionEnquiry(Nullable<int> AMID, string ActionName = "")
        {
            AdmissionEnquiryModel model = new AdmissionEnquiryModel();
            model.AEnquiryNo = CSvc.GetEnquiryNO(1);
            if (AMID.HasValue)
            {
                AdmissionEnquiryMaster _mast = new AdmissionEnquiryMaster();
                _mast = CSvc.GetAdmissionEnquiry(AMID.Value);
                model.AMID = _mast.AMID;
                model.AEnquiryNo = _mast.AEnquiryNo;
                model.AClassName = _mast.AClassName;
                model.AStudentName = _mast.AStudentName;
                model.AGendar = _mast.AGendar;
                model.ACont_Address = _mast.ACont_Address;
                model.ACont_Address2 = _mast.ACont_Address2;
                model.ACity = _mast.ACity;
                model.AContactno = _mast.AContactno;
                model.ADOB = _mast.ADOB;
                model.APdfather = _mast.APdfather;
                model.APdFathermobile = _mast.APdFathermobile;
                model.APQualification = _mast.APQualification;
                model.APFatherdob = _mast.APFatherdob;
                model.APFatheroccuption = _mast.APFatheroccuption;
                model.APMother = _mast.APMother;
                model.Apmothermobile = _mast.Apmothermobile;
                model.ApMotherqualifation = _mast.ApMotherqualifation;
                model.Apmotheroccuption = _mast.Apmotheroccuption;
                model.Apmotherdob = _mast.Apmotherdob;
                model.Apdoanniversary = _mast.Apdoanniversary;
                model.Apemailid = _mast.Apemailid;
                model.Fonameofschool = _mast.Fonameofschool;
                model.Lastexamgiven = _mast.Lastexamgiven;
                model.FoYear = _mast.FoYear;
                model.Fostatus = _mast.Fostatus;
                model.Marks = _mast.Marks;
                model.BoardUniversity = _mast.BoardUniversity;
                model.DateofEnquiry = _mast.DateofEnquiry;
                model.ProspectusNo = _mast.ProspectusNo;
                model.Admissionformno = _mast.Admissionformno;
                model.Prospectusfees = _mast.Prospectusfees;
                model.Registrationfees = _mast.Registrationfees;
                model.Remarks = _mast.Remarks;


            }
          
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.GetClassList = CSvc.GetClassList(_OrgnisationID);
            model.GetGendarList = CSvc.GetGendarList(_OrgnisationID);
            return View(model);
        }

        [HttpPost]
        public ActionResult AdmissionEnquiry(AdmissionEnquiryModel model, FormCollection collection)
        {
            AdmissionEnquiryMaster _master = new AdmissionEnquiryMaster();
            _master = CSvc.GetAdmissionEnquiry(model.AMID);
            _master.AMID = model.AMID;
            _master.AEnquiryNo = model.AEnquiryNo;
            _master.AClassName = model.AClassName;
            _master.AStudentName = model.AStudentName;
            _master.AGendar = model.AGendar;
            _master.ACont_Address = model.ACont_Address;
            _master.ACont_Address2 = model.ACont_Address2;
            _master.ACity = model.ACity;
            _master.AContactno = model.AContactno;
            _master.ADOB = model.ADOB;
            _master.APdfather = model.APdfather;
            _master.APdFathermobile = model.APdFathermobile;
            _master.APQualification = model.APQualification;
            _master.APFatherdob = model.APFatherdob;
            _master.APFatheroccuption = model.APFatheroccuption;
            _master.APMother = model.APMother;
            _master.Apmothermobile = model.Apmothermobile;
            _master.ApMotherqualifation = model.ApMotherqualifation;
            _master.Apmotheroccuption = model.Apmotheroccuption;
            _master.Apmotherdob = model.Apmotherdob;
            _master.Apdoanniversary = model.Apdoanniversary;
            _master.Apemailid = model.Apemailid;
            _master.Fonameofschool = model.Fonameofschool;
            _master.Lastexamgiven = model.Lastexamgiven;
            _master.FoYear = model.FoYear;
            _master.Fostatus = model.Fostatus;
            _master.Marks = model.Marks;
            _master.BoardUniversity = model.BoardUniversity;
            _master.DateofEnquiry = model.DateofEnquiry;
            _master.ProspectusNo = model.ProspectusNo;
            _master.Admissionformno = model.Admissionformno;
            _master.Prospectusfees = model.Prospectusfees;
            _master.Registrationfees = model.Registrationfees;
            _master.Remarks = model.Remarks;
            _master.FMid = _Financialyearid;
            _master.OMID = _OrgnisationID;
            _master.IsActive = true;


            if (collection["btntype"] == "Create")
            {
                _master.Createddate = DateTime.Now;
                _master.CreatedBy = WebSecurity.CurrentUserName;
                _master.Modifieddate = DateTime.Now;
                _master.Modifiedby = WebSecurity.CurrentUserName;
                int ret = CSvc.AddAdmissionEnquiry(_master);
                if (ret > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Enquiry_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Enquiry_ADD_FAIL;
                return RedirectToAction("AdmissionEnquiryList");
            }
            else if (collection["btntype"] == "Update")
            {
                _master.Modifieddate = DateTime.Now;
                _master.Modifiedby = WebSecurity.CurrentUserName;
                int ret = CSvc.UPDAdmissionEnquiry(_master);
                if (ret > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Enquiry_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Enquiry_ADD_FAIL;
                return RedirectToAction("AdmissionEnquiryList");

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteAdmissionEnqry(AdmissionEnquiryModel model)
        {
            if (model != null && model.AMID != default(int))
            {
                AdmissionEnquiryMaster _master = new AdmissionEnquiryMaster();
                _master = CSvc.GetAdmissionEnquiry(model.AMID);
                if (_master != null)
                {
                    _master.IsActive = false;
                    CSvc.DeleteAdmissionEnquiry(_master);
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Enquiry_DELETE_SUCCESS + "  |  Enquiry is: " + model.AEnquiryNo;
                    return RedirectToAction("AdmissionEnquiryList");
                }
            }
            return View();
        }

        public PartialViewResult Multipleenqueryforprint(string multipleenqueryid)
        {
            AdmissionEnquiryModel _model = new AdmissionEnquiryModel();
            try
            {
                _model.OragnisationMaster = CSvc.GetOragnisationDetails(_OrgnisationID);
                _model.GetAddmissionEnqlist = CSvc.GetAdmissionenquerylist(multipleenqueryid);
            }
            catch (Exception ex)
            {

            }
            return PartialView("Multipleenqueryprint", _model);
        }
        #endregion


        public string GetChildMenu(MenuMaster menumaster)
        {
            if (IsChildExist(menumaster.MENU_ID))
            {
                str.Append("<li id='" + menumaster.MENU_ID + "'>" + menumaster.MENUNAME);

                if (menumaster.PermissionList != null && menumaster.PermissionList.Count > 0)
                {
                    str.Append("<ul id='PerRow_" + menumaster.MENU_ID + "'>");
                    foreach (var per in menumaster.PermissionList)
                    {
                        //selectedMenuPermission
                        string menuperstringid = menumaster.MENU_ID + "," + per.PERMISSION_ID;

                        str.Append("<li class='permission' id='" + menuperstringid + "' data-jstree='{\"icon\":\"//jstree.com/tree.png\"}'>" + per.PERMISSIONNAME + "</li>");
                    }
                    str.Append("</ul>");
                }

                var childelementlist = menulist.Where(x => x.PARENTMENUID == menumaster.MENU_ID);

                str.Append("<ul>");
                foreach (var childelement in childelementlist)
                {
                    GetChildMenu(childelement);
                }
                str.Append("</ul>");
            }
            else
            {

                if (menumaster.PermissionList != null && menumaster.PermissionList.Count > 0)
                {
                    str.Append("<li id='" + menumaster.MENU_ID + "'>" + menumaster.MENUNAME);

                    str.Append("<ul id='PerRow_" + menumaster.MENU_ID + "'>");
                    foreach (var per in menumaster.PermissionList)
                    {
                        str.Append("<li class='permission' id='per_" + menumaster.MENU_ID + "," + per.PERMISSION_ID + "' data-jstree='{\"icon\":\"//jstree.com/tree.png\"}'>" + per.PERMISSIONNAME + "</li>");
                    }
                    str.Append("</ul>");
                }
                else
                {
                    str.Append("<li id='menu_" + menumaster.MENU_ID + "'>" + menumaster.MENUNAME);
                }
                str.Append("</li>");
            }
            return str.ToString();
        }
        [ChildActionOnly]
        public bool IsChildExist(int menuid)
        {
            bool ischildexist = false;
            var childelementlist = menulist.Where(x => x.PARENTMENUID == menuid);
            if (childelementlist.Count() > 0)
            {
                ischildexist = true;
            }
            return ischildexist;
        }
        #region Holiday Master
        public ActionResult HolidayList()
        {
            BALCommon obj = new BALCommon(ConStr);
            HolidayMaster model = new HolidayMaster();
            try
            {
                model.ActionName = Constants.BTN_CREATE;
                model.HolidayMasterList = obj.GetHolidaylist(_OrgnisationID, _Financialyearid);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Admin(HolidayMasterGet)", "Error_014", ex, "Admin");
            }

            return View(model);
        }
        public ActionResult HolidayMaster(Nullable<int> Hid, string ActionName = "")
        {
            BALCommon obj = new BALCommon(ConStr);
            HolidayMaster model = new HolidayMaster();
            try
            {
                model.ActionName = Constants.BTN_CREATE;
                if (Hid.HasValue)
                {
                    model.ActionName = ActionName;
                    HolidayMaster HM = obj.GetHolidayDetails(Convert.ToInt32(Hid), _OrgnisationID, _Financialyearid);
                    model.Hid = Convert.ToInt32(Hid);
                    model.Staff = HM.Staff;
                    model.Student = HM.Student;
                    model.HolidayDate = HM.HolidayDate;
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Admin(HolidayMasterGet)", "Error_014", ex, "Admin");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult HolidayMaster(HolidayMaster model, FormCollection collection)
        {
            BALCommon obj = new BALCommon(ConStr);
            string result = "";
            try
            {
                model.SchoolID = _OrgnisationID;
                model.FinancialYear = _Financialyearid;

                if (collection["btntype"].ToUpper() == "Save".ToUpper())
                {
                    model.CreatedBy = WebSecurity.CurrentUserName;
                    model.UpdatedBy = WebSecurity.CurrentUserName;
                    result = obj.SaveHolidayDetails(model);
                }
                else if (collection["btntype"].ToUpper() == "Update".ToUpper())
                {
                    model.UpdatedBy = WebSecurity.CurrentUserName;
                    result = obj.UpdateHolidayDetails(model);
                }

                if (result.Contains("not"))
                    TempData[Constants.MessageInfo.SUCCESS] = result;
                else
                    TempData[Constants.MessageInfo.ERROR] = result;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Admin(HolidayMasterPost)", "Error_014", ex, "Admin");
                TempData[Constants.MessageInfo.ERROR] = ex.Message.ToString();
            }
            return RedirectToAction("HolidayList");
        }
        [HttpPost]
        public ActionResult DeleteHoliday(int Hid)
        {
            string actionStatus = "";
            try
            {
                BALCommon CSFee = new BALCommon(ConStr);
                actionStatus = CSFee.DeleteHolidayDetails(Hid, _OrgnisationID, _Financialyearid);
                TempData[Constants.MessageInfo.SUCCESS] = actionStatus;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Admin(HolidayMasterPost)", "Error_014", ex, "Admin");
            }
            return RedirectToAction("HolidayList");
        }

        #endregion Holiday Master

        #region MasterPage Listing
        public ActionResult ClassList()
        {
            ClassModel model = new ClassModel();
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateClass(Nullable<int> ClassId, string ActionName = "")
        {
            ClassModel model = new ClassModel();
            ClassMaster existingclass = new ClassMaster();
            if (ClassId.HasValue)
            {
                existingclass = CSvc.GetClassByClassid(ClassId.Value);
                model.CMid = existingclass.CMid;
                model.ClassName = existingclass.ClassName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateClass(ClassModel model, FormCollection collection)
        {
            ClassMaster classmast = new ClassMaster();
            classmast.ClassName = model.ClassName.Trim();
            classmast.CMid = model.CMid;
            classmast.SchoolID = _OrgnisationID;
            classmast.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                classmast.Createdate = DateTime.Now;
                classmast.Createdby = WebSecurity.CurrentUserName;
                classmast.Modifieddate = DateTime.Now;
                classmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddClass(classmast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Class_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Class_AlreadyExist_SUCCESS;
                return RedirectToAction("ClassList");
            }
            else
            {
                classmast.Modifieddate = DateTime.Now;
                classmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddClass(classmast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Class_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Class_ADD_FAIL;
                return RedirectToAction("ClassList");
            }

        }
        public JsonResult CheckIsClassNameExist(string classname)
        {
            ClassModel model = new ClassModel();
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            bool isrolenameallowed = model.ClassList.Select(m => m.ClassName == classname).FirstOrDefault();
            return Json(isrolenameallowed, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteClass(ClassModel model)
        {

            if (model != null && model.CMid != default(int))
            {
                ClassMaster classmast = CSvc.GetClassByClassid(model.CMid);
                if (classmast != null)
                {
                    classmast.IsActive = false;
                    classmast.Modifieddate = DateTime.Now;
                    classmast.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddClass(classmast, "DEL") > 0)
                    {
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Class_DELETE_SUCCESS;
                    }
                    else
                    {
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Class_DELETE_FAIL;
                    }
                    return RedirectToAction("ClassList");
                }
            }
            return View();
        }



        public ActionResult SectionList()
        {
            SectionModel model = new SectionModel();
            model.SectionTblList = CSvc.GetSectionListTBL(_OrgnisationID, 0);
            return View(model);
        }
        public ActionResult CreateSection(Nullable<int> Secmid, string ActionName = "")
        {
            SectionModel model = new SectionModel();
            SectionMaster existingsec = new SectionMaster();
            if (Secmid.HasValue)
            {
                existingsec = CSvc.GetSectionBySecmid(Secmid.Value);
                model.Secmid = existingsec.Secmid;
                model.SectionName = existingsec.SectionName;
                model.CMid = existingsec.CMid;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSection(SectionModel model, FormCollection collection)
        {
            SectionMaster secmast = CSvc.GetSectionBySecmid(model.Secmid);
            secmast.SectionName = model.SectionName.Trim();
            secmast.CMid = model.CMid;
            secmast.SchoolId = _OrgnisationID;
            secmast.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                secmast.Createdate = DateTime.Now;
                secmast.Createdby = WebSecurity.CurrentUserName;
                secmast.Modifieddate = DateTime.Now;
                secmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddSection(secmast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Section_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Section_AlreadyExist_SUCCESS;
                return RedirectToAction("SectionList");
            }
            else
            {
                secmast.Modifieddate = DateTime.Now;
                secmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddSection(secmast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Section_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Section_ADD_FAIL;

                return RedirectToAction("SectionList");
            }

        }
        public JsonResult CheckIsSectionNameExist(string sectionname, int classid)
        {
            SectionModel model = new SectionModel();
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, classid);
            bool isrolenameallowed = model.SectionList.Select(m => m.SectionName == sectionname && m.CMid == classid).FirstOrDefault();
            return Json(isrolenameallowed, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteSection(SectionModel model)
        {

            if (model != null && model.Secmid != default(int))
            {
                SectionMaster secmast = CSvc.GetSectionBySecmid(model.Secmid);
                if (secmast != null)
                {
                    secmast.IsActive = false;
                    secmast.Modifieddate = DateTime.Now;
                    secmast.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddSection(secmast, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Section_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Section_DELETE_FAIL;

                    return RedirectToAction("SectionList");
                }
            }
            return View();
        }



        #region RouteListSec
        public ActionResult RouteList()
        {
            RouteModel model = new RouteModel();
            model.RouteList = CSvc.GetRouteList(_OrgnisationID);
            return View(model);
        }

        public ActionResult CreateRoute(Nullable<int> Routemid, string ActionName = "")
        {
            RouteModel model = new RouteModel();
            RouteMaster existingroute = new RouteMaster();
            if (Routemid.HasValue)
            {
                existingroute = CSvc.GetRouteByid(Routemid.Value);
                model.Routemid = existingroute.Routemid;
                model.RouteName = existingroute.RouteName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRoute(RouteModel model, FormCollection collection)
        {
            RouteMaster routemast = new RouteMaster();
            routemast.RouteName = model.RouteName;
            routemast.Routemid = model.Routemid;
            routemast.SchoolId = _OrgnisationID;
            if (collection["btntype"] == "Create")
            {
                routemast.IsActive = true;
                routemast.Createdate = DateTime.Now;
                routemast.Createdby = WebSecurity.CurrentUserName;
                routemast.Modifieddate = DateTime.Now;
                routemast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddRout(routemast, "INS");
                if (_returntype > 0)
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Route_Add_SUCCESS;
                }
                return RedirectToAction("RouteList");
            }
            else
            {
                routemast.Modifieddate = DateTime.Now;
                routemast.Modifiedby = WebSecurity.CurrentUserName;
                CSvc.AddRout(routemast, "UPD");
                TempData[Constants.MessageInfo.SUCCESS] = Constants.Route_UPDATE_SUCCESS;
                return RedirectToAction("RouteList");
            }

        }
        public ActionResult DeleteRoute(RouteModel model)
        {

            if (model != null && model.Routemid != default(int))
            {
                RouteMaster routmast = CSvc.GetRouteByid(model.Routemid);
                if (routmast != null)
                {
                    routmast.IsActive = routmast.IsActive == true ? false : true;
                    routmast.Modifieddate = DateTime.Now;
                    routmast.Modifiedby = WebSecurity.CurrentUserName;
                    CSvc.AddRout(routmast, "UPD");
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Route_DELETE_SUCCESS;
                    return RedirectToAction("RouteList");
                }
            }
            return View();
        }
        #endregion

        #region Bus section
        public ActionResult BusList()
        {
            BusModel model = new BusModel();
            model.BusList = CSvc.GetBuslist(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateBus(Nullable<int> Busmid, string ActionName = "")
        {
            BusModel model = new BusModel();
            BusMaster existingbus = new BusMaster();
            if (Busmid.HasValue)
            {
                existingbus = CSvc.GetBusByid(Busmid.Value);
                model.Busmid = existingbus.Busmid;
                model.BusName = existingbus.BusName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateBus(BusModel model, FormCollection collection)
        {
            BusMaster busmast = new BusMaster();
            busmast.BusName = model.BusName;
            busmast.Busmid = model.Busmid;
            busmast.SchoolId = _OrgnisationID;
            if (collection["btntype"] == "Create")
            {
                busmast.IsActive = true;
                busmast.Createdate = DateTime.Now;
                busmast.Createdby = WebSecurity.CurrentUserName;
                busmast.Modifieddate = DateTime.Now;
                busmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddBus(busmast, "INS");
                if (_returntype > 0)
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Bus_Add_SUCCESS;
                }
                return RedirectToAction("BusList");
            }
            else
            {
                busmast.Modifieddate = DateTime.Now;
                busmast.Modifiedby = WebSecurity.CurrentUserName;
                CSvc.AddBus(busmast, "UPD");
                TempData[Constants.MessageInfo.SUCCESS] = Constants.Bus_UPDATE_SUCCESS;
                return RedirectToAction("BusList");
            }

        }
        public ActionResult DeleteBus(BusModel model)
        {

            if (model != null && model.Busmid != default(int))
            {
                BusMaster busmast = CSvc.GetBusByid(model.Busmid);
                if (busmast != null)
                {
                    busmast.IsActive = busmast.IsActive == true ? false : true;
                    busmast.Modifieddate = DateTime.Now;
                    busmast.Modifiedby = WebSecurity.CurrentUserName;
                    CSvc.AddBus(busmast, "UPD");
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Bus_DELETE_SUCCESS;
                    return RedirectToAction("BusList");
                }
            }
            return View();
        }
        #endregion
        #region Cast Section
        public ActionResult CastList()
        {
            CastModel model = new CastModel();
            model.CastList = CSvc.GetCastList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateCast(Nullable<int> Castmid, string ActionName = "")
        {
            CastModel model = new CastModel();
            CastMaster existingcast = new CastMaster();
            if (Castmid.HasValue)
            {
                existingcast = CSvc.GetCastByid(Castmid.Value);
                model.Castmid = existingcast.Castmid;
                model.CastName = existingcast.CastName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCast(CastModel model, FormCollection collection)
        {
            CastMaster castmast = new CastMaster();
            castmast.CastName = model.CastName;
            castmast.Castmid = model.Castmid;
            castmast.SchoolId = _OrgnisationID;
            castmast.IsActive = true;
            if (collection["btntype"] == "Create")
            {

                castmast.Createdate = DateTime.Now;
                castmast.Createdby = WebSecurity.CurrentUserName;
                castmast.Modifieddate = DateTime.Now;
                castmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddCast(castmast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Cast_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Cast_AlreadyExist_SUCCESS;
                return RedirectToAction("CastList");
            }
            else
            {
                castmast.Modifieddate = DateTime.Now;
                castmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddCast(castmast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Cast_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Cast_ADD_FAIL;
                return RedirectToAction("CastList");
            }

        }
        public ActionResult DeleteCast(CastModel model)
        {

            if (model != null && model.Castmid != default(int))
            {
                CastMaster castmast = CSvc.GetCastByid(model.Castmid);
                if (castmast != null)
                {
                    castmast.IsActive = false;
                    castmast.Modifieddate = DateTime.Now;
                    castmast.Modifiedby = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddCast(castmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Cast_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Cast_DELETE_FAIL;
                    return RedirectToAction("CastList");
                }
            }
            return View();
        }
        #endregion

        #region Category Opration
        public ActionResult CategoryList()
        {
            CategoryModel model = new CategoryModel();
            model.CategoryList = CSvc.GetCategoryMasterList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateCategory(Nullable<int> Catmid, string ActionName = "")
        {
            CategoryModel model = new CategoryModel();
            CategoryMaster existingcategory = new CategoryMaster();
            if (Catmid.HasValue)
            {
                existingcategory = CSvc.GetCategoryByid(Catmid.Value);
                model.Catmid = existingcategory.Catmid;
                model.CatName = existingcategory.CatName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryModel model, FormCollection collection)
        {
            CategoryMaster Categorymast = new CategoryMaster();
            Categorymast.CatName = model.CatName.Trim();
            Categorymast.Catmid = model.Catmid;
            Categorymast.SchoolId = _OrgnisationID;
            Categorymast.IsActive = true;
            if (collection["btntype"] == "Create")
            {

                Categorymast.Createdate = DateTime.Now;
                Categorymast.Createdby = WebSecurity.CurrentUserName;
                Categorymast.Modifieddate = DateTime.Now;
                Categorymast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddCategory(Categorymast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Category_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Category_AlreadyExist_SUCCESS;
                return RedirectToAction("CategoryList");
            }
            else
            {
                Categorymast.Modifieddate = DateTime.Now;
                Categorymast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddCategory(Categorymast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Category_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Category_ADD_FAIL;
                return RedirectToAction("CategoryList");
            }

        }
        public ActionResult DeleteCategory(CategoryModel model)
        {

            if (model != null && model.Catmid != default(int))
            {
                CategoryMaster Catmast = CSvc.GetCategoryByid(model.Catmid);
                if (Catmast != null)
                {
                    Catmast.IsActive = false;
                    Catmast.Modifieddate = DateTime.Now;
                    Catmast.Modifiedby = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddCategory(Catmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Category_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Category_DELETE_FAIL;
                    return RedirectToAction("CategoryList");
                }
            }
            return View();
        }
        #endregion
        #region Gendar Opration
        public ActionResult GendarList()
        {
            GendarModel model = new GendarModel();
            model.GendarList = CSvc.GetGendarList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateGendar(Nullable<int> GMid, string ActionName = "")
        {
            GendarModel model = new GendarModel();
            GendraMaster existinggendar = new GendraMaster();
            if (GMid.HasValue)
            {
                existinggendar = CSvc.GetGendarByid(GMid.Value);
                model.GMid = existinggendar.GMid;
                model.GName = existinggendar.GName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateGendar(GendarModel model, FormCollection collection)
        {
            GendraMaster genmast = new GendraMaster();
            genmast.GName = model.GName.Trim();
            genmast.GMid = model.GMid;
            genmast.SchoolId = _OrgnisationID;
            genmast.IsActive = true;
            if (collection["btntype"] == "Create")
            {

                genmast.Createdate = DateTime.Now;
                genmast.Createdby = WebSecurity.CurrentUserName;
                genmast.Modifieddate = DateTime.Now;
                genmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddGendar(genmast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Gendar_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Gendar_AlreadyExist_SUCCESS;
                return RedirectToAction("GendarList");
            }
            else
            {
                genmast.Modifieddate = DateTime.Now;
                genmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddGendar(genmast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Gendar_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Gendar_ADD_FAIL;
                return RedirectToAction("GendarList");
            }

        }
        public ActionResult DeleteGendar(GendarModel model)
        {

            if (model != null && model.GMid != default(int))
            {
                GendraMaster genmast = CSvc.GetGendarByid(model.GMid);
                if (genmast != null)
                {
                    genmast.IsActive = false;
                    genmast.Modifieddate = DateTime.Now;
                    genmast.Modifiedby = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddGendar(genmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Gendar_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Gendar_DELETE_FAIL;
                    return RedirectToAction("GendarList");
                }
            }
            return View();
        }
        #endregion
        #region Religion Opration
        public ActionResult ReligionList()
        {
            ReligionModel model = new ReligionModel();
            model.ReligionList = CSvc.GetReligionList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateReligion(Nullable<int> Relmid, string ActionName = "")
        {
            ReligionModel model = new ReligionModel();
            ReligionMaster existingreligion = new ReligionMaster();
            if (Relmid.HasValue)
            {
                existingreligion = CSvc.GetReligionByid(Relmid.Value);
                model.Relmid = existingreligion.Relmid;
                model.ReligionName = existingreligion.ReligionName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateReligion(ReligionModel model, FormCollection collection)
        {
            ReligionMaster relimast = new ReligionMaster();
            relimast.ReligionName = model.ReligionName;
            relimast.Relmid = model.Relmid;
            relimast.SchoolId = _OrgnisationID;
            relimast.IsActive = true;
            if (collection["btntype"] == "Create")
            {

                relimast.Createdate = DateTime.Now;
                relimast.Createdby = WebSecurity.CurrentUserName;
                relimast.Modifieddate = DateTime.Now;
                relimast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddReligion(relimast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Religion_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Religion_AlreadyExist_SUCCESS;
                return RedirectToAction("ReligionList");
            }
            else
            {
                relimast.Modifieddate = DateTime.Now;
                relimast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddReligion(relimast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Religion_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Religion_ADD_FAIL;
                return RedirectToAction("ReligionList");
            }

        }

        public ActionResult DeleteReligion(ReligionModel model)
        {

            if (model != null && model.Relmid != default(int))
            {
                ReligionMaster relimast = CSvc.GetReligionByid(model.Relmid);
                if (relimast != null)
                {
                    relimast.IsActive = false;
                    relimast.Modifieddate = DateTime.Now;
                    relimast.Modifiedby = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddReligion(relimast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Religion_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Religion_DELETE_FAIL;
                    return RedirectToAction("ReligionList");
                }
            }
            return View();
        }
        #endregion
        #region Department Opration
        public ActionResult DepartmentList()
        {
            DepartmentModel model = new DepartmentModel();
            model.DepartmentList = CSvc.GetDepartmentList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateDepartment(Nullable<int> Department_id, string ActionName = "")
        {
            DepartmentModel model = new DepartmentModel();
            DepartmentMaster existingdepartment = new DepartmentMaster();
            if (Department_id.HasValue)
            {
                existingdepartment = CSvc.GetDepartmentByid(Department_id.Value);
                model.Department_id = existingdepartment.DEPARTMENT_ID;
                model.DepartmentName = existingdepartment.DEPARTMENTNAME;
                model.DepartmentDesc = existingdepartment.DEPARTMENTDESC;
                model.Remarks = existingdepartment.REMARKS;
                model.Isactive = existingdepartment.ISACTIVE;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateDepartment(DepartmentModel model, FormCollection collection)
        {
            DepartmentMaster departmentmst = new DepartmentMaster();
            departmentmst.DEPARTMENT_ID = model.Department_id;
            departmentmst.DEPARTMENTNAME = model.DepartmentName;
            departmentmst.DEPARTMENTDESC = model.DepartmentDesc;
            departmentmst.REMARKS = model.Remarks;
            departmentmst.SchoolID = _OrgnisationID;
            departmentmst.ISACTIVE = true;
            if (collection["btntype"] == "Create")
            {
                departmentmst.CREATEDDATE = DateTime.Now;
                departmentmst.CREATEDBY = WebSecurity.CurrentUserName;
                departmentmst.MODIFIEDDATE = DateTime.Now;
                departmentmst.MODIFIEDBY = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddDepartment(departmentmst, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Department_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Department_AlreadyExist_SUCCESS;
                return RedirectToAction("DepartmentList");
            }
            else
            {
                departmentmst.MODIFIEDDATE = DateTime.Now;
                departmentmst.MODIFIEDBY = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddDepartment(departmentmst, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Department_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Department_ADD_FAIL;
                return RedirectToAction("DepartmentList");
            }

        }
        public ActionResult DeleteDepartment(DepartmentModel model)
        {

            if (model != null && model.Department_id != default(int))
            {
                DepartmentMaster depmast = CSvc.GetDepartmentByid(model.Department_id);
                if (depmast != null)
                {
                    depmast.ISACTIVE = false;
                    depmast.MODIFIEDDATE = DateTime.Now;
                    depmast.MODIFIEDBY = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddDepartment(depmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Department_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Department_DELETE_FAIL;
                    return RedirectToAction("DepartmentList");
                }
            }
            return View();
        }

        #endregion
        #region Designation Opration


        public ActionResult DesignationList()
        {
            DesignationModel model = new DesignationModel();
            model.DesignationList = CSvc.GetDesignationList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateDesignation(Nullable<int> Designation_id, string ActionName = "")
        {
            DesignationModel model = new DesignationModel();
            DesignationMaster existingdepart = new DesignationMaster();
            if (Designation_id.HasValue)
            {
                existingdepart = CSvc.GetDesignationByid(Designation_id.Value);
                model.Designation_id = existingdepart.DESIGNATION_ID;
                model.DesignationName = existingdepart.DESIGNATIONNAME;
                model.DesignationDesc = existingdepart.DESIGNATIONDESC;
                model.Remarks = existingdepart.REMARKS;
                model.Isactive = existingdepart.ISACTIVE;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateDesignation(DesignationModel model, FormCollection collection)
        {
            DesignationMaster designationmst = new DesignationMaster();
            designationmst.DESIGNATION_ID = model.Designation_id;
            designationmst.DESIGNATIONNAME = model.DesignationName;
            designationmst.DESIGNATIONDESC = model.DesignationDesc;
            designationmst.REMARKS = model.Remarks;
            designationmst.SchoolID = _OrgnisationID;
            designationmst.ISACTIVE = true;
            if (collection["btntype"] == "Create")
            {

                designationmst.CREATEDDATE = DateTime.Now;
                designationmst.CREATEDBY = WebSecurity.CurrentUserName;
                designationmst.MODIFIEDDATE = DateTime.Now;
                designationmst.MODIFIEDBY = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddDesignation(designationmst, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Designation_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Designation_AlreadyExist_SUCCESS;
                return RedirectToAction("DesignationList");
            }
            else
            {
                designationmst.MODIFIEDDATE = DateTime.Now;
                designationmst.MODIFIEDBY = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddDesignation(designationmst, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Designation_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Designation_ADD_FAIL;
                return RedirectToAction("DesignationList");
            }

        }
        public ActionResult DeleteDesignation(DesignationModel model)
        {

            if (model != null && model.Designation_id != default(int))
            {
                DesignationMaster desmast = CSvc.GetDesignationByid(model.Designation_id);
                if (desmast != null)
                {
                    desmast.ISACTIVE = false;
                    desmast.MODIFIEDDATE = DateTime.Now;
                    desmast.MODIFIEDBY = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddDesignation(desmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Designation_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Designation_DELETE_FAIL;
                    return RedirectToAction("DesignationList");
                }
            }
            return View();
        }
        #endregion

        #region Marital Status Master
        public ActionResult MaritalList()
        {
            MartialStatusModel model = new MartialStatusModel();
            model.MartialStatusList = CSvc.GetMartialStatusList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateMarital(Nullable<int> Mrmid, string ActionName = "")
        {
            MartialStatusModel model = new MartialStatusModel();
            MartialStatusMaster existingmaritalstatus = new MartialStatusMaster();
            if (Mrmid.HasValue)
            {
                existingmaritalstatus = CSvc.GetMartialStatusByid(Mrmid.Value);
                model.Mrmid = existingmaritalstatus.Mrmid;
                model.StatusName = existingmaritalstatus.StatusName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateMarital(MartialStatusModel model, FormCollection collection)
        {
            MartialStatusMaster existingmaritalstatus = new MartialStatusMaster();
            existingmaritalstatus.StatusName = model.StatusName;
            existingmaritalstatus.Mrmid = model.Mrmid;
            existingmaritalstatus.SchoolID = _OrgnisationID;
            existingmaritalstatus.IsActice = true;
            if (collection["btntype"] == "Create")
            {
                int _returntype = CSvc.AddMartialStatus(existingmaritalstatus, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.MartialStatus_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.MartialStatus_AlreadyExist_SUCCESS;
                return RedirectToAction("MaritalList");
            }
            else
            {
                int _returntype = CSvc.AddMartialStatus(existingmaritalstatus, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.MartialStatus_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.MartialStatus_ADD_FAIL;
                return RedirectToAction("MaritalList");
            }

        }
        public ActionResult DeleteMarital(MartialStatusModel model)
        {

            if (model != null && model.Mrmid != default(int))
            {
                MartialStatusMaster martialmast = CSvc.GetMartialStatusByid(model.Mrmid);
                if (martialmast != null)
                {
                    martialmast.IsActice = false;
                    int _returntype = CSvc.AddMartialStatus(martialmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.MartialStatus_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.MartialStatus_DELETE_FAIL;
                    return RedirectToAction("MaritalList");
                }
            }
            return View();
        }
        #endregion
        #region Blood Group Master
        public ActionResult BloodGroupList()
        {
            BloodGroupModel model = new BloodGroupModel();
            model.BloodGroupList = CSvc.GetBloodGroupList(_OrgnisationID);
            return View(model);
        }
        public ActionResult Createbloodgroup(Nullable<int> Bdmid, string ActionName = "")
        {
            BloodGroupModel model = new BloodGroupModel();
            BloodGroupMaster existingbloodgroup = new BloodGroupMaster();
            if (Bdmid.HasValue)
            {
                existingbloodgroup = CSvc.GetBloodGroupByid(Bdmid.Value);
                model.Bdmid = existingbloodgroup.Bdmid;
                model.BloodGroupName = existingbloodgroup.BloodGroupName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult Createbloodgroup(BloodGroupModel model, FormCollection collection)
        {
            BloodGroupMaster existingbloodgrp = new BloodGroupMaster();
            existingbloodgrp.BloodGroupName = model.BloodGroupName;
            existingbloodgrp.Bdmid = model.Bdmid;
            existingbloodgrp.SchoolID = _OrgnisationID;
            existingbloodgrp.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                int _returntype = CSvc.AddBloodgroup(existingbloodgrp, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.BloodGroup_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.BloodGroup_AlreadyExist_SUCCESS;
                return RedirectToAction("BloodGroupList");
            }
            else
            {
                int _returntype = CSvc.AddBloodgroup(existingbloodgrp, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.BloodGroup_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.BloodGroup_ADD_FAIL;
                return RedirectToAction("BloodGroupList");
            }

        }
        public ActionResult DeleteBloodgroup(BloodGroupModel model)
        {

            if (model != null && model.Bdmid != default(int))
            {
                BloodGroupMaster bloodgrpmast = CSvc.GetBloodGroupByid(model.Bdmid);
                if (bloodgrpmast != null)
                {
                    bloodgrpmast.IsActive = false;
                    int _returntype = CSvc.AddBloodgroup(bloodgrpmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.BloodGroup_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.BloodGroup_DELETE_FAIL;
                    return RedirectToAction("BloodGroupList");
                }
            }
            return View();
        }
        #endregion

        #region Contact Title
        public ActionResult ContactTitleMasterList()
        {
            ContactTitleModel model = new ContactTitleModel();
            model.ContactTitleMasterList = CSvc.GetContactList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateContactTitle(Nullable<int> CTmid, string ActionName = "")
        {
            ContactTitleModel model = new ContactTitleModel();
            ContactTitleMaster existingcontacttitle = new ContactTitleMaster();
            if (CTmid.HasValue)
            {
                existingcontacttitle = CSvc.GetContacttitleByid(CTmid.Value);
                model.CTmid = existingcontacttitle.CTmid;
                model.ContactName = existingcontacttitle.ContactName;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateContactTitle(ContactTitleModel model, FormCollection collection)
        {
            ContactTitleMaster existingconacttitle = new ContactTitleMaster();
            existingconacttitle.ContactName = model.ContactName.Trim();
            existingconacttitle.CTmid = model.CTmid;
            existingconacttitle.SchoolID = _OrgnisationID;
            existingconacttitle.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                int _returntype = CSvc.AddContacttitle(existingconacttitle, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ContactTitle_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ContactTitle_AlreadyExist_SUCCESS;
                return RedirectToAction("ContactTitleMasterList");
            }
            else
            {
                int _returntype = CSvc.AddContacttitle(existingconacttitle, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ContactTitle_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.ContactTitle_ADD_FAIL;
                return RedirectToAction("contacttitlemasterlist");
            }

        }
        public ActionResult DeleteCreateContactTitle(ContactTitleModel model)
        {

            if (model != null && model.CTmid != default(int))
            {
                ContactTitleMaster contactmast = CSvc.GetContacttitleByid(model.CTmid);
                if (contactmast != null)
                {
                    contactmast.IsActive = false;
                    int _returntype = CSvc.AddContacttitle(contactmast, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.ContactTitle_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.ContactTitle_DELETE_FAIL;
                    return RedirectToAction("contacttitlemasterlist");
                }
            }
            return View();
        }
        #endregion
        #endregion

        #region Student Attendance Management System
        public ActionResult dailyattendance(Nullable<int> Attendance_id, string ActionName = "")
        {
            DailyattendanceModel model = new DailyattendanceModel();
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.Attendancestulist = null;
            return View(model);
        }
        [HttpPost]
        public ActionResult dailyattendance(DailyattendanceModel model, FormCollection collection)
        {
            List<StudentAttendanceMaster> studentattlist = new List<StudentAttendanceMaster>();
            model.Attendancestulist = CSvc.GetAttendancestudentlst(model.SelectClassId, model.SelectSectionId, _OrgnisationID, _Financialyearid).Tables[0];
            /*
            if (collection["btntype"] == "Search")
            {
                model.Attendancestulist = CSvc.GetAttendancestudentlst(model.SelectClassId, model.SelectSectionId, _OrgnisationID, _Financialyearid).Tables[0];
            }
            */
            if (collection["btntype"] == "Save")
            {
                int loopcount;
                int.TryParse(collection["loopcount"], out loopcount);
                for (int i = 1; i <= loopcount; i++)
                {
                    StudentAttendanceMaster _satm = new StudentAttendanceMaster();
                    _satm.ClassId = model.SelectClassId;
                    _satm.SectionId = model.SelectSectionId;
                    _satm.Sattendancedate = model.Attendancedate;
                    _satm.SIndatetime = model.Attendancedate;
                    _satm.SOutdatetime = model.Attendancedate;
                    _satm.Smid = Convert.ToInt32(collection["studentid_" + i]);
                    _satm.SAtstatus = Convert.ToString(collection["present_" + i]);
                    _satm.Reason = Convert.ToString(collection["remarkssec_" + i]);
                    _satm.FMid = _Financialyearid;
                    _satm.SchoolId = _OrgnisationID;
                    _satm.Createddate = DateTime.Now;
                    _satm.Createdby = WebSecurity.CurrentUserName;
                    _satm.Modifieddate = DateTime.Now;
                    _satm.Modifiedby = WebSecurity.CurrentUserName;
                    _satm.Ipaddress = "192.168.10.1";
                    studentattlist.Add(_satm);
                }
                string _returenid = CSvc.StudentAttendanceopration(studentattlist, "INS");
                if (Convert.ToInt32(_returenid) > 0)
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Attendance_Add_SUCCESS;
                    return RedirectToAction("dailyattendance");
                }
                else
                {
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Attendance_ADD_FAIL;
                }
            }
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, model.SelectClassId);
            return View(model);
        }
        public ActionResult attendancesummary(Nullable<int> Attendance_id, string ActionName = "")
        {
            DailyattendanceModel model = new DailyattendanceModel();
            model.Attendancedate = DateTime.Now;
            model.Toattendancedate = DateTime.Now;
            model.DisplaymonthYear = model.Attendancedate.ToString("MMMM yyyy");
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.AttendanceSummarytbl = null;
            return View(model);
        }
        [HttpPost]
        public ActionResult attendancesummary(DailyattendanceModel model, FormCollection collection)
        {

            model.DisplaymonthYear = model.Attendancedate.ToString("MMMM yyyy");
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, model.SelectClassId);
            model.AttendanceSummarytbl = CSvc.GetAttendancesummary(model.Attendancedate, model.Toattendancedate, model.SelectClassId, model.SelectSectionId, _OrgnisationID, _Financialyearid).Tables[0];
            return View(model);
        }
        public ActionResult attendancesheet(Nullable<int> Attendance_id, string ActionName = "")
        {
            DailyattendanceModel model = new DailyattendanceModel();
            model.Attendancedate = DateTime.Now;
            model.DisplaymonthYear = model.Attendancedate.ToString("MMMM yyyy");
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.Attendancesheetlist = null;
            return View(model);
        }
        [HttpPost]
        public ActionResult attendancesheet(DailyattendanceModel model, FormCollection collection)
        {
            model.DisplaymonthYear = model.Attendancedate.ToString("MMMM yyyy");
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            string _days = "01";
            string _month = model.Attendancedate.Month > 9 ? model.Attendancedate.Month.ToString() : "0" + model.Attendancedate.Month.ToString();
            string _year = model.Attendancedate.Year.ToString();
            DateTime _passdatetime = Convert.ToDateTime(_year + "-" + _month + "-" + _days);
            model.Attendancesheetlist = CSvc.GetAttendancesheetlst(_passdatetime, model.SelectClassId, model.SelectSectionId, _OrgnisationID, _Financialyearid).Tables[0];
            return View(model);
        }



        public ActionResult monthlyattendance(Nullable<int> Attendance_id, string ActionName = "")
        {
            DailyattendanceModel model = new DailyattendanceModel();
            model.Attendancedate = DateTime.Now;
            model.Toattendancedate = DateTime.Now;
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.Attendancesheetlist = null;
            model.Monathalyattendancelist = null;
            model.Totalchkbox = 0;
            return View(model);
        }
        [HttpPost]
        public ActionResult monthlyattendance(DailyattendanceModel model, FormCollection collection)
        {
            List<StudentAttendanceMaster> studentattlist = new List<StudentAttendanceMaster>();
            if (collection["btntype"] == "Search")
            {
                DateTime startdate = model.Attendancedate.Date;
                List<Monthalyattendance> _monattendancelist = new List<Monthalyattendance>();
                DateTime enddate = model.Toattendancedate.Date;
                model.Attendancestulist = CSvc.GetmonthalyAttendancestudentlst(model.SelectClassId, model.SelectSectionId, _OrgnisationID, _Financialyearid).Tables[0];
                foreach (DateTime day in EachCalendarday(startdate, enddate))
                {
                    Monthalyattendance _sinatt = new Monthalyattendance();
                    _sinatt.id = Convert.ToDateTime(day).Date.Day.ToString() + " " + Convert.ToDateTime(day).Date.DayOfWeek.ToString();
                    _sinatt.value = day.ToString("dd-MM-yyyy");
                    _monattendancelist.Add(_sinatt);
                }
                model.Monathalyattendancelist = _monattendancelist;
                model.Totalchkbox = _monattendancelist.Count;
            }
            else if (collection["btntype"] == "Save")
            {
                int loopcount;
                int.TryParse(collection["loopcount"], out loopcount);
                for (int i = 1; i <= loopcount; i++)
                {
                    for (int k = 1; k <= model.Totalchkbox; k++)
                    {
                        StudentAttendanceMaster _satm = new StudentAttendanceMaster();
                        _satm.ClassId = model.SelectClassId;
                        _satm.SectionId = model.SelectSectionId;
                        _satm.FMid = _Financialyearid;
                        _satm.SchoolId = _OrgnisationID;
                        _satm.Createddate = DateTime.Now;
                        _satm.Createdby = WebSecurity.CurrentUserName;
                        _satm.Modifieddate = DateTime.Now;
                        _satm.Modifiedby = WebSecurity.CurrentUserName;
                        _satm.Ipaddress = "192.168.10.1";
                        _satm.Smid = Convert.ToInt32(collection["studentid_" + i]);


                        _satm.Sattendancedate = Convert.ToDateTime(collection["daydate_" + i + k]); //model.Attendancedate;
                        _satm.SIndatetime = Convert.ToDateTime(collection["daydate_" + i + k]);
                        _satm.SOutdatetime = Convert.ToDateTime(collection["daydate_" + i + k]);
                        _satm.SAtstatus = Convert.ToString(collection["chk_" + i + k]) == null ? "A" : "P";
                        studentattlist.Add(_satm);
                    }
                }
                if (studentattlist.Count > 0)
                {
                    CSvc.StudentAttendanceopration(studentattlist, "INS");
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Attendance_Add_SUCCESS;
                    return RedirectToAction("monthlyattendance");
                }
            }
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.ClassSectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            model.Attendancesheetlist = null;
            model.Monthalyattendancesheetlist = null;
            return View(model);
        }
        public IEnumerable<DateTime> EachCalendarday(DateTime startdate, DateTime endDate)
        {
            for (var date = startdate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
                         return date;
        }
        #endregion
        public PartialViewResult GetAdmissionForm(int StudentID)
        {
            StudentMaster _StudentMaster = new StudentMaster();
            OragnisationMaster _OragnisationMaster = new OragnisationMaster();
            try
            {
                _StudentMaster = CSvc.GetSingleStudent(StudentID);
                _OragnisationMaster = CSvc.GetOragnisationDetails(_OrgnisationID);
                ViewBag.StudentDetail = _StudentMaster;
                ViewBag.OrganisationDetail = _OragnisationMaster;
            }
            catch { }
            return PartialView("AdmissionForm");
        }
        #region Multiple Student Print
        public PartialViewResult GetAdmissionformmulresult(string _studentid)
        {
            StudentModel _stumod = new StudentModel();
           // List<StudentMaster> _stulst = new List<StudentMaster>();
            try
            {
                _stumod.StudentList = CSvc.GetMultipleStudentslist(_studentid);
                _stumod.OrganisationDetail = CSvc.GetOragnisationDetails(_OrgnisationID);
            }
            catch (Exception ex)
            {

            }
            return PartialView("Admissionformmul", _stumod);

        }
        #endregion

        public void ExportExcelTamplate(DataSet ds, string flname, string tname)
        {
            using (ClosedXML.Excel.XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);
                var worksheet2 = wb.Worksheet("Master");

                foreach (var TamplateColumn in wb.Worksheet("Tamplate").Columns())
                {
                    var validation = TamplateColumn.Cell(2).Value.ToString();
                    var CellAddress = TamplateColumn.Cell(2).Address.ColumnLetter.ToString();
                    if (!string.IsNullOrEmpty(validation))
                    {
                        foreach (var MasterColumn in wb.Worksheet("Master").Columns())
                        {
                            if (MasterColumn.Cell(1).Value.ToString() == validation)
                            {
                                wb.Worksheet("Tamplate").Column(CellAddress).SetDataValidation().List(worksheet2.Range(MasterColumn.Cell(1).Address.ColumnLetter + "2:" + worksheet2.Column(MasterColumn.Cell(1).Address.ColumnLetter.ToString()).LastCellUsed().Address), true);
                            }
                        }
                    }
                }

                //wb.Worksheet("Tamplate").Row(2).Delete()
                worksheet2.Hide();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + flname);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.Protect();
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        [HttpGet]
        public ActionResult BulkImport()
        {
            BulkStudentImport model = new BulkStudentImport();
            return View(model);
        }
        [HttpPost]
        public ActionResult BulkImport(FormCollection collection, HttpPostedFileBase FileUpload)
        {
            BulkStudentImport model = new BulkStudentImport();
            if (collection["btnSearch"].ToUpper().Contains("Download".ToUpper()))
            {
                CSvc = new BALCommon(ConStr);
                DataTable dtMaster = new DataTable();
                DataTable dtTamplate = new DataTable();
                DataSet dsImport = new DataSet();
                DataSet dsMaster = new DataSet();
                int TotalMaster = 0;
                try
                {
                    dsMaster = CSvc.GetMasterForBulkStudentRegistration(_OrgnisationID, _Financialyearid);
                    dtTamplate = dsMaster.Tables[dsMaster.Tables.Count - 1].Copy();
                    dtTamplate.TableName = "Tamplate";
                    dsMaster.Tables.Remove(dsMaster.Tables[dsMaster.Tables.Count - 1]);
                    TotalMaster = dsMaster.Tables.Count;
                    object[] array = new object[TotalMaster];
                    foreach (DataTable tab in dsMaster.Tables)
                    {
                        foreach (DataColumn col in tab.Columns)
                        {
                            dtMaster.Columns.Add(col.ColumnName, typeof(string));
                        }
                    }

                    int l = 0;
                    bool HasValue = true;
                    while (HasValue)
                    {
                        HasValue = false;
                        array = new object[TotalMaster];
                        for (int i = 0; i < TotalMaster; i++)
                        {
                            if (dsMaster.Tables[i].Rows.Count >= (l + 1))
                            {
                                array[i] = dsMaster.Tables[i].Rows[l][0].ToString();
                                HasValue = true;
                            }
                        }
                        l++;
                        if (HasValue)
                            dtMaster.Rows.Add(array);
                    }
                    dtMaster.TableName = "Master";
                    dsImport.Tables.Add(dtTamplate);
                    dsImport.Tables.Add(dtMaster);

                    ExportExcelTamplate(dsImport, "StudentTamplate.xls", "SchoolData");
                }
                catch (Exception ex) { ExecptionLogger.FileHandling("Admin(BulkImport_Download)", "Error_014", ex, "Admin"); }
            }
            if (collection["btnSearch"].ToUpper().Contains("Upload".ToUpper()))
            {
                //DataTable dtDB = Utility.ConvertListToDataTable(model.SalaryDetailsList);
                //DataTable dtExcel = new DataTable();
                try
                {
                    if (FileUpload != null && (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                    {
                        string activeDir = Server.MapPath("~/SchoolMisData/" + "School_" + _OrgnisationID + "/" + DateTime.Now.ToString("dd-MMM-yyyy") + "/" + DateTime.Now.ToString("HH-mm-ss"));
                        DirectoryInfo objDirectoryInfo = new DirectoryInfo(activeDir);
                        if (!Directory.Exists(objDirectoryInfo.FullName))
                        {
                            Directory.CreateDirectory(activeDir);
                        }
                        string filePath = activeDir + "\\" + FileUpload.FileName;
                        if (FileUpload.FileName.EndsWith(".xls") || FileUpload.FileName.EndsWith(".xlsx"))
                        {
                            try
                            {
                                FileUpload.SaveAs(filePath);
                                DataSet ds = Utility.ConvertExceltoDataTable(filePath);
                                DataTable DTStudentData = new DataTable();
                                object[] StudentArray = null;
                                string resp = "";
                                if (ds.Tables["Tamplate"].Rows.Count > 1)
                                {
                                    DTStudentData.Columns.Add("SNO", typeof(int));
                                    DTStudentData.Columns.Add("Adminssionno", typeof(string));
                                    DTStudentData.Columns.Add("Off_dateofadmission", typeof(string));
                                    DTStudentData.Columns.Add("CMid", typeof(string));
                                    DTStudentData.Columns.Add("SecMid", typeof(string));
                                    DTStudentData.Columns.Add("FeeGroup", typeof(string));
                                    DTStudentData.Columns.Add("RollNo", typeof(string));
                                    DTStudentData.Columns.Add("Firstname", typeof(string));
                                    DTStudentData.Columns.Add("Bd_dob", typeof(string));
                                    DTStudentData.Columns.Add("Off_Aadharno", typeof(string));
                                    DTStudentData.Columns.Add("GMid", typeof(string));
                                    DTStudentData.Columns.Add("Bd_contactno", typeof(string));
                                    DTStudentData.Columns.Add("Bd_Emailid", typeof(string));
                                    DTStudentData.Columns.Add("Bd_fathername", typeof(string));
                                    DTStudentData.Columns.Add("Bd_qualification", typeof(string));
                                    DTStudentData.Columns.Add("Bd_fatheroccuption", typeof(string));
                                    DTStudentData.Columns.Add("Bd_fathermob", typeof(string));
                                    DTStudentData.Columns.Add("Bd_mothername", typeof(string));
                                    DTStudentData.Columns.Add("Bd_motherqualification", typeof(string));
                                    DTStudentData.Columns.Add("Bd_Motheroccuption", typeof(string));
                                    DTStudentData.Columns.Add("Bd_mothermob", typeof(string));
                                    DTStudentData.Columns.Add("Bd_dateofannversy", typeof(string));
                                    DTStudentData.Columns.Add("Relmid", typeof(string));
                                    DTStudentData.Columns.Add("Castmid", typeof(string));
                                    DTStudentData.Columns.Add("Categmid", typeof(string));
                                    DTStudentData.Columns.Add("Off_bloodgroup", typeof(string));
                                    DTStudentData.Columns.Add("VisionLeft", typeof(string));
                                    DTStudentData.Columns.Add("Off_lastschool", typeof(string));
                                    DTStudentData.Columns.Add("Off_Examgiven", typeof(string));
                                    DTStudentData.Columns.Add("Off_boardoruniversity", typeof(string));
                                    DTStudentData.Columns.Add("Off_heightweight", typeof(string));
                                    DTStudentData.Columns.Add("Off_Hosalroomno", typeof(string));
                                    DTStudentData.Columns.Add("Off_bedno", typeof(string));
                                    DTStudentData.Columns.Add("Off_biometricno", typeof(string));
                                    DTStudentData.Columns.Add("Off_nationality", typeof(string));
                                    DTStudentData.Columns.Add("Bd_address1", typeof(string));
                                    DTStudentData.Columns.Add("Bd_address2", typeof(string));
                                    DTStudentData.Columns.Add("IsActive", typeof(bool));
                                    DTStudentData.Columns.Add("SchoolID", typeof(int));
                                    DTStudentData.Columns.Add("Createdate", typeof(string));
                                    DTStudentData.Columns.Add("Createdby", typeof(string));
                                    DTStudentData.Columns.Add("Modifiedby", typeof(string));
                                    DTStudentData.Columns.Add("Modifieddate", typeof(string));
                                    DTStudentData.Columns.Add("FinancialYear", typeof(int));
                                    DTStudentData.Columns.Add("Status", typeof(string));
                                    int c = 1;
                                    for (int l = 1; l < ds.Tables["Tamplate"].Rows.Count; l++)
                                    {
                                        resp = "";
                                        StudentArray = new object[DTStudentData.Columns.Count];
                                        StudentArray[0] = c++;
                                        StudentArray[1] = ds.Tables["Tamplate"].Rows[l]["Admission Form No"].ToString();
                                        StudentArray[2] = ds.Tables["Tamplate"].Rows[l]["Date of Admission"].ToString();
                                        StudentArray[3] = ds.Tables["Tamplate"].Rows[l]["Class"].ToString();
                                        StudentArray[4] = ds.Tables["Tamplate"].Rows[l]["Section"].ToString();
                                        StudentArray[5] = ds.Tables["Tamplate"].Rows[l]["Fees Group"].ToString();
                                        StudentArray[6] = ds.Tables["Tamplate"].Rows[l]["Roll Number"].ToString();
                                        StudentArray[7] = ds.Tables["Tamplate"].Rows[l]["Student Name"].ToString();
                                        StudentArray[8] = ds.Tables["Tamplate"].Rows[l]["Student Date of Birth"].ToString();
                                        StudentArray[9] = ds.Tables["Tamplate"].Rows[l]["Aadhar Card UID No"].ToString();
                                        StudentArray[10] = ds.Tables["Tamplate"].Rows[l]["Gender"].ToString();
                                        StudentArray[11] = ds.Tables["Tamplate"].Rows[l]["Mobile No (SMS)"].ToString();
                                        StudentArray[12] = ds.Tables["Tamplate"].Rows[l]["Email Id"].ToString();
                                        StudentArray[13] = ds.Tables["Tamplate"].Rows[l]["Father's Name"].ToString();
                                        StudentArray[14] = ds.Tables["Tamplate"].Rows[l]["Father's Education"].ToString();
                                        StudentArray[15] = ds.Tables["Tamplate"].Rows[l]["Father's Occupation"].ToString();
                                        StudentArray[16] = ds.Tables["Tamplate"].Rows[l]["Father's Mobile No"].ToString();
                                        StudentArray[17] = ds.Tables["Tamplate"].Rows[l]["Mother's Name"].ToString();
                                        StudentArray[18] = ds.Tables["Tamplate"].Rows[l]["Mother's Education"].ToString();
                                        StudentArray[19] = ds.Tables["Tamplate"].Rows[l]["Mother's Occupation"].ToString();
                                        StudentArray[20] = ds.Tables["Tamplate"].Rows[l]["Mother's Mobile No"].ToString();
                                        StudentArray[21] = ds.Tables["Tamplate"].Rows[l]["Parents Wedding Date (for Anniversary Wish)"].ToString();
                                        StudentArray[22] = ds.Tables["Tamplate"].Rows[l]["Religion"].ToString();
                                        StudentArray[23] = ds.Tables["Tamplate"].Rows[l]["Cast"].ToString();
                                        StudentArray[24] = ds.Tables["Tamplate"].Rows[l]["Category"].ToString();
                                        StudentArray[25] = ds.Tables["Tamplate"].Rows[l]["Blood Group"].ToString();
                                        StudentArray[26] = ds.Tables["Tamplate"].Rows[l]["Eye Vision"].ToString();
                                        StudentArray[27] = ds.Tables["Tamplate"].Rows[l]["Last School/Institute/College"].ToString();
                                        StudentArray[28] = ds.Tables["Tamplate"].Rows[l]["Last Exam Given"].ToString();
                                        StudentArray[29] = ds.Tables["Tamplate"].Rows[l]["Board/University"].ToString();
                                        StudentArray[30] = ds.Tables["Tamplate"].Rows[l]["Student Height,Weight"].ToString();
                                        StudentArray[31] = ds.Tables["Tamplate"].Rows[l]["Hostel Room No"].ToString();
                                        StudentArray[32] = ds.Tables["Tamplate"].Rows[l]["Bed No"].ToString();
                                        StudentArray[33] = ds.Tables["Tamplate"].Rows[l]["Biometrics/RFID No"].ToString();
                                        StudentArray[34] = ds.Tables["Tamplate"].Rows[l]["Nationality"].ToString();
                                        StudentArray[35] = ds.Tables["Tamplate"].Rows[l]["Permanent Address"].ToString();
                                        StudentArray[36] = ds.Tables["Tamplate"].Rows[l]["Local Address"].ToString();
                                        StudentArray[37] = true;
                                        StudentArray[38] = _OrgnisationID;
                                        StudentArray[39] = DateTime.Now.ToString();
                                        StudentArray[40] = WebSecurity.CurrentUserName.ToString();
                                        StudentArray[41] = WebSecurity.CurrentUserName.ToString();
                                        StudentArray[42] = DateTime.Now.ToString();
                                        StudentArray[43] = _Financialyearid;
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Class"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Class";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Section"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Section";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Student Name"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Student Name";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Student Date of Birth"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Student Date of Birth";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Gender"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Gender";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Mobile No (SMS)"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Mobile No (SMS)";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Email Id"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Email Id";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Religion"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Religion";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Cast"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Cast";
                                        if (string.IsNullOrEmpty(ds.Tables["Tamplate"].Rows[l]["Category"].ToString()))
                                            resp += ((resp == "") ? "" : ",") + "Category";

                                        StudentArray[44] = (resp == "") ? "" : ("Missing/Incorrect Information:- " + resp);

                                        DTStudentData.Rows.Add(StudentArray);
                                    }
                                    DataTable DTResult = CSvc.StudentRegistration(DTStudentData);
                                    ds.Tables["Tamplate"].Columns.Add("Status");
                                    for (int l = 1; l < DTResult.Rows.Count; l++)
                                    {
                                        ds.Tables["Tamplate"].Rows[l]["Status"] = DTResult.Rows[l - 1]["Status"].ToString();
                                    }
                                    ExportExcelTamplate(ds, "StudentRegistration.xls", "SchoolData");
                                }
                                else
                                {
                                    TempData[Constants.MessageInfo.ERROR] = "No record found in uploaded excel sheet.";
                                }

                            }
                            catch (Exception ex)
                            {
                                if (System.IO.File.Exists(filePath))
                                { System.IO.File.Delete(filePath); }
                                TempData[Constants.MessageInfo.ERROR] = ex.Message.ToString();
                                ExecptionLogger.FileHandling("Admin(BulkImport_upload1)", "Error_014", ex, "Admin");
                            }
                            finally
                            {

                            }
                        }
                        else
                        {
                            TempData[Constants.MessageInfo.ERROR] = "Please upload valid excel file.";
                        }
                    }
                    else
                    {
                        TempData[Constants.MessageInfo.ERROR] = "Please upload valid excel file.";
                    }
                }
                catch (Exception ex)
                {
                    TempData[Constants.MessageInfo.ERROR] = ex.Message.ToString();
                    ExecptionLogger.FileHandling("Admin(BulkImport_upload2)", "Error_014", ex, "Admin");
                }
            }
            return View(model);

        }


        #region CourseManagementSystem
        public ActionResult SubjectList()
        {
            SubjectModel model = new SubjectModel();
            model.SubjectTblList = CSvc.GetSubjectListTBL(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateSubject(Nullable<int> Sumid, string ActionName = "")
        {
            SubjectModel model = new SubjectModel();
            SubjectMaster existingsub = new SubjectMaster();
            if (Sumid.HasValue)
            {
                existingsub = CSvc.GetSubjectBySumid(Sumid.Value);
                model.Sumid = existingsub.Sumid;
                model.Subjectname = existingsub.Subjectname;
                model.Subjectdescription = existingsub.Subjectdescription;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            //model.ClassList = CSvc.GetClassList(_OrgnisationID);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSubject(SubjectModel model, FormCollection collection)
        {
            SubjectMaster submast = CSvc.GetSubjectBySumid(model.Sumid);
            submast.Subjectname = model.Subjectname.Trim();
            submast.Subjectdescription = model.Subjectdescription.Trim();
            submast.schoolid = _OrgnisationID;
            submast.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                submast.Createdate = DateTime.Now;
                submast.Createdby = WebSecurity.CurrentUserName;
                submast.Modifieddate = DateTime.Now;
                submast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddSubject(submast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Subject_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Subject_AlreadyExist_SUCCESS;
                return RedirectToAction("subjectList");
            }
            else
            {
                submast.Modifieddate = DateTime.Now;
                submast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddSubject(submast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Subject_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Subject_ADD_FAIL;

                return RedirectToAction("subjectList");
            }

        }
        public JsonResult CheckIsSubjectNameExist(string _subjectname)
        {
            SubjectModel model = new SubjectModel();
            model.SubjectTblList = CSvc.GetSubjectListTBL(_OrgnisationID);
            var _result = (from myrow in model.SubjectTblList.AsEnumerable()
                           where myrow.Field<string>("Subject Name") == _subjectname
                           select myrow).FirstOrDefault();

            bool issujectnameallowed = _result.ItemArray.Length > 0 ? true : false;
            return Json(issujectnameallowed, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSubject(SubjectModel model)
        {

            if (model != null && model.Sumid != default(int))
            {
                SubjectMaster submst = CSvc.GetSubjectBySumid(model.Sumid);
                if (submst != null)
                {
                    submst.IsActive = false;
                    submst.Modifieddate = DateTime.Now;
                    submst.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddSubject(submst, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Subject_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Subject_DELETE_FAIL;

                    return RedirectToAction("subjectlist");
                }
            }
            return View();
        }

        /*-----------Subject's Chapter Assignment Process------*/
        public ActionResult SubjectChapterList()
        {
            SubjectChapterModel model = new SubjectChapterModel();
            model.SubjectChapterTblList = CSvc.GetSubjectChapterListTBL(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateSubjectChapter(Nullable<int> Scmid, string ActionName = "")
        {
            SubjectChapterModel model = new SubjectChapterModel();
            SubjectchapterMaster existsubchap = new SubjectchapterMaster();
            if (Scmid.HasValue)
            {
                existsubchap = CSvc.GetSubjectChapterBySumid(Scmid.Value);
                model.Scmid = existsubchap.Scmid;
                model.Chaptername = existsubchap.Chaptername;
                model.Chapterdes = existsubchap.Chapterdes;
                model.CMid = existsubchap.CMid;
                model.Sumid = existsubchap.Sumid;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.SubjectList = CSvc.GetSubjectList(_OrgnisationID);
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateSubjectChapter(SubjectChapterModel model, FormCollection collection)
        {
            SubjectchapterMaster subchapmast = CSvc.GetSubjectChapterBySumid(model.Scmid);
            subchapmast.Chaptername = model.Chaptername.Trim();
            subchapmast.Chapterdes = model.Chapterdes.Trim();
            subchapmast.CMid = model.CMid;
            subchapmast.Sumid = model.Sumid;
            subchapmast.schoolid = _OrgnisationID;
            subchapmast.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                subchapmast.Createdate = DateTime.Now;
                subchapmast.Createdby = WebSecurity.CurrentUserName;
                subchapmast.Modifieddate = DateTime.Now;
                subchapmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddSubjectchapter(subchapmast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.SubjectChapter_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.SubjectChapter_AlreadyExist_SUCCESS;
                return RedirectToAction("subjectchapterlist");
            }
            else
            {
                subchapmast.Modifieddate = DateTime.Now;
                subchapmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddSubjectchapter(subchapmast, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.SubjectChapter_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.SubjectChapter_ADD_FAIL;

                return RedirectToAction("subjectchapterlist");
            }

        }
        public JsonResult CheckIsSubjectChapterExist(string _subject, string _classid, string _chaptername)
        {
            SubjectChapterModel model = new SubjectChapterModel();
            model.SubjectchapterList = CSvc.GetSubjectChapterList(_OrgnisationID);
            var _result = (from myrow in model.SubjectchapterList.ToList()
                           where (myrow.Sumid == Convert.ToInt32(_subject) && myrow.CMid == Convert.ToInt32(_classid) && myrow.Chaptername == Convert.ToString(_chaptername))
                           select myrow);

            bool isSubjectChapterallowed = _result.ToList().Count() > 0 ? true : false;
            return Json(isSubjectChapterallowed, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteSubjectChapter(SubjectChapterModel model)
        {

            if (model != null && model.Scmid != default(int))
            {
                SubjectchapterMaster submst = CSvc.GetSubjectChapterBySumid(model.Scmid);
                if (submst != null)
                {
                    submst.IsActive = false;
                    submst.Modifieddate = DateTime.Now;
                    submst.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddSubjectchapter(submst, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.SubjectChapter_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.WARNING] = Constants.Subject_ADD_FAIL;

                    return RedirectToAction("subjectchapterlist");
                }
            }
            return View();
        }
        /*-----------End Subject's Chapter Assignment Process--*/
        /*-----------------Period Master---------------------------*/
        public ActionResult PeriodList()
        {
            PeriodModel model = new PeriodModel();
            //model.PeriodmasterTblList = CSvc.GetSubjectChapterListTBL(_OrgnisationID);
            model.PeriodmasterTblList = CSvc.GetPeriodmasterTblList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreatePeriod(Nullable<int> Pmid, string ActionName = "")
        {
            PeriodModel model = new PeriodModel();
            PeriodMaster existperiod = new PeriodMaster();
            if (Pmid.HasValue)
            {
                existperiod = CSvc.GetPeriodByPmid(Pmid.Value);
                model.Pmid = existperiod.Pmid;
                model.PeriodName = existperiod.PeriodName;
                model.PeriodStart = existperiod.PeriodStart.ToString();
                model.PeriodEnd = existperiod.PeriodEnd.ToString(); ;
                model.Perioddesc = existperiod.Perioddesc;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePeriod(PeriodModel model, FormCollection collection)
        {
            PeriodMaster periodmast = CSvc.GetPeriodByPmid(model.Pmid);
            periodmast.PeriodName = model.PeriodName.Trim();
            periodmast.Perioddesc = model.Perioddesc.Trim();
            periodmast.PeriodStart = model.PeriodStart;
            periodmast.PeriodEnd = model.PeriodEnd;
            periodmast.schoolid = _OrgnisationID;
            periodmast.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                periodmast.Createdate = DateTime.Now;
                periodmast.Createdby = WebSecurity.CurrentUserName;
                periodmast.Modifieddate = DateTime.Now;
                periodmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddPeriod(periodmast, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Period_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Period_AlreadyExist_SUCCESS;
                return RedirectToAction("periodlist");
            }
            else
            {
                periodmast.Modifieddate = DateTime.Now;
                periodmast.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddPeriod(periodmast, "UPD");
                TempData[Constants.MessageInfo.SUCCESS] = Constants.Period_UPDATE_SUCCESS;

                return RedirectToAction("periodlist");
            }

        }
        public JsonResult CheckIsPeriodExist(string _periodname, string _psttime, string _pendtime)
        {
            PeriodModel model = new PeriodModel();
            model.PeriodmasterList = CSvc.GetPeriodMasterList(_OrgnisationID);
            var _result = (from myrow in model.PeriodmasterList.ToList()
                               // where (myrow.PeriodName.ToLower() == _periodname.ToLower() && myrow.PeriodStart == Convert.ToString(_psttime) && myrow.PeriodEnd == Convert.ToString(_pendtime))
                           where (myrow.PeriodStart == Convert.ToString(_psttime) && myrow.PeriodEnd == Convert.ToString(_pendtime))
                           select myrow);

            var _timeresult = (from myrow in model.PeriodmasterList.ToList()
                               where (myrow.PeriodStart == Convert.ToString(_psttime) && myrow.PeriodEnd == Convert.ToString(_pendtime))
                               select myrow);

            bool isnamePeriodallowed = _result.ToList().Count() > 0 ? true : false;
            bool istimePeriodallowed = _result.ToList().Count() > 0 ? true : false;
            Hashtable _arrlst = new Hashtable();
            _arrlst.Add("periodname", isnamePeriodallowed);
            _arrlst.Add("periodtime", istimePeriodallowed);
            return Json(_arrlst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeletePeriod(PeriodModel model)
        {

            if (model != null && model.Pmid != default(int))
            {
                PeriodMaster periodmst = CSvc.GetPeriodByPmid(model.Pmid);
                if (periodmst != null)
                {
                    periodmst.IsActive = false;
                    periodmst.Modifieddate = DateTime.Now;
                    periodmst.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddPeriod(periodmst, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Period_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Period_DELETE_FAIL;

                    return RedirectToAction("periodlist");
                }
            }
            return View();
        }
        /*-----------------End Period Master------------------------*/
        /*-------------------Assign Period to teacher----------------*/
        public ActionResult AssignperiodteacherList()
        {
            AssignperiodteacherModel model = new AssignperiodteacherModel();
            model.AssignperiodteacherTblList = CSvc.GetAssignperiodteacherListTBL(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateAssignperiodteacher(Nullable<int> Apttmid, string ActionName = "")
        {
            AssignperiodteacherModel model = new AssignperiodteacherModel();
            Assignperiodteacher existassignperiod = new Assignperiodteacher();
            if (Apttmid.HasValue)
            {
                existassignperiod = CSvc.GetAssignperiodteacherByApttmid(Apttmid.Value);
                model.Apttmid = existassignperiod.Apttmid;
                model.EMP_ID = existassignperiod.EMP_ID;
                model.Pmid = existassignperiod.Pmid;
                model.CMid = existassignperiod.CMid;
                model.Secmid = existassignperiod.Secmid;
                model.Perioddescription = existassignperiod.Perioddescription;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.TeacherList = CSvc.GetTeacherList(1, _OrgnisationID);
            model.PeriodmasterList = CSvc.GetPeriodMasterList(_OrgnisationID);
            model.ClassmasterList = CSvc.GetClassList(_OrgnisationID);
            model.SectionmasterList = CSvc.GetSectionList(_OrgnisationID, 1);

            return View(model);
        }
        [HttpPost]
        public ActionResult CreateAssignperiodteacher(AssignperiodteacherModel model, FormCollection collection)
        {
            Assignperiodteacher assignperiod = CSvc.GetAssignperiodteacherByApttmid(model.Apttmid);
            assignperiod.Apttmid = model.Apttmid;
            assignperiod.EMP_ID = model.EMP_ID;
            assignperiod.Pmid = model.Pmid;
            assignperiod.CMid = model.CMid;
            assignperiod.Secmid = model.Secmid;
            assignperiod.Perioddescription = model.Perioddescription;
            assignperiod.schoolid = _OrgnisationID;
            assignperiod.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                assignperiod.Createdate = DateTime.Now;
                assignperiod.Createdby = WebSecurity.CurrentUserName;
                assignperiod.Modifieddate = DateTime.Now;
                assignperiod.Modifiedby = WebSecurity.CurrentUserName;
                //var _resjson = CheckIsAssignperiodteacherExist(model.EMP_ID.ToString(), model.Pmid.ToString(), model.CMid.ToString(), model.Secmid.ToString());
                /*-------------Below code check already exist records-----------------*/
                model.AssignperiodteacherList = CSvc.GetAssignperiodteacherList(_OrgnisationID);
                var _result = (from myrow in model.AssignperiodteacherList.ToList()
                               where (myrow.EMP_ID == model.EMP_ID && myrow.Pmid == model.Pmid && myrow.CMid == model.CMid && myrow.Secmid == model.Secmid)
                               select myrow);
                /*----------------Check Teacher Lecture Assign---------------*/
                var _assignresult = (from myrow in model.AssignperiodteacherList.ToList()
                                     where (myrow.EMP_ID == model.EMP_ID && myrow.Pmid == model.Pmid)
                                     select myrow);
                /*-------------End Below code check already exist records-----------------*/
                if (_assignresult.Count() == 0)
                {
                    if (_result.Count() == 0)
                    {
                        int _returntype = CSvc.AddAssignperiodteacher(assignperiod, "INS");
                        if (_returntype > 0)
                            TempData[Constants.MessageInfo.SUCCESS] = Constants.AssignPeriod_Add_SUCCESS;
                        else
                            TempData[Constants.MessageInfo.WARNING] = Constants.AssignPeriod_AlreadyExist_SUCCESS;
                    }
                    else
                    {
                        TempData[Constants.MessageInfo.WARNING] = Constants.AssignPeriod_AlreadyExist_SUCCESS;
                    }
                }
                else
                {
                    TempData[Constants.MessageInfo.WARNING] = "Teacher have Assigned the lecture in another class for this time ";
                }
                return RedirectToAction("assignperiodteacherlist");
            }
            else
            {
                assignperiod.Modifieddate = DateTime.Now;
                assignperiod.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddAssignperiodteacher(assignperiod, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.AssignPeriod_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.AssignPeriod_ADD_FAIL;

                return RedirectToAction("assignperiodteacherlist");
            }

        }

        public JsonResult CheckIsAssignperiodteacherExist(string _EMP_ID, string _Pmid, string _CMid, string _Secmid)
        {
            AssignperiodteacherModel model = new AssignperiodteacherModel();
            model.AssignperiodteacherList = CSvc.GetAssignperiodteacherList(_OrgnisationID);
            var _result = (from myrow in model.AssignperiodteacherList.ToList()
                           where (myrow.EMP_ID.ToString() == _EMP_ID.ToLower() && myrow.Pmid.ToString() == _Pmid.ToString())
                           select myrow);

            bool isAssignperiodteacherallowed = _result.ToList().Count() > 0 ? true : false;
            return Json(isAssignperiodteacherallowed, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteAssignperiodteacher(AssignperiodteacherModel model)
        {

            if (model != null && model.Apttmid != default(int))
            {
                Assignperiodteacher assignperiod = CSvc.GetAssignperiodteacherByApttmid(model.Apttmid);
                if (assignperiod != null)
                {
                    assignperiod.IsActive = false;
                    assignperiod.Modifieddate = DateTime.Now;
                    assignperiod.Modifiedby = WebSecurity.CurrentUserName;
                    CSvc.AddAssignperiodteacher(assignperiod, "DEL");
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.AssignPeriod_DELETE_SUCCESS;
                    //else
                    //TempData[Constants.MessageInfo.SUCCESS] = Constants.AssignPeriod_DELETE_FAIL;

                    return RedirectToAction("assignperiodteacherlist");
                }
            }
            return View();
        }

        /*-------------------End Assign Period to teacher----------------*/
        /*-------------------Assign Lecture content---------------------------*/
        public ActionResult AssignlecturecontentList()
        {
            AssigncontentinlectureModel model = new AssigncontentinlectureModel();
            model.AssigncontentinlectureTblList = CSvc.AssigncontentinlectureTblList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateAssignlecturecontent(Nullable<int> Acdtmid, string ActionName = "")
        {
            AssigncontentinlectureModel model = new AssigncontentinlectureModel();
            Assigncontentinlecture existassigncontenturl = new Assigncontentinlecture();
            if (Acdtmid.HasValue)
            {
                existassigncontenturl = CSvc.GetAssigncontentinlectureByAcdtmid(Acdtmid.Value);
                model.Acdtmid = existassigncontenturl.Acdtmid;
                model.Apttmid = existassigncontenturl.Apttmid;
                model.Sumid = existassigncontenturl.Sumid;
                model.Scmid = existassigncontenturl.Scmid;
                model.Topic = existassigncontenturl.Topic;
                model.Contents = existassigncontenturl.Contents;
                model.Files = existassigncontenturl.Files;
                model.Onlineurl = existassigncontenturl.Onlineurl;
                model.Dates = existassigncontenturl.Dates;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.AssignperiodteacherList = CSvc.GetAssignperiodteacherList(_OrgnisationID);
            model.SubjectList = new List<SubjectMaster>();
            model.SubjectchapterList = new List<SubjectchapterMaster>();
            //model.SubjectList = CSvc.GetSubjectList(_OrgnisationID);
            //model.SubjectchapterList =CSvc.GetSubjectChapterList(_OrgnisationID);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateAssignlecturecontent(AssigncontentinlectureModel model, FormCollection collection)
        {
            Assigncontentinlecture _assigncontentlec = CSvc.GetAssigncontentinlectureByAcdtmid(model.Acdtmid);
            HttpPostedFileBase lecturepath = Request.Files["contentfiles"];
            if (lecturepath.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(lecturepath.FileName);
                string folderpath = Constants.Lecture_IMAGEPAN;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                lecturepath.SaveAs(filepath);
                _assigncontentlec.Files = dbpath;
            }
            _assigncontentlec.Acdtmid = model.Acdtmid;
            _assigncontentlec.Apttmid = model.Apttmid;
            _assigncontentlec.Sumid = model.Sumid;
            _assigncontentlec.Scmid = model.Scmid;
            _assigncontentlec.Topic = model.Topic;
            _assigncontentlec.Contents = model.Contents;
            _assigncontentlec.Onlineurl = model.Onlineurl;
            _assigncontentlec.Dates = model.Dates;
            _assigncontentlec.schoolid = _OrgnisationID;
            _assigncontentlec.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                _assigncontentlec.Createdate = DateTime.Now;
                _assigncontentlec.Createdby = WebSecurity.CurrentUserName;
                _assigncontentlec.Modifieddate = DateTime.Now;
                _assigncontentlec.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddAssigncontentinlecture(_assigncontentlec, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.AssignPeriod_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.AssignPeriod_AlreadyExist_SUCCESS;
                return RedirectToAction("AssignlecturecontentList");
            }
            else
            {
                _assigncontentlec.Modifieddate = DateTime.Now;
                _assigncontentlec.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddAssigncontentinlecture(_assigncontentlec, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.AssignPeriod_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.AssignPeriod_ADD_FAIL;

                return RedirectToAction("AssignlecturecontentList");
            }

        }
        [HttpPost]
        public ActionResult DeleteAssignlecturecontent(AssigncontentinlectureModel model)
        {
            if (model != null && model.Acdtmid != default(int))
            {
                Assigncontentinlecture _assigncontentlec = CSvc.GetAssigncontentinlectureByAcdtmid(model.Acdtmid);
                if (_assigncontentlec != null)
                {
                    _assigncontentlec.IsActive = false;
                    _assigncontentlec.Modifieddate = DateTime.Now;
                    _assigncontentlec.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddAssigncontentinlecture(_assigncontentlec, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Assignleccont_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.WARNING] = Constants.Assignleccont_DELETE_FAIL;

                    return RedirectToAction("AssignlecturecontentList");
                }
            }
            return View();
        }

        public JsonResult Getchapterbaseonsubjectid(int subjectId)
        {
            List<SubjectchapterMaster> SubjectchapterList = CSvc.GetchapterbaseonsubjectList(subjectId);
            Hashtable _hstbl = new Hashtable();
            _hstbl.Add("subjectchapterlst", SubjectchapterList);
            return Json(_hstbl, JsonRequestBehavior.AllowGet);
            //_hstbl return Json(SectionList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getsubjectbaseonlecture(int lectureid)
        {

            List<SubjectMaster> SubjectList = CSvc.GetSubjectbaseonlectureList(lectureid);
            Hashtable _hstbl = new Hashtable();
            _hstbl.Add("subjectlst", SubjectList);
            return Json(_hstbl, JsonRequestBehavior.AllowGet);
            //_hstbl return Json(SectionList, JsonRequestBehavior.AllowGet);
        }
        /*-------------------End Assign Lecture content-----------------------*/
        #endregion
        #region BellSystem
        public ActionResult BellsystemList()
        {
            BellSystemModel model = new BellSystemModel();
            model.BellSystemTblList = CSvc.GetBellSystemListTBL(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateBellsystem(Nullable<int> bmid, string ActionName = "")
        {
            BellSystemModel model = new BellSystemModel();
            BellSystemMaster existingsub = new BellSystemMaster();
            if (bmid.HasValue)
            {
                existingsub = CSvc.GetBellSystemBybmid(bmid.Value);
                model.bmid = existingsub.bmid;
                model.Pmid = existingsub.Pmid;
                model.Belltitle = existingsub.Belltitle;
                model.Bellsongpath = existingsub.Bellsongpath;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.PeriodmasterList = CSvc.GetPeriodMasterList(_OrgnisationID);
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBellsystem(BellSystemModel model, FormCollection collection)
        {
            BellSystemMaster bellsys = CSvc.GetBellSystemBybmid(model.bmid);
            HttpPostedFileBase alarmpath = Request.Files["contentfiles"];
            if (alarmpath.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(alarmpath.FileName);
                string folderpath = Constants.Alarm_Files;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                alarmpath.SaveAs(filepath);
                bellsys.Bellsongpath = dbpath;
            }
            bellsys.Pmid = model.Pmid;
            bellsys.Belltitle = model.Belltitle.Trim();
            // bellsys.Bellsongpath = model.Bellsongpath.Trim();
            bellsys.schoolid = _OrgnisationID;
            bellsys.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                bellsys.Createdate = DateTime.Now;
                bellsys.Createdby = WebSecurity.CurrentUserName;
                bellsys.Modifieddate = DateTime.Now;
                bellsys.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddBellSystem(bellsys, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Bellsystem_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Bellsystem_AlreadyExist_SUCCESS;
                return RedirectToAction("BellsystemList");
            }
            else
            {
                bellsys.Modifieddate = DateTime.Now;
                bellsys.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddBellSystem(bellsys, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Bellsystem_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Bellsystem_ADD_FAIL;

                return RedirectToAction("BellsystemList");
            }

        }
        public JsonResult CheckIsBellSystemExist(int Pmid)
        {
            BellSystemModel model = new BellSystemModel();
            model.BellSystemTblList = CSvc.GetBellSystemListTBL(_OrgnisationID);
            var _result = (from myrow in model.BellSystemTblList.AsEnumerable()
                           where myrow.Field<int>("Pmid") == Pmid
                           select myrow).FirstOrDefault();

            bool issujectnameallowed = _result.ItemArray.Length > 0 ? true : false;
            return Json(issujectnameallowed, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteBellSystem(BellSystemModel model)
        {

            if (model != null && model.bmid != default(int))
            {
                BellSystemMaster submst = CSvc.GetBellSystemBybmid(model.bmid);
                if (submst != null)
                {
                    submst.IsActive = false;
                    submst.Modifieddate = DateTime.Now;
                    submst.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddBellSystem(submst, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Bellsystem_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.WARNING] = Constants.Bellsystem_DELETE_FAIL;

                    return RedirectToAction("BellsystemList");
                }
            }
            return View();
        }

        #endregion

        #region Mangestudentleave
        public ActionResult Managementstudentleave()
        {
            //  model.StudenttblList = CSvc.GetStudentTbl(1, _OrgnisationID, _Financialyearid);
            StudentleaveModel model = new StudentleaveModel();
            Studentleavedetail _obj = new Studentleavedetail();
            model.StudentleaveListTbl = CSvc.GetStudentleaveTbl(_obj, null, null, _OrgnisationID, _Financialyearid);
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            return View(model);
        }
        [HttpPost]
        public ActionResult Managementstudentleave(StudentleaveModel model)
        {
            Studentleavedetail _obj = new Studentleavedetail();
            _obj.Leavestartdate = model.Leavestartdate;
            _obj.Leaveenddate = model.Leaveenddate;
            _obj.LeaveStatus = model.LeaveStatus;
            model.StudentleaveListTbl = CSvc.GetStudentleaveTbl(_obj, model.CMid, model.SecMid, _OrgnisationID, _Financialyearid);
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteStudentleave(StudentleaveModel model)
        {

            if (model != null && model.Slmid != default(int))
            {
                Studentleavedetail _stleave = CSvc.GetStudentleavedetailBySlmid(model.Slmid);
                
                if (_stleave != null)
                {
                    _stleave.Modifieddate = DateTime.Now;
                    _stleave.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddStudentleave(_stleave, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Studentleave_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.WARNING] = Constants.Studentleave_DELETE_FAIL;

                    return RedirectToAction("Managementstudentleave");
                }
            }
            return View();
        }

        public ActionResult studentallyleave(Nullable<int> Slmid,string ActionName="")
        {
            StudentleaveModel model = new StudentleaveModel();
            Studentleavedetail _studleave = new Studentleavedetail();
            if (Slmid.HasValue)
            {
                Studentleavedetail _stleave = CSvc.GetStudentleavedetailBySlmid(Convert.ToInt32(Slmid.HasValue));
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            model.Studentdetaillist = CSvc.GetStudentddl(true, _OrgnisationID, _Financialyearid);
           
            return View(model);
        }
        [HttpPost]
        public ActionResult studentallyleave(StudentleaveModel model,FormCollection collection)
        {
            Studentleavedetail _stleave = CSvc.GetStudentleavedetailBySlmid(model.Slmid);
            HttpPostedFileBase leaveattach = Request.Files["leavedocumentimge"];
            if (leaveattach.ContentLength>0)
            {
                string _FileName = Path.GetFileName(leaveattach.FileName);
                string folderpath = Constants.Alarm_Files;
                string guidstring = Guid.NewGuid().ToString();
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                leaveattach.SaveAs(filepath);
                _stleave.Leavedocument = dbpath;
            }
            _stleave.Leavestartdate = model.Leavestartdate;
            _stleave.Leaveenddate = model.Leaveenddate;
            _stleave.LeaveRreason = model.LeaveRreason;
            _stleave.Smid = model.Smid;
            _stleave.SchoolId = _OrgnisationID;
            _stleave.FMid = _Financialyearid;
            _stleave.LeaveStatus = "Pending";
            if (model.Slmid==0)
            {
                _stleave.Createddate = DateTime.Now;
                _stleave.CreatedBy = WebSecurity.CurrentUserName;
                _stleave.Modifieddate = DateTime.Now;
                _stleave.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddStudentleave(_stleave, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Studentleave_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Studentleave_AlreadyExist_SUCCESS;
              
            }
            return RedirectToAction("Managementstudentleave");

        }
        public JsonResult Processstudentleavestatus(string seekid,string leavestatus)
        {
            Studentleavedetail _stleave = new Studentleavedetail();
            String _msg = "Some Issue try after some time !";
            if (Convert.ToInt32(seekid)>0)
            {
                 _stleave = CSvc.GetStudentleavedetailBySlmid(Convert.ToInt32(seekid));
                _stleave.LeaveStatus = leavestatus;
                _stleave.Remarks = leavestatus;
                _stleave.Modifieddate = DateTime.Now;
                _stleave.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddStudentleave(_stleave, "UPD");
                if (_returntype > 0)
                    _msg = Constants.Studentleave_UPDATE_SUCCESS;
            }
            return Json(_msg, JsonRequestBehavior.AllowGet);


        }
        public PartialViewResult Getstudentrecordsresult(string _studentid)
        {
            StudentModel _stumod = new StudentModel();
            try
            {
                _stumod.StudentList = CSvc.GetMultipleStudentslist(_studentid);
                _stumod.OrganisationDetail = CSvc.GetOragnisationDetails(_OrgnisationID);
            }
            catch (Exception ex)
            {

            }
            return PartialView("Studentdetailpreview", _stumod);

        }

        public ActionResult Promotestudent()
        {
            BellSystemModel model = new BellSystemModel();
            return View(model);
        }
        public ActionResult inactivestudent()
        {
            StudentModel model = new StudentModel();
            model.StudenttblList = CSvc.GetStudentTbl(model.CMid, model.SecMid, model.Startdate, model.Enddate, 0, _OrgnisationID, _Financialyearid);
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            return View(model);
        }
        [HttpPost]
        public ActionResult inactivestudent(StudentModel model,FormCollection collection)
        {
            model.StudenttblList = CSvc.GetStudentTbl(model.CMid, model.SecMid, model.Startdate, model.Enddate,0, _OrgnisationID, _Financialyearid);

            string commandType = collection["rptbtn"];
            if (commandType == "Export")
            {
                if (model.StudenttblList.Tables[1] != null)
                {
                    string folderpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Files";
                    bool exists = System.IO.Directory.Exists(folderpath);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(folderpath);
                    string filepath = folderpath + "\\" + Guid.NewGuid() + "_StudentList.xls";

                    CommonExport.ExportToExcel(model.StudenttblList.Tables[1], "Student Records", filepath);
                    byte[] file = System.IO.File.ReadAllBytes(filepath);
                    if (System.IO.File.Exists(filepath))
                        System.IO.File.Delete(filepath);
                    return File(file, "application/vnd.ms-excel", "Student Records.xls");
                }

            }
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, model.CMid);
            return View(model);
        }


     
        public ActionResult strengthstudent()
        {
            StudentModel model = new StudentModel();
            model.StudentstrengthtblList = CSvc.GetStudentStrengthTbl(model.CMid, model.SecMid, _OrgnisationID, _Financialyearid);
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, 1);
            return View(model);
        }
        [HttpPost]
        public ActionResult strengthstudent(StudentModel model,FormCollection collection)
        {
            model.StudentstrengthtblList = CSvc.GetStudentStrengthTbl(model.CMid, model.SecMid, _OrgnisationID, _Financialyearid);
            model.ClassList = CSvc.GetClassList(_OrgnisationID);
            model.SectionList = CSvc.GetSectionList(_OrgnisationID, model.CMid);
            return View(model);
        }
        #endregion

        #region New Work For Fees Management
        public ActionResult Duefeelist()
        {
            BellSystemModel model = new BellSystemModel();
            return View(model);
        }
        public ActionResult Feesduelistsummary()
        {
            BellSystemModel model = new BellSystemModel();
            return View(model);
        }
        #endregion

        #region HR Management System
        public ActionResult Salaryheadlist()
        {
            SalaryheadModel model = new SalaryheadModel();
            model.SalaryheadTblList = CSvc.GetSalaryheadTbl(_OrgnisationID, _Financialyearid);
            return View(model);

        }
        public ActionResult Salaryhead(Nullable<int> Shmid, string ActionName = "")
        {
            SalaryheadModel _model = new SalaryheadModel();
            if (Shmid.HasValue)
            {
                Salaryheadmaster _master = CSvc.GetSalaryheaddetailbyShmid(Convert.ToInt32(Shmid.HasValue));
                _model.Shmid = _master.Shmid;
                _model.Headname = _master.Headname;
                _model.Alias = _master.Alias;
                _model.Displayname = _master.Displayname;
                _model.Headtype = _master.Headtype;
                _model.Defaultvalue = _master.Defaultvalue;
                _model.Leavededucation = _master.Leavededucation;
                _model.Esiapplicable = _master.Esiapplicable;
                _model.Epfapplicable = _master.Epfapplicable;
                _model.Calculationtype = _master.Calculationtype;
                _model.Displaysequence = _master.Displaysequence;
            }
            _model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(_model);
        }
        [HttpPost]
        public ActionResult Salaryhead(SalaryheadModel model,FormCollection collection)
        {
            Salaryheadmaster _master = new Salaryheadmaster();
            _master.Shmid = model.Shmid;
            _master.Headname = model.Headname;
            _master.Alias = model.Alias;
            _master.Displayname = model.Displayname;
            _master.Headtype = model.Headtype;
            _master.Defaultvalue = model.Defaultvalue;
            _master.Leavededucation = model.Leavededucation;
            _master.Esiapplicable = model.Esiapplicable;
            _master.Epfapplicable = model.Epfapplicable;
            _master.Calculationtype = model.Calculationtype;
            _master.Displaysequence = model.Displaysequence;
            _master.FinancialYear =_Financialyearid;
            _master.SchoolId = _OrgnisationID;
            if (collection["btntype"].ToUpper() == "Save".ToUpper())
            {
                _master.Createdby = WebSecurity.CurrentUserName;
                _master.Modifiedby = WebSecurity.CurrentUserName;
                _master.Createdate = DateTime.Now;
                _master.Modifieddate = DateTime.Now;
       
                int _returntype = CSvc.AddSalaryheaddetail(_master, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Salaryhead_Add_SUCCESS;
                else if (_returntype==-10)
                    TempData[Constants.MessageInfo.WARNING] = "Salary Head Already Exist !";
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Salaryhead_ADD_FAIL;
            }
            else
            {
                _master.Modifiedby = WebSecurity.CurrentUserName;
                _master.Modifieddate = DateTime.Now;
                int _returntype = CSvc.AddSalaryheaddetail(_master, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Salaryhead_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.Salaryhead_ADD_FAIL;
            }
            return RedirectToAction("Salaryheadlist");

        }

        public ActionResult DeleteSalaryhead(int Shmid)
        {
            Salaryheadmaster _master = CSvc.GetSalaryheaddetailbyShmid(Shmid);
            int _returntype = CSvc.AddSalaryheaddetail(_master, "DEL");
            if (_returntype > 0)
                TempData[Constants.MessageInfo.SUCCESS] = Constants.Salaryhead_DELETE_SUCCESS;
            else
                TempData[Constants.MessageInfo.WARNING] = Constants.Salaryhead_DELETE_FAIL;
            return RedirectToAction("Salaryheadlist");

        }
     
        public ActionResult Salaryheadassignlist()
        {
            AssignheadtoemployeeModel model = new AssignheadtoemployeeModel();

            model.AssignheadtoemployeetblList = CSvc.GetSalaryheadassignTbl(_OrgnisationID,_Financialyearid);
            return View(model);
        }
        public ActionResult Salaryheadassign(Nullable<int> Ashmid, string ActionName="")
        {
            AssignheadtoemployeeModel _model = new AssignheadtoemployeeModel();
            _model.Salaryheadlist = CSvc.GetSalaryheadlist(_OrgnisationID, _Financialyearid,0);
            _model.EmployeeList = CSvc.GetEmployeeList(1, _OrgnisationID);
            _model.Shiftlist = CSvc.GetShiftmasterList(_OrgnisationID);
            _model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(_model);

        }
        [HttpPost]
        public ActionResult Salaryheadassign(AssignheadtoemployeeModel model,FormCollection collection)
        {
            Assignheadtoemployee _assinsal = new Assignheadtoemployee();
            int loopcount;
            _assinsal.EMP_ID = model.EMP_ID;
            _assinsal.IsPF = model.IsPF;
            _assinsal.PFEmployeeamt = model.PFEmployeeamt;
            _assinsal.PFEmployeramt = model.PFEmployeramt;
            _assinsal.IsESI = model.IsESI;
            _assinsal.IsPFType = model.IsPFType;
            _assinsal.IsESIType = model.IsESIType;
            _assinsal.ESIEmployeeamt = model.ESIEmployeeamt;
            _assinsal.ESIEmployeramt = model.ESIEmployeramt;
            _assinsal.IsTDS = model.IsTDS;
            _assinsal.Taxcategory = model.Taxcategory;
            _assinsal.Shfittype = model.Shfittype;
            _assinsal.Createdate = DateTime.Now;
            _assinsal.Modifieddate = DateTime.Now;
            _assinsal.Createdby = WebSecurity.CurrentUserName;
            _assinsal.Modifiedby = WebSecurity.CurrentUserName;
            _assinsal.SchoolId = _OrgnisationID;
            _assinsal.FinancialYear = _Financialyearid;
            if (model.EMP_ID > 0)
            {
                int.TryParse(collection["loopcount"], out loopcount);
                DataTable _dt = new DataTable();
                _dt.Columns.Add("Id", typeof(int));
                _dt.Columns.Add("Shmid", typeof(int));
                _dt.Columns.Add("Defaultvalue", typeof(string));
                _dt.Columns.Add("Applyfrom", typeof(string));
                for (int i = 1; i <= loopcount; i++)
                {
                    DataRow dr = _dt.NewRow();
                    dr[0] = i;
                    dr[1] = Convert.ToInt32(collection["salheadid_" + i.ToString()]);
                    dr[2] = Convert.ToString(collection["salaryhead_" + i.ToString()]);
                    dr[3] = Convert.ToString(collection["Applyfrom_" + i.ToString()]);
                    _dt.Rows.Add(dr);

                }
                if (_dt.Rows.Count > 0)
                {
                    int returntye = CSvc.Assignsalaryheadtoemployee(_dt, _assinsal, "INS");
                    if (returntye > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Salaryheadassign_Add_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.WARNING] = Constants.Salaryheadassign_ADD_FAIL;
                    return RedirectToAction("Salaryheadassignlist");
                }
            }
            else
            {
                TempData[Constants.MessageInfo.WARNING] = "First Select Employee ";
            }
            model.Salaryheadlist = CSvc.GetSalaryheadlist(_OrgnisationID, _Financialyearid, 0);
            model.EmployeeList = CSvc.GetEmployeeList(1, _OrgnisationID);
            model.Shiftlist = CSvc.GetShiftmasterList(_OrgnisationID);
            return View(model);
        }

        public ActionResult TDSSetting()
        {
            TDSSettingModel model = new TDSSettingModel();
            model.TDSSettinglistTbl = CSvc.GetTDSSettingListTbl(_OrgnisationID, _Financialyearid);
            return View(model);
        }
        public ActionResult CreateTDSSetting(Nullable<int> Tdsmid, string ActionName = "")
        {
            TDSSettingModel model = new TDSSettingModel();
            TDSSettingmaster existingtds = new TDSSettingmaster();
            if (Tdsmid.HasValue)
            {
                existingtds = CSvc.GetTDSSettingByTdsmid(Tdsmid.Value);
                model.Tdsmid = existingtds.Tdsmid;
                model.Taxcategory = existingtds.Taxcategory;
                model.TYear = existingtds.TYear;
                model.ApplicableFrom = existingtds.ApplicableFrom;
                model.TaxRate = existingtds.TaxRate;
                model.Tminamount = existingtds.Tminamount;
                model.Tmaxamount = existingtds.Tmaxamount;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateTDSSetting(TDSSettingModel model,FormCollection collection)
        {
            TDSSettingmaster master = new TDSSettingmaster();
            master.Tdsmid = model.Tdsmid;
            master.Taxcategory = model.Taxcategory;
            master.TYear = model.TYear;
            master.ApplicableFrom = model.ApplicableFrom;
            master.TaxRate = model.TaxRate;
            master.Tminamount = model.Tminamount;
            master.Tmaxamount = model.Tmaxamount;
            master.Schoolid = _OrgnisationID;
            master.FinancialYear = _Financialyearid;
            if (collection["btntype"] == "Create")
            {
                master.Createdate = DateTime.Now;
                master.Createdby = WebSecurity.CurrentUserName;
                master.Modifieddate = DateTime.Now;
                master.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddTDSSetting(master, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.TDSSetting_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.TDSSetting_AlreadyExist_SUCCESS;

            }
            else
            {
                master.Createdate = DateTime.Now;
                master.Createdby = WebSecurity.CurrentUserName;
                master.Modifieddate = DateTime.Now;
                master.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddTDSSetting(master, "UPD");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.TDSSetting_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.TDSSetting_ADD_FAIL;
            }
            return RedirectToAction("TDSSetting");
        }

        public ActionResult DeleteTDSSetting(TDSSettingModel model)
        {

            if (model != null && model.Tdsmid != default(int))
            {
                TDSSettingmaster existingtds = CSvc.GetTDSSettingByTdsmid(model.Tdsmid);
                if (existingtds != null)
                {

                    existingtds.Modifieddate = DateTime.Now;
                    existingtds.Modifiedby = WebSecurity.CurrentUserName;
                    int _returntype = CSvc.AddTDSSetting(existingtds, "DEL");
                    if (_returntype > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.TDSSetting_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.TDSSetting_DELETE_FAIL;
                    return RedirectToAction("TDSSetting");
                }
            }
            return View();
        }
        public PartialViewResult Employeesalaryhead(string employeeid)
        {
            AssignheadtoemployeeModel _model = new AssignheadtoemployeeModel();
            try
            {
                _model.OragnisationMaster = CSvc.GetOragnisationDetails(_OrgnisationID);
                _model.Salaryheadlist = CSvc.GetSalaryheadlist(_OrgnisationID, _Financialyearid,Convert.ToInt32(employeeid));
            }
            catch (Exception ex)
            {

            }
            return PartialView("Employeesalaryhead", _model);
        }

        public ActionResult ShiftList()
        {
            ShiftModel model = new ShiftModel();
            model.ShiftmasterTblList = CSvc.GetShiftmasterTblList(_OrgnisationID);
            return View(model);
        }
        public ActionResult CreateShift(Nullable<int> Shiftmid, string ActionName = "")
        {
            ShiftModel model = new ShiftModel();
            ShiftMaster existshift = new ShiftMaster();
            if (Shiftmid.HasValue)
            {
                existshift = CSvc.GetShiftByShiftmid(Shiftmid.Value);
                model.Shiftmid = existshift.Shiftmid;
                model.ShiftName = existshift.ShiftName;
                model.ShiftStart = existshift.ShiftStart.ToString();
                model.ShiftEnd = existshift.ShiftEnd.ToString(); ;
                model.Shiftdesc = existshift.Shiftdesc;
            }
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateShift(ShiftModel model, FormCollection collection)
        {
            ShiftMaster existshift = CSvc.GetShiftByShiftmid(model.Shiftmid);
            existshift.ShiftName = model.ShiftName.Trim();
            existshift.Shiftdesc = model.Shiftdesc.Trim();
            existshift.ShiftStart = model.ShiftStart;
            existshift.ShiftEnd = model.ShiftEnd;
            existshift.schoolid = _OrgnisationID;
            existshift.IsActive = true;
            if (collection["btntype"] == "Create")
            {
                existshift.Createdate = DateTime.Now;
                existshift.Createdby = WebSecurity.CurrentUserName;
                existshift.Modifieddate = DateTime.Now;
                existshift.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddShift(existshift, "INS");
                if (_returntype > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Shift_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Shift_AlreadyExist_SUCCESS;
                return RedirectToAction("ShiftList");
            }
            else
            {
                existshift.Modifieddate = DateTime.Now;
                existshift.Modifiedby = WebSecurity.CurrentUserName;
                int _returntype = CSvc.AddShift(existshift, "UPD");
                TempData[Constants.MessageInfo.SUCCESS] = Constants.Shift_UPDATE_SUCCESS;

                return RedirectToAction("ShiftList");
            }

        }

        public JsonResult CheckIsShiftExist(string shiftname, string shiftsttime, string shiftendtime)
        {
            ShiftModel model = new ShiftModel();
            model.ShiftmasterList = CSvc.GetShiftmasterList(_OrgnisationID);
            var _result = (from myrow in model.ShiftmasterList.ToList()
                               // where (myrow.PeriodName.ToLower() == _periodname.ToLower() && myrow.PeriodStart == Convert.ToString(_psttime) && myrow.PeriodEnd == Convert.ToString(_pendtime))
                           where (myrow.ShiftStart == Convert.ToString(shiftsttime) && myrow.ShiftEnd == Convert.ToString(shiftendtime))
                           select myrow);

            var _timeresult = (from myrow in model.ShiftmasterList.ToList()
                               where (myrow.ShiftStart == Convert.ToString(shiftsttime) && myrow.ShiftEnd == Convert.ToString(shiftendtime))
                               select myrow);

            bool isnamePeriodallowed = _result.ToList().Count() > 0 ? true : false;
            bool istimePeriodallowed = _result.ToList().Count() > 0 ? true : false;
            Hashtable _arrlst = new Hashtable();
            _arrlst.Add("shiftname", isnamePeriodallowed);
            _arrlst.Add("shifttime", istimePeriodallowed);
            return Json(_arrlst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteShift(ShiftModel model)
        {

            if (model != null && model.Shiftmid != default(int))
            {
                ShiftMaster shiftmst = CSvc.GetShiftByShiftmid(model.Shiftmid);
                if (shiftmst != null)
                {
                    shiftmst.IsActive = false;
                    shiftmst.Modifieddate = DateTime.Now;
                    shiftmst.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddShift(shiftmst, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Shift_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Shift_DELETE_FAIL;

                    return RedirectToAction("ShiftList");
                }
            }
            return View();
        }
        #endregion

        #region Online Exmination System
        public ActionResult QuizeexammasterList()
        {
            QuizeexammasterModel model = new QuizeexammasterModel();
            model.QuizeexamlistTbl = CSvc.GetQuizeexamListTBL(_OrgnisationID, _Financialyearid);
            return View(model);
        }
        public ActionResult Quizeexammaster(Nullable<int> Qzmid, string ActionName = "")
        {
            QuizeexammasterModel model = new QuizeexammasterModel();
            Quizeexammaster quizmast = new Quizeexammaster();
            if (Qzmid.HasValue)
            {
                quizmast = CSvc.GetQuizeexamByid(Qzmid.Value);
                model.Qzmid = quizmast.Qzmid;
                model.Examtitle = quizmast.Examtitle;
                model.Classid = quizmast.Classid;
                model.Subjectid = quizmast.Subjectid;
                model.Rightque = quizmast.Rightque;
                model.Wrongque = quizmast.Wrongque;
                model.ExamTime = quizmast.ExamTime;
                model.Description = quizmast.Description;
                model.Totalquestion = quizmast.Totalquestion;
            }
            model.ClassMasterList = CSvc.GetClassList(_OrgnisationID);
            model.SubjectList = CSvc.GetSubjectList(_OrgnisationID);
            model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Quizeexammaster(QuizeexammasterModel model, FormCollection collection)
        {
            Quizeexammaster quizmast = CSvc.GetQuizeexamByid(model.Qzmid);
            quizmast.Qzmid = model.Qzmid;
            quizmast.Examtitle = model.Examtitle;
            quizmast.Classid = model.Classid;
            quizmast.Subjectid = model.Subjectid;
            quizmast.Rightque = model.Rightque;
            quizmast.Wrongque = model.Wrongque;
            quizmast.ExamTime = model.ExamTime;
            quizmast.Examdate = model.Examdate;
            quizmast.Description = model.Description;
            quizmast.Totalquestion = model.Totalquestion;
            quizmast.SchoolId = _OrgnisationID;
            quizmast.FinancialYear = _Financialyearid;
            quizmast.Createdate = DateTime.Now;
            quizmast.Createdby = WebSecurity.CurrentUserName;
            quizmast.Modifieddate = DateTime.Now;
            quizmast.Modifiedby = WebSecurity.CurrentUserName;
            if (model.Qzmid == 0)
            {
                int _returntype = CSvc.AddQuizeexam(quizmast, "INS");
                if (_returntype == 1)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Quiz_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Quiz_ADD_FAIL;
            }
            else
            {
                int _returntype = CSvc.AddQuizeexam(quizmast, "UPD");
                if (_returntype == 2)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Quiz_UPDATE_SUCCESS;
                else
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.Quiz_ADD_FAIL;
            }
            return RedirectToAction("QuizeexammasterList");
        }
        [HttpPost]
        public ActionResult DeleteQuizeexammaster(QuizeexammasterModel model)
        {
            if (model != null && model.Qzmid != default(int))
            {
                Quizeexammaster quizmast = CSvc.GetQuizeexamByid(model.Qzmid);
                if (quizmast != null)
                {
                    quizmast.Modifieddate = DateTime.Now;
                    quizmast.Modifiedby = WebSecurity.CurrentUserName;
                    if (CSvc.AddQuizeexam(quizmast, "DEL") > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Quiz_DELETE_SUCCESS;
                    else
                        TempData[Constants.MessageInfo.SUCCESS] = Constants.Quiz_ADD_FAIL;

                    return RedirectToAction("QuizeexammasterList");
                }
            }
            return View();
        }

        public ActionResult Createquestionwithoption(Nullable<int> Qzmid, string ActionName = "")
        {

            if (Qzmid.HasValue)
            {
                QuestionmasterModel model = new QuestionmasterModel();
                Quizeexammaster _qmaster = CSvc.GetQuizeexamByid(Convert.ToInt32(Qzmid));
                model.Qzmid = _qmaster.Qzmid;
                model.Examtitle = _qmaster.Examtitle;
                model.Questionlist = CSvc.Genratenoofquestion(_qmaster.Totalquestion);
                model.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
                return View(model);
            }
            return RedirectToAction("QuizeexammasterList");
        }

        [HttpPost]
        public ActionResult Createquestionwithoption(QuestionmasterModel model, FormCollection collection)
        {
            Quizeexammaster master = CSvc.GetQuizeexamByid(model.Qzmid);
            master.SchoolId = _OrgnisationID;
            master.FinancialYear = _Financialyearid;
            master.Createdate = DateTime.Now;
            master.Createdby = WebSecurity.CurrentUserName;
            master.Modifieddate = DateTime.Now;
            master.Modifiedby = WebSecurity.CurrentUserName;
            int loopcount;
            int.TryParse(master.Totalquestion.ToString(), out loopcount);
            DataTable _dtqueans = new DataTable();
            _dtqueans.Columns.Add("Qmid", typeof(int));
            _dtqueans.Columns.Add("Question", typeof(string));
            _dtqueans.Columns.Add("Option1", typeof(string));
            _dtqueans.Columns.Add("Option2", typeof(string));
            _dtqueans.Columns.Add("Option3", typeof(string));
            _dtqueans.Columns.Add("Option4", typeof(string));
            _dtqueans.Columns.Add("Anser", typeof(string));
            for (int i = 1; i <= loopcount; i++)
            {
                DataRow _dr = _dtqueans.NewRow();
                _dr[0] = i;
                _dr[1] = Convert.ToString(collection["quesname_" + i.ToString()]);
                _dr[2] = Convert.ToString(collection["option_1" + i.ToString()]);
                _dr[3] = Convert.ToString(collection["option_2" + i.ToString()]);
                _dr[4] = Convert.ToString(collection["option_3" + i.ToString()]);
                _dr[5] = Convert.ToString(collection["option_4" + i.ToString()]);
                _dr[6] = Convert.ToString(collection["answerid_" + i.ToString()]);
                _dtqueans.Rows.Add(_dr);
            }
            if (_dtqueans.Rows.Count > 0)
            {
                int returntye = CSvc.Addquestionans(_dtqueans, master, "INS");
                if (returntye > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.QuestionAddwoption_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.QuestionAddwoption_ADD_FAIL;
            }
            return RedirectToAction("QuizeexammasterList");
        }

        public ActionResult Startexamination(Nullable<int> Qzmid, string ActionName = "")
        {
            StartexaminationModel model = new StartexaminationModel();
            if (Qzmid.HasValue)
            {
                Quizeexammaster _qmaster = CSvc.GetQuizeexamByid(Convert.ToInt32(Qzmid));
                model.Qzmid = _qmaster.Qzmid;
                model.Examtitle = _qmaster.Examtitle;
                model.Questionlist = CSvc.startexam(Convert.ToInt32(Qzmid));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Startexamination(StartexaminationModel model, FormCollection collection)
        {
            Quizeexammaster master = CSvc.GetQuizeexamByid(model.Qzmid);
            master.SchoolId = _OrgnisationID;
            master.FinancialYear = _Financialyearid;
            master.Createdate = DateTime.Now;
            master.Createdby = WebSecurity.CurrentUserName;
            master.Modifieddate = DateTime.Now;
            master.Modifiedby = WebSecurity.CurrentUserName;
            int loopcount;
            int.TryParse(master.Totalquestion.ToString(), out loopcount);
            DataTable _dtstartexam = new DataTable();
            _dtstartexam.Columns.Add("Qmid", typeof(int));
            _dtstartexam.Columns.Add("Question", typeof(string));
            _dtstartexam.Columns.Add("SelectedAns", typeof(string));
            _dtstartexam.Columns.Add("Userid", typeof(string));
            for (int i = 1; i <= loopcount; i++)
            {
                DataRow _dr = _dtstartexam.NewRow();
                _dr[0] = i;
                _dr[1] = Convert.ToString(collection["questionid_" + i.ToString()]);
                _dr[2] = Convert.ToString(collection["option_" + i.ToString()]);
                _dr[3] = WebSecurity.CurrentUserId;
                _dtstartexam.Rows.Add(_dr);
            }
            if (_dtstartexam.Rows.Count > 0)
            {
                int returntye = CSvc.Quizrecording(_dtstartexam, master);
                if (returntye > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = Constants.QuestionAddwoption_Add_SUCCESS;
                else
                    TempData[Constants.MessageInfo.WARNING] = Constants.QuestionAddwoption_ADD_FAIL;
            }
            return RedirectToAction("QuizeexammasterList");

        }

        public JsonResult Getassignonlinestudent(string _selectedstudent, string quazid)
        {
            try
            {
                Onlineexamstudent _osexam = new Onlineexamstudent();
                _osexam.quazid = Convert.ToInt32(quazid);
                _osexam.SchoolId = _OrgnisationID;
                _osexam.FinancialYear = _Financialyearid;
                _osexam.Createdate = DateTime.Now;
                _osexam.Createdby = WebSecurity.CurrentUserName;
                _osexam.Modifieddate = DateTime.Now;
                _osexam.Modifiedby = WebSecurity.CurrentUserName;
                int returntye = CSvc.Assignstudent4online(_osexam, _selectedstudent);
                return Json(returntye, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion Online Exmination System

    }
}