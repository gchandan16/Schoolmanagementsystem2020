using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
namespace DumpAPI.ActionFilters
{
    /// <summary>
    /// Custom Authentication Filter Extending basic Authentication
    /// </summary>
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public ApiAuthenticationFilter()
        {
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        /// 

        
        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            //var provider = actionContext.ControllerContext.Configuration
            //                   .DependencyResolver.GetService(typeof(IUserServices)) as IUserServices;
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            BALCommon CSvc = new BALCommon(ConStr);


            var userid = CSvc.UserAvailablity(username, password);
                if (userid > 0)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                        basicAuthenticationIdentity.UserId = userid;
                    return true;
                }
          
            return false;
        }
        
    }
}