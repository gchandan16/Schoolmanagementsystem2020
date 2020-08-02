using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WEB.AppCode;
using SHARED;
using WEB.Models;
using System.ServiceModel;
using ERROR;
using BAL;
using NumToWord;
using System.Web.Helpers;
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace WEB.Controllers
{
    [Authorize]
    [PageActionFilter]
    public class StudentController : Controller
    {
        readonly string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        BALCommon CSvc;
        BALStudent BALObj;
        int _Financialyearid = 0;
        int _OrgnisationID = 0;
        int _UserType = 0;
        int _UserTypeBaseID = 0;
        public StudentController()
        {
            CSvc = new BALCommon(ConStr);
            BALObj = new BALStudent(ConStr);
            SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
            _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
            _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);
            _UserType = Convert.ToInt32(_SessionInfo.UserType);
            _UserTypeBaseID = Convert.ToInt32(_SessionInfo.UserTypeBaseID);
        }
        public JsonResult GetClassList()
        {
            List<ClassMaster> obj = new List<ClassMaster>();
            try
            {
                obj = BALObj.GetClassList(_OrgnisationID);
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentIDCard()
        {
            BaseModel Obj = new BaseModel();
            return View(Obj);
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public PartialViewResult GenerateIDCard(string ClassID, string SectionID, string CardType)
        {
            List<StudentMaster> objStudent = new List<StudentMaster>();
            IDCard IDCardDetails = new IDCard();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                objStudent = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, Convert.ToInt32(ClassID), Convert.ToInt32(SectionID), 0, "");
                IDCardDetails.OragnisationDetails = CSvc.GetOragnisationDetails(_OrgnisationID);
                IDCardDetails.StudentDetailsList = objStudent;
                IDCardDetails.CardType = CardType;
            }
            catch { }


            return PartialView("IDCard", IDCardDetails);
        }

        public ActionResult StudentRollNo()
        {
            StudnetRollNo Obj = new StudnetRollNo();
            List<ClassMaster> _ClassList = new List<ClassMaster>();
            try
            {
                Obj.SectionList = new List<SectionMaster>();
                Obj.ClassList = BALObj.GetClassList(_OrgnisationID);
            }
            catch { }
            return View(Obj);
        }
        [HttpPost]
        public ActionResult StudentRollNo(StudnetRollNo Model)
        {
            int res = 0;
            try
            {
                foreach(var master in Model.StudentList)
                {
                    master.FinancialYear = _Financialyearid;
                    master.SchoolID = _OrgnisationID;
                    master.Modifiedby = WebSecurity.CurrentUserName;
                    master.Modifieddate = DateTime.Now;
                    master.RollNo = string.IsNullOrEmpty(master.RollNo) ? "0" : master.RollNo;
                    res += CSvc.UpdateStudentDetails(master);
                }
            }
            catch
            {

            }
            TempData[Constants.MessageInfo.SUCCESS] = res + " record(s) updated successfully.";
            return RedirectToAction("StudentRollNo");
        }
        public JsonResult GetSectionList(string ClassID)
        {
            List<SectionMaster> obj = new List<SectionMaster>();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                obj = CSvc.GetSectionList(_OrgnisationID, Convert.ToInt32(ClassID));// userMasters.SchoolID);
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult StudentPromote()
        //{
        //    StudentPromote Obj = new StudentPromote();
        //    List<ClassMaster> _ClassList = new List<ClassMaster>();
        //    try
        //    {
        //        Obj.FinancialYearID = _Financialyearid;
        //        Obj.SectionList = new List<SectionMaster>();
        //        Obj.ClassList = BALObj.GetClassList(_OrgnisationID);
        //    }
        //    catch { }
        //    return View(Obj);
        //}
        //[HttpPost]
        //public ActionResult StudentPromote(StudnetRollNo Model)
        //{
        //    int res = 0;
        //    try
        //    {
        //        foreach (var master in Model.StudentList)
        //        {
        //            master.FinancialYear = _Financialyearid;
        //            master.SchoolID = _OrgnisationID;
        //            master.Modifiedby = WebSecurity.CurrentUserName;
        //            master.Modifieddate = DateTime.Now;
        //            master.RollNo = string.IsNullOrEmpty(master.RollNo) ? "0" : master.RollNo;
        //            res += CSvc.UpdateStudentDetails(master);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    TempData[Constants.MessageInfo.SUCCESS] = res + " record(s) updated successfully.";
        //    return RedirectToAction("StudentPromote");
        //}
        #region Promote Student
        public JsonResult GetStudentListToPromote(string ClassID, string SectionID, string FinancialYearID)
        {
            List<StudentMaster> obj = new List<StudentMaster>();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                obj = CSvc.GetStudentList(_OrgnisationID, Convert.ToInt32(FinancialYearID), Convert.ToInt32(ClassID), Convert.ToInt32(SectionID), 0, "");
            }
            catch { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult PromoteStudent()
        {
            PromoteStudent Obj = new PromoteStudent();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                Obj.FinancialYearList = CSvc.GetFinancialYearList();
                Obj.ClassList = BALObj.GetClassList(_OrgnisationID);
            }
            catch (Exception ex)
            {

            }
            return View(Obj);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult PromoteStudent(string[] AdmissionNo, string PrevFYID, string NextFYID, string ClassID, string SectionID)
        {
            string result = "";
            DataTable DTAdmissionNo = new DataTable();
            BALStudent CSvc = new BALStudent(ConStr);
            try
            {
                if (AdmissionNo.Count() > 0)
                {
                    DTAdmissionNo.Columns.Add("Cnt");
                    DTAdmissionNo.Columns.Add("strValue");
                    for (int l = 0; l < AdmissionNo.Count(); l++)
                    {
                        DataRow dr = DTAdmissionNo.NewRow();
                        dr[0] = (l + 1);
                        dr[1] = AdmissionNo[l];
                        DTAdmissionNo.Rows.Add(dr);
                    }
                    result = CSvc.PromoteStudent(DTAdmissionNo, Convert.ToInt32(PrevFYID), Convert.ToInt32(NextFYID), Convert.ToInt32(ClassID), Convert.ToInt32(SectionID));
                }

            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion Promote Student
    }

}
