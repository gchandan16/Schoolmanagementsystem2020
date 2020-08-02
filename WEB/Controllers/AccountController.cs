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
using System.Web.Helpers;

namespace WEB.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //Commented By Manish
        //comment again
        //ChannelFactory<ICommonSrv> CF = new ChannelFactory<ICommonSrv>("COMMON");
        //ICommonSrv CSvc;
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public AccountController()
        {
            //CSvc  = (ICommonSrv)CF.CreateChannel();
        }
        //test done
        int a = 0;//for testing 
        StringBuilder str = new StringBuilder();
        List<MenuMaster> menulist;
        [AllowAnonymous]
        public ActionResult Create()
        {
            //Create org
            //BALCommon CSvc = new BALCommon(ConStr);
            SignupModel model = new SignupModel();
            //model.CountryList = CSvc.GetCountryList(0);
            //model.CityList = CSvc.GetCityList(0, 0);
            //model.StateList = CSvc.GetStateList(0, 0);
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(SignupModel model, FormCollection collection)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            OragnisationMaster master = new OragnisationMaster();
            //model.CountryList = CSvc.GetCountryList(0);
            //model.CityList = CSvc.GetCityList(0, 0);
            //model.StateList = CSvc.GetStateList(0, 0);
            if (ModelState.IsValid)
            {
                try
                {
                    master = CSvc.GetOragnisationAlready(model.LEmailId);
                    if (master.OMID == 0)
                    {
                        HttpPostedFileBase empimg = Request.Files["emppathimage"];
                        string folderpath = Constants.EMPATTACHMENT;
                        
                        if (empimg.ContentLength > 0)
                        {
                            string guidstring = Guid.NewGuid().ToString();
                            string _FileName = Path.GetFileName(empimg.FileName);
                            string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                            string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                            empimg.SaveAs(filepath);
                            master.OrgImage = dbpath;
                        }
                        else
                        {
                            string _FileName = "schooldummylogo.png";
                            string dbpath = Path.Combine(folderpath + _FileName);
                            master.OrgImage = dbpath;
                        }
                        master.Oname = model.Oname;
                        master.BOAddress = model.BOAddress;
                        master.BOAddress2 = model.BOAddress2;
                        master.BOCity = model.CITY_ID;
                        master.BOPincode = model.BOPincode;
                        master.LCountry = model.COUNTRY_ID;
                        master.LState = model.STATE_ID;
                        master.LDistict = model.LDistict;
                        master.LArea = model.LArea;
                        master.LEmailId = model.LEmailId;
                        master.LMobile = model.LMobile;
                        master.LPhone = model.LPhone;
                        master.LWebsite = model.LWebsite;
                        master.OAfficilate = model.OAfficilate;
                        master.OlicNo = model.OlicNo;
                        master.OTaxNo = model.OTaxNo;
                        master.OPanNo = model.OPanNo;
                        master.OContactNo = model.OContactNo;
                        master.IsActive = false;
                        master.Createddate = DateTime.Now;
                        master.Modifieddate = DateTime.Now;
                        master.CreatedBy = "EndUser";
                        master.ModifiedBy = "EndUser";
                        master.Otype = "INS"; // to check
                        int _retua = CSvc.OragnasitionBasicopation(master);
                        if (_retua > 0)
                        {
                            string Password = Utility.GenerateRandomPassword();
                            
                            WebSecurity.CreateUserAndAccount(master.LEmailId, Password, new { Name = master.OContactNo, Mobile = master.LMobile, EmailId = master.LEmailId, Address = master.BOAddress, RoleId = 1, CITY_ID = master.BOCity, STATE_ID = master.LState, COUNTRY_ID = master.LCountry, ISACTIVE = 0, SchoolID = _retua });
                            CSvc.Firstuserconfigure(_retua);//first user configure
                            //TempData[Constants.MessageInfo.SUCCESS] = Constants.Orgnisation_ADD_SUCCESS;
                            #region SendMail
                            MailDetails _MailDetails = new MailDetails();
                            _MailDetails.ToMailIDs = master.LEmailId;
                            _MailDetails.HTMLBody = true;
                            _MailDetails.Subject = "Organisation Registration";
                            _MailDetails.Body = BALMail.TemplateOrganisation(master, AESEncrytDecry.EncryptStringAES(Password));
                            if (BALMail.SendMail(_MailDetails))
                            {
                                TempData[Constants.MessageInfo.SUCCESS] = Constants.Orgnisation_ADD_SUCCESS + ", Please check your mail inbox for more information.";
                            }
                            else
                            {
                                TempData[Constants.MessageInfo.SUCCESS] = Constants.Orgnisation_ADD_SUCCESS;
                            }
                            #endregion SendMail
                            return RedirectToAction("login");

                        }
                    }
                    else
                    {
                        TempData[Constants.MessageInfo.SUCCESS] = "Orgnisation is already Exist !";
                        return RedirectToAction("Create");
                    }
                }
                catch (Exception ex)
                {
                    WebSecurity.Logout();
                    ExecptionLogger.FileHandling("Account(Create_Post)", "Error_014", ex, "Account");
                }

            }

            return View(model);
        }
        #region Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                if (WebSecurity.CurrentUserId != 0)
                {
                    WebSecurity.Logout();
                }
                List<FinancialYear> FYList = CSvc.GetFinancialYearList();
                TempData["FinancialYearList"] = FYList;
                TempData["CurrentFinancialYear"] = FYList.Where(x => x.SelectedYear).Select(x => x.FID).FirstOrDefault();
                ViewBag.ReturnUrl = returnUrl;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Account(UserLogin)", "Error_007", ex, "Account");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                //WebSecurity.CurrentUserName
                // WebSecurity.CurrentUserId
                //ChannelFactory<IMembershipSrv> CF = new ChannelFactory<IMembershipSrv>("MEMBERSHIP");
                //IMembershipSrv CSvc = (IMembershipSrv)CF.CreateChannel();
                //SHARED.UserProfile userProfile = new SHARED.UserProfile();
                //userProfile = CSvc.GetUserProfile(model.UserName);
                //CF.Close();
                if (ModelState.IsValid && WebSecurity.Login(model.UserName, AESEncrytDecry.DecryptStringAES(model.Password), persistCookie: model.RememberMe))
                {
                    //ChannelFactory<ICommonSrv> CF = new ChannelFactory<ICommonSrv>("COMMON");
                    //ICommonSrv CSvc = (ICommonSrv)CF.CreateChannel();
                    BALCommon CSvc = new BALCommon(ConStr);
                    try
                    {
                        UserMasters userMasters = CSvc.getUserProfile(model.UserName);
                        //CF.Close();
                        if (!userMasters.ISACTIVE)
                        {
                            TempData["Msg"] = "Please Contact To Technical Team.";
                            WebSecurity.Logout();
                        }
                        else
                        {
                            if (model.FID > 0 && CSvc.InsertUpdateSession(model.UserName, model.FID) == 1)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                WebSecurity.Logout();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        WebSecurity.Logout();
                        ExecptionLogger.FileHandling("Account(UserLogin_getUserProfilePost)", "Error_014", ex, "Account");
                    }

                }
                else
                {
                    TempData["Msg"] = "UserName and/or Password is incorrect.";
                }
            }
            catch (Exception ex)
            {
                WebSecurity.Logout();
                ExecptionLogger.FileHandling("Account(UserLoginPost)", "Error_014", ex, "Account");
            }
            //return View(model);
            return RedirectToAction("Login");
        }
        #region logoff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                if (WebSecurity.CurrentUserId != 0)
                    WebSecurity.Logout();
            }
            catch (Exception ex)
            { ExecptionLogger.FileHandling("Account(LogOff)", "Error_014", ex, "Account"); }
            return RedirectToAction("Login", "Account");
        }
        #endregion logoff
        #region ResetPassword
        [HttpGet]
        [PageActionFilter]
        public ActionResult ResetPassword()
        {
            BALCommon CSvc = new BALCommon(ConStr);
            UserMasters _umaster = CSvc.GetByUserId(WebSecurity.CurrentUserId);
            ResetForgotPassword obj = new ResetForgotPassword
            {
                UserId = WebSecurity.CurrentUserId,
                UserName = WebSecurity.CurrentUserName,
                Name= _umaster.FISRTNAME,
                Lastname = _umaster.LASTNAME,
                IMAGE = _umaster.IMAGE,
                Mobile = _umaster.Mobile,
                EmailId = _umaster.EMAILID

            };
            return View(obj);
        }
        [HttpPost]
        [PageActionFilter]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetForgotPassword model, FormCollection collection)
        {
            string sResetToken = WebSecurity.GeneratePasswordResetToken(model.UserName, 30000);
            HttpPostedFileBase _profilepic = Request.Files["employeephotoimge"];
            string folderpath = Constants.USERPICK;
            BALCommon CSvc = new BALCommon(ConStr);
            UserMasters _um = CSvc.GetByUserId(WebSecurity.CurrentUserId);
            if (_profilepic.ContentLength > 0)
            {
                string guidstring = Guid.NewGuid().ToString();
                string _FileName = Path.GetFileName(_profilepic.FileName);
                string filepath = Path.Combine(Server.MapPath(folderpath) + guidstring + "_" + _FileName);
                string dbpath = Path.Combine(folderpath + guidstring + "_" + _FileName);
                _profilepic.SaveAs(filepath);
                model.IMAGE = dbpath;
            }
            
            _um.UserId = model.UserId;
            _um.USERNAME = model.UserName;
            _um.FISRTNAME = model.Name;
            _um.LASTNAME = model.Lastname;
            _um.Mobile = model.Mobile;
            _um.EMAILID = model.EmailId;
            _um.MODIFIEDBY = model.UserName;
            _um.MODIFIEDDATE = DateTime.Now;


            if (model.UserId == WebSecurity.CurrentUserId && WebSecurity.ResetPassword(sResetToken, model.NewPassword))
            {
                CSvc.AddUserprofile(_um,"UPD");
                TempData[Constants.MessageInfo.SUCCESS] = "Password reset successfully.";
            }
            else
                TempData[Constants.MessageInfo.ERROR] = "Password could not reset.";

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ResetForgotPassword obj = new ResetForgotPassword();
            return View(obj);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ResetForgotPassword model)
        {
            BALCommon obj = new BALCommon(ConStr);
            try
            {
                
                string OTP = obj.getUserProfile(model.UserName).OTP;
                if (!string.IsNullOrEmpty(OTP) && model.OTP == OTP)
                {
                    string sResetToken = WebSecurity.GeneratePasswordResetToken(model.UserName, 30000);
                    if (WebSecurity.ResetPassword(sResetToken, model.NewPassword))
                        TempData[Constants.MessageInfo.SUCCESS] = "Password has been updated successfully.";
                    else
                        TempData[Constants.MessageInfo.ERROR] = "Password could not updated.";
                }
                else
                {
                    TempData[Constants.MessageInfo.ERROR] = "Incorrect OTP.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        [ValidateJsonAntiForgeryToken]
        public JsonResult GetOTP(string UID)
        {
            BALCommon obj = new BALCommon(ConStr);
            bool res = false ;
            string message = "";
            try
            {
                string OTP = Utility.GetRendomString(6, "N");
                res = obj.SetOTP(UID, OTP);
                #region SendMail
                if (res)
                {
                    BALCommon CSvc = new BALCommon(ConStr);
                    UserMasters userMasters = CSvc.getUserProfile(UID);
                    MailDetails _MailDetails = new MailDetails();
                    _MailDetails.ToMailIDs = userMasters.EMAILID;
                    _MailDetails.HTMLBody = true;
                    _MailDetails.Subject = "Reset OTP";
                    _MailDetails.Body = BALMail.TemplateResetOTP(userMasters, AESEncrytDecry.EncryptStringAES(OTP));
                    if (BALMail.SendMail(_MailDetails))
                    {
                        message = "OTP has been sent to your registered mail id.";
                    }
                    else
                    {
                        message = "Please contact technical team";
                    }
                }
                else
                {
                    message = "Please contact technical team";
                }
                #endregion SendMail
            }
            catch (Exception ex)
            {
                res = false;
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion ResetPassword
        #endregion Login

        #region ForValidationPurpose
        [AllowAnonymous]
        public JsonResult EmailExists(string LEmailId)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            OragnisationMaster master = new OragnisationMaster();
            master = CSvc.GetOragnisationAlready(LEmailId);
            return Json(!String.Equals(LEmailId, master.LEmailId, StringComparison.OrdinalIgnoreCase));
        }
        #endregion
        [AllowAnonymous]
        public JsonResult GetGeoDetails(string PinCode)
        {
            GEO obj = new GEO();
            BALCommon CScmn = new BALCommon(ConStr);
            try
            {
                obj = CScmn.GetGeoDetails(PinCode);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Fee(GetGeoDetails)", "Error_014", ex, "Fee");
            }
            finally
            {
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            AntiForgery.Validate(cookie != null ? cookie.Value : null, httpContext.Request.Headers["__RequestVerificationToken"]);
        }
    }
    
}

