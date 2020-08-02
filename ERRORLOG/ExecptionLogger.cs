using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Diagnostics;
namespace ERROR
{
    public class ExecptionLogger
    {
        //public static string get_Error_Det(string Err_code)
        //{
        //    string s = Error_Resource.ResourceManager.GetString(Err_code);
        //    return s;
        //}
        public static void FileHandling(string Err_Service, string Err_Code, Exception Err_Msg, string Err_Module)
        {
            try
            {
                string dir1 = "";
                string newPathResp1 = "";
                dir1 = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionLogger/") + DateTime.Now.ToString("dd-MM-yyyy");// @"C:\ImportPnrXml\" + DateTime.Now.ToString("dd-MM-yyyy") + "\\";//System.Configuration.ConfigurationManager.AppSettings["ImportXmlPath"].ToString().Trim(); //
                if (!Directory.Exists(dir1))
                {
                    Directory.CreateDirectory(dir1);
                }
                newPathResp1 = Path.Combine(dir1, Err_Service.ToString() + ".txt");
                FileStream fs1 = new FileStream(newPathResp1, FileMode.Append, FileAccess.Write);
                StreamWriter sw1 = new StreamWriter(fs1);
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(Err_Msg, true);
                string ErrorLineNo = (trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()).ToString();
                //string Err_Source = (trace.GetFrame((trace.FrameCount - 1)).GetFileName()).ToString();
                //sw1.Write(ex.Message + " " + ex + ex.StackTrace +"LineNo"+ ErrorLineNo + Err_Source);
                //sw1.Write(sw1.NewLine);
                //sw1.Write(Err_Msg.Message.ToString());
                //sw1.Write(sw1.NewLine);
                sw1.Write("Message:" + Err_Msg.Message.ToString());
                sw1.Write(sw1.NewLine);
                sw1.Write("Line NO:" + ErrorLineNo);
                sw1.Write(sw1.NewLine);
                sw1.Flush();
                sw1.Close();
                fs1.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    string dir1 = "";
                    string newPathResp1 = "";
                    dir1 = System.Web.HttpContext.Current.Server.MapPath("~/ExceptionLogger/") + DateTime.Now.ToString("dd-MM-yyyy");// @"C:\ImportPnrXml\" + DateTime.Now.ToString("dd-MM-yyyy") + "\\";//System.Configuration.ConfigurationManager.AppSettings["ImportXmlPath"].ToString().Trim(); //
                    if (!Directory.Exists(dir1))
                    {
                        Directory.CreateDirectory(dir1);
                    }
                    newPathResp1 = Path.Combine(dir1, Err_Service.ToString() + "2" + ".txt");
                    FileStream fs1 = new FileStream(newPathResp1, FileMode.Append, FileAccess.Write);
                    StreamWriter sw1 = new StreamWriter(fs1);
                    //System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(Err_Msg, true);
                    //string ErrorLineNo = (trace.GetFrame((trace.FrameCount - 1)).GetFileLineNumber()).ToString();
                    //string Err_Source = (trace.GetFrame((trace.FrameCount - 1)).GetFileName()).ToString();
                    sw1.Write(ex.Message.ToString());
                    sw1.Write(sw1.NewLine);
                    sw1.Flush();
                    sw1.Close();
                    fs1.Close();
                }
                catch
                {
                }
            }
        }
    }
}
