using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SHARED;
using ERROR;
namespace BAL
{
    [Serializable()]
    public class BALMail
    {
        public static bool SendMail(MailDetails MailDetails)
        {
            bool flag = false;
            System.Net.Mail.SmtpClient objMail = new System.Net.Mail.SmtpClient();
            System.Net.Mail.MailMessage msgMail = new System.Net.Mail.MailMessage();
            msgMail.To.Clear();
            try
            {
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["MailUID"].ToString().Trim(), ConfigurationManager.AppSettings["MailUName"].ToString().Trim());
                string fromPassword = ConfigurationManager.AppSettings["MailPassword"].ToString().Trim();

                if (!string.IsNullOrEmpty(MailDetails.ToMailIDs))
                {
                    foreach (var MailID in MailDetails.ToMailIDs.Split(','))
                    {
                        if (!string.IsNullOrEmpty(MailID.Trim()))
                            msgMail.To.Add(new System.Net.Mail.MailAddress(MailID.Trim()));
                    }
                }

                var smtp = new SmtpClient
                {
                    Host = Convert.ToString(ConfigurationManager.AppSettings["Host"].ToString().Trim()),//"smtp.gmail.com",
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString().Trim()),
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"].ToString().Trim()),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress.Address, msgMail.To.ToString())
                {
                    Subject = MailDetails.Subject,
                    IsBodyHtml = MailDetails.HTMLBody,
                    Body = MailDetails.Body,

                })
                {
                    smtp.Send(message);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(sendMail)", "Error_014", ex, "BALMail");
                flag = false;
            }
            return flag;
        }
        public static string TemplateOrganisation(OragnisationMaster _OragnisationMaster, string Password)
        {
            string HTML = "";
            try
            {
                HTML += Header(_OragnisationMaster.OContactNo);
                HTML += "<br /><br />";
                HTML += OrganisationBody(_OragnisationMaster, Password);
                HTML += "<br /><br /><br /><br />";
                HTML += Footer("igenussolution Team");
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(TemplateOrganisation)", "Error_014", ex, "BALMail");
                HTML = "";
            }
            return HTML;
        }
        private static string OrganisationBody(OragnisationMaster _OragnisationMaster, string Password)
        {
            string Body = "";
            try
            {
                Body += "Thank you for registering. ";
                Body += "<br /><br />";
                Body += "Please find credentials to login.";
                Body += "<br />";
                Body += "<b>User Name: </b>" + _OragnisationMaster.LEmailId;
                Body += "<br />";
                Body += "<b>Password: </b>" + AESEncrytDecry.DecryptStringAES(Password);
                Body += "<br /><br />";
                Body += "<b>URL: </b>" + ConfigurationManager.AppSettings["WebSite"].ToString().Trim();
                Body += "<br /><br />";
                Body += "Our executive will contact you and after verification, we will activate your account.";
                Body += "<br />";
                Body += "Note:- Please change your password after successful login.";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(Header)", "Error_014", ex, "BALMail");
                Body = "";
            }
            return Body;
        }


        public static string TemplateResetOTP(UserMasters userMasters, string OTP)
        {
            string HTML = "";
            try
            {
                HTML += Header(userMasters.FISRTNAME + " " + userMasters.LASTNAME);
                HTML += "<br /><br />";
                HTML += ResetOTPBody(userMasters, OTP);
                HTML += "<br /><br /><br /><br />";
                HTML += Footer("igenussolution Team");
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(TemplateOrganisation)", "Error_014", ex, "BALMail");
                HTML = "";
            }
            return HTML;
        }
        private static string ResetOTPBody(UserMasters userMasters, string OTP)
        {
            string Body = "";
            try
            {
                Body += "<b>User Name: </b>" + userMasters.USERNAME;
                Body += "<br />";
                Body += "<b>OTP: </b>" + AESEncrytDecry.DecryptStringAES(OTP);
                Body += "<br /><br />";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(Header)", "Error_014", ex, "BALMail");
                Body = "";
            }
            return Body;
        }

        public static string TemplateUserRegistration(UserMasters model, string Password)
        {
            string HTML = "";
            try
            {
                HTML += Header(model.USERNAME);
                HTML += "<br /><br />";
                HTML += UserRegistrationBody(model, Password);
                HTML += "<br /><br /><br /><br />";
                HTML += Footer("igenussolution Team");
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(TemplateOrganisation)", "Error_014", ex, "BALMail");
                HTML = "";
            }
            return HTML;
        }
        private static string UserRegistrationBody(UserMasters model, string Password)
        {
            string Body = "";
            try
            {
                Body += "Thank you for registering. ";
                Body += "<br /><br />";
                Body += "Please find credentials to login.";
                Body += "<br />";
                Body += "<b>User Name: </b>" + model.USERNAME;
                Body += "<br />";
                Body += "<b>Password: </b>" + AESEncrytDecry.DecryptStringAES(Password);
                Body += "<br /><br />";
                Body += "<b>URL: </b>" + ConfigurationManager.AppSettings["WebSite"].ToString().Trim();
                Body += "<br /><br />";
                Body += "Note:- Please change your password after successful login.";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(Header)", "Error_014", ex, "BALMail");
                Body = "";
            }
            return Body;
        }






        private static string Header(string HeaderName)
        {
            string header = "";
            try
            {
                header += "Dear " + HeaderName + ",";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(Header)", "Error_014", ex, "BALMail");
                header = "";
            }
            return header;
        }
        private static string Footer(string FooterName)
        {
            string footer = "";
            try
            {
                footer += "<b>Thanks & Regards," + "<br /> <br />" + FooterName + "</b>";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("BALMail(Footer)", "Error_014", ex, "BALMail");
                footer = "";
            }
            return footer;
        }
    }
}
