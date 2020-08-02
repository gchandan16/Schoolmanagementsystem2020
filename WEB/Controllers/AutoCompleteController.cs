using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WEB.Filters;
using WEB.Models;
using System.ServiceModel;
using System.Web.Script.Serialization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SHARED;
using BAL;
using System.IO;
using ERROR;
using WEB.AppCode;
namespace WEB.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AutoCompleteController : Controller
    {
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        BALCommon CSvc;
        int _Financialyearid = 0;
        int _OrgnisationID = 0;
        int _UserType = 0;
        int _UserTypeBaseID = 0;
        string _FinacialOrgid = "";
        public AutoCompleteController()
        {
            CSvc = new BALCommon(ConStr);
            SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
            _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
            _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);
            _UserType = Convert.ToInt32(_SessionInfo.UserType);
            _UserTypeBaseID = Convert.ToInt32(_SessionInfo.UserTypeBaseID);


        }

        public JsonResult GetStudentList(string src)
        {
            List<StudentMaster> _lst = new List<StudentMaster>();
            BALFee CSFee = new BALFee(ConStr);
            try
            {


                _lst = CSFee.SearchStudentList(src, _OrgnisationID, _Financialyearid);




                return Json(_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(_lst, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEmployeeList(string src)
        {
            List<EmployeeMaster> _lst = new List<EmployeeMaster>();
            BALCommon CSvc = new BALCommon(ConStr); BALFee CSFee = new BALFee(ConStr);
            try
            {
                _lst = CSFee.SearchEmployeeList(src, _OrgnisationID);
                return Json(_lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(_lst, JsonRequestBehavior.AllowGet);
            }
        }

    }

}

