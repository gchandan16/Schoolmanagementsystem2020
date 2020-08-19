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
using Newtonsoft;

namespace WEB.Controllers
{
    [Authorize]
    [PageActionFilter]
    public class FeeController : Controller
    {
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        BALCommon CSvc;
        int _Financialyearid = 0;
        int _OrgnisationID = 0;
        string _FinacialOrgid = "";
        public FeeController()
        {
            CSvc = new BALCommon(ConStr);
            SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
            _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
            _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);

        }
        public ActionResult FeeHeads(string FeeId = "", string ActionName = "")
        {
            FeeHead FH = new FeeHead();
            FeeHead model = new FeeHead();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            //FH.ActionName = string.IsNullOrEmpty(ActionName) ? Constants.BTN_CREATE : ActionName;
            //ChannelFactory<ICommonSrv> CF = new ChannelFactory<ICommonSrv>("COMMON");
            //ChannelFactory<IFeeSrv> CFFee = new ChannelFactory<IFeeSrv>("FEE");
            //ICommonSrv CSvc = (ICommonSrv)CF.CreateChannel();
            //IFeeSrv CSFee = (IFeeSrv)CFFee.CreateChannel();
            try
            {
                model.ActionName = Constants.BTN_CREATE;
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                FH.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                FH.CreatedBy = WebSecurity.CurrentUserName;
                FH.Action = "GET";
                List<FeeHead> lstFeehead = CSFee.GetFeeHeads(FH);
                if (!string.IsNullOrEmpty(ActionName) && ActionName.ToUpper() == "EDIT" && !string.IsNullOrEmpty(FeeId))
                {
                    model = lstFeehead.Where(x => x.FeeID == FeeId).FirstOrDefault();
                    model.ActionName = Constants.BTN_EDIT;
                }
                model.lstFeeHead = lstFeehead;
                model.FrequecyLst = CSFee.GetDDList("FREQUENCY", _OrgnisationID); //userMasters.SchoolID
                model.RefundableLst = CSFee.GetDDList("Refundable", _OrgnisationID); //userMasters.SchoolID
            }
            catch (Exception ex) { }
            finally
            {
                //CF.Close();
                //CFFee.Close();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeeHeads(FeeHead feeHead, FormCollection collection)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            string actionStatus = "";
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);

                feeHead.Active = true;
                feeHead.CreatedBy = WebSecurity.CurrentUserName;
                feeHead.SchoolID = _OrgnisationID;//userMasters.SchoolID;

                if (collection["btntype"].ToUpper() == "Save".ToUpper())
                {
                    feeHead.Action = "INS";

                }
                else if (collection["btntype"].ToUpper() == "Update".ToUpper())
                {
                    feeHead.Action = "UPDATE";
                }
                actionStatus = CSFee.SaveFeeHead(feeHead);
                TempData[Constants.MessageInfo.SUCCESS] = actionStatus;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Fee(SaveFeeTerm)", "Error_014", ex, "Fee");
            }
            finally
            {

            }
            return RedirectToAction("FeeHeads");
        }
        public JsonResult GetTermDetails(string feeid)
        {
            FeeHeadDescription FHD = new FeeHeadDescription();
            //ChannelFactory<IFeeSrv> CFFee = new ChannelFactory<IFeeSrv>("FEE");
            //IFeeSrv CSFee = (IFeeSrv)CFFee.CreateChannel();
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                FHD.FeeID = feeid;
                FHD.Action = "GET";
                FHD.lstFeeHeadDesc = CSFee.GetFeeHeadDescription(FHD);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Fee(SaveFeeTerm)", "Error_014", ex, "Fee");
            }
            finally
            {
                //CFFee.Close();
            }
            return Json(FHD.lstFeeHeadDesc, JsonRequestBehavior.AllowGet);
        }
        [ValidateJsonAntiForgeryToken]
        public JsonResult SaveFeeDescription(string feeid, string FeeDescName, string StartDate, string EndDate, string DueDate, string monthdetail)
        {
            FeeHeadDescription FHD = new FeeHeadDescription();
            BALFee CSFee = new BALFee(ConStr);

            //ChannelFactory<IFeeSrv> CFFee = new ChannelFactory<IFeeSrv>("FEE");
            //IFeeSrv CSFee = (IFeeSrv)CFFee.CreateChannel();
            string actionStatus = "";
            try
            {
                FHD.FeeID = feeid;
                FHD.FeeDescName = FeeDescName;
                FHD.StartDate = StartDate;
                FHD.EndDate = EndDate;
                FHD.DueDate = DueDate;
                FHD.Months = monthdetail;
                FHD.CreatedBy = WebSecurity.CurrentUserName;
                FHD.Action = "INS";
                FHD.Active = true;
                actionStatus = CSFee.SaveFeeHeadDescription(FHD);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Fee(SaveFeeTerm)", "Error_014", ex, "Fee");
            }
            finally
            {
                //CFFee.Close();
            }
            return Json(actionStatus, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFeeHeads(string FeeID)
        {
            string actionStatus = "";
            try
            {
                BALFee CSFee = new BALFee(ConStr);
                actionStatus = CSFee.DeleteFeeHead(FeeID);
                TempData[Constants.MessageInfo.SUCCESS] = actionStatus;
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("FeeHeads", "Fee");
        }
        public JsonResult DeleteFeeHeadDescription(string feeid, string FeeDescID)
        {
            FeeHeadDescription FHD = new FeeHeadDescription();
            BALFee CSFee = new BALFee(ConStr);
            //ChannelFactory<IFeeSrv> CFFee = new ChannelFactory<IFeeSrv>("FEE");
            //IFeeSrv CSFee = (IFeeSrv)CFFee.CreateChannel();
            string actionStatus = "";
            try
            {
                FHD.FeeID = feeid;
                FHD.FeeDescID = FeeDescID;
                FHD.Action = "DEL";
                actionStatus = CSFee.DeleteFeeHeadDescription(FHD);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Fee(DeleteFeeHeadDescription)", "Error_014", ex, "Fee");
            }
            finally
            {
                //CFFee.Close();
            }
            return Json(actionStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FeeGroup(string FeeGroupId = "", string ActionName = "")
        {
            FeeGroup FG = new FeeGroup();
            FeeGroup model = new FeeGroup();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {

                model.ActionName = Constants.BTN_CREATE;
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                FG.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                FG.CreatedBy = WebSecurity.CurrentUserName;
                FG.Action = "GET";
                List<FeeGroup> lstFeeGroup = CSFee.GetFeeGroup(FG);
                model.lstFeeGroup = lstFeeGroup;
                List<FeeGroup> editData = null;

                if (!string.IsNullOrEmpty(ActionName) && ActionName.ToUpper() == "EDIT" && !string.IsNullOrEmpty(FeeGroupId))
                {
                    editData = new List<FeeGroup>();
                    editData = lstFeeGroup.Where(x => x.FeeGroupName == (lstFeeGroup.Where(y => y.FeeGroupID == FeeGroupId).Select(y => y.FeeGroupName).FirstOrDefault())).ToList();
                    model.FeeGroupID = editData[0].FeeGroupID;
                    model.FeeGroupName = editData[0].FeeGroupName;
                    model.Frequency = editData[0].Frequency;
                    model.ActionName = Constants.BTN_EDIT;
                }
                #region Get Fee Head List
                FeeHead FH = new FeeHead();
                FH.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                FH.CreatedBy = WebSecurity.CurrentUserName;
                FH.Action = "GET";
                List<FeeHead> lstFeehead = CSFee.GetFeeHeads(FH);
                List<FeeHeadList> obj = new List<FeeHeadList>();
                foreach (var LFH in lstFeehead)
                {
                    obj.Add(new FeeHeadList
                    {
                        ID = LFH.FeeID,
                        Check = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? true : false : false,
                        Name = LFH.FeeHeadName,
                        Amount = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.Amount).FirstOrDefault() : "0" : "0",
                        Apr = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Apr).FirstOrDefault() : "0" : "0",
                        May = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.May).FirstOrDefault() : "0" : "0",
                        Jun = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Jun).FirstOrDefault() : "0" : "0",
                        Jul = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Jul).FirstOrDefault() : "0" : "0",
                        Aug = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Aug).FirstOrDefault() : "0" : "0",
                        Sep = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Sep).FirstOrDefault() : "0" : "0",
                        Oct = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Oct).FirstOrDefault() : "0" : "0",
                        Nov = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Nov).FirstOrDefault() : "0" : "0",
                        Dec = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Dec).FirstOrDefault() : "0" : "0",
                        Jan = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Jan).FirstOrDefault() : "0" : "0",
                        Feb = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Feb).FirstOrDefault() : "0" : "0",
                        Mar = (editData != null && editData.Count > 0) ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Any() ? editData.Where(x => x.FeeHeadID == LFH.FeeID).Select(x => x.feeHead.Mar).FirstOrDefault() : "0" : "0",
                        months = LFH.months,
                    });
                }
                model.feeHeadList = obj;
                #endregion Get Fee Head List
                model.FrequecyLst = CSFee.GetDDList("FREQUENCY", _OrgnisationID); //userMasters.SchoolID
            }
            catch (Exception ex) { }
            finally
            {
                //CF.Close();
                //CFFee.Close();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeeGroup(FeeGroup feeGroup, FormCollection collection)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            string actionStatus = "";
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);

                feeGroup.Active = true;
                feeGroup.CreatedBy = WebSecurity.CurrentUserName;
                feeGroup.SchoolID = _OrgnisationID;//userMasters.SchoolID;

                if (collection["btntype"].ToUpper() == "Save".ToUpper())
                {
                    FeeGroup obj = new FeeGroup();
                    foreach (var FH in feeGroup.feeHeadList)
                    {
                        if (FH.Check)
                        {
                            obj.Action = "INS";
                            obj.CreatedBy = WebSecurity.CurrentUserName;
                            obj.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                            obj.FeeGroupName = feeGroup.FeeGroupName;
                            obj.Frequency = feeGroup.Frequency;
                            obj.FeeHeadName = FH.Name;
                            obj.FeeHeadID = FH.ID;
                            obj.Amount = FH.Amount;
                            obj.feeHead = FH;
                            obj.Active = true;
                            actionStatus = CSFee.SaveFeeGroup(obj);
                        }
                    }
                }
                else if (collection["btntype"].ToUpper() == "Update".ToUpper())
                {
                    feeGroup.Action = "UPDATE";
                    actionStatus = CSFee.DeleteFeeGroup("", feeGroup.FeeGroupName);
                    if (actionStatus.ToUpper().Contains("SUCCESS"))
                    {
                        FeeGroup obj = new FeeGroup();
                        foreach (var FH in feeGroup.feeHeadList)
                        {
                            if (FH.Check)
                            {
                                obj.Action = "INS";
                                obj.CreatedBy = WebSecurity.CurrentUserName;
                                obj.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                                obj.FeeGroupName = feeGroup.FeeGroupName;
                                obj.Frequency = feeGroup.Frequency;
                                obj.FeeHeadName = FH.Name;
                                obj.FeeHeadID = FH.ID;
                                obj.Amount = FH.Amount;
                                obj.feeHead = FH;
                                obj.Active = true;
                                actionStatus = CSFee.SaveFeeGroup(obj);
                            }
                        }
                    }

                }

                TempData[Constants.MessageInfo.SUCCESS] = actionStatus;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Fee(SaveFeeGroup)", "Error_014", ex, "Fee");
            }
            finally
            {

            }
            return RedirectToAction("FeeGroup");
        }
        [HttpPost]
        public ActionResult DeleteFeeGroup(string FeeGroupID, string FeeGroupName)
        {
            string actionStatus = "";
            try
            {
                BALFee CSFee = new BALFee(ConStr);
                actionStatus = CSFee.DeleteFeeGroup(FeeGroupID, FeeGroupName);
                TempData[Constants.MessageInfo.SUCCESS] = actionStatus;
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("FeeGroup", "Fee");
        }

        public ActionResult FeeGroupAllocation()
        {
            FeeGroupAllocation model = new FeeGroupAllocation();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                model.FeeForList = CSFee.GetDDList("FeeFor", _OrgnisationID);// userMasters.SchoolID);
                FeeGroup FG = new FeeGroup();
                FG.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                FG.CreatedBy = WebSecurity.CurrentUserName;
                FG.Action = "GET";
                List<FeeGroup> lstFeeGroup = CSFee.GetFeeGroup(FG);
                model.lstFeeGroup = lstFeeGroup;

            }
            catch (Exception ex) { }
            finally
            {

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeeGroupAllocation(FeeGroupAllocation model)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            int res = 0;
            try
            {
                if (!string.IsNullOrEmpty(model.FeeFor))
                {
                    //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                    if (model.FeeFor == "C")
                    {
                        res = 0;
                        for (int i = 0; i < model.SectionList.Where(x => x.Check).ToList().Count; i++)
                        {
                            res += CSFee.UpdateStudentFeeGroupName(model.FeeGroupName, model.SectionList[i].SectionID, 0, _OrgnisationID);// userMasters.SchoolID);
                        }
                    }
                    else if (model.FeeFor == "S")
                    {
                        res = 0;
                        for (int i = 0; i < model.StudentList.Where(x => x.Check).ToList().Count; i++)
                        {
                            res += CSFee.UpdateStudentFeeGroupName(model.FeeGroupName, model.SectionID, model.StudentList[i].Smid, _OrgnisationID);//userMasters.SchoolID);
                        }
                    }
                    if (res > 0)
                        TempData[Constants.MessageInfo.SUCCESS] = res + " rows inserted.";
                    else
                        TempData[Constants.MessageInfo.WARNING] = res + " row inserted.";
                }
            }
            catch (Exception ex) { }
            finally
            {

            }
            return RedirectToAction("FeeGroupAllocation");
        }
        public JsonResult GetFeeHeadDetails(string feeGroupName, string AdmissionNo)
        {
            List<FeeHeadList> obj = new List<FeeHeadList>();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                obj = CSFee.GetFeeHeadListForFeeGroup(feeGroupName, _OrgnisationID, AdmissionNo, _Financialyearid);// userMasters.SchoolID);
            }
            catch { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClassList()
        {
            List<ClassMaster> obj = new List<ClassMaster>();
            BALCommon CSvc = new BALCommon(ConStr);

            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                obj = CSvc.GetClassList(_OrgnisationID);// userMasters.SchoolID);
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClassSectionList()
        {
            List<ClassSectionMaster> obj = new List<ClassSectionMaster>();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                obj = CSFee.GetClassSection(0, _OrgnisationID);// userMasters.SchoolID);
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentMaster(int StudentID, int SectionID)
        {
            List<StudentMaster> obj = new List<StudentMaster>();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                obj = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, 0, SectionID, 0, "");
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FeeCollection()
        {
            FeeCollection model = new FeeCollection();
            return View(model);
        }
        //[HttpPost]
        //public ActionResult FeeCollection(FeeCollection model)
        //{
        //    int rno = 0;
        //    BALFee CSFee = new BALFee(ConStr);
        //    List<FeeHeadList> objFeeHead = new List<FeeHeadList>();
        //    BALCommon CSvc = new BALCommon(ConStr);
        //    try
        //    {
        //        #region TotalmonthFee and TotalmonthFeeBreakup
        //        //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
        //        objFeeHead = CSFee.GetFeeHeadListForFeeGroup(model.Student.FeeGroup, userMasters.SchoolID);
        //        string months = "";
        //        decimal TotalFee = 0;
        //        string TotalFeesBreakUp = "";
        //        if (model.months.April)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "April" : ",April";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Apr));
        //            var BreakUp = "Apr:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Apr:"  ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Apr;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.May)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "May" : ",May";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.May));
        //            var BreakUp = "May:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "May:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.May;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.June)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "June" : ",June";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Jun));
        //            var BreakUp = "Jun:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Jun:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Jun;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.July)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "July" : ",July";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Jul));
        //            var BreakUp = "Jul:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Jul:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Jul;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.August)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "August" : ",August";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Aug));
        //            var BreakUp = "Aug:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Aug:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Aug;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.September)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "September" : ",September";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Sep));
        //            var BreakUp = "Sep:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Sep:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Sep;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.October)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "October" : ",October";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Oct));
        //            var BreakUp = "Oct:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Oct:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Oct;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.November)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "November" : ",November";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Nov));
        //            var BreakUp = "Nov:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Nov:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Nov;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.December)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "December" : ",December";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Dec));
        //            var BreakUp = "Dec:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Dec:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Dec;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.January)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "January" : ",January";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Jan));
        //            var BreakUp = "Jan:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Jan:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Jan;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.February)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "February" : ",February";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Feb));
        //            var BreakUp = "Feb:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Feb:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Feb;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }
        //        if (model.months.March)
        //        {
        //            months += string.IsNullOrEmpty(months) ? "March" : ",March";
        //            TotalFee += objFeeHead.Sum(x => Convert.ToDecimal(x.Mar));
        //            var BreakUp = "Mar:";
        //            foreach (var objFH in objFeeHead)
        //            {
        //                BreakUp += BreakUp == "Mar:" ? "" : "_";
        //                BreakUp += objFH.Name + "#" + objFH.Mar;
        //            }
        //            TotalFeesBreakUp += !string.IsNullOrEmpty(TotalFeesBreakUp) ? (";" + BreakUp) : BreakUp;
        //        }

        //        #endregion TotalmonthFee and TotalmonthFeeBreakup
        //        FeeDeposit obj = new FeeDeposit
        //        {
        //            Action = "Deposit",
        //            AdmissionNo = model.Student.Adminssionno,
        //            Balance = model.Balance,
        //            Concession = model.Concession,
        //            Fine = model.Fine,
        //            months = months,
        //            MOP = model.MOP,
        //            MOPRemark = model.MOPRemark,
        //            Payment = model.Payment,
        //            Remarks = model.Remarks,
        //            StudentID = model.Student.Smid,
        //            StudentName = model.Student.Firstname,
        //            TotalFees = Convert.ToString(TotalFee),
        //            TotalFeesBreakUp = TotalFeesBreakUp,
        //            WaiveOff = model.WaiveOff,
        //            FeeGroup = model.Student.FeeGroup,
        //            SchoolId = userMasters.SchoolID,
        //            Arrears = model.Arrears,
        //        };
        //        rno = CSFee.FeeDeposit(obj);
        //        TempData["rno"] = rno;
        //    }
        //    catch (Exception ex) { ExecptionLogger.FileHandling("Fee(FeeCollection_Post)", "Error_014", ex, "Fee"); }
        //    return RedirectToAction("FeeCollection", new { rno = rno });
        //}
        public static String RenderViewToString(ControllerContext context, String viewPath, object model = null)
        {
            context.Controller.ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(context, viewPath, null);
                var viewContext = new ViewContext(context, viewResult.View, context.Controller.ViewData, context.Controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(context, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult FeeCollection(FeeDeposit feeDeposit)
        {
            int rno = 0;
            BALFee CSFee = new BALFee(ConStr);
            List<FeeHeadList> objFeeHead = new List<FeeHeadList>();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                feeDeposit.Action = "Deposit";
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                feeDeposit.SchoolId = _OrgnisationID;//userMasters.SchoolID;
                feeDeposit.FinancialYear = _Financialyearid;
                feeDeposit.CreatedBy = WebSecurity.CurrentUserName;
                rno = CSFee.FeeDeposit(feeDeposit);
            }
            catch (Exception ex) { ExecptionLogger.FileHandling("Fee(FeeCollection_Post)", "Error_014", ex, "Fee"); }
            return Json(rno, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetFeeDepositHistory(int StudentID, string AdmissionNo)
        {
            List<FeeDeposit> obj = new List<FeeDeposit>();
            FeeDeposit FeeDepositDetails = new FeeDeposit();
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                FeeDepositDetails.StudentID = StudentID;
                FeeDepositDetails.AdmissionNo = AdmissionNo;
                FeeDepositDetails.FinancialYear = _Financialyearid;
                FeeDepositDetails.SchoolId = _OrgnisationID;
                FeeDepositDetails.Action = "SELECT";
                obj = CSFee.GetFeeDeposit(FeeDepositDetails);
            }
            catch { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetFeeReceipt(int ReceiptNo)
        {
            FeeReciept feeReciept = new FeeReciept();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            FeeDeposit FeeDepositDetails = new FeeDeposit();
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                FeeDepositDetails.ReceiptNo = Convert.ToString(ReceiptNo);
                FeeDepositDetails.SchoolId = _OrgnisationID;//userMasters.SchoolID;
                FeeDepositDetails.FinancialYear = _Financialyearid;
                FeeDepositDetails.Action = "SELECT";
                feeReciept.FeeDetails = CSFee.GetFeeDepositByReceiptNo(FeeDepositDetails);
                feeReciept.OragnisationDetails = CSvc.GetOragnisationDetails(feeReciept.FeeDetails.SchoolId);
                feeReciept.StudentDetails = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, 0, 0, 0, feeReciept.FeeDetails.AdmissionNo).FirstOrDefault();

            }
            catch { }
            return PartialView("FeeReceipt", feeReciept);
        }
        public JsonResult GetSingleStudent(int StudentID)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            StudentMaster Obj = new StudentMaster();
            try
            {
                Obj = CSvc.GetSingleStudent(StudentID);
            }
            catch { }
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }

        #region Ledger
        [HttpGet]
        public ActionResult Ledger()
        {
            Ledger model = new Ledger();
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                model.CreatedBy = WebSecurity.CurrentUserName;
                model.SchoolId = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                model.LedgerList = CSvc.GerLedgerReport(model);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ledger(Ledger model)
        {
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                model.CreatedBy = WebSecurity.CurrentUserName;
                model.SchoolId = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                model.LedgerList = CSvc.GerLedgerReport(model);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpGet]
        public ActionResult LedgerEntry()
        {
            Ledger model = new Ledger();
            try
            {
                model.EffectiveDate = DateTime.Now.ToString("dd/MMM/yyyy");
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LedgerEntry(Ledger model, FormCollection collection)
        {
            BALFee obj = new BALFee(ConStr);
            try
            {
                if (collection["btnType"] == "Debit")
                {
                    model.Debit = model.Amount;
                    model.Credit = 0;
                }
                else if (collection["btnType"] == "Credit")
                {
                    model.Credit = model.Amount;
                    model.Debit = 0;
                }
                model.CreatedBy = WebSecurity.CurrentUserName;
                model.SchoolId = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                obj.InsertLedgerEntry(model);
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Ledger");
        }
        #endregion Ledger
        #region Salary
        [HttpGet]
        public ActionResult SalaryCreation()
        {
            EmployeeAttendance model = new EmployeeAttendance();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalaryCreation(EmployeeAttendance model, FormCollection collection)
        {
            List<EmployeeMaster> _EmployeeList = new List<EmployeeMaster>();
            BALFee objBALFee = new BALFee(ConStr);
            BALCommon CSvc = new BALCommon(ConStr);
            int totalRec = 0;
            try
            {
                if (collection["btnSearch"].ToUpper().Contains("Search".ToUpper()))
                {
                    model.SchoolID = _OrgnisationID;
                    model.FinancialYear = _Financialyearid;
                    model = objBALFee.GetEmployeeAttendance((EmployeeAttendance)model.Clone());
                }
                else if (collection["btnSearch"].ToUpper().Contains("Display".ToUpper()) || collection["btnSearch"].ToUpper().Contains("Submit".ToUpper()))
                {
                    EmployeeAttendance obj = new EmployeeAttendance();
                    obj.SchoolID = _OrgnisationID;
                    obj.FinancialYear = _Financialyearid;
                    obj.MonthYear = model.MonthYear;
                    obj = objBALFee.GetEmployeeAttendance((EmployeeAttendance)obj.Clone());
                    List<EmployeeMaster> EmployeeList = objBALFee.GetEmployeeList(1, _OrgnisationID);
                    List<TDSSettingmaster> TDSList = CSvc.GetTDSSettingList(_OrgnisationID, _Financialyearid);
                    for (int i = 0; i < model.EmployeeAttendanceList.Count(); i++)
                    {
                        int EmployeeID = model.EmployeeAttendanceList[i].EmployeeDetails.EMP_ID;
                        var tempCopy = (AttendanceDetails)model.EmployeeAttendanceList[i].AttendanceDetail;
                        model.EmployeeAttendanceList[i].AttendanceDetail = obj.EmployeeAttendanceList.Where(x => x.EmployeeDetails.EMP_ID == EmployeeID).Select(x => x.AttendanceDetail).FirstOrDefault();
                        model.EmployeeAttendanceList[i].EmployeeDetails = obj.EmployeeAttendanceList.Where(x => x.EmployeeDetails.EMP_ID == EmployeeID).Select(x => x.EmployeeDetails).FirstOrDefault();
                        model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalAbsent = tempCopy.ModifiedTotalAbsent;
                        model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalHoliday = tempCopy.ModifiedTotalHoliday;
                        model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalMissingAttendence = tempCopy.ModifiedTotalMissingAttendence;
                        model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalPresent = tempCopy.ModifiedTotalPresent;
                        model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalWO = tempCopy.ModifiedTotalWO;

                        model.EmployeeAttendanceList[i].DaySalary = "0";
                        if (model.EmployeeAttendanceList[i].Check)
                        {
                            double TotalSalary = 0;
                            List<Salaryheadmaster> EmployeeSalaryHeadList = CSvc.GetSalaryheadlist(_OrgnisationID, _Financialyearid, Convert.ToInt32(EmployeeID));
                            decimal LeaveBal = !model.EmployeeAttendanceList[i].LeaveInCash ? 0 : Convert.ToDecimal(model.EmployeeAttendanceList[i].EmployeeDetails.LEAVE_PL_ENTITLED) + Convert.ToDecimal(model.EmployeeAttendanceList[i].EmployeeDetails.LEAVE_CL_ENTITLED) + Convert.ToDecimal(model.EmployeeAttendanceList[i].EmployeeDetails.LEAVE_SL_ENTITLED);
                            decimal Deduction = model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalUnPaidLeave;
                            if (!model.EmployeeAttendanceList[i].LeaveInCash)
                            {
                                Deduction += model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalAbsent;
                            }
                            else
                            {
                                if (LeaveBal < model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalAbsent)
                                {
                                    Deduction += (model.EmployeeAttendanceList[i].AttendanceDetail.ModifiedTotalAbsent - LeaveBal);
                                }
                            }

                            model.EmployeeAttendanceList[i].TotalEarning = EmployeeSalaryHeadList.Where(x => x.Headtype.ToUpper() == "E").Sum(x => x.Defaultvalue);
                            model.EmployeeAttendanceList[i].TotalDeduction = EmployeeSalaryHeadList.Where(x => x.Headtype.ToUpper() == "D").Sum(x => x.Defaultvalue);
                            model.EmployeeAttendanceList[i].MonthSalary = model.EmployeeAttendanceList[i].TotalEarning - model.EmployeeAttendanceList[i].TotalDeduction;
                            model.EmployeeAttendanceList[i].EmployeePFAmount = 0;
                            model.EmployeeAttendanceList[i].EmployerPFAmount = 0;
                            if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF).Any())
                            {
                                if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF && x.PFType.ToUpper() == "FIXED".ToUpper()).Any())
                                {
                                    model.EmployeeAttendanceList[i].EmployeePFAmount = EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF && x.PFType.ToUpper() == "FIXED".ToUpper()).Select(x => x.PFEmployeeAmount).FirstOrDefault();
                                    model.EmployeeAttendanceList[i].EmployerPFAmount = EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF && x.PFType.ToUpper() == "FIXED".ToUpper()).Select(x => x.PFEmployerAmount).FirstOrDefault();
                                }
                                else if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF && x.PFType.ToUpper() == "Percentage".ToUpper()).Any())
                                {
                                    model.EmployeeAttendanceList[i].EmployeePFAmount = (EmployeeSalaryHeadList.Where(x => x.Epfapplicable.ToUpper() == "YES").Sum(x => x.Defaultvalue) * EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF && x.PFType.ToUpper() == "Percentage".ToUpper()).Select(x => x.PFEmployeeAmount).FirstOrDefault()) / 100;
                                    model.EmployeeAttendanceList[i].EmployerPFAmount = (EmployeeSalaryHeadList.Where(x => x.Epfapplicable.ToUpper() == "YES").Sum(x => x.Defaultvalue) * EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsPF && x.PFType.ToUpper() == "Percentage".ToUpper()).Select(x => x.PFEmployerAmount).FirstOrDefault()) / 100;
                                }
                            }
                            model.EmployeeAttendanceList[i].EmployeeESIAmount = 0;
                            model.EmployeeAttendanceList[i].EmployerESIAmount = 0;
                            if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI).Any())
                            {
                                if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI && x.PFType.ToUpper() == "FIXED".ToUpper()).Any())
                                {
                                    model.EmployeeAttendanceList[i].EmployeeESIAmount = EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI && x.PFType.ToUpper() == "FIXED".ToUpper()).Select(x => x.ESIEmployeeAmount).FirstOrDefault();
                                    model.EmployeeAttendanceList[i].EmployerESIAmount = EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI && x.PFType.ToUpper() == "FIXED".ToUpper()).Select(x => x.ESIEmployerAmount).FirstOrDefault();
                                }
                                else if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI && x.PFType.ToUpper() == "Percentage".ToUpper()).Any())
                                {
                                    model.EmployeeAttendanceList[i].EmployeeESIAmount = (EmployeeSalaryHeadList.Where(x => x.Esiapplicable.ToUpper() == "YES").Sum(x => x.Defaultvalue) * EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI && x.PFType.ToUpper() == "Percentage".ToUpper()).Select(x => x.ESIEmployeeAmount).FirstOrDefault()) / 100;
                                    model.EmployeeAttendanceList[i].EmployerESIAmount = (EmployeeSalaryHeadList.Where(x => x.Esiapplicable.ToUpper() == "YES").Sum(x => x.Defaultvalue) * EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsESI && x.PFType.ToUpper() == "Percentage".ToUpper()).Select(x => x.ESIEmployerAmount).FirstOrDefault()) / 100;
                                }
                            }
                            TotalSalary = model.EmployeeAttendanceList[i].TotalEarning - model.EmployeeAttendanceList[i].TotalDeduction - model.EmployeeAttendanceList[i].EmployeeESIAmount - model.EmployeeAttendanceList[i].EmployerESIAmount - model.EmployeeAttendanceList[i].EmployeePFAmount - model.EmployeeAttendanceList[i].EmployerPFAmount - model.EmployeeAttendanceList[i].EmployeeTDS;
                            if (Deduction > 0)
                            {
                                TotalSalary = Math.Round(TotalSalary - ((EmployeeSalaryHeadList.Where(x => x.Leavededucation.ToUpper() == "YES").Sum(x => x.Defaultvalue) / obj.TotalDaysInMonth) * Convert.ToDouble(Deduction)), 2);
                            }
                            model.EmployeeAttendanceList[i].EmployeeTDS = 0;
                            if (EmployeeList.Where(x => x.EMP_ID == EmployeeID && x.IsTDS).Any() && TDSList.Where(x => x.Taxcategory == EmployeeList.Where(y => y.EMP_ID == EmployeeID).Select(y => y.TaxCategory).FirstOrDefault()).Any())
                            {
                                double MaxAmpunt = TDSList.Where(x => x.Taxcategory == EmployeeList.Where(y => y.EMP_ID == EmployeeID).Select(y => y.TaxCategory).FirstOrDefault()).Select(z => z.Tmaxamount).FirstOrDefault() / 12;
                                double MinAmpunt = TDSList.Where(x => x.Taxcategory == EmployeeList.Where(y => y.EMP_ID == EmployeeID).Select(y => y.TaxCategory).FirstOrDefault()).Select(z => z.Tminamount).FirstOrDefault() / 12;
                                double TaxRate = TDSList.Where(x => x.Taxcategory == EmployeeList.Where(y => y.EMP_ID == EmployeeID).Select(y => y.TaxCategory).FirstOrDefault()).Select(z => z.TaxRate).FirstOrDefault();
                                if (TotalSalary <= MaxAmpunt && TotalSalary >= MinAmpunt)
                                {
                                    model.EmployeeAttendanceList[i].EmployeeTDS = (TotalSalary * TaxRate) / 100;
                                    TotalSalary = TotalSalary - model.EmployeeAttendanceList[i].EmployeeTDS;
                                }
                            }
                            model.EmployeeAttendanceList[i].PaidSalary = Convert.ToString(TotalSalary);
                        }
                    }

                    if (collection["btnSearch"].ToUpper().Contains("Submit".ToUpper()))
                    {
                        EmployeeSalary ObjEmployeeSalary = new EmployeeSalary();
                        List<EmployeeSalaryDetails> objEmployeeSalaryDetails = new List<EmployeeSalaryDetails>();
                        ObjEmployeeSalary.SchoolID = _OrgnisationID;
                        ObjEmployeeSalary.FinancialYear = _Financialyearid;
                        ObjEmployeeSalary.MonthYear = model.MonthYear;
                        ObjEmployeeSalary.TotalDays = model.TotalDaysInMonth;
                        for (int sal = 0; sal < model.EmployeeAttendanceList.Count(); sal++)
                        {
                            if (model.EmployeeAttendanceList[sal].Check)
                            {
                                EmployeeSalaryDetails objD = new EmployeeSalaryDetails();
                                objD.EMP_ID = model.EmployeeAttendanceList[sal].EmployeeDetails.EMP_ID;
                                objD.EMP_CODE = model.EmployeeAttendanceList[sal].EmployeeDetails.EMPCODE;
                                objD.EmployeeName = (model.EmployeeAttendanceList[sal].EmployeeDetails.FIRSTNAME + " " + model.EmployeeAttendanceList[sal].EmployeeDetails.LASTNAME).Trim();
                                objD.WorkingDays = model.EmployeeAttendanceList[sal].AttendanceDetail.TotalWorkingDaysList.Count();
                                objD.Present = model.EmployeeAttendanceList[sal].AttendanceDetail.ModifiedTotalPresent;
                                objD.Absent = model.EmployeeAttendanceList[sal].AttendanceDetail.ModifiedTotalAbsent;
                                objD.Holiday = model.EmployeeAttendanceList[sal].AttendanceDetail.ModifiedTotalHoliday;
                                objD.PaidLeave = model.EmployeeAttendanceList[sal].AttendanceDetail.ModifiedTotalPaidLeave;
                                objD.UnPaidLeave = model.EmployeeAttendanceList[sal].AttendanceDetail.ModifiedTotalUnPaidLeave;
                                objD.WeekOff = model.EmployeeAttendanceList[sal].AttendanceDetail.ModifiedTotalWO;
                                objD.PresentDays = String.Join(",", model.EmployeeAttendanceList[sal].AttendanceDetail.PresentList);
                                objD.AbsentDays = String.Join(",", model.EmployeeAttendanceList[sal].AttendanceDetail.AbsentList);
                                objD.HolidayDays = String.Join(",", model.EmployeeAttendanceList[sal].AttendanceDetail.HolidayList);
                                objD.PaidLeaveDays = String.Join(",", model.EmployeeAttendanceList[sal].AttendanceDetail.PaidLeaveList);
                                objD.UnPaidLeaveDays = String.Join(",", model.EmployeeAttendanceList[sal].AttendanceDetail.UnPaidLeaveList);
                                objD.WeekOffDays = String.Join(",", model.EmployeeAttendanceList[sal].AttendanceDetail.WOList);
                                objD.MonthSalary = Convert.ToDecimal(model.EmployeeAttendanceList[sal].MonthSalary);
                                objD.PaidSalary = Convert.ToDecimal(model.EmployeeAttendanceList[sal].PaidSalary);
                                objD.TotalEarning = Convert.ToDecimal(model.EmployeeAttendanceList[sal].TotalEarning);
                                objD.TotalDeduction = Convert.ToDecimal(model.EmployeeAttendanceList[sal].TotalDeduction);
                                objD.EmployeePFAmount = Convert.ToDecimal(model.EmployeeAttendanceList[sal].EmployeePFAmount);
                                objD.EmployerPFAmount = Convert.ToDecimal(model.EmployeeAttendanceList[sal].EmployerPFAmount);
                                objD.EmployeeESIAmount = Convert.ToDecimal(model.EmployeeAttendanceList[sal].EmployeeESIAmount);
                                objD.EmployerESIAmount = Convert.ToDecimal(model.EmployeeAttendanceList[sal].EmployerESIAmount);
                                objD.EmployeeTDS = Convert.ToDecimal(model.EmployeeAttendanceList[sal].EmployeeTDS);
                                objD.DaySalary = 0;// Math.Round(Convert.ToDecimal(model.EmployeeAttendanceList[sal].DaySalary), 2);
                                objD.ModeOfPayment = model.EmployeeAttendanceList[sal].EmployeeDetails.ModeOfPayment;
                                objD.BankName = model.EmployeeAttendanceList[sal].EmployeeDetails.BANKNAME;
                                objD.AccountNo = model.EmployeeAttendanceList[sal].EmployeeDetails.BANKACNO;
                                objD.User = WebSecurity.CurrentUserName;
                                objEmployeeSalaryDetails.Add(objD);
                            }
                        }
                        ObjEmployeeSalary.SalaryDetailsList = objEmployeeSalaryDetails;
                        totalRec = objBALFee.InsertSalaryEntry(ObjEmployeeSalary);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (collection["btnSearch"].ToUpper().Contains("Submit".ToUpper()))
                {
                    TempData[Constants.MessageInfo.SUCCESS] = "Total " + totalRec + " inserted successfully.";
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EmployeeSalary()
        {
            EmployeeSalary model = new EmployeeSalary();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeSalary(EmployeeSalary model, FormCollection collection, HttpPostedFileBase FileUpload)
        {
            BALFee objBALFee = new BALFee(ConStr);
            try
            {
                model.SchoolID = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                model.TotalDays = DateTime.DaysInMonth(Convert.ToInt32(Convert.ToDateTime(model.MonthYear).ToString("yyyy")), Convert.ToInt32(Convert.ToDateTime(model.MonthYear).ToString("MM")));
                model = objBALFee.GetEmployeeSalaryList(model, "");
                if (collection["btnSearch"].ToUpper().Contains("Download".ToUpper()))
                {
                    //Utility.ListtoDataTable lsttodt = new Utility.ListtoDataTable();
                    DataTable dt = Utility.ConvertListToDataTable(model.SalaryDetailsList);
                    if (dt.Select("PaymentStatus='False'").Any())
                    {
                        string FileName = "Salary_" + model.MonthYear.Replace("/", "") + "_" + DateTime.Now.ToString("ddMMyyyyHHmssfff") + ".xls";
                        ExportExcel(dt.Select("PaymentStatus='False'").CopyToDataTable(), FileName);
                    }
                    else
                    {
                        TempData[Constants.MessageInfo.WARNING] = "No record found.";
                    }
                }
                if (collection["btnSearch"].ToUpper().Contains("Upload".ToUpper()))
                {
                    DataTable dtDB = Utility.ConvertListToDataTable(model.SalaryDetailsList);
                    DataTable dtExcel = new DataTable();
                    if (FileUpload != null && (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                    {
                        string activeDir = Server.MapPath("~/EmployeeSalary/" + "School_" + _OrgnisationID + "/" + model.MonthYear.Replace("/", ""));
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
                                if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                                FileUpload.SaveAs(filePath);
                                var ds = Utility.ConvertExceltoDataTable(filePath);
                                if (ds.Tables.Count == 1)
                                {
                                    dtExcel = ds.Tables[0];
                                    DataColumnCollection ExcelColumns = dtExcel.Columns;
                                    if (!ExcelColumns.Contains("Remark"))
                                    {
                                        DataColumn workCol = dtExcel.Columns.Add("Remark", typeof(string));
                                        string FileName = "Salary_" + model.MonthYear.Replace("/", "") + "_" + DateTime.Now.ToString("ddMMyyyyHHmssfff") + ".xls";
                                        if (dtExcel.Rows.Count == 0)
                                        {
                                            TempData[Constants.MessageInfo.ERROR] = "No record found in uploaded excel sheet.";
                                        }
                                        else if (dtDB.Select("PaymentStatus='False'").Length == 0)
                                        {
                                            TempData[Constants.MessageInfo.ERROR] = "Payment has been updated for all employees for month: " + model.MonthYear.Replace("/", " ");
                                        }
                                        else
                                        {
                                            ExportExcel(UpdateEmployeeSalaryStatus(dtDB.Select("PaymentStatus='False'").CopyToDataTable(), dtExcel, model), FileName);
                                        }
                                    }
                                    else
                                    {
                                        TempData[Constants.MessageInfo.ERROR] = "Please upload valid excel file, Column 'Remark' exists.";
                                    }
                                }



                            }
                            catch (Exception ex)
                            {
                                if (System.IO.File.Exists(filePath))
                                { System.IO.File.Delete(filePath); }
                                TempData[Constants.MessageInfo.ERROR] = "Please upload valid excel file";
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
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public void ExportExcel(DataTable K, string flname)
        {
            using (ClosedXML.Excel.XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(K, "Employee");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + flname);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        private DataTable UpdateEmployeeSalaryStatus(DataTable DtDB, DataTable DtExcel, EmployeeSalary model)
        {
            BALFee objBALFee = new BALFee(ConStr);
            DataTable DtExcelCopy = new DataTable();
            model.SalaryDetailsList = new List<EmployeeSalaryDetails>();
            List<EmployeeSalaryDetails> obj = null;
            try
            {
                foreach (DataRow ExcelRow in DtExcel.Select())
                {
                    //DataRow[] DBRow = ;// 
                    if (DtDB.Select("Counter = '" + ExcelRow["Counter"] + "' AND EMP_ID = '" + ExcelRow["EMP_ID"] + "' AND EMP_CODE = '" + ExcelRow["EMP_CODE"] + "' ").Count() == 1)
                    {
                        try
                        {
                            if (Convert.ToString(ExcelRow["PaymentStatus"]).ToUpper() == "False".ToUpper())
                            {
                                ExcelRow["Remark"] = "Not updated.(PaymentStatus is False.)";
                            }
                            else
                            {
                                try
                                {
                                    obj = new List<EmployeeSalaryDetails>
                                    {
                                        new EmployeeSalaryDetails
                                        {
                                        Counter = Convert.ToInt32(ExcelRow["Counter"]),
                                        EMP_ID = Convert.ToInt32(ExcelRow["EMP_ID"]),
                                        EMP_CODE = Convert.ToString(ExcelRow["EMP_CODE"]),
                                        PaymentRemark = Convert.ToString(ExcelRow["PaymentRemark"]),
                                        PaymentStatus = Convert.ToBoolean(1),
                                        PaymentDate = string.IsNullOrEmpty(Convert.ToString(ExcelRow["PaymentDate"])) ? Convert.ToDateTime(ExcelRow["PaymentDate"]) : DateTime.Now,
                                        PaymentBy = string.IsNullOrEmpty(Convert.ToString(ExcelRow["PaymentBy"])) ? WebSecurity.CurrentUserName : Convert.ToString(ExcelRow["PaymentBy"]),
                                        User = WebSecurity.CurrentUserName,
                                        ModeOfPayment = Convert.ToString(ExcelRow["ModeOfPayment"]),
                                        BankName = Convert.ToString(ExcelRow["BankName"]),
                                        AccountNo = Convert.ToString(ExcelRow["AccountNo"]),
                                        }
                                    };
                                    model.SalaryDetailsList = obj;
                                    if (objBALFee.UpdateSalary(model) == 1)
                                    {
                                        ExcelRow["Remark"] = "Updated";
                                    }
                                    else
                                    {
                                        ExcelRow["Remark"] = "Not updated.";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ExcelRow["Remark"] = ex.Message.ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExcelRow["Remark"] = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        ExcelRow["Remark"] = "No record found in database/Payment has already done.";
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return DtExcel;
        }
        #endregion Salary

        #region  Fee Concession
        public ActionResult FeeConcession()
        {
            FeeConcession obj = new FeeConcession();
            return View(obj);
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
        public JsonResult GetStudentList(string ClassID, string SectionID, string AdmissionNo)
        {
            List<StudentMaster> obj = new List<StudentMaster>();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                obj = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, Convert.ToInt32(ClassID), Convert.ToInt32(SectionID), 0, AdmissionNo);
            }
            catch { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult FeeConcession(FeeConcessionDetails FeeConcessionDetails)
        {
            int res = 0;
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                FeeConcessionDetails.Action = FeeConcessionDetails.FeeConID != 0 ? "UPD" : "INS";
                FeeConcessionDetails.SchoolID = _OrgnisationID;
                FeeConcessionDetails.FinancialYearID = _Financialyearid;

                res = CSFee.AssignFeeConcession(FeeConcessionDetails);

            }
            catch
            {
                res = 1;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAssignFeeConcession(string FeeConID)
        {
            FeeConcessionDetails Obj = new FeeConcessionDetails();
            BALFee CSFee = new BALFee(ConStr);
            Obj.FinancialYearID = _Financialyearid;
            Obj.SchoolID = _OrgnisationID;
            Obj.FeeConID = Convert.ToInt32(FeeConID);
            Obj.Action = "GET";
            Obj = CSFee.GetAssignFeeConcession(Obj);
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }
        #endregion Fee Concession



        public ActionResult daybook()
        {
            Ledger model = new Ledger();
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                model.CreatedBy = WebSecurity.CurrentUserName;
                model.SchoolId = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                model.FromDate = DateTime.Now.ToString("dd/MMM/yyyy");
                model.LedgerList = CSvc.GerLedgerReport(model);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult daybook(Ledger model)
        {
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                model.CreatedBy = WebSecurity.CurrentUserName;
                model.SchoolId = _OrgnisationID;
                model.FinancialYear = _Financialyearid;
                model.EndDate = model.FromDate;
                model.LedgerList = CSvc.GerLedgerReport(model);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        #region DueFeeList
        public ActionResult DueFeeList()
        {
            BALCommon CSvc = new BALCommon(ConStr);
            DueFeeList obj = new DueFeeList();
            try
            {
                obj.ClassList = CSvc.GetClassList(_OrgnisationID);// userMasters.SchoolID);
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }
        public JsonResult GetDueFeeList(int ClassID, int SectionID, int FromMonth, int ToMonth)
        {
            List<StudentMaster> objStudentList = new List<StudentMaster>();
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                //if (FromMonth == "0" || FromMonth == "")
                //    FromMonth = "1";
                //if (ToMonth == "0" || ToMonth == "")
                //    ToMonth = Convert.ToString(DateTime.ParseExact(DateTime.Now.ToString("MMMM"), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month);
                objStudentList = CSvc.GetDueFeeList(_OrgnisationID, _Financialyearid, ClassID, SectionID, FromMonth, ToMonth);
            }
            catch { }

            return Json(objStudentList, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetUnPaidMonthDetails(string FeeGroupName, string AdmissionNo, string UnPaidMonth)
        {
            DueFeeListDetail Obj = new DueFeeListDetail();
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                var FeeHeadList = CSFee.GetFeeHeadListForFeeGroup(FeeGroupName, _OrgnisationID, AdmissionNo, _Financialyearid);// userMasters.SchoolID);
                FeeHeadList FeeHeadlst = new FeeHeadList();
                FeeHeadlst.Apr = "0";
                FeeHeadlst.May = "0";
                FeeHeadlst.Jun = "0";
                FeeHeadlst.Jul = "0";
                FeeHeadlst.Aug = "0";
                FeeHeadlst.Sep = "0";
                FeeHeadlst.Oct = "0";
                FeeHeadlst.Nov = "0";
                FeeHeadlst.Dec = "0";
                FeeHeadlst.Jan = "0";
                FeeHeadlst.Feb = "0";
                FeeHeadlst.Mar = "0";
                FeeHeadlst.Amount = "0";
                decimal TotalAmpunt = 0;
                foreach (var mnth in UnPaidMonth.Split(','))
                {
                    if (mnth == "April")
                    {
                        FeeHeadlst.Apr = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Apr)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Apr)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Apr);
                    }
                    else if (mnth == "May")
                    {
                        FeeHeadlst.May = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.May)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.May)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.May);
                    }
                    else if (mnth == "June")
                    {
                        FeeHeadlst.Jun = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Jun)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Jun)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Jun);
                    }
                    else if (mnth == "July")
                    {
                        FeeHeadlst.Jul = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Jul)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Jul)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Jul);
                    }
                    else if (mnth == "August")
                    {
                        FeeHeadlst.Aug = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Aug)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Aug)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Aug);
                    }
                    else if (mnth == "September")
                    {
                        FeeHeadlst.Sep = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Sep)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Sep)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Sep);
                    }
                    else if (mnth == "October")
                    {
                        FeeHeadlst.Oct = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Oct)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Oct)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Oct);
                    }
                    else if (mnth == "November")
                    {
                        FeeHeadlst.Nov = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Nov)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Nov)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Nov);
                    }
                    else if (mnth == "December")
                    {
                        FeeHeadlst.Dec = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Dec)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Dec)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Dec);
                    }
                    else if (mnth == "January")
                    {
                        FeeHeadlst.Jan = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Jan)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Jan)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Jan);
                    }
                    else if (mnth == "February")
                    {
                        FeeHeadlst.Feb = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Feb)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Feb)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Feb);
                    }
                    else if (mnth == "March")
                    {
                        FeeHeadlst.Mar = Convert.ToString(FeeHeadList.Where(y => y.Name != "Concession").Sum(x => Convert.ToDecimal(x.Mar)) - FeeHeadList.Where(y => y.Name == "Concession").Sum(x => Convert.ToDecimal(x.Mar)));
                        TotalAmpunt += Convert.ToDecimal(FeeHeadlst.Mar);
                    }
                }
                FeeHeadlst.Amount = Convert.ToString(TotalAmpunt);
                Obj.DueFeeList = FeeHeadlst;
                Obj.OragnisationDetails = CSvc.GetOragnisationDetails(_OrgnisationID);
                Obj.StudentDetails = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, 0, 0, 0, AdmissionNo).FirstOrDefault();
            }
            catch (Exception ex) { }
            return PartialView("DueFeeDetail", Obj);
        }

        #endregion DueFeeList
        #region TotalDueFeeList
        public ActionResult TotalDueFeeList()
        {
            BALCommon CSvc = new BALCommon(ConStr);
            DueFeeList obj = new DueFeeList();
            try
            {
                obj.ClassList = CSvc.GetClassList(_OrgnisationID);// userMasters.SchoolID);
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }
        public JsonResult GetTotalDueFeeList(int ClassID, int SectionID)
        {
            List<StudentMaster> objStudentList = new List<StudentMaster>();
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                int FromMonth = 1;
                int ToMonth = DateTime.ParseExact(DateTime.Now.ToString("MMMM"), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month - 3;
                objStudentList = CSvc.GetDueFeeList(_OrgnisationID, _Financialyearid, ClassID, SectionID, FromMonth, ToMonth);
            }
            catch { }

            return Json(objStudentList, JsonRequestBehavior.AllowGet);
        }

        #endregion DueFeeList

        public ActionResult IndisciplineList()
        {
            Indiscipline model = new Indiscipline();
            BALFee BALObj = new BALFee(ConStr);
            try
            {
                model.IndisciplineDetailList = BALObj.GetIndisciplineList(_OrgnisationID, _Financialyearid);
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ManageIndiscipline()
        {
            Indiscipline model = new Indiscipline();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageIndiscipline(Indiscipline model)
        {
            BALFee BALObj = new BALFee(ConStr);
            IndisciplineDetails _IndisciplineDetails = new IndisciplineDetails();
            try
            {
                if (!string.IsNullOrEmpty(model.Student.Adminssionno))
                {
                    _IndisciplineDetails.Status = model.Status;
                    _IndisciplineDetails.Remark = model.Remarks;
                    _IndisciplineDetails.FineAmount = model.FineAmount;
                    _IndisciplineDetails.AdmissionNo = model.Student.Adminssionno;
                    _IndisciplineDetails.SchoolId = _OrgnisationID;
                    _IndisciplineDetails.FinancialYearID = _Financialyearid;
                    _IndisciplineDetails.CreatedBy = WebSecurity.CurrentUserName;
                    var res = BALObj.SaveIndiscipline(_IndisciplineDetails);
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("IndisciplineList");
        }

        public PartialViewResult GetIndisciplineFeeReceipt(int IndisciplineID, string AdmissionNo)
        {
            IndisciplineFeeReciept feeReciept = new IndisciplineFeeReciept();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            IndisciplineDetails FeeDetails = new IndisciplineDetails();
            try
            {
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                FeeDetails.Counter = IndisciplineID;
                FeeDetails.SchoolId = _OrgnisationID;//userMasters.SchoolID;
                FeeDetails.FinancialYearID = _Financialyearid;
                feeReciept.FeeDetails = CSFee.GetIndisciplineFeeDepositByReceiptNo(FeeDetails);
                feeReciept.OragnisationDetails = CSvc.GetOragnisationDetails(_OrgnisationID);
                feeReciept.StudentDetails = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, 0, 0, 0, AdmissionNo).FirstOrDefault();

            }
            catch { }
            return PartialView("IndisciplineFeeReceipt", feeReciept);
        }
        public ActionResult UnallocatedFeeGroup()
        {
            UnallocatedFeeGroup model = new UnallocatedFeeGroup();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                FeeGroup FG = new FeeGroup();
                FG.SchoolID = _OrgnisationID;//userMasters.SchoolID;
                FG.CreatedBy = WebSecurity.CurrentUserName;
                FG.Action = "GET";
                model.lstFeeGroup = CSFee.GetFeeGroup(FG);
                model.ClassList = CSvc.GetClassList(_OrgnisationID);
            }
            catch (Exception ex) { }
            finally
            {

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnallocatedFeeGroup(UnallocatedFeeGroup model)
        {
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            int res = 0;
            try
            {
                res = 0;
                for (int i = 0; i < model.StudentList.Where(x => x.Check).ToList().Count; i++)
                {
                    res += CSFee.UpdateStudentFeeGroupName(model.FeeGroupName, model.StudentList[i].SecMid, model.StudentList[i].Smid, _OrgnisationID);//userMasters.SchoolID);
                }
                if (res > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = res + " rows inserted.";
                else
                    TempData[Constants.MessageInfo.WARNING] = res + " row inserted.";

            }
            catch (Exception ex) { }
            finally
            {

            }
            return RedirectToAction("UnallocatedFeeGroup");
        }
        public JsonResult UnallocatedFeeStudentMaster(string ClassID)
        {
            List<StudentMaster> obj = new List<StudentMaster>();
            BALCommon CSvc = new BALCommon(ConStr);
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                obj = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, string.IsNullOrEmpty(ClassID) ? 0 : Convert.ToInt32(ClassID), 0, 0, "").Where(x => string.IsNullOrEmpty(x.FeeGroup)).ToList();
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExtraFees()
        {
            ExtraFee model = new ExtraFee();
            return View(model);
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult ExtraFeeDeposit(FeeDeposit feeDeposit)
        {
            int rno = 0;
            BALFee CSFee = new BALFee(ConStr);
            List<FeeHeadList> objFeeHead = new List<FeeHeadList>();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                feeDeposit.TotalFeesBreakUp = DateTime.Now.ToString("MMM") + ":" + feeDeposit.TotalFeesBreakUp;
                feeDeposit.months = DateTime.Now.ToString("MMMM");
                feeDeposit.Action = "ExtraDeposit";
                //UserMasters userMasters = CSvc.getUserProfile(WebSecurity.CurrentUserName);
                feeDeposit.SchoolId = _OrgnisationID;//userMasters.SchoolID;
                feeDeposit.FinancialYear = _Financialyearid;
                feeDeposit.CreatedBy = WebSecurity.CurrentUserName;
                rno = CSFee.FeeDeposit(feeDeposit);
            }
            catch (Exception ex) { ExecptionLogger.FileHandling("Fee(ExtraFees_Post)", "Error_014", ex, "Fee"); }
            return Json(rno, JsonRequestBehavior.AllowGet);

        }


        #region DueFeeListMonthWise
        [HttpGet]
        public ActionResult DueFeesMonthWise()
        {
            BALCommon CSvc = new BALCommon(ConStr);
            DueFeesMonthWise obj = new DueFeesMonthWise();
            try
            {
                obj.ClassList = CSvc.GetClassList(_OrgnisationID);// userMasters.SchoolID);
                obj.SessionList = CSvc.GetFinancialYearList();
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }
        public JsonResult DueFeesMonthWise(int ClassID, int SectionID, int TillMonth, int Session)
        {
            List<StudentMaster> objStudentList = new List<StudentMaster>();
            BALFee CSvc = new BALFee(ConStr);
            try
            {
                //if (FromMonth == "0" || FromMonth == "")
                //    FromMonth = "1";
                //if (ToMonth == "0" || ToMonth == "")
                //    ToMonth = Convert.ToString(DateTime.ParseExact(DateTime.Now.ToString("MMMM"), "MMMM", System.Globalization.CultureInfo.InvariantCulture).Month);
                objStudentList = CSvc.GetDueFeeList(_OrgnisationID, Session, ClassID, SectionID, 1, TillMonth);
            }
            catch { }

            return Json(objStudentList, JsonRequestBehavior.AllowGet);
        }

        #endregion DueFeeListMonthWise


        #region FineConcessionReport
        [HttpGet]
        public ActionResult FineConcessionReport()
        {
            BALCommon CSvc = new BALCommon(ConStr);
            FineConcession obj = new FineConcession();
            List<SearchType> _searchType = new List<SearchType>();
            try
            {
                _searchType.Add(new SearchType
                {
                    searchID = "1",
                    searchName = "Fine"
                });
                _searchType.Add(new SearchType
                {
                    searchID = "2",
                    searchName = "Concession"
                });
                obj.SessionList = CSvc.GetFinancialYearList();
                obj.searchType = _searchType;
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FineConcessionReport(FineConcession obj)
        {
            BALFee ObjFee = new BALFee(ConStr);
            List<SearchType> _searchType = new List<SearchType>();
            try
            {
                obj.SchoolID = _OrgnisationID;



                _searchType.Add(new SearchType
                {
                    searchID = "1",
                    searchName = "Fine"
                });
                _searchType.Add(new SearchType
                {
                    searchID = "2",
                    searchName = "Concession"
                });
                obj.SessionList = CSvc.GetFinancialYearList();
                obj.searchType = _searchType;
                DataTable dt = new DataTable();
                if (obj.Search == "1")
                {
                    dt = ObjFee.GetFineConcessionList(obj).Select("Fine<>0", "").CopyToDataTable();
                }
                else if (obj.Search == "2")
                {
                    dt = ObjFee.GetFineConcessionList(obj).Select("Concession<>0", "").CopyToDataTable();
                }
                if (dt.Rows.Count > 0)
                {
                    obj.Report = dt;
                }
            }
            catch (Exception ex)
            {

            }
            return View(obj);
        }

        #endregion FineConcessionReport


        #region FeeRefund
        public JsonResult GetFeeRefundHistory(string AdmissionNo)
        {
            BALFee ObjFee = new BALFee(ConStr);
            FeeRefund Obj = new SHARED.FeeRefund();
            {
                StudentMaster SM = new SHARED.StudentMaster();
                SM.Adminssionno = AdmissionNo;
                Obj.Student = SM;
                Obj.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                Obj.SchoolID = _OrgnisationID;
                Obj.FinancialYear = _Financialyearid;
                Obj.Report = ObjFee.GetFeeRefundList(Obj);
            }

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(Obj.Report), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FeeRefund()
        {
            BALFee ObjFee = new BALFee(ConStr);
            FeeRefund Obj = new SHARED.FeeRefund();
            {
                StudentMaster SM = new SHARED.StudentMaster();
                SM.Adminssionno = "";
                Obj.Student = SM;
                Obj.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
                Obj.SchoolID = _OrgnisationID;
                Obj.FinancialYear = _Financialyearid;
                Obj.Report = ObjFee.GetFeeRefundList(Obj);
            }
            return View(Obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeeRefund(FeeRefund Obj)
        {
            BALFee CSFee = new BALFee(ConStr);
            try
            {
                Obj.SchoolID = _OrgnisationID;
                Obj.FinancialYear = _Financialyearid;
                Obj.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string resposne = CSFee.SaveFeeRefund(Obj);
                if (Convert.ToInt32(resposne) > 0)
                    TempData[Constants.MessageInfo.SUCCESS] = "Inserted successfully.";
                else
                    TempData[Constants.MessageInfo.ERROR] = "Some error occure. Please try again.";
            }
            catch
            {

            }
            return RedirectToAction("FeeRefund");
        }
        #endregion FeeRefund
    }

}
