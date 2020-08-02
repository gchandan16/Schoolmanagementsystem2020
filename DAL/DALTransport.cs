using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED;
using ERROR;
namespace DAL
{
    public class DALTransport
    {
        private SqlDatabase DBHelper;
        private SqlCommand cmd = null;
        private string ConStr = "";
        public DALTransport(string connectionstring)
        {
            DBHelper = new SqlDatabase(connectionstring);
            ConStr = connectionstring;
        }

        public List<VehicleDetails> GetVehicleList(Vehicle model)
        {
            List<VehicleDetails> obj = new List<VehicleDetails>();
            try
            {
                cmd = new SqlCommand("USP_Vehicle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@VehicleID", (model.VehicleDetailList != null && model.VehicleDetailList.Count() > 0) ? model.VehicleDetailList[0].VehicleID : 0);
                cmd.Parameters.AddWithValue("@Action", model.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new VehicleDetails
                    {
                        AllowedSeats = Convert.ToInt32(dr["AllowedSeats"]),
                        CCTV = Convert.ToString(dr["CCTV"]),
                        GPS = Convert.ToString(dr["GPS"]),
                        InsuranceExpiry = Convert.ToString(dr["InsuranceExpiry"]),
                        IsActive = Convert.ToString(dr["IsActive"]),
                        OwnershipType = Convert.ToString(dr["OwnershipType"]),
                        PollutionExpiry = Convert.ToString(dr["PollutionExpiry"]),
                        TotalSeats = Convert.ToInt32(dr["TotalSeats"]),
                        VehicleSpecification = Convert.ToString(dr["VehicleSpecification"]),
                        VehicleID = Convert.ToInt32(dr["VehicleID"]),
                        State = Convert.ToString(dr["State"]),
                        Sequential = Convert.ToString(dr["Sequential"]),
                        RTO = Convert.ToString(dr["RTO"]),
                        UniqueNo = Convert.ToString(dr["UniqueNo"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(GetVehicleList)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int InsertUpdateVehicleDetails(VehicleDetails model)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_Vehicle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@VehicleID", model.VehicleID);
                cmd.Parameters.AddWithValue("@State", model.State.ToUpper());
                cmd.Parameters.AddWithValue("@Sequential", model.Sequential.ToUpper());
                cmd.Parameters.AddWithValue("@RTO", model.RTO.ToUpper());
                cmd.Parameters.AddWithValue("@UniqueNo", model.UniqueNo.ToUpper());
                cmd.Parameters.AddWithValue("@TotalSeats", model.TotalSeats);
                cmd.Parameters.AddWithValue("@AllowedSeats", model.AllowedSeats);
                cmd.Parameters.AddWithValue("@OwnershipType", Convert.ToBoolean(model.OwnershipType));
                cmd.Parameters.AddWithValue("@InsuranceExpiry", model.InsuranceExpiry);
                cmd.Parameters.AddWithValue("@PollutionExpiry", model.PollutionExpiry);
                cmd.Parameters.AddWithValue("@GPS", Convert.ToBoolean(model.GPS));
                cmd.Parameters.AddWithValue("@CCTV", Convert.ToBoolean(model.CCTV));
                cmd.Parameters.AddWithValue("@VehicleSpecification", model.VehicleSpecification);
                cmd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(model.IsActive));
                cmd.Parameters.AddWithValue("@Action", model.Action);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);

                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(UpdateVehicleDetails)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }


        public List<DriverDetails> GetDriverList(Driver model)
        {
            List<DriverDetails> obj = new List<DriverDetails>();
            #region GEt All Vehicle
            Vehicle VehicleModel = new Vehicle();
            VehicleModel.SchoolID = model.SchoolID;
            VehicleModel.FinancialYearID = model.FinancialYearID;
            VehicleModel.Action = "GET";
            List<VehicleDetails> VehicleObj = new List<VehicleDetails>();
            VehicleObj = GetVehicleList(VehicleModel);
            #endregion Get All Vehicle


            DALFee objDALFee = new DALFee(ConStr);
            List<EmployeeMaster> EmployeeList = objDALFee.GetEmployeeList(1, model.SchoolID);
            try
            {
                cmd = new SqlCommand("USP_Driver");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@VehicleID", (model.DriverDetailList != null && model.DriverDetailList.Count() > 0) ? model.DriverDetailList[0].DriverID : 0);
                cmd.Parameters.AddWithValue("@Action", model.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new DriverDetails
                    {
                        DriverID = Convert.ToInt32(dr["DriverID"]),
                        Name = Convert.ToString(dr["Name"]),
                        DOB = Convert.ToString(dr["DOB"]),
                        PresentAddress = Convert.ToString(dr["PresentAddress"]),
                        PermanentAddress = Convert.ToString(dr["PermanentAddress"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                        IsEmployee = Convert.ToString(dr["IsEmployee"]),
                        LicenseNo = Convert.ToString(dr["LicenseNo"]),
                        SchoolID = Convert.ToInt32(dr["SchoolID"]),
                        FinancialYearID = Convert.ToInt32(dr["FinancialYearID"]),
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                        VehicleID = Convert.ToInt32(dr["VehicleID"]),
                        IsActive=Convert.ToString(dr["IsActive"]),
                        VehicleDetail = VehicleObj.Where(x => x.VehicleID == Convert.ToInt32(dr["VehicleID"])).FirstOrDefault(),
                        EmployeeDetail = EmployeeList.Where(x => x.EMP_ID == Convert.ToInt32(dr["EmployeeID"])).FirstOrDefault()
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(GetVehicleList)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int InsertUpdateDriverDetails(DriverDetails model)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_Driver");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@DriverID", model.DriverID);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@DOB", model.DOB);
                cmd.Parameters.AddWithValue("@PresentAddress", model.PresentAddress);
                cmd.Parameters.AddWithValue("@PermanentAddress", model.PermanentAddress);
                cmd.Parameters.AddWithValue("@Phone", model.Phone);
                cmd.Parameters.AddWithValue("@LicenseNo", model.LicenseNo);
                cmd.Parameters.AddWithValue("@IsEmployee", Convert.ToBoolean(model.IsEmployee));
                cmd.Parameters.AddWithValue("@EmployeeID", model.EmployeeID);
                cmd.Parameters.AddWithValue("@VehicleID", model.VehicleID);
                cmd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(model.IsActive));
                cmd.Parameters.AddWithValue("@Action", model.Action);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);

                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(InsertUpdateDriverDetails)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }


        public List<RouteDetails> GetRouteList(Route model)
        {
            List<RouteDetails> obj = new List<RouteDetails>();
            #region GEt All Vehicle
            Vehicle VehicleModel = new Vehicle();
            VehicleModel.SchoolID = model.SchoolID;
            VehicleModel.FinancialYearID = model.FinancialYearID;
            VehicleModel.Action = "GET";
            List<VehicleDetails> VehicleObj = new List<VehicleDetails>();
            VehicleObj = GetVehicleList(VehicleModel);
            #endregion Get All Vehicle


            DALFee objDALFee = new DALFee(ConStr);
            List<EmployeeMaster> EmployeeList = objDALFee.GetEmployeeList(1, model.SchoolID);
            try
            {
                cmd = new SqlCommand("USP_Route");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@VehicleID", (model.RouteDetailList != null && model.RouteDetailList.Count() > 0) ? model.RouteDetailList[0].RouteID : 0);
                cmd.Parameters.AddWithValue("@Action", model.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new RouteDetails
                    {
                        RouteID = Convert.ToInt32(dr["RouteID"]),
                        RouteName = Convert.ToString(dr["RouteName"]),
                        VehicleID = Convert.ToInt32(dr["VehicleID"]),
                        SchoolID = Convert.ToInt32(dr["SchoolID"]),
                        FinancialYearID = Convert.ToInt32(dr["FinancialYearID"]),
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                        IsActive = Convert.ToString(dr["IsActive"]),
                        VehicleDetail = VehicleObj.Where(x => x.VehicleID == Convert.ToInt32(dr["VehicleID"])).FirstOrDefault(),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(GetVehicleList)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int InsertUpdateRouteDetails(RouteDetails model)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_Route");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@RouteID", model.RouteID);
                cmd.Parameters.AddWithValue("@RouteName", model.RouteName);
                cmd.Parameters.AddWithValue("@VehicleID", model.VehicleID);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@Action", model.Action);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(InsertUpdateDriverDetails)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public List<PickDropPoint> GetPickDropPointList(RouteDetails model)
        {
            List<PickDropPoint> obj = new List<PickDropPoint>();

            try
            {
                cmd = new SqlCommand("USP_PickDropPoint");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@RouteID", model.RouteID);
                cmd.Parameters.AddWithValue("@Action", "GET");
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new PickDropPoint
                    {
                        RouteID = Convert.ToInt32(dr["RouteID"]),
                        PickDropID = Convert.ToInt32(dr["PickDropID"]),
                        PickDropPointName = Convert.ToString(dr["PickDropPointName"]),
                        PickupTime = Convert.ToString(dr["PickupTime"]),
                        DropTime = Convert.ToString(dr["DropTime"]),
                        Fee = Convert.ToDouble(dr["Fee"]),
                        SchoolID = Convert.ToInt32(dr["SchoolID"]),
                        FinancialYearID = Convert.ToInt32(dr["FinancialYearID"]),
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(GetVehicleList)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int InsertUpdatePickDropPointDetails(List<PickDropPoint> Points)
        {
            int result = 0;
            try
            {
                foreach (var model in Points)
                {
                    cmd = new SqlCommand("USP_PickDropPoint");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                    cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                    cmd.Parameters.AddWithValue("@PickDropID", model.PickDropID);
                    cmd.Parameters.AddWithValue("@PickDropPointName", model.PickDropPointName);
                    cmd.Parameters.AddWithValue("@PickupTime", model.PickupTime);
                    cmd.Parameters.AddWithValue("@DropTime", model.DropTime);
                    cmd.Parameters.AddWithValue("@Fee", model.Fee);
                    cmd.Parameters.AddWithValue("@RouteID", model.RouteID);
                    cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                    cmd.Parameters.AddWithValue("@Action", model.Action);
                    cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                    result += Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(InsertUpdateDriverDetails)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public int AssignTransport(TransportDetails transportDetail)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_AssignTransport");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TPCostID", transportDetail.TPCostID);
                cmd.Parameters.AddWithValue("@SchoolID", transportDetail.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", transportDetail.FinancialYearID);
                cmd.Parameters.AddWithValue("@StudentID", transportDetail.StudentID);
                cmd.Parameters.AddWithValue("@RouteID", transportDetail.RouteID);
                cmd.Parameters.AddWithValue("@PickDropPointID", transportDetail.PickDropPointID);
                cmd.Parameters.AddWithValue("@Fee", transportDetail.Fee);
                cmd.Parameters.AddWithValue("@Apr", transportDetail.Apr);
                cmd.Parameters.AddWithValue("@May", transportDetail.May);
                cmd.Parameters.AddWithValue("@Jun", transportDetail.Jun);
                cmd.Parameters.AddWithValue("@Jul", transportDetail.Jul);
                cmd.Parameters.AddWithValue("@Aug", transportDetail.Aug);
                cmd.Parameters.AddWithValue("@Sep", transportDetail.Sep);
                cmd.Parameters.AddWithValue("@Oct", transportDetail.Oct);
                cmd.Parameters.AddWithValue("@Nov", transportDetail.Nov);
                cmd.Parameters.AddWithValue("@Dec", transportDetail.Dec);
                cmd.Parameters.AddWithValue("@Jan", transportDetail.Jan);
                cmd.Parameters.AddWithValue("@Feb", transportDetail.Feb);
                cmd.Parameters.AddWithValue("@Mar", transportDetail.Mar);
                cmd.Parameters.AddWithValue("@Action", transportDetail.Action);
                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(AssignTransport)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public TransportDetails GetAssignTransport(TransportDetails model)
        {
            TransportDetails obj = new TransportDetails();

            try
            {
                cmd = new SqlCommand("USP_AssignTransport");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@TPCostID", model.TPCostID);
                cmd.Parameters.AddWithValue("@Action", model.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.TPCostID = Convert.ToInt32(dr["TPId"]);
                    obj.FinancialYearID = Convert.ToInt32(dr["FinancialYearID"]);
                    obj.RouteID = Convert.ToInt32(dr["RouteID"]);
                    obj.PickDropPointID = Convert.ToInt32(dr["PickDropPointId"]);
                    obj.Fee = Convert.ToDecimal(dr["Fee"]);
                    obj.Apr = Convert.ToDecimal(dr["Apr"]);
                    obj.May = Convert.ToDecimal(dr["May"]);
                    obj.Jun = Convert.ToDecimal(dr["Jun"]);
                    obj.Jul = Convert.ToDecimal(dr["Jul"]);
                    obj.Aug = Convert.ToDecimal(dr["Aug"]);
                    obj.Sep = Convert.ToDecimal(dr["Sep"]);
                    obj.Oct = Convert.ToDecimal(dr["Oct"]);
                    obj.Nov = Convert.ToDecimal(dr["Nov"]);
                    obj.Dec = Convert.ToDecimal(dr["Dec"]);
                    obj.Jan = Convert.ToDecimal(dr["Jan"]);
                    obj.Feb = Convert.ToDecimal(dr["Feb"]);
                    obj.Mar = Convert.ToDecimal(dr["Mar"]);
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALTransport(GetVehicleList)", "Error_014", ex, "DALTransport");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
    }
}
