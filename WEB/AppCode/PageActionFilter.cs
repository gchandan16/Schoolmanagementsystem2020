using SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;
using ERROR;
using System.ServiceModel;
using WebMatrix.WebData;
using System.Text;
using BAL;

namespace WEB.AppCode
{
    public class PageActionFilter : ActionFilterAttribute
    {
        private readonly UrlHelper newurl;
        StringBuilder str = new StringBuilder();
        List<MenuMaster> menulist;
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            // HttpSessionStateBase session = filterContext.HttpContext.Session;
            UserMasters user = CSvc.GetByUserId(WebSecurity.CurrentUserId);   // _userRepository.GetByUserId(user.USER_ID);
           // UserMasters user = (UserMasters)session[Constants.Session.USER];
            string text = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string viewname = filterContext.ActionDescriptor.ActionName;
            string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            string mailurl = HttpContext.Current.Request.Url.ToString();
            bool urlreffrer = HttpContext.Current.Request.Url.IsAbsoluteUri;
            Uri urlreffref = HttpContext.Current.Request.UrlReferrer;
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
            string url = string.Empty;
            url = urlHelper.Content(mailurl);
            HttpBrowserCapabilities httpBrowser = HttpContext.Current.Request.Browser;
            // if (!text.Contains("Account") && ((user == null && !session.IsNewSession) || session.IsNewSession))
            if (!text.Contains("Account") && user==null)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.Redirect("~/Account/SessionLogin", true);

                }
                else
                    filterContext.HttpContext.Response.Redirect(url, true);

            }
            Uri urlref = HttpContext.Current.Request.UrlReferrer;
            if (urlref==null)
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login");
            }
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            str = new StringBuilder();
            var model = (BaseModel)filterContext.Controller.ViewData.Model;
            if (model != null)
            {
                BALCommon CSvc = new BALCommon(ConStr);
                #region Get All Permitted Menu List
                SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
                int _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
                int _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);


                var ALLMENUPERMISSION = CSvc.GetAllMenuListByUserId(WebSecurity.CurrentUserId, _OrgnisationID);
                var USERINFO = CSvc.GetUserInfoByuserId(WebSecurity.CurrentUserId, _OrgnisationID);
                #endregion Get All Permitted Menu List
                menulist = ALLMENUPERMISSION;
                string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
                MenuMaster reqMenu = (from s in menulist
                                      where s.URL.ToLower() == absolutePath.ToLower()
                                      select s).FirstOrDefault();
                model.PermissionNameList = new List<string>();
                model.PermissionNameList.Add("Default");// its only for null checking
                if (reqMenu != null && reqMenu.PermissionNameList != null)
                {
                    model.PermissionNameList = reqMenu.PermissionNameList;
                }
                model.BaseSessionInfo = _SessionInfo;
                model.BaseUserInfo = USERINFO;  
                model.usermenu = GetUserMenuList(menulist);
            }
        }
        public string GetUserMenuList(List<MenuMaster> menulist)
        {

            foreach (var item in menulist.Where(x => x.PARENTMENUID == default(int)).ToList())
            {
                GetChildMenu(item);

            }
            return str.ToString();
        }
        public string GetChildMenu(MenuMaster menumaster)
        {
            if (IsChildExist(menumaster.MENU_ID))
            {

                var childelementlist = menulist.Where(x => x.PARENTMENUID == menumaster.MENU_ID);
                //<a href="#"><i class="fa fa-users"></i> <span> Student Management</span> <i class="fa fa-angle-left pull-right"> </i> <span class="label label-primary pull-right">14</span></a>
                str.Append("<li class='treeview'>");
                str.Append("<a href='#" + menumaster.URL + "' ><i class='"+ menumaster.icon + "'></i> <span>"+ FirstCharToUpper(menumaster.MENUNAME) +"</span> <i class='fa fa-angle-left pull-right'> </i> <span class='label label-primary pull-right'>"+ childelementlist .Count().ToString()+ "</span></a>");
              //  str.Append("<li class='treeview'><a href='#" + menumaster.URL + "' title='" + menumaster.MENUNAME + "' data-toggle='collapse'><em class='icon-layers'></em><span>" + menumaster.MENUNAME + "</span><span class='caret'></span></a>");

                

                str.Append("<ul class='treeview-menu' id='" + menumaster.URL + "'>");
                foreach (var childelement in childelementlist)
                {
                    GetChildMenu(childelement);
                }
                str.Append("</ul>");
                str.Append("</li>");
            }
            else
            {
                str.Append("<li class=' '><a href=" + menumaster.URL + " title='" + menumaster.MENUNAME + "'><i class='" + menumaster.icon + "'></i> <span>" + menumaster.MENUNAME + "</span></a></li>");
            }
            return str.ToString();
        }
        public static string FirstCharToUpper(string value)
        {
            char[] array = value.ToLower().ToCharArray();
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
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

    }


    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            // log the error by using your own method
            string message = filterContext.Exception.ToString();
           // Log.Error(message);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }



    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PreventDuplicateRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Request["__RequestVerificationToken"] == null)
                return;

            var currentToken = HttpContext.Current.Request["__RequestVerificationToken"].ToString();

            if (HttpContext.Current.Session["LastProcessedToken"] == null)
            {
                HttpContext.Current.Session["LastProcessedToken"] = currentToken;
                return;
            }

            lock (HttpContext.Current.Session["LastProcessedToken"])
            {
                var lastToken = HttpContext.Current.Session["LastProcessedToken"].ToString();

                if (lastToken == currentToken)
                {
                    filterContext.HttpContext.Response.Redirect("~/Error/Error?Status=" + "DoubleClick", true);
                    throw new System.ArgumentException("Double click");
                }

                HttpContext.Current.Session["LastProcessedToken"] = currentToken;
            }
        }
    }
}