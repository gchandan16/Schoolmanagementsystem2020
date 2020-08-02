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
using BAL;
using ERROR;
using System.Web.Helpers;

namespace WEB.Controllers
{
    [Authorize]
    [PageActionFilter]
    public class HomeController : Controller
    {
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        BALCommon CSvc;
        int _Financialyearid = 0;
        int _OrgnisationID = 0;
        int _UserType = 0;
        int _UserTypeBaseID = 0;
        string _FinacialOrgid = "";
        public HomeController()
        {
            CSvc = new BALCommon(ConStr);
            SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
            _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
            _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);
            _UserType = Convert.ToInt32(_SessionInfo.UserType);
            _UserTypeBaseID = Convert.ToInt32(_SessionInfo.UserTypeBaseID);

        }

        public ActionResult Index()
        {
            BaseModel model = new BaseModel();
            // UserMasters currentUser = new UserMasters();
            //currentUser.USER_ID = WebSecurity.CurrentUserId;
            //currentUser.USERNAME = WebSecurity.CurrentUserName;
            //var currentmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //model.FromDate = currentmonth;
            //model.ToDate = currentmonth.AddMonths(1).AddDays(-1);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Leave()
        {
            Leave model = new Leave();
            List<Leave> _Lst = new List<Leave>();
            CSvc = new BALCommon(ConStr);
            try
            {
                Leave obj = new Leave
                {
                    FinancialYear = _Financialyearid,
                    SchoolID = _OrgnisationID,
                    SenderID = _UserTypeBaseID,
                    ApproverID = _UserTypeBaseID,
                    Action = "GETSENDER"
                };
                _Lst = CSvc.GetLeaveReport(obj);

                obj.Action = "GETAPPROVER";
                var ApproverLst = CSvc.GetLeaveReport(obj);

                foreach (var AppLst in ApproverLst)
                    _Lst.Add(AppLst);
                model.LeaveList = _Lst;
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpGet]
        public ActionResult LeaveRequest()
        {
            Leave model = new Leave();
            CSvc = new BALCommon(ConStr);
            List<selectList> SL = new List<selectList>();
            try
            {
                EmployeeMaster EM = CSvc.GetSingleEmployee(_UserTypeBaseID);
                SL.Add(new selectList
                {
                    Text = "CL",
                    Value = "CL_" + EM.LEAVE_CL_ENTITLED
                });
                SL.Add(new selectList
                {
                    Text = "PL",
                    Value = "PL_" + EM.LEAVE_PL_ENTITLED
                });
                SL.Add(new selectList
                {
                    Text = "SL",
                    Value = "SL_" + EM.LEAVE_SL_ENTITLED
                });
                model.LeaveTypeList = SL;
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveRequest(Leave model)
        {
            CSvc = new BALCommon(ConStr);
            try
            {
                EmployeeMaster EMself = CSvc.GetSingleEmployee(_UserTypeBaseID);
                EmployeeMaster EMParent = CSvc.GetSingleEmployee(EMself.ParentID);
                model.SenderID = _UserTypeBaseID;
                model.SenderName = EMself.FIRSTNAME;
                model.SenderType = Convert.ToString(_UserType);
                model.ApproverID = EMself.ParentID;
                model.ApproverName = EMParent.FIRSTNAME;
                model.ApproverType = Convert.ToString(_UserType);
                model.LeaveStatus = "Pending";
                model.SchoolID = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                model.LeaveType = model.LeaveType.Split('_')[0];
                model.Action = "INS";
                bool res = CSvc.InsertUpdateLeaveRequest(model);
                if (res)
                    TempData[Constants.MessageInfo.SUCCESS] = "Request sent successfully.";
                else
                    TempData[Constants.MessageInfo.ERROR] = "Request could not sent successfully.";
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Leave");
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult UpdateLeaveRequest(Leave model)
        {
            CSvc = new BALCommon(ConStr);
            bool res = false;
            try
            {
                model.Action = "UPD";
                 res = CSvc.InsertUpdateLeaveRequest(model);
            }
            catch (Exception ex)
            {

            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DashboardDetails()
        {
            DashboardDetails obj = new DashboardDetails();
            CSvc = new BALCommon(ConStr);
            obj = CSvc.GetDashboardDetails(_OrgnisationID, _Financialyearid, DateTime.Now);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

    }
    
}
