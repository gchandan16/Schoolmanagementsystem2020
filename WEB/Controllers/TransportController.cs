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
    public class TransportController : Controller
    {
        readonly string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        BALCommon CSvc;
        BALTransport BALObj;
        int _Financialyearid = 0;
        int _OrgnisationID = 0;
        int _UserType = 0;
        int _UserTypeBaseID = 0;
        string _FinacialOrgid = "";
        public TransportController()
        {
            CSvc = new BALCommon(ConStr);
            BALObj = new BALTransport(ConStr);
            SessionInfo _SessionInfo = CSvc.GetSessionDetails(WebSecurity.CurrentUserName, DateTime.Now);
            _OrgnisationID = Convert.ToInt32(_SessionInfo.SchoolID);
            _Financialyearid = Convert.ToInt32(_SessionInfo.FinancialYearID);
            _UserType = Convert.ToInt32(_SessionInfo.UserType);
            _UserTypeBaseID = Convert.ToInt32(_SessionInfo.UserTypeBaseID);

        }
        public ActionResult VehicleMaster()
        {
            Vehicle model = new Vehicle();
            try
            {
                model.SchoolID = _OrgnisationID;
                model.FinancialYearID = _Financialyearid;
                model.Action = "GET";
                model.VehicleDetailList = BALObj.GetVehicleList(model);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Transport(VehicleMasterGet)", "Error_014", ex, "Transport");
            }

            return View(model);
        }
        public ActionResult AddVehicle(string VehicleID = "", string ActionName = "")
        {
            VehicleDetails model = new VehicleDetails();
            model.ActionName = Constants.BTN_CREATE;
            if (ActionName.ToUpper() == "EDIT" && !string.IsNullOrEmpty(VehicleID))
            {
                Vehicle _Vehicle = new Vehicle();
                _Vehicle.SchoolID = _OrgnisationID;
                _Vehicle.FinancialYearID = _Financialyearid;
                _Vehicle.Action = "GET";
                model = BALObj.GetVehicleList(_Vehicle).Where(x => x.VehicleID == Convert.ToInt32(VehicleID)).FirstOrDefault();
                model.ActionName = Constants.BTN_EDIT;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicle(VehicleDetails VehicleDetail, FormCollection collection)
        {
            try
            {
                VehicleDetail.SchoolID = _OrgnisationID;
                VehicleDetail.FinancialYearID = _Financialyearid;
                VehicleDetail.CreatedBy = WebSecurity.CurrentUserName;
                if (collection["btntype"].ToUpper() == "Save".ToUpper())
                {
                    VehicleDetail.Action = "INS";
                }
                else if (collection["btntype"].ToUpper() == "Update".ToUpper())
                {
                    VehicleDetail.Action = "UPD";
                }
                int res = BALObj.InsertUpdateVehicleDetails(VehicleDetail);
                if (res < 0)
                    TempData[Constants.MessageInfo.WARNING] = "Vehicle is already exists.";
                else if (res == 0)
                    TempData[Constants.MessageInfo.WARNING] = "Unable to " + collection["btntype"].ToLower();
                else
                    TempData[Constants.MessageInfo.SUCCESS] = res + ((res == 1) ? " record " : " records ") + collection["btntype"].ToLower() + " successfully.";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Transport(AddVehicle_Post)", "Error_014", ex, "Transport");
            }
            return RedirectToAction("VehicleMaster");
        }


        public ActionResult DriverMaster()
        {
            Driver model = new Driver();
            try
            {
                model.SchoolID = _OrgnisationID;
                model.FinancialYearID = _Financialyearid;
                model.Action = "GET";
                model.DriverDetailList = BALObj.GetDriverList(model);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Transport(VehicleMasterGet)", "Error_014", ex, "Transport");
            }

            return View(model);
        }
        public ActionResult AddDriver(string DriverID = "", string ActionName = "")
        {
            DriverDetails model = new DriverDetails();
            try
            {
                model.ActionName = Constants.BTN_CREATE;
                if (ActionName.ToUpper() == "EDIT" && !string.IsNullOrEmpty(DriverID))
                {
                    Driver _Driver = new Driver();
                    _Driver.SchoolID = _OrgnisationID;
                    _Driver.FinancialYearID = _Financialyearid;
                    _Driver.Action = "GET";
                    model = BALObj.GetDriverList(_Driver).Where(x => x.DriverID == Convert.ToInt32(DriverID)).FirstOrDefault();
                    model.ActionName = Constants.BTN_EDIT;
                }

                #region Get Vehicle List
                Vehicle Vehiclemodel = new Vehicle();
                Vehiclemodel.SchoolID = _OrgnisationID;
                Vehiclemodel.FinancialYearID = _Financialyearid;
                Vehiclemodel.Action = "GET";
                List<VehicleDetails> _lst = BALObj.GetVehicleList(Vehiclemodel);
                var VList = new List<SelectListItem>();
                foreach (var lst in _lst)
                {
                    VList.Add(new SelectListItem
                    {
                        Value = Convert.ToString(lst.VehicleID),
                        Text = lst.State + "-" + lst.Sequential + "-" + lst.RTO + "-" + lst.UniqueNo
                    });
                }
                TempData["VehicleList"] = VList;
                #endregion Get Vehicle List
            }
            catch { }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDriver(DriverDetails DriverDetail, FormCollection collection)
        {
            try
            {
                DriverDetail.SchoolID = _OrgnisationID;
                DriverDetail.FinancialYearID = _Financialyearid;
                DriverDetail.CreatedBy = WebSecurity.CurrentUserName;
                if (collection["btntype"].ToUpper() == "Save".ToUpper())
                {
                    DriverDetail.Action = "INS";
                }
                else if (collection["btntype"].ToUpper() == "Update".ToUpper())
                {
                    DriverDetail.Action = "UPD";
                }
                if (!Convert.ToBoolean(DriverDetail.IsEmployee))
                {
                    DriverDetail.EmployeeID = 0;
                }
                int res = BALObj.InsertUpdateDriverDetails(DriverDetail);
                if (res < 0)
                    TempData[Constants.MessageInfo.WARNING] = "Driver is already exists.";
                else if (res == 0)
                    TempData[Constants.MessageInfo.WARNING] = "Unable to " + collection["btntype"].ToLower();
                else
                    TempData[Constants.MessageInfo.SUCCESS] = res + ((res == 1) ? " record " : " records ") + collection["btntype"].ToLower() + " successfully.";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Transport(AddDriver_Post)", "Error_014", ex, "Transport");
            }
            return RedirectToAction("DriverMaster");
        }


        public ActionResult RouteMaster()
        {
            Route model = new Route();
            try
            {
                model.SchoolID = _OrgnisationID;
                model.FinancialYearID = _Financialyearid;
                model.Action = "GET";
                model.RouteDetailList = BALObj.GetRouteList(model);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Transport(VehicleMasterGet)", "Error_014", ex, "Transport");
            }
            return View(model);
        }
        public ActionResult AddRoute(string RouteID = "", string ActionName = "")
        {
            RouteDetails model = new RouteDetails();
            try
            {
                model.ActionName = Constants.BTN_CREATE;
                if (ActionName.ToUpper() == "EDIT" && !string.IsNullOrEmpty(RouteID))
                {
                    Route _Route = new Route();
                    _Route.SchoolID = _OrgnisationID;
                    _Route.FinancialYearID = _Financialyearid;
                    _Route.Action = "GET";
                    model = BALObj.GetRouteList(_Route).Where(x => x.RouteID == Convert.ToInt32(RouteID)).FirstOrDefault();
                    model.PickDropPointLst = BALObj.GetPickDropPointList(model);
                    model.ActionName = Constants.BTN_EDIT;
                }
                else
                {
                    List<PickDropPoint> _Lst = new List<PickDropPoint>();
                    _Lst.Add(new PickDropPoint());
                    model.PickDropPointLst = _Lst;
                }
                #region Get Drive List
                //Driver DriverModel = new Driver();
                //DriverModel.SchoolID = _OrgnisationID;
                //DriverModel.FinancialYearID = _Financialyearid;
                //DriverModel.Action = "GET";
                //var _Dlst = BALObj.GetDriverList(DriverModel);
                //var DList = new List<SelectListItem>();
                //foreach (var lst in _Dlst)
                //{
                //    DList.Add(new SelectListItem
                //    {
                //        Value = Convert.ToString(lst.DriverID),
                //        Text = Convert.ToString(lst.DriverID),
                //    });
                //}
                //TempData["ListDrivers"] = DList;
                #endregion Get Drive List
                #region Get Vehicle List
                Vehicle VehicleModel = new Vehicle();
                VehicleModel.SchoolID = _OrgnisationID;
                VehicleModel.FinancialYearID = _Financialyearid;
                VehicleModel.Action = "GET";
                List<VehicleDetails> _Vlst = BALObj.GetVehicleList(VehicleModel);
                var VList = new List<SelectListItem>();
                foreach (var lst in _Vlst)
                {
                    VList.Add(new SelectListItem
                    {
                        Value = Convert.ToString(lst.VehicleID),
                        Text = lst.State + "-" + lst.Sequential + "-" + lst.RTO + "-" + lst.UniqueNo
                    });
                }
                TempData["ListVehicles"] = VList;
                #endregion Get Vehicle List

            }
            catch { }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoute(RouteDetails RouteDetail, FormCollection collection)
        {
            int res = 0;
            try
            {
                RouteDetail.SchoolID = _OrgnisationID;
                RouteDetail.FinancialYearID = _Financialyearid;
                RouteDetail.CreatedBy = WebSecurity.CurrentUserName;
                RouteDetail.IsActive = "1";
                if (collection["btntype"].ToUpper() == "Save".ToUpper())
                {
                    RouteDetail.Action = "INS";
                    res = BALObj.InsertUpdateRouteDetails(RouteDetail);
                    if (res > 0)
                    {
                        RouteDetail.PickDropPointLst.ToList().ForEach(p =>
                        {
                            p.RouteID = res;
                            p.IsActive = "1";
                            p.SchoolID = _OrgnisationID;
                            p.FinancialYearID = _Financialyearid;
                            p.Action = "INS";
                            p.CreatedBy = WebSecurity.CurrentUserName;
                        });
                        BALObj.InsertUpdatePickDropPointDetails(RouteDetail.PickDropPointLst);
                    }
                }
                else if (collection["btntype"].ToUpper() == "Update".ToUpper())
                {
                    RouteDetail.Action = "UPD";
                    res = BALObj.InsertUpdateRouteDetails(RouteDetail);
                    if (res > 0)
                    {
                        RouteDetail.PickDropPointLst.ToList().ForEach(p =>
                        {
                            p.RouteID = RouteDetail.RouteID;
                            p.IsActive = "1";
                            p.SchoolID = _OrgnisationID;
                            p.FinancialYearID = _Financialyearid;
                            p.Action = p.PickDropID == 0 ? "INS" : "UPD";
                            p.CreatedBy = WebSecurity.CurrentUserName;
                        });
                        BALObj.InsertUpdatePickDropPointDetails(RouteDetail.PickDropPointLst);
                    }
                }
                if (res < 0)
                    TempData[Constants.MessageInfo.WARNING] = "Route is already exists.";
                else if (res == 0)
                    TempData[Constants.MessageInfo.WARNING] = "Unable to " + collection["btntype"].ToLower();
                else
                    TempData[Constants.MessageInfo.SUCCESS] = res + ((res == 1) ? " record " : " records ") + collection["btntype"].ToLower() + " successfully.";
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("Transport(AddDriver_Post)", "Error_014", ex, "Transport");
            }
            return RedirectToAction("RouteMaster");
        }


        public ActionResult AssignTransport()
        {
            AssignTransport obj = new AssignTransport();
            return View(obj);
        }

        public JsonResult GetClassList()
        {
            List<ClassMaster> obj = new List<ClassMaster>();
            BALCommon CSvc = new BALCommon(ConStr);

            try
            {
                obj = CSvc.GetClassList(_OrgnisationID);// userMasters.SchoolID);
            }
            catch { }

            return Json(obj, JsonRequestBehavior.AllowGet);
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
                obj = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, Convert.ToInt32(ClassID), Convert.ToInt32(SectionID),0, AdmissionNo);
            }
            catch { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRouteList()
        {
            Route model = new Route();
            List<RouteDetails> Obj = null;
            try
            {
                model.SchoolID = _OrgnisationID;
                model.FinancialYearID = _Financialyearid;
                model.Action = "GET";
                Obj = BALObj.GetRouteList(model);
            }
            catch
            {
                Obj = new List<RouteDetails>();
            }
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPickDropRouteList(string RouteID)
        {
            RouteDetails model = new RouteDetails();
            List<PickDropPoint> Obj = null;
            try
            {
                model.FinancialYearID = _Financialyearid;
                model.SchoolID = _OrgnisationID;
                model.RouteID = Convert.ToInt32(RouteID);
                Obj = BALObj.GetPickDropPointList(model);
            }
            catch
            {
                Obj = new List<PickDropPoint>();
            }
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAssignTransport(string TPCostID)
        {
            TransportDetails Obj = new TransportDetails();
            Obj.FinancialYearID = _Financialyearid;
            Obj.SchoolID = _OrgnisationID;
            Obj.TPCostID = Convert.ToInt32(TPCostID);
            Obj.Action = "GET";
            Obj = BALObj.GetAssignTransport(Obj);
            return Json(Obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult AssignTransport(TransportDetails transportDetail)
        {
            int res = 0;
            try
            {
                transportDetail.Action = transportDetail.TPCostID != 0 ? "UPD" : "INS";
                transportDetail.SchoolID = _OrgnisationID;
                transportDetail.FinancialYearID = _Financialyearid;

                res = BALObj.AssignTransport(transportDetail);

            }
            catch
            {
                res = 1;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [ValidateJsonAntiForgeryToken]
        public JsonResult GetVehicleDetails(string VehicleID)
        {
            DriverDetails _Dlst = null;
            try
            {
                Driver DriverModel = new Driver
                {
                    SchoolID = _OrgnisationID,
                    FinancialYearID = _Financialyearid,
                    Action = "GET"
                };
                _Dlst = BALObj.GetDriverList(DriverModel).Where(x => x.VehicleID == Convert.ToInt32(VehicleID)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _Dlst = new DriverDetails();
            }
            return Json(_Dlst, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AllocationReport()
        {
            AllocationReport Obj = new AllocationReport();
            return View(Obj);
        }
      

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult SearchAllocationRepost(string SearchBy, string ClassID, string SectionID, string AdmissionNo, string RouteID)
        {

            List<StudentMaster> objStudent = new List<StudentMaster>();
            List<Route> objRoute = new List<Route>();
            BALCommon CSvc = new BALCommon(ConStr);
            try
            {
                if (SearchBy == "Student")
                {
                    objStudent = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, Convert.ToInt32(ClassID), Convert.ToInt32(SectionID), 0, AdmissionNo).Where(x=>x.TPCostID > 0).ToList();
                }
                else if(SearchBy == "Route")
                {
                    objStudent = CSvc.GetStudentList(_OrgnisationID, _Financialyearid, 0, 0, Convert.ToInt32(RouteID), "").Where(x => x.TPCostID > 0).OrderBy(x => x.RouteName).ToList();
                }
                return Json(objStudent, JsonRequestBehavior.AllowGet);
            }
            catch { }
            return Json("No Record Found", JsonRequestBehavior.AllowGet);
        }
    }

}
