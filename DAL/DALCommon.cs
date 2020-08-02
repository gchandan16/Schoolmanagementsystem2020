using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED;
using System.Data.SqlClient;
using System.Data;
using ERROR;

namespace DAL
{
    public class DALCommon
    {
        private SqlDatabase DBHelper;
        private SqlCommand cmd = null;
        DataSet ds = null;
        public DALCommon(string connectionstring)
        {
            DBHelper = new SqlDatabase(connectionstring);
        }

        public DataSet GetCommonDashboardData(DashboardMaster obj)
        {
            DataSet ds = new DataSet();
            return ds;
        }

        public List<MenuMaster> GetAllMenuByStatus(int status, int SchoolID)
        {
            List<MenuMaster> obj = new List<MenuMaster>();
            ds = new DataSet();
            try
            {
                //  cmd = new SqlCommand("usp_MENU_GetAllMenuListByUserId");
                cmd = new SqlCommand("USP_MENU_GETALLMENULISTBYROLEBYSTATUS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISACTIVE", status);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new MenuMaster
                        {
                            CREATEDBY = Convert.ToString(ds.Tables[0].Rows[l]["CreatedBy"]),
                            CREATEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["CreatedDate"]),
                            DISPLAYORDER = Convert.ToInt32(ds.Tables[0].Rows[l]["DisplayOrder"]),
                            IMAGEPATH = Convert.ToString(ds.Tables[0].Rows[l]["ImagePath"]),
                            ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            //ISDISPLAY = Convert.ToString(ds.Tables[0].Rows[l][""]),
                            MENUCODE = Convert.ToString(ds.Tables[0].Rows[l]["MenuCode"]),
                            MENUNAME = Convert.ToString(ds.Tables[0].Rows[l]["MenuName"]),
                            MENU_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["Menu_Id"]),
                            PAGETITLE = Convert.ToString(ds.Tables[0].Rows[l]["PageTitle"]),
                            PARENTMENUID = Convert.ToInt32(ds.Tables[0].Rows[l]["ParentMenuID"]),
                            PermissionList = GetPermissionByMenuid(Convert.ToInt32(ds.Tables[0].Rows[l]["Menu_Id"]), SchoolID),
                            PermissionNameList = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[l]["Permissions"])) ? Convert.ToString(ds.Tables[0].Rows[l]["Permissions"]).Split(',').ToList() : null,
                            //Permissions = Convert.ToString(ds.Tables[0].Rows[l][""]),
                            URL = Convert.ToString(ds.Tables[0].Rows[l]["URL"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAllMenuByStatus)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<MenuMaster> GetMenuListByUserId(int userId)
        {
            List<MenuMaster> obj = new List<MenuMaster>();
            return obj;
        }
        public List<MenuMaster> GetAllMenuListByUserId(int userId, int SchoolID)
        {
            List<MenuMaster> obj = new List<MenuMaster>();
            ds = new DataSet();
            try
            {
                //  cmd = new SqlCommand("usp_MENU_GetAllMenuListByUserId");
                cmd = new SqlCommand("USP_MENU_GETALLMENULISTBYROLEBYUSERID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new MenuMaster
                        {
                            CREATEDBY = Convert.ToString(ds.Tables[0].Rows[l]["CreatedBy"]),
                            CREATEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["CreatedDate"]),
                            DISPLAYORDER = Convert.ToInt32(ds.Tables[0].Rows[l]["DisplayOrder"]),
                            IMAGEPATH = Convert.ToString(ds.Tables[0].Rows[l]["ImagePath"]),
                            ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            icon = Convert.ToString(ds.Tables[0].Rows[l]["MenuIcon"]),
                            MENUCODE = Convert.ToString(ds.Tables[0].Rows[l]["MenuCode"]),
                            MENUNAME = Convert.ToString(ds.Tables[0].Rows[l]["MenuName"]),
                            MENU_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["Menu_Id"]),
                            PAGETITLE = Convert.ToString(ds.Tables[0].Rows[l]["PageTitle"]),
                            PARENTMENUID = Convert.ToInt32(ds.Tables[0].Rows[l]["ParentMenuID"]),
                            //  PermissionList = Convert.ToString(ds.Tables[0].Rows[l]["Permissions"]),
                            PermissionNameList = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[l]["Permissions"])) ? Convert.ToString(ds.Tables[0].Rows[l]["Permissions"]).Split(',').ToList() : null,
                            //Permissions = Convert.ToString(ds.Tables[0].Rows[l][""]),
                            URL = Convert.ToString(ds.Tables[0].Rows[l]["URL"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAllMenuListByUserId)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<PermissionMaster> GetPermissionByMenuid(int menuId, int SchoolID)
        {
            List<PermissionMaster> _permissionlst = new List<PermissionMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_MENU_GETMENUPERMISSIONLISTBYMENUID");
            try
            {
                //  cmd = new SqlCommand("usp_MENU_GetAllMenuListByUserId");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@menuId", menuId);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        _permissionlst.Add(new PermissionMaster
                        {
                            PERMISSION_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["PERMISSION_ID"]),
                            PERMISSIONNAME = Convert.ToString(ds.Tables[0].Rows[l]["PERMISSIONNAME"]),
                            PERMISSIONDESC = Convert.ToString(ds.Tables[0].Rows[l]["PERMISSIONDESC"]),
                            ISACTIVE = Convert.ToInt32(ds.Tables[0].Rows[l]["ISACTIVE"]),
                            CREATEDBY = Convert.ToString(ds.Tables[0].Rows[l]["CREATEDBY"]),
                            CREATEDON = Convert.ToDateTime(ds.Tables[0].Rows[l]["CREATEDON"]),
                            MODIFIEDBY = Convert.ToString(ds.Tables[0].Rows[l]["MODIFIEDBY"]),
                            MODIFIEDON = Convert.ToDateTime(ds.Tables[0].Rows[l]["MODIFIEDON"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetPermissionByMenuid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _permissionlst;
        }

        public List<UserMasters> GetAllUser(int SchoolID)
        {
            List<UserMasters> obj = new List<UserMasters>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_UserList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new UserMasters
                        {
                            UserId = Convert.ToInt32(ds.Tables[0].Rows[l]["UserId"]),
                            USERNAME = Convert.ToString(ds.Tables[0].Rows[l]["UserName"]),
                            FISRTNAME = Convert.ToString(ds.Tables[0].Rows[l]["Name"]),
                            Mobile = Convert.ToString(ds.Tables[0].Rows[l]["Mobile"]),
                            Usertypename = Convert.ToString(ds.Tables[0].Rows[l]["UName"]),
                            EMAILID = Convert.ToString(ds.Tables[0].Rows[l]["EmailId"]),
                            ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[l]["ISACTIVE"]),
                            //    MODIFIEDBY = Convert.ToString(ds.Tables[0].Rows[l]["MODIFIEDBY"]),
                            //    MODIFIEDON = Convert.ToDateTime(ds.Tables[0].Rows[l]["MODIFIEDON"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetPermissionByMenuid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public void AddUser(UserMasters UserMasters)
        {

        }
        public int UpdateUser(UserMasters UserMasters)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet _ds = new DataSet();
                cmd.CommandText = "SP_UPDUserProfile";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserMasters.UserId);
                cmd.Parameters.AddWithValue("@RoleId", UserMasters.ROLE_ID);
                cmd.Parameters.AddWithValue("@SchoolID", UserMasters.SchoolID);
                cmd.Parameters.AddWithValue("@ISACTIVE", UserMasters.ISACTIVE);
                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(UpdateUser)", "Error_014", ex, "UpdateUser");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }

        public int DeleteUser(UserMasters UserMasters)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet _ds = new DataSet();
                cmd.CommandText = "SP_DELETEUserProfile";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserMasters.UserId);
                cmd.Parameters.AddWithValue("@ISACTIVE", UserMasters.ISACTIVE);
                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(DeleteUser)", "Error_014", ex, "AddRole");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }

        public bool IsUserNameAllow(string UserName, int userId)
        {
            return true;
        }

        public UserMasters GetByUserId(int userId)
        {
            UserMasters obj = new UserMasters();
            SqlCommand cmd = new SqlCommand("SP_UserProfile");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.UserId = Convert.ToInt32(dr["UserId"]);
                    obj.USERNAME = Convert.ToString(dr["UserName"]);
                    obj.FISRTNAME = Convert.ToString(dr["Name"]);
                    obj.LASTNAME = Convert.ToString(dr["Lastname"]);
                    obj.Mobile = Convert.ToString(dr["Mobile"]);
                    obj.EMAILID = Convert.ToString(dr["EmailId"]);
                    obj.Usertype = Convert.ToInt32(dr["UserType"]);
                    obj.MODIFIEDBY = Convert.ToString(dr["UpdatedBy"]);
                    obj.ADDRESS = Convert.ToString(dr["Address"]);
                    obj.ROLE_ID = Convert.ToInt32(dr["RoleId"]);
                    obj.CITY_ID = Convert.ToInt32(dr["CITY_ID"]);
                    obj.STATE_ID = Convert.ToInt32(dr["STATE_ID"]);
                    obj.COUNTRY_ID = Convert.ToInt32(dr["COUNTRY_ID"]);
                    obj.ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]);
                    obj.IMAGE = Convert.ToString(dr["IMAGE"]);
                    obj.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                    obj.UsertypebaseId = Convert.ToInt32(dr["UsertypebaseId"].ToString().Length == 0 ? "0" : dr["UsertypebaseId"].ToString());

                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetByUserId)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public UserInfo GetUserInfoByuserId(int userId, int SchoolID)
        {
            UserInfo obj = new UserInfo();
            SqlCommand cmd = new SqlCommand("usp_User_GetUserInfoByuserId");
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.FULLNAME = Convert.ToString(dr["FULLNAME"]);
                    obj.EMPIMGPATH = Convert.ToString(dr["EMPIMGPATH"]);
                    obj.BIRTHDATE = Convert.ToString(dr["BIRTHDATE"]);
                    obj.CityName = Convert.ToString(dr["CITYNAME"]);
                    obj.COUNTRYNAME = Convert.ToString(dr["DEPARTMENTNAME"]);
                    obj.CurrentDate = Convert.ToString(dr["CURRENTDATE"]);
                    obj.DesgnationName = Convert.ToString(dr["DESGNATIONNAME"]);
                    obj.EMAIL = Convert.ToString(dr["EMAIL"]);
                    obj.MOBILENO = Convert.ToString(dr["MOBILENO"]);
                    obj.StateName = Convert.ToString(dr["STATENAME"]);
                    obj.USERCODE = Convert.ToString(dr["USERNAME"]);
                    obj.USERNAME = Convert.ToString(dr["USERNAME"]);
                    obj.UserType_Name = Convert.ToString(dr["USERTYPE_NAME"]);
                    obj.User_ID = Convert.ToInt32(dr["USER_ID"]);
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetUserInfoByuserId)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public UserMasters ValidateUser(string UserName, string Password)
        {
            UserMasters obj = new UserMasters();
            return obj;
        }

        public void Insert(UserMasters usermaster)
        {

        }

        public void Update(UserMasters usermaster)
        {

        }
        public UserMasters GetUserProfile(string UserName)
        {
            UserMasters obj = new UserMasters();
            obj.ISACTIVE = false;
            ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetUserProfile");
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        obj.ADDRESS = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS"]);
                        obj.CITY_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["CITY_ID"]);
                        obj.COUNTRY_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["COUNTRY_ID"]);
                        obj.CREATEDBY = Convert.ToString(ds.Tables[0].Rows[0]["UpdatedBy"]);
                        obj.EMAILID = Convert.ToString(ds.Tables[0].Rows[0]["EmailId"]);
                        obj.FISRTNAME = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                        obj.IMAGE = Convert.ToString(ds.Tables[0].Rows[0]["IMAGE"]);

                        obj.LASTNAME = Convert.ToString(ds.Tables[0].Rows[0]["Lastname"]);
                        // //obj.MenuPermissionList = !string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]
                        obj.ROLE_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleId"]);
                        obj.STATE_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["STATE_ID"]);
                        obj.USERNAME = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                        obj.UserId = Convert.ToInt32(ds.Tables[0].Rows[0]["UserId"]);
                        obj.ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISACTIVE"]);
                        obj.SchoolID = Convert.ToInt32(ds.Tables[0].Rows[0]["SchoolID"]);
                        obj.OTP = Convert.ToString(ds.Tables[0].Rows[0]["OTP"]);
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetUserProfile)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }


        #region RoleManagement
        public List<RoleMaster> GetRoleList(int SchoolID)
        {
            List<RoleMaster> _obj = new List<RoleMaster>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("usp_RoleMaster_GetAll");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        _obj.Add(new RoleMaster
                        {
                            ROLE_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["Role_id"]),
                            ROLENAME = Convert.ToString(ds.Tables[0].Rows[l]["Rolename"]),
                            ROLEDESCRIPTION = Convert.ToString(ds.Tables[0].Rows[l]["ROLEDESCRIPTION"]),
                            ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[l]["ISACTIVE"]),
                            CREATEDBY = Convert.ToString(ds.Tables[0].Rows[l]["CREATEDBY"]),
                            CREATEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["CREATEDDATE"]),
                            MenuName = Convert.ToString(ds.Tables[0].Rows[l]["MenuName"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetRoleList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _obj;
        }
        #endregion

        #region Country-State-City
        public List<CountryMaster> GetCountryList(int SchoolID)
        {
            List<CountryMaster> _lst = null;
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_CountryMster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                _lst = new List<CountryMaster>();

                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new CountryMaster
                    {
                        COUNTRY_ID = DBNull.Value != reader["COUNTRY_ID"] ? (int)reader["COUNTRY_ID"] : default(int),
                        COUNTRYNAME = DBNull.Value != reader["COUNTRYNAME"] ? (string)reader["COUNTRYNAME"] : default(string),
                        COUNTRYDESC = DBNull.Value != reader["COUNTRYDESC"] ? (string)reader["COUNTRYDESC"] : default(string),
                        ISACTIVE = DBNull.Value != reader["ISACTIVE"] ? (bool)reader["ISACTIVE"] : default(bool),
                        CREATEDBY = DBNull.Value != reader["CREATEDBY"] ? (string)reader["CREATEDBY"] : default(string),
                        CREATEDDATE = DBNull.Value != reader["CREATEDDATE"] ? (DateTime)reader["CREATEDDATE"] : default(DateTime),
                        MODIFIEDBY = DBNull.Value != reader["MODIFIEDBY"] ? (string)reader["MODIFIEDBY"] : default(string),
                        MODIFIEDDATE = DBNull.Value != reader["MODIFIEDDATE"] ? (DateTime)reader["MODIFIEDDATE"] : default(DateTime),
                        COUNTRYCODE = DBNull.Value != reader["COUNTRYCODE"] ? (string)reader["COUNTRYCODE"] : default(string),
                    });

                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetCountryList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }

        public List<StateMaster> GetStateList(int? COUNTRY_ID, int SchoolID)
        {
            List<StateMaster> _lst = new List<StateMaster>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_StateMster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@COUNTRY_ID", COUNTRY_ID);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new StateMaster
                    {
                        STATE_ID = DBNull.Value != reader["STATE_ID"] ? (int)reader["STATE_ID"] : default(int),
                        STATECODE = DBNull.Value != reader["STATECODE"] ? (string)reader["STATECODE"] : default(string),
                        STATENAME = DBNull.Value != reader["STATENAME"] ? (string)reader["STATENAME"] : default(string),
                        STATEDESC = DBNull.Value != reader["STATEDESC"] ? (string)reader["STATEDESC"] : default(string),
                        ISACTIVE = DBNull.Value != reader["ISACTIVE"] ? (bool)reader["ISACTIVE"] : default(bool),
                        COUNTRY_ID = DBNull.Value != reader["COUNTRY_ID"] ? (int)reader["COUNTRY_ID"] : default(int),
                        CREATEDBY = DBNull.Value != reader["CREATEDBY"] ? (string)reader["CREATEDBY"] : default(string),
                        CREATEDDATE = DBNull.Value != reader["CREATEDDATE"] ? (DateTime)reader["CREATEDDATE"] : default(DateTime),
                        MODIFIEDBY = DBNull.Value != reader["MODIFIEDBY"] ? (string)reader["MODIFIEDBY"] : default(string),
                        MODIFIEDDATE = DBNull.Value != reader["MODIFIEDDATE"] ? (DateTime)reader["MODIFIEDDATE"] : default(DateTime),

                    });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStateList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }

        public List<CityMaster> GetCityList(int? STATE_ID, int SchoolID)
        {
            List<CityMaster> _lst = new List<CityMaster>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_CityMster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new CityMaster
                    {
                        CITY_ID = DBNull.Value != reader["CITY_ID"] ? (int)reader["CITY_ID"] : default(int),
                        CITYCODE = DBNull.Value != reader["CITYCODE"] ? (string)reader["CITYCODE"] : default(string),
                        CITYNAME = DBNull.Value != reader["CITYNAME"] ? (string)reader["CITYNAME"] : default(string),
                        CITYDESC = DBNull.Value != reader["CITYDESC"] ? (string)reader["CITYDESC"] : default(string),
                        STATE_ID = DBNull.Value != reader["STATE_ID"] ? (int)reader["STATE_ID"] : default(int),
                        ISACTIVE = DBNull.Value != reader["ISACTIVE"] ? (bool)reader["ISACTIVE"] : default(bool),
                        CREATEDBY = DBNull.Value != reader["CREATEDBY"] ? (string)reader["CREATEDBY"] : default(string),
                        CREATEDDATE = DBNull.Value != reader["CREATEDDATE"] ? (DateTime)reader["CREATEDDATE"] : default(DateTime),
                        MODIFIEDBY = DBNull.Value != reader["MODIFIEDBY"] ? (string)reader["MODIFIEDBY"] : default(string),
                        MODIFIEDDATE = DBNull.Value != reader["MODIFIEDDATE"] ? (DateTime)reader["MODIFIEDDATE"] : default(DateTime),

                    });

                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetCityList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }
        #endregion

        #region OrgnisationOpration
        public OragnisationMaster GetOragnisationAlready(string LEmailId)
        {
            OragnisationMaster _lst = new OragnisationMaster();
            try
            {
                cmd = new SqlCommand("SP_Orgnisationmexist");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LEmailId", LEmailId);
                var reader = DBHelper.ExecuteReader(cmd);
                {
                    while (reader.Read())

                        _lst.OMID = DBNull.Value != reader["OMID"] ? (int)reader["OMID"] : default(int);
                    _lst.Oname = DBNull.Value != reader["Oname"] ? (string)reader["Oname"] : default(string);
                    _lst.BOAddress = DBNull.Value != reader["BOAddress"] ? (string)reader["BOAddress"] : default(string);
                    _lst.BOAddress2 = DBNull.Value != reader["BOAddress2"] ? (string)reader["BOAddress2"] : default(string);
                    _lst.BOCity = DBNull.Value != reader["BOCity"] ? (int)reader["BOCity"] : default(int);
                    _lst.BOPincode = DBNull.Value != reader["BOPincode"] ? (string)reader["BOPincode"] : default(string);
                    _lst.LCountry = DBNull.Value != reader["LCountry"] ? (int)reader["LCountry"] : default(int);
                    _lst.LState = DBNull.Value != reader["LState"] ? (int)reader["LState"] : default(int);
                    _lst.LDistict = DBNull.Value != reader["LDistict"] ? (string)reader["LDistict"] : default(string);
                    _lst.LArea = DBNull.Value != reader["LArea"] ? (string)reader["LArea"] : default(string);
                    _lst.LEmailId = DBNull.Value != reader["LEmailId"] ? (string)reader["LEmailId"] : default(string);
                    _lst.LMobile = DBNull.Value != reader["LMobile"] ? (string)reader["LMobile"] : default(string);
                    _lst.LPhone = DBNull.Value != reader["LPhone"] ? (string)reader["LPhone"] : default(string);
                    _lst.LWebsite = DBNull.Value != reader["LWebsite"] ? (string)reader["LWebsite"] : default(string);
                    _lst.OAfficilate = DBNull.Value != reader["OAfficilate"] ? (string)reader["OAfficilate"] : default(string);
                    _lst.OlicNo = DBNull.Value != reader["OlicNo"] ? (string)reader["OlicNo"] : default(string);
                    _lst.OTaxNo = DBNull.Value != reader["OTaxNo"] ? (string)reader["OTaxNo"] : default(string);
                    _lst.OPanNo = DBNull.Value != reader["OPanNo"] ? (string)reader["OPanNo"] : default(string);
                    _lst.OContactNo = DBNull.Value != reader["OContactNo"] ? (string)reader["OContactNo"] : default(string);
                    _lst.IsActive = DBNull.Value != reader["IsActive"] ? (bool)reader["IsActive"] : default(bool);
                    _lst.Createddate = DBNull.Value != reader["Createddate"] ? (DateTime)reader["Createddate"] : default(DateTime);
                    _lst.CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string);
                    _lst.Modifieddate = DBNull.Value != reader["Modifieddate"] ? (DateTime)reader["Modifieddate"] : default(DateTime);
                    _lst.ModifiedBy = DBNull.Value != reader["ModifiedBy"] ? (string)reader["ModifiedBy"] : default(string);


                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(OragnasitionBasicopation)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }


        public int OragnasitionBasicopation(OragnisationMaster _Omaster)
        {
            int rettype = -1;
            try
            {
                cmd = new SqlCommand("SP_Orgnisationmaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OMID", _Omaster.OMID);
                cmd.Parameters.AddWithValue("@Oname", _Omaster.Oname);
                cmd.Parameters.AddWithValue("@BOAddress", _Omaster.BOAddress);
                cmd.Parameters.AddWithValue("@BOAddress2", _Omaster.BOAddress2);
                cmd.Parameters.AddWithValue("@BOCity", _Omaster.BOCity);
                cmd.Parameters.AddWithValue("@BOPincode", _Omaster.BOPincode);
                cmd.Parameters.AddWithValue("@LCountry", _Omaster.LCountry);
                cmd.Parameters.AddWithValue("@LState", _Omaster.LState);
                cmd.Parameters.AddWithValue("@LDistict", _Omaster.LDistict);
                cmd.Parameters.AddWithValue("@LArea", _Omaster.LArea);
                cmd.Parameters.AddWithValue("@LEmailId", _Omaster.LEmailId);
                cmd.Parameters.AddWithValue("@LMobile", _Omaster.LMobile);
                cmd.Parameters.AddWithValue("@LPhone", _Omaster.LPhone);
                cmd.Parameters.AddWithValue("@LWebsite", _Omaster.LWebsite);
                cmd.Parameters.AddWithValue("@OAfficilate", _Omaster.OAfficilate);
                cmd.Parameters.AddWithValue("@OlicNo", _Omaster.OlicNo);
                cmd.Parameters.AddWithValue("@OTaxNo", _Omaster.OTaxNo);
                cmd.Parameters.AddWithValue("@OPanNo", _Omaster.OPanNo);
                cmd.Parameters.AddWithValue("@OContactNo", _Omaster.OContactNo);
                cmd.Parameters.AddWithValue("@IsActive", _Omaster.IsActive);
                cmd.Parameters.AddWithValue("@Createddate", _Omaster.Createddate);
                cmd.Parameters.AddWithValue("@CreatedBy", _Omaster.CreatedBy);
                cmd.Parameters.AddWithValue("@Modifieddate", _Omaster.Modifieddate);
                cmd.Parameters.AddWithValue("@ModifiedBy", _Omaster.ModifiedBy);
                cmd.Parameters.AddWithValue("@OrgImage", _Omaster.OrgImage);
                cmd.Parameters.AddWithValue("@Otype", _Omaster.Otype);
                //  rettype=   DBHelper.ExecuteNonQuery(cmd);
                var _result = DBHelper.ExecuteScalar(cmd);
                rettype = Convert.ToInt32(_result);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(OragnasitionBasicopation)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return rettype;
        }

        public int AddEmployee(EmployeeMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddEmployeemaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_ID", _master.EMP_ID);
                cmd.Parameters.AddWithValue("@EMPCODE", _master.EMPCODE);
                cmd.Parameters.AddWithValue("@ISACTIVE", _master.ISACTIVE);
                cmd.Parameters.AddWithValue("@DEPARTMENT_ID", _master.DEPARTMENT_ID);
                cmd.Parameters.AddWithValue("@DESIGNATION_ID", _master.DESIGNATION_ID);
                cmd.Parameters.AddWithValue("@CONTACTTITLE", _master.CONTACTTITLE);
                cmd.Parameters.AddWithValue("@FIRSTNAME", _master.FIRSTNAME);
                cmd.Parameters.AddWithValue("@MIDDLENAME", _master.MIDDLENAME);
                cmd.Parameters.AddWithValue("@LASTNAME", _master.LASTNAME);
                cmd.Parameters.AddWithValue("@MOBILENUMBER", _master.MOBILENUMBER);
                cmd.Parameters.AddWithValue("@EMAIL", _master.EMAIL);
                cmd.Parameters.AddWithValue("@BIRTHDATE", _master.BIRTHDATE);
                cmd.Parameters.AddWithValue("@GENDER", _master.GENDER);
                cmd.Parameters.AddWithValue("@BLOODGROUP", _master.BLOODGROUP);
                cmd.Parameters.AddWithValue("@MARITALSTATUS", _master.MARITALSTATUS);
                cmd.Parameters.AddWithValue("@QUALIFICATION1", _master.QUALIFICATION1);
                cmd.Parameters.AddWithValue("@QUALIFICATION2", _master.QUALIFICATION2);
                cmd.Parameters.AddWithValue("@JOININGDATE", _master.JOININGDATE);
                cmd.Parameters.AddWithValue("@CONFIRMATIONDATE", _master.CONFIRMATIONDATE);
                cmd.Parameters.AddWithValue("@LEAVINGDATE", _master.LEAVINGDATE);
                cmd.Parameters.AddWithValue("@SALARY", _master.SALARY);
                cmd.Parameters.AddWithValue("@BANKACNO", _master.BANKACNO);
                cmd.Parameters.AddWithValue("@BANKNAME", _master.BANKNAME);
                cmd.Parameters.AddWithValue("@BANKBRANCH", _master.BANKBRANCH);
                cmd.Parameters.AddWithValue("@PFNO", _master.PFNO);
                cmd.Parameters.AddWithValue("@PANNO", _master.PANNO);
                cmd.Parameters.AddWithValue("@REMARKS", _master.REMARKS);
                cmd.Parameters.AddWithValue("@CREATEDBY", _master.CREATEDBY);
                cmd.Parameters.AddWithValue("@CREATEDDATE", _master.CREATEDDATE);
                cmd.Parameters.AddWithValue("@MODIFIEDBY", _master.MODIFIEDBY);
                cmd.Parameters.AddWithValue("@MODIFIEDDATE", _master.MODIFIEDDATE);
                cmd.Parameters.AddWithValue("@FATHER_SPOUSE", _master.FATHER_SPOUSE);
                cmd.Parameters.AddWithValue("@PF_ESTD_CODE", _master.PF_ESTD_CODE);
                cmd.Parameters.AddWithValue("@PF_UAN", _master.PF_UAN);
                cmd.Parameters.AddWithValue("@VPF_CONTB_RATE", _master.VPF_CONTB_RATE);
                cmd.Parameters.AddWithValue("@IFSC_CODE", _master.IFSC_CODE);
                cmd.Parameters.AddWithValue("@LEAVE_PL_ENTITLED", _master.LEAVE_PL_ENTITLED);
                cmd.Parameters.AddWithValue("@LEAVE_CL_ENTITLED", _master.LEAVE_CL_ENTITLED);
                cmd.Parameters.AddWithValue("@LEAVE_SL_ENTITLED", _master.LEAVE_SL_ENTITLED);
                //  cmd.Parameters.AddWithValue("@TOTAL_LEAVES_ENTITLED", _master.TOTAL_LEAVES_ENTITLED);
                cmd.Parameters.AddWithValue("@GROSS_BASIC", _master.GROSS_BASIC);
                cmd.Parameters.AddWithValue("@GROSS_HRA", _master.GROSS_HRA);
                cmd.Parameters.AddWithValue("@GROSS_CONVEYANCE", _master.GROSS_CONVEYANCE);
                cmd.Parameters.AddWithValue("@GROSS_CHILDREN_EDUCATION", _master.GROSS_CHILDREN_EDUCATION);
                cmd.Parameters.AddWithValue("@GROSS_UNIFORM_MAINTENANCE", _master.GROSS_UNIFORM_MAINTENANCE);
                cmd.Parameters.AddWithValue("@GROSS_CAR_AMOUNT", _master.GROSS_CAR_AMOUNT);
                cmd.Parameters.AddWithValue("@GROSS_SPECIAL_ALLOWANCE", _master.GROSS_SPECIAL_ALLOWANCE);
                cmd.Parameters.AddWithValue("@GROSS_SALARY", _master.GROSS_SALARY);
                cmd.Parameters.AddWithValue("@EMERGENCY_CONT_PRS", _master.EMERGENCY_CONT_PRS);
                cmd.Parameters.AddWithValue("@EMERGENCY_CONT_NO", _master.EMERGENCY_CONT_NO);
                cmd.Parameters.AddWithValue("@REPORTING_MANAGER", _master.REPORTING_MANAGER);
                cmd.Parameters.AddWithValue("@PANIMGPATH", _master.PANIMGPATH);
                cmd.Parameters.AddWithValue("@EMPIMGPATH", _master.EMPIMGPATH);
                cmd.Parameters.AddWithValue("@SPOUSE", _master.SPOUSE);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@ParentID", _master.ParentID);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddEmployee)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddStudent(StudentMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_Addstudentmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Smid", _master.Smid);
                cmd.Parameters.AddWithValue("@Adminssionno", _master.Adminssionno);
                cmd.Parameters.AddWithValue("@Enquiryno", _master.Enquiryno);
                cmd.Parameters.AddWithValue("@Firstname", _master.Firstname);
                cmd.Parameters.AddWithValue("@CMid", _master.CMid);
                cmd.Parameters.AddWithValue("@SecMid", _master.SecMid);
                cmd.Parameters.AddWithValue("@RollNo", _master.RollNo);
                cmd.Parameters.AddWithValue("@RouteMid", _master.RouteMid);
                cmd.Parameters.AddWithValue("@BusMid", _master.BusMid);
                cmd.Parameters.AddWithValue("@Castmid", _master.Castmid);
                cmd.Parameters.AddWithValue("@Categmid", _master.Categmid);
                cmd.Parameters.AddWithValue("@HouseMid", _master.HouseMid);
                cmd.Parameters.AddWithValue("@Relmid", _master.Relmid);
                cmd.Parameters.AddWithValue("@GMid", _master.GMid);
                cmd.Parameters.AddWithValue("@CITY_ID", _master.CITY_ID);
                cmd.Parameters.AddWithValue("@STATE_ID", _master.STATE_ID);
                cmd.Parameters.AddWithValue("@COUNTRY_ID", _master.COUNTRY_ID);
                cmd.Parameters.AddWithValue("@Bd_address1", _master.Bd_address1);
                cmd.Parameters.AddWithValue("@Bd_address2", _master.Bd_address2);
                cmd.Parameters.AddWithValue("@Bd_City", _master.Bd_City);
                cmd.Parameters.AddWithValue("@Bd_contactno", _master.Bd_contactno);
                cmd.Parameters.AddWithValue("@Bd_dob", _master.Bd_dob);
                cmd.Parameters.AddWithValue("@Bd_fathername", _master.Bd_fathername);
                cmd.Parameters.AddWithValue("@Bd_fathermob", _master.Bd_fathermob);
                cmd.Parameters.AddWithValue("@Bd_qualification", _master.Bd_qualification);
                cmd.Parameters.AddWithValue("@Bd_fatheroccuption", _master.Bd_fatheroccuption);
                cmd.Parameters.AddWithValue("@Bd_fathdob", _master.Bd_fathdob);
                cmd.Parameters.AddWithValue("@Bd_mothername", _master.Bd_mothername);
                cmd.Parameters.AddWithValue("@Bd_mothermob", _master.Bd_mothermob);
                cmd.Parameters.AddWithValue("@Bd_motherqualification", _master.Bd_motherqualification);
                cmd.Parameters.AddWithValue("@Bd_Motheroccuption", _master.Bd_Motheroccuption);
                cmd.Parameters.AddWithValue("@Bd_Mothredob", _master.Bd_Mothredob);
                cmd.Parameters.AddWithValue("@Bd_dateofannversy", _master.Bd_dateofannversy);
                cmd.Parameters.AddWithValue("@Bd_Emailid", _master.Bd_Emailid);
                cmd.Parameters.AddWithValue("@Off_lastschool", _master.Off_lastschool);
                cmd.Parameters.AddWithValue("@Off_remarks", _master.Off_remarks);
                cmd.Parameters.AddWithValue("@Off_Examgiven", _master.Off_Examgiven);
                cmd.Parameters.AddWithValue("@Off_Year", _master.Off_Year);
                cmd.Parameters.AddWithValue("@Off_Status", _master.Off_Status);
                cmd.Parameters.AddWithValue("@Off_marks", _master.Off_marks);
                cmd.Parameters.AddWithValue("@Off_boardoruniversity", _master.Off_boardoruniversity);
                cmd.Parameters.AddWithValue("@Off_bloodgroup", _master.Off_bloodgroup);
                cmd.Parameters.AddWithValue("@VisionLeft", _master.VisionLeft);
                cmd.Parameters.AddWithValue("@Off_grno", _master.Off_grno);
                cmd.Parameters.AddWithValue("@Off_Visionright", _master.Off_Visionright);
                cmd.Parameters.AddWithValue("@Off_heightweight", _master.Off_heightweight);
                cmd.Parameters.AddWithValue("@Off_Dentailhygine", _master.Off_Dentailhygine);
                cmd.Parameters.AddWithValue("@Off_Hosalroomno", _master.Off_Hosalroomno);
                cmd.Parameters.AddWithValue("@Off_bedno", _master.Off_bedno);
                cmd.Parameters.AddWithValue("@Off_Scholarshipno", _master.Off_Scholarshipno);
                cmd.Parameters.AddWithValue("@Off_TC", _master.Off_TC);
                cmd.Parameters.AddWithValue("@Off_CC", _master.Off_CC);
                cmd.Parameters.AddWithValue("@Off_ReportC", _master.Off_ReportC);
                cmd.Parameters.AddWithValue("@Off_Dobcertificate", _master.Off_Dobcertificate);
                cmd.Parameters.AddWithValue("@Off_admissionno", _master.Off_admissionno);
                cmd.Parameters.AddWithValue("@Off_dateofadmission", _master.Off_dateofadmission);
                cmd.Parameters.AddWithValue("@Off_Ledgerbalance", _master.Off_Ledgerbalance);
                cmd.Parameters.AddWithValue("@Off_feesbalance", _master.Off_feesbalance);
                cmd.Parameters.AddWithValue("@Off_Comments", _master.Off_Comments);
                cmd.Parameters.AddWithValue("@Off_Aadharno", _master.Off_Aadharno);
                cmd.Parameters.AddWithValue("@Off_biometricno", _master.Off_biometricno);
                cmd.Parameters.AddWithValue("@Off_nationality", _master.Off_nationality);
                cmd.Parameters.AddWithValue("@Off_childuniqueno", _master.Off_childuniqueno);
                cmd.Parameters.AddWithValue("@Off_sessionno", _master.Off_sessionno);
                cmd.Parameters.AddWithValue("@Off_family", _master.Off_family);
                cmd.Parameters.AddWithValue("@Off_stausinschool", _master.Off_stausinschool);
                cmd.Parameters.AddWithValue("@Off_discontinuethedate", _master.Off_discontinuethedate);
                cmd.Parameters.AddWithValue("@studentimage", _master.studentimage);
                cmd.Parameters.AddWithValue("@motherimage", _master.motherimage);
                cmd.Parameters.AddWithValue("@fatherimage", _master.fatherimage);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@FinancialYear", _master.FinancialYear);
                cmd.Parameters.AddWithValue("@FeeGroup", _master.FeeGroup);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddStudent)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int UpdateStudentDetails(StudentMaster _master)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_UpdateStudentDetails");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Smid", _master.Smid);
                cmd.Parameters.AddWithValue("@Adminssionno", _master.Adminssionno);
                cmd.Parameters.AddWithValue("@Enquiryno", _master.Enquiryno);
                cmd.Parameters.AddWithValue("@Firstname", _master.Firstname);
                cmd.Parameters.AddWithValue("@CMid", _master.CMid);
                cmd.Parameters.AddWithValue("@SecMid", _master.SecMid);
                cmd.Parameters.AddWithValue("@RollNo", _master.RollNo);
                cmd.Parameters.AddWithValue("@RouteMid", _master.RouteMid);
                cmd.Parameters.AddWithValue("@BusMid", _master.BusMid);
                cmd.Parameters.AddWithValue("@Castmid", _master.Castmid);
                cmd.Parameters.AddWithValue("@Categmid", _master.Categmid);
                cmd.Parameters.AddWithValue("@HouseMid", _master.HouseMid);
                cmd.Parameters.AddWithValue("@Relmid", _master.Relmid);
                cmd.Parameters.AddWithValue("@GMid", _master.GMid);
                cmd.Parameters.AddWithValue("@CITY_ID", _master.CITY_ID);
                cmd.Parameters.AddWithValue("@STATE_ID", _master.STATE_ID);
                cmd.Parameters.AddWithValue("@COUNTRY_ID", _master.COUNTRY_ID);
                cmd.Parameters.AddWithValue("@Bd_address1", _master.Bd_address1);
                cmd.Parameters.AddWithValue("@Bd_address2", _master.Bd_address2);
                cmd.Parameters.AddWithValue("@Bd_City", _master.Bd_City);
                cmd.Parameters.AddWithValue("@Bd_contactno", _master.Bd_contactno);
                cmd.Parameters.AddWithValue("@Bd_dob", _master.Bd_dob);
                cmd.Parameters.AddWithValue("@Bd_fathername", _master.Bd_fathername);
                cmd.Parameters.AddWithValue("@Bd_fathermob", _master.Bd_fathermob);
                cmd.Parameters.AddWithValue("@Bd_qualification", _master.Bd_qualification);
                cmd.Parameters.AddWithValue("@Bd_fatheroccuption", _master.Bd_fatheroccuption);
                cmd.Parameters.AddWithValue("@Bd_fathdob", _master.Bd_fathdob);
                cmd.Parameters.AddWithValue("@Bd_mothername", _master.Bd_mothername);
                cmd.Parameters.AddWithValue("@Bd_mothermob", _master.Bd_mothermob);
                cmd.Parameters.AddWithValue("@Bd_motherqualification", _master.Bd_motherqualification);
                cmd.Parameters.AddWithValue("@Bd_Motheroccuption", _master.Bd_Motheroccuption);
                cmd.Parameters.AddWithValue("@Bd_Mothredob", _master.Bd_Mothredob);
                cmd.Parameters.AddWithValue("@Bd_dateofannversy", _master.Bd_dateofannversy);
                cmd.Parameters.AddWithValue("@Bd_Emailid", _master.Bd_Emailid);
                cmd.Parameters.AddWithValue("@Off_lastschool", _master.Off_lastschool);
                cmd.Parameters.AddWithValue("@Off_remarks", _master.Off_remarks);
                cmd.Parameters.AddWithValue("@Off_Examgiven", _master.Off_Examgiven);
                cmd.Parameters.AddWithValue("@Off_Year", _master.Off_Year);
                cmd.Parameters.AddWithValue("@Off_Status", _master.Off_Status);
                cmd.Parameters.AddWithValue("@Off_marks", _master.Off_marks);
                cmd.Parameters.AddWithValue("@Off_boardoruniversity", _master.Off_boardoruniversity);
                cmd.Parameters.AddWithValue("@Off_bloodgroup", _master.Off_bloodgroup);
                cmd.Parameters.AddWithValue("@VisionLeft", _master.VisionLeft);
                cmd.Parameters.AddWithValue("@Off_grno", _master.Off_grno);
                cmd.Parameters.AddWithValue("@Off_Visionright", _master.Off_Visionright);
                cmd.Parameters.AddWithValue("@Off_heightweight", _master.Off_heightweight);
                cmd.Parameters.AddWithValue("@Off_Dentailhygine", _master.Off_Dentailhygine);
                cmd.Parameters.AddWithValue("@Off_Hosalroomno", _master.Off_Hosalroomno);
                cmd.Parameters.AddWithValue("@Off_bedno", _master.Off_bedno);
                cmd.Parameters.AddWithValue("@Off_Scholarshipno", _master.Off_Scholarshipno);
                cmd.Parameters.AddWithValue("@Off_TC", _master.Off_TC);
                cmd.Parameters.AddWithValue("@Off_CC", _master.Off_CC);
                cmd.Parameters.AddWithValue("@Off_ReportC", _master.Off_ReportC);
                cmd.Parameters.AddWithValue("@Off_Dobcertificate", _master.Off_Dobcertificate);
                cmd.Parameters.AddWithValue("@Off_admissionno", _master.Off_admissionno);
                cmd.Parameters.AddWithValue("@Off_dateofadmission", _master.Off_dateofadmission);
                cmd.Parameters.AddWithValue("@Off_Ledgerbalance", _master.Off_Ledgerbalance);
                cmd.Parameters.AddWithValue("@Off_feesbalance", _master.Off_feesbalance);
                cmd.Parameters.AddWithValue("@Off_Comments", _master.Off_Comments);
                cmd.Parameters.AddWithValue("@Off_Aadharno", _master.Off_Aadharno);
                cmd.Parameters.AddWithValue("@Off_biometricno", _master.Off_biometricno);
                cmd.Parameters.AddWithValue("@Off_nationality", _master.Off_nationality);
                cmd.Parameters.AddWithValue("@Off_childuniqueno", _master.Off_childuniqueno);
                cmd.Parameters.AddWithValue("@Off_sessionno", _master.Off_sessionno);
                cmd.Parameters.AddWithValue("@Off_family", _master.Off_family);
                cmd.Parameters.AddWithValue("@Off_stausinschool", _master.Off_stausinschool);
                cmd.Parameters.AddWithValue("@Off_discontinuethedate", _master.Off_discontinuethedate);
                cmd.Parameters.AddWithValue("@studentimage", _master.studentimage);
                cmd.Parameters.AddWithValue("@motherimage", _master.motherimage);
                cmd.Parameters.AddWithValue("@fatherimage", _master.fatherimage);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@FinancialYear", _master.FinancialYear);
                cmd.Parameters.AddWithValue("@FeeGroup", _master.FeeGroup);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(UpdateStudentDetails)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int Firstuserconfigure(int SchoolID)
        {

            SqlCommand cmd = new SqlCommand("SP_FirstuserConfigure");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                var dr = DBHelper.ExecuteScalar(cmd);
                return Convert.ToInt32(dr);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Firstuserconfigure)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return 0;
        }


        #endregion
        #region UsertypeMaster
        public List<UserTypeMaster> UsertypeMasterlist(int SchoolID)
        {
            List<UserTypeMaster> obj = new List<UserTypeMaster>();
            SqlCommand cmd = new SqlCommand("SP_Usertype");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new UserTypeMaster() { UTMID = Convert.ToInt32(dr["UTMID"]), UName = Convert.ToString(dr["UName"]) });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(UsertypeMasterlist)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        #endregion

        #region Employee Management System
        public List<EmployeeMaster> GetEmployeeList(int IsActive, int SchoolID)
        {
            List<EmployeeMaster> _emplist = new List<EmployeeMaster>();
            SqlCommand cmd = new SqlCommand("SP_GetEmployeeList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _emplist.Add(new EmployeeMaster() { EMP_ID = Convert.ToInt32(dr["EMP_ID"]), FIRSTNAME = Convert.ToString(dr["EMPCODE"]) + "-" + Convert.ToString(dr["FIRSTNAME"]) });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetEmployeeList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _emplist;
        }

        public EmployeeMaster GetSingleEmployee(int EMP_ID)
        {
            EmployeeMaster _empy = new EmployeeMaster();
            SqlCommand cmd = new SqlCommand("SP_GetSingleEmployee");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
                var reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _empy = new EmployeeMaster()
                    {
                        EMP_ID = DBNull.Value != reader["EMP_ID"] ? (int)reader["EMP_ID"] : default(int),
                        FIRSTNAME = DBNull.Value != reader["FIRSTNAME"] ? (string)reader["FIRSTNAME"] : default(string),
                        BANKACNO = DBNull.Value != reader["BANKACNO"] ? (string)reader["BANKACNO"] : default(string),
                        BANKBRANCH = DBNull.Value != reader["BANKBRANCH"] ? (string)reader["BANKBRANCH"] : default(string),
                        BANKNAME = DBNull.Value != reader["BANKNAME"] ? (string)reader["BANKNAME"] : default(string),
                        BIRTHDATE = DBNull.Value != reader["BIRTHDATE"] ? (DateTime)reader["BIRTHDATE"] : default(DateTime?),
                        BLOODGROUP = DBNull.Value != reader["BLOODGROUP"] ? (string)reader["BLOODGROUP"] : default(string),
                        CONFIRMATIONDATE = DBNull.Value != reader["CONFIRMATIONDATE"] ? (DateTime)reader["CONFIRMATIONDATE"] : default(DateTime?),
                        CONTACTTITLE = DBNull.Value != reader["CONTACTTITLE"] ? (int)reader["CONTACTTITLE"] : default(int),
                        CREATEDBY = DBNull.Value != reader["CREATEDBY"] ? (string)reader["CREATEDBY"] : default(string),
                        CREATEDDATE = DBNull.Value != reader["CREATEDDATE"] ? (DateTime)reader["CREATEDDATE"] : default(DateTime?),
                        DEPARTMENT_ID = DBNull.Value != reader["DEPARTMENT_ID"] ? (int)reader["DEPARTMENT_ID"] : default(int),
                        DESIGNATION_ID = DBNull.Value != reader["DESIGNATION_ID"] ? (int)reader["DESIGNATION_ID"] : default(int),
                        EMAIL = DBNull.Value != reader["EMAIL"] ? (string)reader["EMAIL"] : default(string),
                        EMERGENCY_CONT_NO = DBNull.Value != reader["EMERGENCY_CONT_NO"] ? (string)reader["EMERGENCY_CONT_NO"] : default(string),
                        EMERGENCY_CONT_PRS = DBNull.Value != reader["EMERGENCY_CONT_PRS"] ? (string)reader["EMERGENCY_CONT_PRS"] : default(string),
                        EMPCODE = DBNull.Value != reader["EMPCODE"] ? (string)reader["EMPCODE"] : default(string),
                        EMPIMGPATH = DBNull.Value != reader["EMPIMGPATH"] ? (string)reader["EMPIMGPATH"] : default(string),
                        FATHER_SPOUSE = DBNull.Value != reader["FATHER_SPOUSE"] ? (string)reader["FATHER_SPOUSE"] : default(string),
                        GENDER = DBNull.Value != reader["GENDER"] ? (string)reader["GENDER"] : default(string),
                        GROSS_BASIC = DBNull.Value != reader["GROSS_BASIC"] ? (int)reader["GROSS_BASIC"] : default(int),
                        GROSS_CAR_AMOUNT = DBNull.Value != reader["GROSS_CAR_AMOUNT"] ? (int)reader["GROSS_CAR_AMOUNT"] : default(int),
                        GROSS_CHILDREN_EDUCATION = DBNull.Value != reader["GROSS_CHILDREN_EDUCATION"] ? (int)reader["GROSS_CHILDREN_EDUCATION"] : default(int),
                        GROSS_CONVEYANCE = DBNull.Value != reader["GROSS_CONVEYANCE"] ? (int)reader["GROSS_CONVEYANCE"] : default(int),
                        GROSS_HRA = DBNull.Value != reader["GROSS_HRA"] ? (int)reader["EMGROSS_HRAP_ID"] : default(int),
                        GROSS_SALARY = DBNull.Value != reader["GROSS_SALARY"] ? (int)reader["GROSS_SALARY"] : default(int),
                        GROSS_SPECIAL_ALLOWANCE = DBNull.Value != reader["GROSS_SPECIAL_ALLOWANCE"] ? (int)reader["GROSS_SPECIAL_ALLOWANCE"] : default(int),
                        GROSS_UNIFORM_MAINTENANCE = DBNull.Value != reader["GROSS_UNIFORM_MAINTENANCE"] ? (int)reader["GROSS_UNIFORM_MAINTENANCE"] : default(int),
                        IFSC_CODE = DBNull.Value != reader["IFSC_CODE"] ? (string)reader["IFSC_CODE"] : default(string),
                        ISACTIVE = DBNull.Value != reader["ISACTIVE"] ? Convert.ToBoolean(Convert.ToInt32(reader["ISACTIVE"])) : false,
                        JOININGDATE = DBNull.Value != reader["JOININGDATE"] ? (DateTime)reader["JOININGDATE"] : default(DateTime?),
                        LASTNAME = DBNull.Value != reader["LASTNAME"] ? (string)reader["LASTNAME"] : default(string),
                        LEAVE_CL_ENTITLED = DBNull.Value != reader["LEAVE_CL_ENTITLED"] ? (string)reader["LEAVE_CL_ENTITLED"] : default(string),
                        LEAVE_PL_ENTITLED = DBNull.Value != reader["LEAVE_PL_ENTITLED"] ? (string)reader["LEAVE_PL_ENTITLED"] : default(string),
                        LEAVE_SL_ENTITLED = DBNull.Value != reader["LEAVE_SL_ENTITLED"] ? (string)reader["LEAVE_SL_ENTITLED"] : default(string),
                        LEAVINGDATE = DBNull.Value != reader["LEAVINGDATE"] ? (DateTime)reader["LEAVINGDATE"] : default(DateTime?),
                        MARITALSTATUS = DBNull.Value != reader["MARITALSTATUS"] ? (string)reader["MARITALSTATUS"] : default(string),
                        MIDDLENAME = DBNull.Value != reader["MIDDLENAME"] ? (string)reader["MIDDLENAME"] : default(string),
                        MOBILENUMBER = DBNull.Value != reader["MOBILENUMBER"] ? (string)reader["MOBILENUMBER"] : default(string),
                        MODIFIEDBY = DBNull.Value != reader["MODIFIEDBY"] ? (string)reader["MODIFIEDBY"] : default(string),
                        MODIFIEDDATE = DBNull.Value != reader["MODIFIEDDATE"] ? (DateTime)reader["MODIFIEDDATE"] : default(DateTime?),
                        PANIMGPATH = DBNull.Value != reader["PANIMGPATH"] ? (string)reader["PANIMGPATH"] : default(string),
                        PANNO = DBNull.Value != reader["PANNO"] ? (string)reader["PANNO"] : default(string),
                        ParentID = DBNull.Value != reader["ParentID"] ? (int)reader["ParentID"] : default(int),
                        PFNO = DBNull.Value != reader["PFNO"] ? (string)reader["PFNO"] : default(string),
                        PF_ESTD_CODE = DBNull.Value != reader["PF_ESTD_CODE"] ? (string)reader["PF_ESTD_CODE"] : default(string),
                        PF_UAN = DBNull.Value != reader["PF_UAN"] ? (string)reader["PF_UAN"] : default(string),
                        QUALIFICATION1 = DBNull.Value != reader["QUALIFICATION1"] ? (string)reader["QUALIFICATION1"] : default(string),
                        QUALIFICATION2 = DBNull.Value != reader["QUALIFICATION2"] ? (string)reader["QUALIFICATION2"] : default(string),
                        REMARKS = DBNull.Value != reader["REMARKS"] ? (string)reader["REMARKS"] : default(string),
                        REPORTING_MANAGER = DBNull.Value != reader["REPORTING_MANAGER"] ? (int)reader["REPORTING_MANAGER"] : default(int),
                        SALARY = DBNull.Value != reader["SALARY"] ? (decimal)reader["SALARY"] : default(decimal),
                        SPOUSE = DBNull.Value != reader["SPOUSE"] ? (string)reader["SPOUSE"] : default(string),
                        VPF_CONTB_RATE = DBNull.Value != reader["VPF_CONTB_RATE"] ? (int)reader["VPF_CONTB_RATE"] : default(int),
                        ModeOfPayment = DBNull.Value != reader["ModeOfPayment"] ? (string)reader["ModeOfPayment"] : default(string),
                    };

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSingleEmployee)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _empy;
        }
        #endregion

        #region Student Management System
        public DataTable GetStudentbaseonAdmissionno(string Adminssionno, int SchoolID)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetStudentbaseonAdmissionno");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Adminssionno", Adminssionno);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStudentbaseonAdmissionno)", "Error_014", ex, "DALCommon");
            }
            return _ds.Tables[0];
        }
        public DataSet GetStudentTbl(Nullable<int> classid, Nullable<int> sectionid, Nullable<DateTime> Startdate, Nullable<DateTime> Enddate, int IsActive, int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_TBLGetStudentList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CMid", classid);
                cmd.Parameters.AddWithValue("@SecMid", sectionid);
                cmd.Parameters.AddWithValue("@Startdate", Startdate);
                cmd.Parameters.AddWithValue("@Enddate", Enddate);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_TBLGetStudentList)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }

        public DataSet GetStudentStrengthTbl(Nullable<int> classid, Nullable<int> sectionid, int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("USP_GetStudentStrengthTbl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CMid", classid);
                cmd.Parameters.AddWithValue("@SecMid", sectionid);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_TBLGetStudentList)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }
        //public List<StudentMaster> GetStudentList(int IsActive, int SchoolID)
        //{
        //    List<StudentMaster> _stulist = new List<StudentMaster>();
        //    SqlCommand cmd = new SqlCommand("SP_GetStudentList");
        //    try
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@IsActive", IsActive);
        //        cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
        //        var dr = DBHelper.ExecuteReader(cmd);
        //        while (dr.Read())
        //        {
        //            _stulist.Add(new StudentMaster()
        //            {
        //                Smid = Convert.ToInt32(dr["Smid"]),
        //                Enquiryno = dr["Enquiryno"] == DBNull.Value ? "" : Convert.ToString(dr["Enquiryno"]),
        //                Firstname = dr["Firstname"] == DBNull.Value ? "" : Convert.ToString(dr["Firstname"]),
        //                // Lastname = Convert.ToString(dr["Lastname"]),
        //                CMid = dr["CMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CMid"]),
        //                SecMid = dr["SecMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SecMid"]),
        //                RollNo = dr["RollNo"] == DBNull.Value ? "0" : Convert.ToString(dr["RollNo"]),
        //                RouteMid = dr["RouteMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["RouteMid"]),
        //                BusMid = dr["BusMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BusMid"]),
        //                Castmid = dr["Castmid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Castmid"]),
        //                Categmid = dr["Categmid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Categmid"]),
        //                HouseMid = dr["HouseMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HouseMid"]),
        //                GMid = dr["GMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["GMid"]),
        //                Bd_address1 = dr["Bd_address1"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_address1"]),
        //                Bd_address2 = dr["Bd_address2"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_address2"]),
        //                Bd_City = dr["Bd_City"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_City"]),
        //                Bd_contactno = dr["Bd_contactno"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_contactno"]),
        //                Bd_dob = dr["Bd_dob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_dob"]),
        //                Bd_fathername = dr["Bd_fathername"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fathername"]),
        //                Bd_fathermob = dr["Bd_fathermob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fathermob"]),
        //                Bd_qualification = dr["Bd_qualification"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_qualification"]),
        //                Bd_fatheroccuption = dr["Bd_fatheroccuption"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fatheroccuption"]),
        //                Bd_fathdob = dr["Bd_fathdob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fathdob"]),
        //                Bd_mothername = dr["Bd_mothername"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_mothername"]),
        //                Bd_mothermob = dr["Bd_mothermob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_mothermob"]),
        //                Bd_motherqualification = dr["Bd_motherqualification"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_motherqualification"]),
        //                Bd_Motheroccuption = dr["Bd_Motheroccuption"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_Motheroccuption"]),
        //                Bd_Mothredob = dr["Bd_Mothredob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_Mothredob"]),
        //                Bd_dateofannversy = dr["Bd_dateofannversy"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_dateofannversy"]),
        //                Bd_Emailid = dr["Bd_Emailid"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_Emailid"]),
        //                Off_lastschool = dr["Off_lastschool"] == DBNull.Value ? "" : Convert.ToString(dr["Off_lastschool"]),
        //                Off_remarks = dr["Off_remarks"] == DBNull.Value ? "" : Convert.ToString(dr["Off_remarks"]),
        //                Off_Examgiven = dr["Off_Examgiven"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Examgiven"]),
        //                Off_Year = dr["Off_Year"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Year"]),
        //                Off_Status = dr["Off_Status"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Status"]),
        //                Off_marks = dr["Off_marks"] == DBNull.Value ? "" : Convert.ToString(dr["Off_marks"]),
        //                Off_boardoruniversity = dr["Off_boardoruniversity"] == DBNull.Value ? "" : Convert.ToString(dr["Off_boardoruniversity"]),
        //                Off_bloodgroup = dr["Off_bloodgroup"] == DBNull.Value ? "" : Convert.ToString(dr["Off_bloodgroup"]),
        //                VisionLeft = dr["VisionLeft"] == DBNull.Value ? "" : Convert.ToString(dr["VisionLeft"]),
        //                Off_grno = dr["Off_grno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_grno"]),
        //                Off_Visionright = dr["Off_Visionright"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Visionright"]),
        //                Off_heightweight = dr["Off_heightweight"] == DBNull.Value ? "" : Convert.ToString(dr["Off_heightweight"]),
        //                // Off_weight = Convert.ToString(dr["Off_weight"]),
        //                Off_Dentailhygine = dr["Off_Dentailhygine"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Dentailhygine"]),
        //                Off_Hosalroomno = dr["Off_Hosalroomno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Hosalroomno"]),
        //                Off_bedno = dr["Off_bedno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_bedno"]),
        //                Off_Scholarshipno = dr["Off_Scholarshipno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Scholarshipno"]),
        //                Off_TC = dr["Off_TC"] == DBNull.Value ? "" : Convert.ToString(dr["Off_TC"]),
        //                Off_CC = dr["Off_CC"] == DBNull.Value ? "" : Convert.ToString(dr["Off_CC"]),
        //                Off_ReportC = dr["Firstname"] == DBNull.Value ? "" : Convert.ToString(dr["Off_ReportC"]),
        //                Off_Dobcertificate = dr["Off_Dobcertificate"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Dobcertificate"]),
        //                Off_admissionno = dr["Off_admissionno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_admissionno"]),
        //                Off_dateofadmission = dr["Off_dateofadmission"] == DBNull.Value ? "" : Convert.ToString(dr["Off_dateofadmission"]),
        //                Off_Ledgerbalance = dr["Off_Ledgerbalance"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Ledgerbalance"]),
        //                Off_feesbalance = dr["Off_feesbalance"] == DBNull.Value ? "" : Convert.ToString(dr["Off_feesbalance"]),
        //                Off_Comments = dr["Off_Comments"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Comments"]),
        //                Off_Aadharno = dr["Off_Aadharno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Aadharno"]),
        //                Off_biometricno = dr["Off_biometricno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_biometricno"]),
        //                Off_childuniqueno = dr["Off_childuniqueno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_childuniqueno"]),
        //                Off_sessionno = dr["Off_sessionno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_sessionno"]),
        //                Off_family = dr["Off_family"] == DBNull.Value ? "" : Convert.ToString(dr["Off_family"]),
        //                Off_stausinschool = dr["Off_stausinschool"] == DBNull.Value ? "" : Convert.ToString(dr["Off_stausinschool"]),
        //                Off_discontinuethedate = dr["Off_discontinuethedate"] == DBNull.Value ? "" : Convert.ToString(dr["Off_discontinuethedate"]),
        //                studentimage = dr["studentimage"] == DBNull.Value ? "" : Convert.ToString(dr["studentimage"]),
        //                motherimage = dr["motherimage"] == DBNull.Value ? "" : Convert.ToString(dr["motherimage"]),
        //                fatherimage = dr["fatherimage"] == DBNull.Value ? "" : Convert.ToString(dr["fatherimage"]),
        //                IsActive = dr["IsActive"] == DBNull.Value ? true : Convert.ToBoolean(dr["IsActive"]),
        //                SchoolID = dr["SchoolID"] == DBNull.Value ? 1 : Convert.ToInt32(dr["SchoolID"]),
        //                Createdate = dr["Createdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Createdate"]),
        //                Createdby = dr["Createdby"] == DBNull.Value ? "Admin" : Convert.ToString(dr["Createdby"]),
        //                Modifiedby = dr["Modifiedby"] == DBNull.Value ? "Admin" : Convert.ToString(dr["Modifiedby"]),
        //                Modifieddate = dr["Modifieddate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Modifieddate"])

        //            });

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExecptionLogger.FileHandling("DALCommon(GetStudentList)", "Error_014", ex, "DALCommon");
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //    return _stulist;
        //}

        public StudentMaster GetSingleStudent(int Smid)
        {
            StudentMaster _stud = new StudentMaster();
            SqlCommand cmd = new SqlCommand("SP_GetSingleStudent");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Smid", Smid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _stud = new StudentMaster()
                    {
                        Smid = Convert.ToInt32(dr["Smid"]),
                        Adminssionno = dr["Adminssionno"] != DBNull.Value ? Convert.ToString(dr["Adminssionno"]) : default(string),
                        Enquiryno = dr["Enquiryno"] != DBNull.Value ? Convert.ToString(dr["Enquiryno"]) : default(string),
                        Firstname = dr["Firstname"] != DBNull.Value ? Convert.ToString(dr["Firstname"]) : default(string),
                        CMid = dr["CMid"] != DBNull.Value ? Convert.ToInt32(dr["CMid"]) : default(int),

                        SecMid = dr["SecMid"] != DBNull.Value ? Convert.ToInt32(dr["SecMid"]) : default(int),

                        RollNo = dr["RollNo"] != DBNull.Value ? Convert.ToString(dr["RollNo"]) : default(string),
                        RouteMid = dr["RouteMid"] != DBNull.Value ? Convert.ToInt32(dr["RouteMid"]) : default(int),
                        BusMid = dr["BusMid"] != DBNull.Value ? Convert.ToInt32(dr["BusMid"]) : default(int),
                        Castmid = dr["Castmid"] != DBNull.Value ? Convert.ToInt32(dr["Castmid"]) : default(int),
                        Categmid = dr["Categmid"] != DBNull.Value ? Convert.ToInt32(dr["Categmid"]) : default(int),
                        HouseMid = dr["HouseMid"] != DBNull.Value ? Convert.ToInt32(dr["HouseMid"]) : default(int),
                        Relmid = dr["Relmid"] != DBNull.Value ? Convert.ToInt32(dr["Relmid"]) : default(int),
                        GMid = dr["GMid"] != DBNull.Value ? Convert.ToInt32(dr["GMid"]) : default(int),
                        CITY_ID = dr["CITY_ID"] != DBNull.Value ? Convert.ToInt32(dr["CITY_ID"]) : default(int),
                        STATE_ID = dr["STATE_ID"] != DBNull.Value ? Convert.ToInt32(dr["STATE_ID"]) : default(int),
                        COUNTRY_ID = dr["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COUNTRY_ID"]) : default(int),
                        Bd_address1 = dr["Bd_address1"] != DBNull.Value ? Convert.ToString(dr["Bd_address1"]) : default(string),
                        Bd_address2 = dr["Bd_address2"] != DBNull.Value ? Convert.ToString(dr["Bd_address2"]) : default(string),
                        Bd_City = dr["Bd_City"] != DBNull.Value ? Convert.ToString(dr["Bd_City"]) : default(string),
                        Bd_contactno = dr["Bd_contactno"] != DBNull.Value ? Convert.ToString(dr["Bd_contactno"]) : default(string),
                        Bd_dob = dr["Bd_dob"] != DBNull.Value ? Convert.ToString(dr["Bd_dob"]) : default(string),//?null: Convert.ToDateTime(dr["Bd_dob"]),
                        Bd_fathername = dr["Bd_fathername"] != DBNull.Value ? Convert.ToString(dr["Bd_fathername"]) : default(string),
                        Bd_fathermob = dr["Bd_fathermob"] != DBNull.Value ? Convert.ToString(dr["Bd_fathermob"]) : default(string),
                        Bd_qualification = dr["Bd_qualification"] != DBNull.Value ? Convert.ToString(dr["Bd_qualification"]) : default(string),
                        Bd_fatheroccuption = dr["Bd_fatheroccuption"] != DBNull.Value ? Convert.ToString(dr["Bd_fatheroccuption"]) : default(string),
                        Bd_fathdob = dr["Bd_fathdob"] != DBNull.Value ? Convert.ToString(dr["Bd_fathdob"]) : default(string),
                        Bd_mothername = dr["Bd_mothername"] != DBNull.Value ? Convert.ToString(dr["Bd_mothername"]) : default(string),
                        Bd_mothermob = dr["Bd_mothermob"] != DBNull.Value ? Convert.ToString(dr["Bd_mothermob"]) : default(string),
                        Bd_motherqualification = dr["Bd_motherqualification"] != DBNull.Value ? Convert.ToString(dr["Bd_motherqualification"]) : default(string),
                        Bd_Motheroccuption = dr["Bd_Motheroccuption"] != DBNull.Value ? Convert.ToString(dr["Bd_Motheroccuption"]) : default(string),
                        Bd_Mothredob = dr["Bd_Mothredob"] != DBNull.Value ? Convert.ToString(dr["Bd_Mothredob"]) : default(string),
                        Bd_dateofannversy = dr["Bd_dateofannversy"] != DBNull.Value ? Convert.ToString(dr["Bd_dateofannversy"]) : default(string),
                        Bd_Emailid = dr["Bd_Emailid"] != DBNull.Value ? Convert.ToString(dr["Bd_Emailid"]) : default(string),
                        Off_lastschool = dr["Off_lastschool"] != DBNull.Value ? Convert.ToString(dr["Off_lastschool"]) : default(string),
                        Off_remarks = dr["Off_remarks"] != DBNull.Value ? Convert.ToString(dr["Off_remarks"]) : default(string),
                        Off_Examgiven = dr["Off_Examgiven"] != DBNull.Value ? Convert.ToString(dr["Off_Examgiven"]) : default(string),
                        Off_Year = dr["Off_Year"] != DBNull.Value ? Convert.ToString(dr["Off_Year"]) : default(string),
                        Off_Status = dr["Off_Status"] != DBNull.Value ? Convert.ToString(dr["Off_Status"]) : default(string),
                        Off_marks = dr["Off_marks"] != DBNull.Value ? Convert.ToString(dr["Off_marks"]) : default(string),
                        Off_boardoruniversity = dr["Off_boardoruniversity"] != DBNull.Value ? Convert.ToString(dr["Off_boardoruniversity"]) : default(string),
                        Off_bloodgroup = dr["Off_bloodgroup"] != DBNull.Value ? Convert.ToString(dr["Off_bloodgroup"]) : default(string),
                        VisionLeft = dr["VisionLeft"] != DBNull.Value ? Convert.ToString(dr["VisionLeft"]) : default(string),
                        Off_grno = dr["Off_grno"] != DBNull.Value ? Convert.ToString(dr["Off_grno"]) : default(string),
                        Off_Visionright = dr["Off_Visionright"] != DBNull.Value ? Convert.ToString(dr["Off_Visionright"]) : default(string),
                        Off_heightweight = dr["Off_heightweight"] != DBNull.Value ? Convert.ToString(dr["Off_heightweight"]) : default(string),
                        Off_Dentailhygine = dr["Off_Dentailhygine"] != DBNull.Value ? Convert.ToString(dr["Off_Dentailhygine"]) : default(string),
                        Off_Hosalroomno = dr["Off_Hosalroomno"] != DBNull.Value ? Convert.ToString(dr["Off_Hosalroomno"]) : default(string),
                        Off_bedno = dr["Off_bedno"] != DBNull.Value ? Convert.ToString(dr["Off_bedno"]) : default(string),
                        Off_Scholarshipno = dr["Off_Scholarshipno"] != DBNull.Value ? Convert.ToString(dr["Off_Scholarshipno"]) : default(string),
                        Off_TC = dr["Off_TC"] != DBNull.Value ? Convert.ToString(dr["Off_TC"]) : default(string),
                        Off_CC = dr["Off_CC"] != DBNull.Value ? Convert.ToString(dr["Off_CC"]) : default(string),
                        Off_ReportC = dr["Off_ReportC"] != DBNull.Value ? Convert.ToString(dr["Off_ReportC"]) : default(string),
                        Off_Dobcertificate = dr["Off_Dobcertificate"] != DBNull.Value ? Convert.ToString(dr["Off_Dobcertificate"]) : default(string),
                        Off_admissionno = dr["Off_admissionno"] != DBNull.Value ? Convert.ToString(dr["Off_admissionno"]) : default(string),
                        Off_dateofadmission = dr["Off_dateofadmission"] != DBNull.Value ? Convert.ToString(dr["Off_dateofadmission"]) : default(string),
                        Off_Ledgerbalance = dr["Off_Ledgerbalance"] != DBNull.Value ? Convert.ToString(dr["Off_Ledgerbalance"]) : default(string),
                        Off_feesbalance = dr["Off_feesbalance"] != DBNull.Value ? Convert.ToString(dr["Off_feesbalance"]) : default(string),
                        Off_Comments = dr["Off_Comments"] != DBNull.Value ? Convert.ToString(dr["Off_Comments"]) : default(string),
                        Off_Aadharno = dr["Off_Aadharno"] != DBNull.Value ? Convert.ToString(dr["Off_Aadharno"]) : default(string),
                        Off_biometricno = dr["Off_biometricno"] != DBNull.Value ? Convert.ToString(dr["Off_biometricno"]) : default(string),
                        Off_nationality = dr["Off_nationality"] != DBNull.Value ? Convert.ToString(dr["Off_nationality"]) : default(string),
                        Off_childuniqueno = dr["Off_childuniqueno"] != DBNull.Value ? Convert.ToString(dr["Off_childuniqueno"]) : default(string),
                        Off_sessionno = dr["Off_sessionno"] != DBNull.Value ? Convert.ToString(dr["Off_sessionno"]) : default(string),
                        Off_family = dr["Off_family"] != DBNull.Value ? Convert.ToString(dr["Off_family"]) : default(string),
                        Off_stausinschool = dr["Off_stausinschool"] != DBNull.Value ? Convert.ToString(dr["Off_stausinschool"]) : default(string),
                        Off_discontinuethedate = dr["Off_discontinuethedate"] != DBNull.Value ? Convert.ToString(dr["Off_discontinuethedate"]) : default(string),
                        studentimage = dr["studentimage"] != DBNull.Value ? Convert.ToString(dr["studentimage"]) : default(string),
                        motherimage = dr["motherimage"] != DBNull.Value ? Convert.ToString(dr["motherimage"]) : default(string),
                        fatherimage = dr["fatherimage"] != DBNull.Value ? Convert.ToString(dr["fatherimage"]) : default(string),
                        IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : default(bool),
                        SchoolID = dr["SchoolID"] != DBNull.Value ? Convert.ToInt32(dr["SchoolID"]) : default(int),
                        Createdate = dr["Createdate"] != DBNull.Value ? Convert.ToDateTime(dr["Createdate"]) : default(DateTime),
                        Createdby = dr["Createdby"] != DBNull.Value ? Convert.ToString(dr["Createdby"]) : default(string),
                        Modifiedby = dr["Modifiedby"] != DBNull.Value ? Convert.ToString(dr["Modifiedby"]) : default(string),
                        Modifieddate = dr["Modifieddate"] != DBNull.Value ? Convert.ToDateTime(dr["Modifieddate"]) : default(DateTime),
                        TPCost = dr["TPCost"] != DBNull.Value ? Convert.ToBoolean(dr["TPCost"]) : default(bool),
                        FeeGroup = dr["FeeGroup"] != DBNull.Value ? Convert.ToString(dr["FeeGroup"]) : default(string),
                        FinancialYear = dr["SchoolID"] != DBNull.Value ? Convert.ToInt32(dr["FinancialYear"]) : default(int),
                        ClassName = dr["ClassName"] != DBNull.Value ? Convert.ToString(dr["ClassName"]) : default(string),
                        SectionName = dr["SectionName"] != DBNull.Value ? Convert.ToString(dr["SectionName"]) : default(string),
                        ReligionName = dr["ReligionName"] != DBNull.Value ? Convert.ToString(dr["ReligionName"]) : default(string),
                        CastName = dr["CastName"] != DBNull.Value ? Convert.ToString(dr["CastName"]) : default(string),
                        CatName = dr["CatName"] != DBNull.Value ? Convert.ToString(dr["CatName"]) : default(string),
                        GName = dr["GName"] != DBNull.Value ? Convert.ToString(dr["GName"]) : default(string),
                        CITYNAME = dr["CityName"] != DBNull.Value ? Convert.ToString(dr["CityName"]) : default(string),
                        DistrictName = dr["DistrictName"] != DBNull.Value ? Convert.ToString(dr["DistrictName"]) : default(string),
                        STATENAME = dr["StateName"] != DBNull.Value ? Convert.ToString(dr["StateName"]) : default(string),
                        COUNTRYNAME = dr["CountryName"] != DBNull.Value ? Convert.ToString(dr["CountryName"]) : default(string),
                        RouteName = dr["RouteName"] != DBNull.Value ? Convert.ToString(dr["RouteName"]) : default(string),
                        PickDropPointName = dr["PickDropPointName"] != DBNull.Value ? Convert.ToString(dr["PickDropPointName"]) : default(string),
                        BloodGroupName = dr["BloodGroupName"] != DBNull.Value ? Convert.ToString(dr["BloodGroupName"]) : default(string),
                        VehicleNumber = dr["VehicleNumber"] != DBNull.Value ? Convert.ToString(dr["VehicleNumber"]) : default(string),
                    };

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSingleStudent)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _stud;
        }
        #endregion

        public List<ClassMaster> GetClassList(int SchoolID)
        {
            List<ClassMaster> obj = new List<ClassMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_ClassMasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new ClassMaster
                        {
                            CMid = Convert.ToInt32(ds.Tables[0].Rows[l]["CMid"]),
                            ClassName = Convert.ToString(ds.Tables[0].Rows[l]["ClassName"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetClassList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public BusMaster GetBusByid(int Busmid)
        {
            BusMaster obj = new BusMaster();
            SqlCommand cmd = new SqlCommand("SP_GetBusBymid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Busmid", Busmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Busmid = Convert.ToInt32(dr["Busmid"]);
                    obj.BusName = Convert.ToString(dr["BusName"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetBusByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public ReligionMaster GetReligionByid(int Relmid)
        {
            ReligionMaster obj = new ReligionMaster();
            SqlCommand cmd = new SqlCommand("SP_GetReligionmidBymid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Relmid", Relmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Relmid = Convert.ToInt32(dr["Relmid"]);
                    obj.ReligionName = Convert.ToString(dr["ReligionName"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetReligionByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public DepartmentMaster GetDepartmentByid(int Department_id)
        {
            DepartmentMaster obj = new DepartmentMaster();
            SqlCommand cmd = new SqlCommand("SP_GetDepartmentByid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Department_id", Department_id);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.DEPARTMENT_ID = Convert.ToInt32(dr["DEPARTMENT_ID"]);
                    obj.DEPARTMENTNAME = Convert.ToString(dr["DEPARTMENTNAME"]);
                    obj.DEPARTMENTDESC = Convert.ToString(dr["DEPARTMENTDESC"]);
                    obj.SchoolID = Convert.ToInt32(dr["SchoolId"]);
                    obj.ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]);
                    obj.REMARKS = Convert.ToString(dr["REMARKS"]);
                    obj.CREATEDBY = Convert.ToString(dr["CREATEDBY"]);
                    obj.MODIFIEDBY = Convert.ToString(dr["MODIFIEDBY"]);
                    obj.CREATEDDATE = Convert.ToDateTime(dr["CREATEDDATE"]);
                    obj.MODIFIEDDATE = Convert.ToDateTime(dr["MODIFIEDDATE"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetDepartmentByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public DesignationMaster GetDesignationByid(int DESIGNATION_ID)
        {
            DesignationMaster obj = new DesignationMaster();
            SqlCommand cmd = new SqlCommand("SP_GetDesignationByid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DESIGNATION_ID", DESIGNATION_ID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.DESIGNATION_ID = Convert.ToInt32(dr["DESIGNATION_ID"]);
                    obj.DESIGNATIONNAME = Convert.ToString(dr["DESIGNATIONNAME"]);
                    obj.DESIGNATIONDESC = Convert.ToString(dr["DESIGNATIONDESC"]);
                    obj.SchoolID = Convert.ToInt32(dr["SchoolId"]);
                    obj.ISACTIVE = Convert.ToBoolean(dr["ISACTIVE"]);
                    obj.REMARKS = Convert.ToString(dr["REMARKS"]);
                    obj.CREATEDBY = Convert.ToString(dr["CREATEDBY"]);
                    obj.MODIFIEDBY = Convert.ToString(dr["MODIFIEDBY"]);
                    obj.CREATEDDATE = Convert.ToDateTime(dr["CREATEDDATE"]);
                    obj.MODIFIEDDATE = Convert.ToDateTime(dr["MODIFIEDDATE"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetDesignationByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public GendraMaster GetGendarByid(int GMid)
        {
            GendraMaster obj = new GendraMaster();
            SqlCommand cmd = new SqlCommand("SP_GetGendarmidBymid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GMid", GMid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.GMid = Convert.ToInt32(dr["GMid"]);
                    obj.GName = Convert.ToString(dr["GName"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetGendarByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public CategoryMaster GetCategoryByid(int Catmid)
        {
            CategoryMaster obj = new CategoryMaster();
            SqlCommand cmd = new SqlCommand("SP_GetCatmidBymid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Catmid", Catmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Catmid = Convert.ToInt32(dr["Catmid"]);
                    obj.CatName = Convert.ToString(dr["CatName"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetCategoryByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public BloodGroupMaster GetBloodGroupByid(int Bdmid)
        {
            BloodGroupMaster obj = new BloodGroupMaster();
            SqlCommand cmd = new SqlCommand("SP_GetBloodGroupByid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bdmid", Bdmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Bdmid = Convert.ToInt32(dr["Bdmid"]);
                    obj.BloodGroupName = Convert.ToString(dr["BloodGroupName"]);
                    obj.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetBloodGroupByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public ContactTitleMaster GetContacttitleByid(int CTmid)
        {
            ContactTitleMaster obj = new ContactTitleMaster();
            SqlCommand cmd = new SqlCommand("SP_GetContacttitleByid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CTmid", CTmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.CTmid = Convert.ToInt32(dr["CTmid"]);
                    obj.ContactName = Convert.ToString(dr["ContactName"]);
                    obj.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetContacttitleByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }


        public MartialStatusMaster GetMartialStatusByid(int Mrmid)
        {
            MartialStatusMaster obj = new MartialStatusMaster();
            SqlCommand cmd = new SqlCommand("SP_GetMartialStatusByid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mrmid", Mrmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Mrmid = Convert.ToInt32(dr["Mrmid"]);
                    obj.StatusName = Convert.ToString(dr["StatusName"]);
                    obj.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetMartialStatusByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public CastMaster GetCastByid(int Castmid)
        {
            CastMaster obj = new CastMaster();
            SqlCommand cmd = new SqlCommand("SP_GetCastBymid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Castmid", Castmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Castmid = Convert.ToInt32(dr["Castmid"]);
                    obj.CastName = Convert.ToString(dr["CastName"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetBusByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int AddReligion(ReligionMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddReligionmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Relmid", _master.Relmid);
                cmd.Parameters.AddWithValue("@ReligionName", _master.ReligionName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddReligion)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddDepartment(DepartmentMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddDEPARTMENTmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DEPARTMENT_ID", _master.DEPARTMENT_ID);
                cmd.Parameters.AddWithValue("@DEPARTMENTNAME", _master.DEPARTMENTNAME);
                cmd.Parameters.AddWithValue("@DEPARTMENTDESC", _master.DEPARTMENTDESC);
                cmd.Parameters.AddWithValue("@REMARKS", _master.REMARKS);
                cmd.Parameters.AddWithValue("@IsActive", _master.ISACTIVE);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@Createdate", _master.CREATEDDATE);
                cmd.Parameters.AddWithValue("@Createdby", _master.CREATEDBY);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.MODIFIEDBY);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.MODIFIEDDATE);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddDepartment)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddDesignation(DesignationMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddDESIGNATIONmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DESIGNATION_ID", _master.DESIGNATION_ID);
                cmd.Parameters.AddWithValue("@DESIGNATIONNAME", _master.DESIGNATIONNAME);
                cmd.Parameters.AddWithValue("@DESIGNATIONDESC", _master.DESIGNATIONDESC);
                cmd.Parameters.AddWithValue("@REMARKS", _master.REMARKS);
                cmd.Parameters.AddWithValue("@IsActive", _master.ISACTIVE);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@Createdate", _master.CREATEDDATE);
                cmd.Parameters.AddWithValue("@Createdby", _master.CREATEDBY);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.MODIFIEDBY);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.MODIFIEDDATE);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddDesignation)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddGendar(GendraMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddGendarmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GMid", _master.GMid);
                cmd.Parameters.AddWithValue("@GName", _master.GName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddGendar)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddCategory(CategoryMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddCategorymaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Catmid", _master.Catmid);
                cmd.Parameters.AddWithValue("@CatName", _master.CatName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddCategory)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public int AddContacttitle(ContactTitleMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddContacttitle");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CTmid", _master.CTmid);
                cmd.Parameters.AddWithValue("@ContactName", _master.ContactName);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddContacttitle)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddBloodgroup(BloodGroupMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddBloodGroupMaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bdmid", _master.Bdmid);
                cmd.Parameters.AddWithValue("@BloodGroupName", _master.BloodGroupName);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddMartialStatus)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddMartialStatus(MartialStatusMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddMartialStatusmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mrmid", _master.Mrmid);
                cmd.Parameters.AddWithValue("@StatusName", _master.StatusName);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActice);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddMartialStatus)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public int AddCast(CastMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddCastmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Castmid", _master.Castmid);
                cmd.Parameters.AddWithValue("@CastName", _master.CastName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddCast)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddBus(BusMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_Addbusmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Busmid", _master.Busmid);
                cmd.Parameters.AddWithValue("@BusName", _master.BusName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddBus)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public RouteMaster GetRouteByid(int Routemid)
        {
            RouteMaster obj = new RouteMaster();
            SqlCommand cmd = new SqlCommand("SP_GetRouteBymid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Routemid", Routemid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Routemid = Convert.ToInt32(dr["Routemid"]);
                    obj.RouteName = Convert.ToString(dr["RouteName"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSectionBySecmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public SectionMaster GetSectionBySecmid(int Secmid)
        {
            SectionMaster obj = new SectionMaster();
            SqlCommand cmd = new SqlCommand("SP_GetSectionBySecmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Secmid", Secmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Secmid = Convert.ToInt32(dr["Secmid"]);
                    obj.SectionName = Convert.ToString(dr["SectionName"]);
                    obj.CMid = Convert.ToInt32(dr["CMid"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSectionBySecmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int AddSection(SectionMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_AddSectionmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Secmid", _master.Secmid);
                cmd.Parameters.AddWithValue("@SectionName", _master.SectionName);
                cmd.Parameters.AddWithValue("@CMid", _master.CMid);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddSection)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public ClassMaster GetClassByClassid(int CMid)
        {
            ClassMaster obj = new ClassMaster();
            SqlCommand cmd = new SqlCommand("SP_GetClassByClassid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CMid", CMid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.CMid = Convert.ToInt32(dr["CMid"]);
                    obj.ClassName = Convert.ToString(dr["ClassName"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetClassByClassid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddRout(RouteMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_Addroutemaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Routemid", _master.Routemid);
                cmd.Parameters.AddWithValue("@RouteName", _master.RouteName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolId);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_Addroutemaster)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public int AddClass(ClassMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("SP_Addclassmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CMid", _master.CMid);
                cmd.Parameters.AddWithValue("@ClassName", _master.ClassName);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", _master.SchoolID);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_Addclassmaster)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public List<GendraMaster> GetGendarList(int SchoolID)
        {
            List<GendraMaster> obj = new List<GendraMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GendarMasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new GendraMaster
                        {
                            GMid = Convert.ToInt32(ds.Tables[0].Rows[l]["GMid"]),
                            GName = Convert.ToString(ds.Tables[0].Rows[l]["GName"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetGendarList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<ContactTitleMaster> GetContactList(int SchoolID)
        {
            List<ContactTitleMaster> obj = new List<ContactTitleMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_ContactTitleMasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new ContactTitleMaster
                        {
                            CTmid = Convert.ToInt32(ds.Tables[0].Rows[l]["CTmid"]),
                            ContactName = Convert.ToString(ds.Tables[0].Rows[l]["ContactName"]),
                            SchoolID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetContactList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<BloodGroupMaster> GetBloodGroupList(int SchoolID)
        {
            List<BloodGroupMaster> obj = new List<BloodGroupMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_BloodGroupMasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new BloodGroupMaster
                        {
                            Bdmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Bdmid"]),
                            BloodGroupName = Convert.ToString(ds.Tables[0].Rows[l]["BloodGroupName"]),
                            SchoolID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetBloodGroupList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<MartialStatusMaster> GetMaritalStatusList(int SchoolID)
        {
            List<MartialStatusMaster> obj = new List<MartialStatusMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_MartialStatusMaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new MartialStatusMaster
                        {
                            Mrmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Mrmid"]),
                            StatusName = Convert.ToString(ds.Tables[0].Rows[l]["StatusName"]),
                            SchoolID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetMaritalStatusList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<Feegroupstudentfrm> GetFeegroupList(int SchoolID)
        {
            List<Feegroupstudentfrm> obj = new List<Feegroupstudentfrm>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_FeeGroupList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new Feegroupstudentfrm
                        {
                            FeeGroupID = Convert.ToString(ds.Tables[0].Rows[l]["FeeGroupName"]),
                            FeeGroupName = Convert.ToString(ds.Tables[0].Rows[l]["FeeGroupName"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetFeegroupList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddAdmissionEnquiry(AdmissionEnquiryMaster _addenqmasters)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet _ds = new DataSet();
                cmd.CommandText = "SP_AddAdmissionEnquiry";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AMID", _addenqmasters.AMID);
                cmd.Parameters.AddWithValue("@AEnquiryNo", _addenqmasters.AEnquiryNo);
                cmd.Parameters.AddWithValue("@AClassName", _addenqmasters.AClassName);
                cmd.Parameters.AddWithValue("@AStudentName", _addenqmasters.AStudentName);
                cmd.Parameters.AddWithValue("@AGendar", _addenqmasters.AGendar);
                cmd.Parameters.AddWithValue("@ACont_Address", _addenqmasters.ACont_Address);
                cmd.Parameters.AddWithValue("@ACont_Address2", _addenqmasters.ACont_Address2);
                cmd.Parameters.AddWithValue("@ACity", _addenqmasters.ACity);
                cmd.Parameters.AddWithValue("@AContactno", _addenqmasters.AContactno);
                cmd.Parameters.AddWithValue("@ADOB", _addenqmasters.ADOB);
                cmd.Parameters.AddWithValue("@APdfather", _addenqmasters.APdfather);
                cmd.Parameters.AddWithValue("@APdFathermobile", _addenqmasters.APdFathermobile);
                cmd.Parameters.AddWithValue("@APQualification", _addenqmasters.APQualification);
                cmd.Parameters.AddWithValue("@APFatherdob", _addenqmasters.APFatherdob);
                cmd.Parameters.AddWithValue("@APFatheroccuption", _addenqmasters.APFatheroccuption);
                cmd.Parameters.AddWithValue("@APMother", _addenqmasters.APMother);
                cmd.Parameters.AddWithValue("@Apmothermobile", _addenqmasters.Apmothermobile);
                cmd.Parameters.AddWithValue("@ApMotherqualifation", _addenqmasters.ApMotherqualifation);
                cmd.Parameters.AddWithValue("@Apmotheroccuption", _addenqmasters.Apmotheroccuption);
                cmd.Parameters.AddWithValue("@Apmotherdob", _addenqmasters.Apmotherdob);
                cmd.Parameters.AddWithValue("@Apdoanniversary", _addenqmasters.Apdoanniversary);
                cmd.Parameters.AddWithValue("@Apemailid", _addenqmasters.Apemailid);
                cmd.Parameters.AddWithValue("@Fonameofschool", _addenqmasters.Fonameofschool);
                cmd.Parameters.AddWithValue("@Lastexamgiven", _addenqmasters.Lastexamgiven);
                cmd.Parameters.AddWithValue("@FoYear", _addenqmasters.FoYear);
                cmd.Parameters.AddWithValue("@Fostatus", _addenqmasters.Fostatus);
                cmd.Parameters.AddWithValue("@Marks", _addenqmasters.Marks);
                cmd.Parameters.AddWithValue("@BoardUniversity", _addenqmasters.BoardUniversity);
                cmd.Parameters.AddWithValue("@DateofEnquiry", _addenqmasters.DateofEnquiry);
                cmd.Parameters.AddWithValue("@ProspectusNo", _addenqmasters.ProspectusNo);
                cmd.Parameters.AddWithValue("@Admissionformno", _addenqmasters.Admissionformno);
                cmd.Parameters.AddWithValue("@Prospectusfees", _addenqmasters.Prospectusfees);
                cmd.Parameters.AddWithValue("@Registrationfees", _addenqmasters.Registrationfees);
                cmd.Parameters.AddWithValue("@Remarks", _addenqmasters.Remarks);
                cmd.Parameters.AddWithValue("@Createddate", _addenqmasters.Createddate);
                cmd.Parameters.AddWithValue("@CreatedBy", _addenqmasters.CreatedBy);
                cmd.Parameters.AddWithValue("@Modifieddate", _addenqmasters.Modifieddate);
                cmd.Parameters.AddWithValue("@Modifiedby", _addenqmasters.Modifiedby);
                cmd.Parameters.AddWithValue("@FMid", _addenqmasters.FMid);
                cmd.Parameters.AddWithValue("@OMID", _addenqmasters.OMID);
                cmd.Parameters.AddWithValue("@IsActive", _addenqmasters.IsActive);
                result = Convert.ToInt32(DBHelper.ExecuteNonQuery(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddAdmissionEnquiry)", "Error_014", ex, "UpdateUser");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }

        public int UPDAdmissionEnquiry(AdmissionEnquiryMaster _addenqmasters)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet _ds = new DataSet();
                cmd.CommandText = "SP_UPDAdmissionEnquiry";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AMID", _addenqmasters.AMID);
                cmd.Parameters.AddWithValue("@AEnquiryNo", _addenqmasters.AEnquiryNo);
                cmd.Parameters.AddWithValue("@AClassName", _addenqmasters.AClassName);
                cmd.Parameters.AddWithValue("@AStudentName", _addenqmasters.AStudentName);
                cmd.Parameters.AddWithValue("@AGendar", _addenqmasters.AGendar);
                cmd.Parameters.AddWithValue("@ACont_Address", _addenqmasters.ACont_Address);
                cmd.Parameters.AddWithValue("@ACont_Address2", _addenqmasters.ACont_Address2);
                cmd.Parameters.AddWithValue("@ACity", _addenqmasters.ACity);
                cmd.Parameters.AddWithValue("@AContactno", _addenqmasters.AContactno);
                cmd.Parameters.AddWithValue("@ADOB", (_addenqmasters.ADOB));
                cmd.Parameters.AddWithValue("@APdfather", _addenqmasters.APdfather);
                cmd.Parameters.AddWithValue("@APdFathermobile", _addenqmasters.APdFathermobile);
                cmd.Parameters.AddWithValue("@APQualification", _addenqmasters.APQualification);
                cmd.Parameters.AddWithValue("@APFatherdob", (_addenqmasters.APFatherdob));
                cmd.Parameters.AddWithValue("@APFatheroccuption", _addenqmasters.APFatheroccuption);
                cmd.Parameters.AddWithValue("@APMother", _addenqmasters.APMother);
                cmd.Parameters.AddWithValue("@Apmothermobile", _addenqmasters.Apmothermobile);
                cmd.Parameters.AddWithValue("@ApMotherqualifation", _addenqmasters.ApMotherqualifation);
                cmd.Parameters.AddWithValue("@Apmotheroccuption", _addenqmasters.Apmotheroccuption);
                cmd.Parameters.AddWithValue("@Apmotherdob", (_addenqmasters.Apmotherdob));
                cmd.Parameters.AddWithValue("@Apdoanniversary", (_addenqmasters.Apdoanniversary));
                cmd.Parameters.AddWithValue("@Apemailid", _addenqmasters.Apemailid);
                cmd.Parameters.AddWithValue("@Fonameofschool", _addenqmasters.Fonameofschool);
                cmd.Parameters.AddWithValue("@Lastexamgiven", _addenqmasters.Lastexamgiven);
                cmd.Parameters.AddWithValue("@FoYear", _addenqmasters.FoYear);
                cmd.Parameters.AddWithValue("@Fostatus", _addenqmasters.Fostatus);
                cmd.Parameters.AddWithValue("@Marks", _addenqmasters.Marks);
                cmd.Parameters.AddWithValue("@BoardUniversity", _addenqmasters.BoardUniversity);
                cmd.Parameters.AddWithValue("@DateofEnquiry",(_addenqmasters.DateofEnquiry));
                cmd.Parameters.AddWithValue("@ProspectusNo", _addenqmasters.ProspectusNo);
                cmd.Parameters.AddWithValue("@Admissionformno", _addenqmasters.Admissionformno);
                cmd.Parameters.AddWithValue("@Prospectusfees", _addenqmasters.Prospectusfees);
                cmd.Parameters.AddWithValue("@Registrationfees", _addenqmasters.Registrationfees);
                cmd.Parameters.AddWithValue("@Remarks", _addenqmasters.Remarks);
                cmd.Parameters.AddWithValue("@Createddate", _addenqmasters.Createddate);
                cmd.Parameters.AddWithValue("@CreatedBy", _addenqmasters.CreatedBy);
                cmd.Parameters.AddWithValue("@Modifieddate", _addenqmasters.Modifieddate);
                cmd.Parameters.AddWithValue("@Modifiedby", _addenqmasters.Modifiedby);
                cmd.Parameters.AddWithValue("@FMid", _addenqmasters.FMid);
                cmd.Parameters.AddWithValue("@OMID", _addenqmasters.OMID);
                cmd.Parameters.AddWithValue("@IsActive", _addenqmasters.IsActive);
                result = Convert.ToInt32(DBHelper.ExecuteNonQuery(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(UPDAdmissionEnquiry)", "Error_014", ex, "UpdateUser");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }

        public void DeleteAdmissionEnquiry(AdmissionEnquiryMaster _addenqmasters)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet _ds = new DataSet();
                cmd.CommandText = "SP_DeleteAdmissionEnquiry";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AMID", _addenqmasters.AMID);
                cmd.Parameters.AddWithValue("@IsActive", _addenqmasters.IsActive);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(DeleteAdmissionEnquiry)", "Error_014", ex, "DeleteAdmissionEnquiry");
            }
            finally
            {
                cmd.Dispose();
            }

        }
        public List<AdmissionEnquiryMaster> GetAdmissionenquerylist(string multipleenqueryid)
        {
            List<AdmissionEnquiryMaster> _enqlist = new List<AdmissionEnquiryMaster>();
            DataSet ds = new DataSet();//SP_GetAdmissionEnquiry
            SqlCommand cmd = new SqlCommand("USP_GetAdmissionenquerylist");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@multipleenqueryid", multipleenqueryid);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        AdmissionEnquiryMaster obj = new AdmissionEnquiryMaster();
                        obj.AMID = Convert.ToInt32(ds.Tables[0].Rows[l]["AMID"]);
                        obj.AEnquiryNo = Convert.ToString(ds.Tables[0].Rows[l]["AEnquiryNo"]);
                        obj.AClassName = Convert.ToString(ds.Tables[0].Rows[l]["AClassName"]);
                        obj.AStudentName = Convert.ToString(ds.Tables[0].Rows[l]["AStudentName"]);
                        obj.AGendar = Convert.ToString(ds.Tables[0].Rows[l]["AGendar"]);
                        obj.ACont_Address = Convert.ToString(ds.Tables[0].Rows[l]["ACont_Address"]);
                        obj.ACont_Address2 = Convert.ToString(ds.Tables[0].Rows[l]["ACont_Address2"]);
                        obj.ACity = Convert.ToString(ds.Tables[0].Rows[l]["ACity"]);
                        obj.AContactno = Convert.ToString(ds.Tables[0].Rows[l]["AContactno"]);
                        obj.ADOB = Convert.ToString(ds.Tables[0].Rows[l]["ADOB"]);
                        obj.APdfather = Convert.ToString(ds.Tables[0].Rows[l]["APdfather"]);
                        obj.APdFathermobile = Convert.ToString(ds.Tables[0].Rows[l]["APdFathermobile"]);
                        obj.APQualification = Convert.ToString(ds.Tables[0].Rows[l]["APQualification"]);
                        obj.APFatherdob = Convert.ToString(ds.Tables[0].Rows[l]["APFatherdob"]);
                        obj.APFatheroccuption = Convert.ToString(ds.Tables[0].Rows[l]["APFatheroccuption"]);
                        obj.APMother = Convert.ToString(ds.Tables[0].Rows[l]["APMother"]);
                        obj.Apmothermobile = Convert.ToString(ds.Tables[0].Rows[l]["Apmothermobile"]);
                        obj.ApMotherqualifation = Convert.ToString(ds.Tables[0].Rows[l]["ApMotherqualifation"]);
                        obj.Apmotheroccuption = Convert.ToString(ds.Tables[0].Rows[l]["Apmotheroccuption"]);
                        obj.Apmotherdob = Convert.ToString(ds.Tables[0].Rows[l]["Apmotherdob"]);
                        obj.Apdoanniversary = Convert.ToString(ds.Tables[0].Rows[l]["Apdoanniversary"]);
                        obj.Apemailid = Convert.ToString(ds.Tables[0].Rows[l]["Apemailid"]);
                        obj.Fonameofschool = Convert.ToString(ds.Tables[0].Rows[l]["Fonameofschool"]);
                        obj.Lastexamgiven = Convert.ToString(ds.Tables[0].Rows[l]["Lastexamgiven"]);
                        obj.FoYear = Convert.ToString(ds.Tables[0].Rows[l]["FoYear"]);
                        obj.Fostatus = Convert.ToString(ds.Tables[0].Rows[l]["Fostatus"]);
                        obj.Marks = Convert.ToString(ds.Tables[0].Rows[l]["Marks"]);
                        obj.BoardUniversity = Convert.ToString(ds.Tables[0].Rows[l]["BoardUniversity"]);
                        obj.DateofEnquiry = Convert.ToString(ds.Tables[0].Rows[l]["DateofEnquiry"]);
                        obj.ProspectusNo = Convert.ToString(ds.Tables[0].Rows[l]["ProspectusNo"]);
                        obj.Admissionformno = Convert.ToString(ds.Tables[0].Rows[l]["Admissionformno"]);
                        obj.Prospectusfees = Convert.ToDouble(ds.Tables[0].Rows[l]["Prospectusfees"]);
                        obj.Registrationfees = Convert.ToDouble(ds.Tables[0].Rows[l]["Registrationfees"]);
                        obj.Remarks = Convert.ToString(ds.Tables[0].Rows[l]["Remarks"]);
                        obj.Createddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createddate"]);
                        obj.CreatedBy = Convert.ToString(ds.Tables[0].Rows[l]["CreatedBy"]);
                        obj.Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]);
                        obj.Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]);
                        obj.FMid = Convert.ToInt32(ds.Tables[0].Rows[l]["FMid"]);
                        obj.OMID = Convert.ToInt32(ds.Tables[0].Rows[l]["OMID"]);
                        obj.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]);
                        _enqlist.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAdmissionEnquiry)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _enqlist;
        }

        public AdmissionEnquiryMaster GetAdmissionEnquiry(int AMID)
        {
            AdmissionEnquiryMaster obj = new AdmissionEnquiryMaster();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetAdmissionEnquiry");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AMID", AMID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.AMID = Convert.ToInt32(ds.Tables[0].Rows[l]["AMID"]);
                        obj.AEnquiryNo = Convert.ToString(ds.Tables[0].Rows[l]["AEnquiryNo"]);
                        obj.AClassName = Convert.ToString(ds.Tables[0].Rows[l]["AClassName"]);
                        obj.AStudentName = Convert.ToString(ds.Tables[0].Rows[l]["AStudentName"]);
                        obj.AGendar = Convert.ToString(ds.Tables[0].Rows[l]["AGendar"]);
                        obj.ACont_Address = Convert.ToString(ds.Tables[0].Rows[l]["ACont_Address"]);
                        obj.ACont_Address2 = Convert.ToString(ds.Tables[0].Rows[l]["ACont_Address2"]);
                        obj.ACity = Convert.ToString(ds.Tables[0].Rows[l]["ACity"]);
                        obj.AContactno = Convert.ToString(ds.Tables[0].Rows[l]["AContactno"]);
                        obj.ADOB = Convert.ToString(ds.Tables[0].Rows[l]["ADOB"]);
                        obj.APdfather = Convert.ToString(ds.Tables[0].Rows[l]["APdfather"]);
                        obj.APdFathermobile = Convert.ToString(ds.Tables[0].Rows[l]["APdFathermobile"]);
                        obj.APQualification = Convert.ToString(ds.Tables[0].Rows[l]["APQualification"]);
                        obj.APFatherdob = Convert.ToString(ds.Tables[0].Rows[l]["APFatherdob"]);
                        obj.APFatheroccuption = Convert.ToString(ds.Tables[0].Rows[l]["APFatheroccuption"]);
                        obj.APMother = Convert.ToString(ds.Tables[0].Rows[l]["APMother"]);
                        obj.Apmothermobile = Convert.ToString(ds.Tables[0].Rows[l]["Apmothermobile"]);
                        obj.ApMotherqualifation = Convert.ToString(ds.Tables[0].Rows[l]["ApMotherqualifation"]);
                        obj.Apmotheroccuption = Convert.ToString(ds.Tables[0].Rows[l]["Apmotheroccuption"]);
                        obj.Apmotherdob = Convert.ToString(ds.Tables[0].Rows[l]["Apmotherdob"]);
                        obj.Apdoanniversary = Convert.ToString(ds.Tables[0].Rows[l]["Apdoanniversary"]);
                        obj.Apemailid = Convert.ToString(ds.Tables[0].Rows[l]["Apemailid"]);
                        obj.Fonameofschool = Convert.ToString(ds.Tables[0].Rows[l]["Fonameofschool"]);
                        obj.Lastexamgiven = Convert.ToString(ds.Tables[0].Rows[l]["Lastexamgiven"]);
                        obj.FoYear = Convert.ToString(ds.Tables[0].Rows[l]["FoYear"]);
                        obj.Fostatus = Convert.ToString(ds.Tables[0].Rows[l]["Fostatus"]);
                        obj.Marks = Convert.ToString(ds.Tables[0].Rows[l]["Marks"]);
                        obj.BoardUniversity = Convert.ToString(ds.Tables[0].Rows[l]["BoardUniversity"]);
                        obj.DateofEnquiry = Convert.ToString(ds.Tables[0].Rows[l]["DateofEnquiry"]);
                        obj.ProspectusNo = Convert.ToString(ds.Tables[0].Rows[l]["ProspectusNo"]);
                        obj.Admissionformno = Convert.ToString(ds.Tables[0].Rows[l]["Admissionformno"]);
                        obj.Prospectusfees = Convert.ToDouble(ds.Tables[0].Rows[l]["Prospectusfees"]);
                        obj.Registrationfees = Convert.ToDouble(ds.Tables[0].Rows[l]["Registrationfees"]);
                        obj.Remarks = Convert.ToString(ds.Tables[0].Rows[l]["Remarks"]);
                        obj.Createddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createddate"]);
                        obj.CreatedBy = Convert.ToString(ds.Tables[0].Rows[l]["CreatedBy"]);
                        obj.Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]);
                        obj.Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]);
                        obj.FMid = Convert.ToInt32(ds.Tables[0].Rows[l]["FMid"]);
                        obj.OMID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"]);
                        obj.IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]);

                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAdmissionEnquiry)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public string GetEmpCodeId(int SchoolId)
        {
            string _results = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "GetMaxEmployee";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
                _results = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetMaxEmployee)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }

        public string GetEnquiryNO(int OMID)
        {
            string _results = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_GetEnquiryNO";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", OMID);
                _results = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetEnquiryNO)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }

        public SessionInfo GetSessionDetails(string Username, Nullable<DateTime> Currentdate)
        {
            SessionInfo obj = new SessionInfo();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_GetSessionDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", Username);
                cmd.Parameters.AddWithValue("@Checkdate", Currentdate);
                var reader = DBHelper.ExecuteReader(cmd);
                {
                    while (reader.Read())
                    {
                        obj.FinancialYear = DBNull.Value != reader["FinancialYear"] ? (string)reader["FinancialYear"] : default(string);
                        obj.FinancialYearID = DBNull.Value != reader["FinancialID"] ? (int)reader["FinancialID"] : default(int);
                        obj.SchoolID = DBNull.Value != reader["SchoolID"] ? (int)reader["SchoolID"] : default(int);
                        obj.SchoolName = DBNull.Value != reader["SchoolName"] ? (string)reader["SchoolName"] : default(string);
                        obj.UserType = DBNull.Value != reader["UserType"] ? (int)reader["UserType"] : default(int);
                        obj.UserTypeBaseID = DBNull.Value != reader["UserTypeBaseID"] ? (int)reader["UserTypeBaseID"] : default(int);
                        obj.SchoolLogo = DBNull.Value != reader["SchoolLogo"] ? (string)reader["SchoolLogo"] : default(string);
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSessionDetails)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public DataSet GetAdmissionEnquiryTblList(Nullable<int> classid, Nullable<DateTime> Startdate, Nullable<DateTime> Enddate, int FMID, int OMID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetAdmissionEnquiryTblList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classid", classid);
                cmd.Parameters.AddWithValue("@Startdate", Startdate);
                cmd.Parameters.AddWithValue("@Enddate", Enddate);
                cmd.Parameters.AddWithValue("@FMID", FMID);
                cmd.Parameters.AddWithValue("@SchoolID", OMID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAdmissionEnquiryTblList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds;
        }
        public List<AdmissionEnquiryMaster> GetAdmissionEnquiryList(int FMID, int OMID)
        {
            List<AdmissionEnquiryMaster> obj = new List<AdmissionEnquiryMaster>();
            SqlCommand cmd = new SqlCommand("SP_GetAdmissionEnquiryList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FMID", FMID);
                cmd.Parameters.AddWithValue("@SchoolID", OMID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new AdmissionEnquiryMaster
                        {
                            AMID = Convert.ToInt32(ds.Tables[0].Rows[l]["AMID"]),
                            AEnquiryNo = Convert.ToString(ds.Tables[0].Rows[l]["AEnquiryNo"]),
                            AClassName = Convert.ToString(ds.Tables[0].Rows[l]["AClassName"]),
                            AStudentName = Convert.ToString(ds.Tables[0].Rows[l]["AStudentName"]),
                            AGendar = Convert.ToString(ds.Tables[0].Rows[l]["AGendar"]),
                            ACont_Address = Convert.ToString(ds.Tables[0].Rows[l]["ACont_Address"]),
                            ACont_Address2 = Convert.ToString(ds.Tables[0].Rows[l]["ACont_Address2"]),
                            ACity = Convert.ToString(ds.Tables[0].Rows[l]["ACity"]),
                            AContactno = Convert.ToString(ds.Tables[0].Rows[l]["AContactno"]),
                            ADOB = Convert.ToString(ds.Tables[0].Rows[l]["ADOB"]),
                            APdfather = Convert.ToString(ds.Tables[0].Rows[l]["APdfather"]),
                            APdFathermobile = Convert.ToString(ds.Tables[0].Rows[l]["APdFathermobile"]),
                            APQualification = Convert.ToString(ds.Tables[0].Rows[l]["APQualification"]),
                            APFatherdob = Convert.ToString(ds.Tables[0].Rows[l]["APFatherdob"]),
                            APFatheroccuption = Convert.ToString(ds.Tables[0].Rows[l]["APFatheroccuption"]),
                            APMother = Convert.ToString(ds.Tables[0].Rows[l]["APMother"]),
                            Apmothermobile = Convert.ToString(ds.Tables[0].Rows[l]["Apmothermobile"]),
                            ApMotherqualifation = Convert.ToString(ds.Tables[0].Rows[l]["ApMotherqualifation"]),
                            Apmotheroccuption = Convert.ToString(ds.Tables[0].Rows[l]["Apmotheroccuption"]),
                            Apmotherdob = Convert.ToString(ds.Tables[0].Rows[l]["Apmotherdob"]),
                            Apdoanniversary = Convert.ToString(ds.Tables[0].Rows[l]["Apdoanniversary"]),
                            Apemailid = Convert.ToString(ds.Tables[0].Rows[l]["Apemailid"]),
                            Fonameofschool = Convert.ToString(ds.Tables[0].Rows[l]["Fonameofschool"]),
                            Lastexamgiven = Convert.ToString(ds.Tables[0].Rows[l]["Lastexamgiven"]),
                            FoYear = Convert.ToString(ds.Tables[0].Rows[l]["FoYear"]),
                            Fostatus = Convert.ToString(ds.Tables[0].Rows[l]["Fostatus"]),
                            Marks = Convert.ToString(ds.Tables[0].Rows[l]["Marks"]),
                            BoardUniversity = Convert.ToString(ds.Tables[0].Rows[l]["BoardUniversity"]),
                            DateofEnquiry = Convert.ToString(ds.Tables[0].Rows[l]["DateofEnquiry"]),
                            ProspectusNo = Convert.ToString(ds.Tables[0].Rows[l]["ProspectusNo"]),
                            Admissionformno = Convert.ToString(ds.Tables[0].Rows[l]["Admissionformno"]),
                            Prospectusfees = Convert.ToDouble(ds.Tables[0].Rows[l]["Prospectusfees"]),
                            Registrationfees = Convert.ToDouble(ds.Tables[0].Rows[l]["Registrationfees"]),
                            Remarks = Convert.ToString(ds.Tables[0].Rows[l]["Remarks"]),
                            Createddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createddate"]),
                            CreatedBy = Convert.ToString(ds.Tables[0].Rows[l]["CreatedBy"]),
                            Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]),
                            Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]),
                            FMid = Convert.ToInt32(ds.Tables[0].Rows[l]["FMid"]),
                            OMID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetGendarList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<SectionMaster> GetSectionList(int SchoolID, int CMid)
        {
            List<SectionMaster> obj = new List<SectionMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_SectionList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@CMid", CMid);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new SectionMaster
                        {
                            Secmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Secmid"]),
                            SectionName = Convert.ToString(ds.Tables[0].Rows[l]["SectionName"]),
                            SchoolId = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolId"]),
                            CMid = Convert.ToInt32(ds.Tables[0].Rows[l]["CMid"]),
                            //  IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSectionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public DataTable GetSectionListTBL(int SchoolID, int CMid)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_SectionList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@CMid", CMid);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSectionListTBL)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public List<DepartmentMaster> GetDepartmentList(int SchoolID)
        {
            List<DepartmentMaster> obj = new List<DepartmentMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetDepartmentList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new DepartmentMaster
                        {
                            DEPARTMENT_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["DEPARTMENT_ID"]),
                            DEPARTMENTNAME = Convert.ToString(ds.Tables[0].Rows[l]["DEPARTMENTNAME"]),
                            DEPARTMENTDESC = Convert.ToString(ds.Tables[0].Rows[l]["DEPARTMENTDESC"]),
                            REMARKS = Convert.ToString(ds.Tables[0].Rows[l]["REMARKS"]),
                            ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[l]["ISACTIVE"]),
                            CREATEDBY = Convert.ToString(ds.Tables[0].Rows[l]["CREATEDBY"]),
                            CREATEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["CREATEDDATE"]),
                            MODIFIEDBY = Convert.ToString(ds.Tables[0].Rows[l]["MODIFIEDBY"]),
                            MODIFIEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["MODIFIEDDATE"]),
                            SchoolID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetDepartmentList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<DesignationMaster> GetDesignationList(int SchoolID)
        {
            List<DesignationMaster> obj = new List<DesignationMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetDesignationList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new DesignationMaster
                        {
                            DESIGNATION_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["DESIGNATION_ID"]),
                            DESIGNATIONNAME = Convert.ToString(ds.Tables[0].Rows[l]["DESIGNATIONNAME"]),
                            DESIGNATIONDESC = Convert.ToString(ds.Tables[0].Rows[l]["DESIGNATIONDESC"]),
                            REMARKS = Convert.ToString(ds.Tables[0].Rows[l]["REMARKS"]),
                            ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[l]["ISACTIVE"]),
                            CREATEDBY = Convert.ToString(ds.Tables[0].Rows[l]["CREATEDBY"]),
                            CREATEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["CREATEDDATE"]),
                            MODIFIEDBY = Convert.ToString(ds.Tables[0].Rows[l]["MODIFIEDBY"]),
                            MODIFIEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[l]["MODIFIEDDATE"]),
                            SchoolID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetDepartmentList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<ReligionMaster> GetReligionList(int SchoolID)
        {
            List<ReligionMaster> obj = new List<ReligionMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetReligionList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new ReligionMaster
                        {
                            Relmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Relmid"]),
                            ReligionName = Convert.ToString(ds.Tables[0].Rows[l]["ReligionName"]),
                            SchoolId = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolId"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetReligionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<CategoryMaster> GetCategoryMasterList(int SchoolID)
        {
            List<CategoryMaster> obj = new List<CategoryMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetCategoryMasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new CategoryMaster
                        {
                            Catmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Catmid"]),
                            CatName = Convert.ToString(ds.Tables[0].Rows[l]["CatName"]),
                            SchoolId = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolId"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetReligionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }


        public List<MartialStatusMaster> GetMartialStatusList(int SchoolID)
        {
            List<MartialStatusMaster> obj = new List<MartialStatusMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetMartialStatusList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new MartialStatusMaster
                        {
                            Mrmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Mrmid"]),
                            StatusName = Convert.ToString(ds.Tables[0].Rows[l]["StatusName"]),
                            SchoolID = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolID"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetReligionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<CastMaster> GetCastList(int SchoolID)
        {
            List<CastMaster> obj = new List<CastMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetCastList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new CastMaster
                        {
                            Castmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Castmid"]),
                            CastName = Convert.ToString(ds.Tables[0].Rows[l]["CastName"]),
                            SchoolId = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolId"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetCastList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<RouteMaster> GetRouteList(int SchoolID)
        {
            List<RouteMaster> obj = new List<RouteMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetRouteList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new RouteMaster
                        {
                            Routemid = Convert.ToInt32(ds.Tables[0].Rows[l]["Routemid"]),
                            RouteName = Convert.ToString(ds.Tables[0].Rows[l]["RouteName"]),
                            SchoolId = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolId"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetRouteList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<BusMaster> GetBuslist(int SchoolID)
        {
            List<BusMaster> obj = new List<BusMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_GetBuslist");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new BusMaster
                        {
                            Busmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Busmid"]),
                            BusName = Convert.ToString(ds.Tables[0].Rows[l]["BusName"]),
                            SchoolId = Convert.ToInt32(ds.Tables[0].Rows[l]["SchoolId"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetRouteList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public OragnisationMaster GetOragnisationDetails(int OMID)
        {
            OragnisationMaster _lst = new OragnisationMaster();
            try
            {
                cmd = new SqlCommand("SP_Orgnisationmaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OMID", OMID);
                cmd.Parameters.AddWithValue("@Otype", "GET");
                var reader = DBHelper.ExecuteReader(cmd);
                {
                    while (reader.Read())
                    {
                        _lst.OMID = DBNull.Value != reader["OMID"] ? (int)reader["OMID"] : default(int);
                        _lst.Oname = DBNull.Value != reader["Oname"] ? (string)reader["Oname"] : default(string);
                        _lst.BOAddress = DBNull.Value != reader["BOAddress"] ? (string)reader["BOAddress"] : default(string);
                        _lst.BOAddress2 = DBNull.Value != reader["BOAddress2"] ? (string)reader["BOAddress2"] : default(string);
                        _lst.BOCity = DBNull.Value != reader["BOCity"] ? (int)reader["BOCity"] : default(int);
                        _lst.BOPincode = DBNull.Value != reader["BOPincode"] ? (string)reader["BOPincode"] : default(string);
                        _lst.LCountry = DBNull.Value != reader["LCountry"] ? (int)reader["LCountry"] : default(int);
                        _lst.LState = DBNull.Value != reader["LState"] ? (int)reader["LState"] : default(int);
                        _lst.LDistict = DBNull.Value != reader["LDistict"] ? (string)reader["LDistict"] : default(string);
                        _lst.LArea = DBNull.Value != reader["LArea"] ? (string)reader["LArea"] : default(string);
                        _lst.LEmailId = DBNull.Value != reader["LEmailId"] ? (string)reader["LEmailId"] : default(string);
                        _lst.LMobile = DBNull.Value != reader["LMobile"] ? (string)reader["LMobile"] : default(string);
                        _lst.LPhone = DBNull.Value != reader["LPhone"] ? (string)reader["LPhone"] : default(string);
                        _lst.LWebsite = DBNull.Value != reader["LWebsite"] ? (string)reader["LWebsite"] : default(string);
                        _lst.OAfficilate = DBNull.Value != reader["OAfficilate"] ? (string)reader["OAfficilate"] : default(string);
                        _lst.OlicNo = DBNull.Value != reader["OlicNo"] ? (string)reader["OlicNo"] : default(string);
                        _lst.OTaxNo = DBNull.Value != reader["OTaxNo"] ? (string)reader["OTaxNo"] : default(string);
                        _lst.OPanNo = DBNull.Value != reader["OPanNo"] ? (string)reader["OPanNo"] : default(string);
                        _lst.OContactNo = DBNull.Value != reader["OContactNo"] ? (string)reader["OContactNo"] : default(string);
                        _lst.IsActive = DBNull.Value != reader["IsActive"] ? (bool)reader["IsActive"] : default(bool);
                        _lst.Createddate = DBNull.Value != reader["Createddate"] ? (DateTime)reader["Createddate"] : default(DateTime);
                        _lst.CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string);
                        _lst.Modifieddate = DBNull.Value != reader["Modifieddate"] ? (DateTime)reader["Modifieddate"] : default(DateTime);
                        _lst.ModifiedBy = DBNull.Value != reader["ModifiedBy"] ? (string)reader["ModifiedBy"] : default(string);
                        _lst.OrgImage = DBNull.Value != reader["OrgImage"] ? (string)reader["OrgImage"] : default(string);
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(OragnasitionBasicopation)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }
        #region Holiday Master
        public string Applicationfprmano(int Schoolid, int Classid, int Section, int Financialyear)
        {
            string _applicationno = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_Applicationformno";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Schoolid", Schoolid);
                cmd.Parameters.AddWithValue("@Classid", Classid);
                cmd.Parameters.AddWithValue("@Section", Section);
                cmd.Parameters.AddWithValue("@Financialyear", Financialyear);
                _applicationno = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Applicationfprmano)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _applicationno;
        }
        public string SaveHolidayDetails(HolidayMaster obj)
        {
            string _results = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_HolidayMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HolidayDate", obj.HolidayDate);
                cmd.Parameters.AddWithValue("@Student", obj.Student);
                cmd.Parameters.AddWithValue("@Staff", obj.Staff);
                cmd.Parameters.AddWithValue("@CreatedBy", obj.CreatedBy);
                cmd.Parameters.AddWithValue("@UpdatedBy", obj.UpdatedBy);
                cmd.Parameters.AddWithValue("@SchoolID", obj.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                cmd.Parameters.AddWithValue("@Action", "INS");
                _results = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetFinancialSchoolID)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        public string UpdateHolidayDetails(HolidayMaster obj)
        {
            string _results = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_HolidayMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Hid", obj.Hid);
                cmd.Parameters.AddWithValue("@HolidayDate", obj.HolidayDate);
                cmd.Parameters.AddWithValue("@Student", obj.Student);
                cmd.Parameters.AddWithValue("@Staff", obj.Staff);
                cmd.Parameters.AddWithValue("@UpdatedBy", obj.UpdatedBy);
                cmd.Parameters.AddWithValue("@SchoolID", obj.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                cmd.Parameters.AddWithValue("@Action", "UPD");
                _results = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetFinancialSchoolID)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        public string DeleteHolidayDetails(int Hid, int _OrgnisationID, int _Financialyearid)
        {
            string _results = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "SP_HolidayMaster";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Hid", Hid);
                cmd.Parameters.AddWithValue("@SchoolID", _OrgnisationID);
                cmd.Parameters.AddWithValue("@FinancialYear", _Financialyearid);
                cmd.Parameters.AddWithValue("@Action", "DEL");
                _results = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetFinancialSchoolID)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        public List<HolidayMaster> GetHolidaylist(int SchoolID, int FinancialYear)
        {
            List<HolidayMaster> _lst = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_HolidayMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@Action", "GET");
                _lst = new List<HolidayMaster>();

                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new HolidayMaster
                    {
                        Hid = DBNull.Value != reader["Hid"] ? (int)reader["Hid"] : default(int),
                        HolidayDate = DBNull.Value != reader["HolidayDate"] ? (string)Convert.ToDateTime(reader["HolidayDate"]).ToString("dd/MMM/yyyy") : default(string),
                        Staff = DBNull.Value != reader["Staff"] ? (bool)reader["Staff"] : default(bool),
                        Student = DBNull.Value != reader["Student"] ? (bool)reader["Student"] : default(bool),
                        CreatedDate = DBNull.Value != reader["CreatedDate"] ? (string)Convert.ToString(reader["CreatedDate"]) : default(string),
                        CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string),
                        UpdatedDate = DBNull.Value != reader["UpdatedDate"] ? (string)Convert.ToString(reader["UpdatedDate"]) : default(string),
                        UpdatedBy = DBNull.Value != reader["UpdatedBy"] ? (string)reader["UpdatedBy"] : default(string),
                    });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetRouteList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }
        public HolidayMaster GetHolidayDetails(int Hid, int SchoolID, int FinancialYear)
        {
            HolidayMaster _lst = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_HolidayMaster");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Hid", Hid);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@Action", "GET");
                _lst = new HolidayMaster();

                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Hid = DBNull.Value != reader["Hid"] ? (int)reader["Hid"] : default(int);
                    _lst.HolidayDate = DBNull.Value != reader["HolidayDate"] ? (string)Convert.ToDateTime(reader["HolidayDate"]).ToString("dd/MMM/yyyy") : default(string);
                    _lst.Staff = DBNull.Value != reader["Staff"] ? (bool)reader["Staff"] : default(bool);
                    _lst.Student = DBNull.Value != reader["Student"] ? (bool)reader["Student"] : default(bool);
                    _lst.CreatedDate = DBNull.Value != reader["CreatedDate"] ? (string)Convert.ToString(reader["CreatedDate"]) : default(string);
                    _lst.CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string);
                    _lst.UpdatedDate = DBNull.Value != reader["UpdatedDate"] ? (string)Convert.ToString(reader["UpdatedDate"]) : default(string);
                    _lst.UpdatedBy = DBNull.Value != reader["UpdatedBy"] ? (string)reader["UpdatedBy"] : default(string);
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetRouteList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }
        #endregion Holiday Master
        #region ResetPassword
        public bool SetOTP(string UserName, string OTP)
        {
            bool _results = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "updateUserOTP";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@OTP", OTP);
                _results = DBHelper.ExecuteNonQuery(cmd) == 0 ? false : true;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(setOTP)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        #endregion ResetPassword

        #region Leave
        public List<Leave> GetLeaveReport(Leave obj)
        {
            List<Leave> _lst = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("USP_LEAVE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", obj.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                cmd.Parameters.AddWithValue("@SenderID", obj.SenderID);
                cmd.Parameters.AddWithValue("@ApproverID", obj.ApproverID);
                cmd.Parameters.AddWithValue("@Action", obj.Action);
                _lst = new List<Leave>();

                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new Leave
                    {
                        Lid = DBNull.Value != reader["Lid"] ? (int)reader["Lid"] : default(int),
                        SenderID = DBNull.Value != reader["SenderID"] ? (int)reader["SenderID"] : default(int),
                        SenderName = DBNull.Value != reader["SenderName"] ? (string)reader["SenderName"] : default(string),
                        SenderType = DBNull.Value != reader["SenderType"] ? (string)reader["SenderType"] : default(string),
                        ApproverID = DBNull.Value != reader["ApproverID"] ? (int)reader["ApproverID"] : default(int),
                        ApproverName = DBNull.Value != reader["ApproverName"] ? (string)reader["ApproverName"] : default(string),
                        ApproverType = DBNull.Value != reader["ApproverType"] ? (string)reader["ApproverType"] : default(string),
                        HalfDay = DBNull.Value != reader["HalfDay"] ? (bool)reader["HalfDay"] : default(bool),
                        LeaveType = DBNull.Value != reader["LeaveType"] ? (string)reader["LeaveType"] : default(string),
                        LeaveFrom = DBNull.Value != reader["LeaveFrom"] ? (string)reader["LeaveFrom"] : default(string),
                        LeaveTo = DBNull.Value != reader["LeaveTo"] ? (string)reader["LeaveTo"] : default(string),
                        TotalLeaves = DBNull.Value != reader["TotalLeaves"] ? (int)reader["TotalLeaves"] : default(int),
                        LeaveStatus = DBNull.Value != reader["LeaveStatus"] ? (string)reader["LeaveStatus"] : default(string),
                        SenderRemark = DBNull.Value != reader["SenderRemark"] ? (string)reader["SenderRemark"] : default(string),
                        ApproverRemark = DBNull.Value != reader["ApproverRemark"] ? (string)reader["ApproverRemark"] : default(string),
                        SchoolID = DBNull.Value != reader["SchoolID"] ? (int)reader["SchoolID"] : default(int),
                        FinancialYear = DBNull.Value != reader["FinancialYear"] ? (int)reader["FinancialYear"] : default(int),
                        CreatedDate = DBNull.Value != reader["CreatedDate"] ? Convert.ToString(reader["CreatedDate"]) : default(string),
                        UpdatedDate = DBNull.Value != reader["UpdatedDate"] ? Convert.ToString(reader["UpdatedDate"]) : default(string),
                        Action = obj.Action,
                    });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetLeaveReport)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }

        public bool InsertUpdateLeaveRequest(Leave obj)
        {
            bool _results = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "USP_LEAVE";
                DataTable _mstmenupermission = new DataTable();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Lid", obj.Lid);
                cmd.Parameters.AddWithValue("@SenderID", obj.SenderID);
                cmd.Parameters.AddWithValue("@SenderName", obj.SenderName);
                cmd.Parameters.AddWithValue("@SenderType", obj.SenderType);
                cmd.Parameters.AddWithValue("@ApproverID", obj.ApproverID);
                cmd.Parameters.AddWithValue("@ApproverName", obj.ApproverName);
                cmd.Parameters.AddWithValue("@ApproverType", obj.ApproverType);
                cmd.Parameters.AddWithValue("@HalfDay", obj.HalfDay);
                cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                cmd.Parameters.AddWithValue("@LeaveFrom", obj.LeaveFrom);
                cmd.Parameters.AddWithValue("@LeaveTo", obj.LeaveTo);
                cmd.Parameters.AddWithValue("@TotalLeaves", obj.TotalLeaves);
                cmd.Parameters.AddWithValue("@SenderRemark", obj.SenderRemark);
                cmd.Parameters.AddWithValue("@ApproverRemark", obj.ApproverRemark);
                cmd.Parameters.AddWithValue("@LeaveStatus", obj.LeaveStatus);
                cmd.Parameters.AddWithValue("@SchoolID", obj.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                cmd.Parameters.AddWithValue("@Action", obj.Action);
                _results = DBHelper.ExecuteNonQuery(cmd) == 0 ? false : true;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(InsertLeaveRequest)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }

        #endregion Leave
        public int InsertUpdateSession(string UserName, int FinancialID)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_SESSIONRECORD");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@FinancialID", FinancialID);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_Addroutemaster)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public List<FinancialYear> GetFinancialYearList()
        {
            List<FinancialYear> _lst = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("GetFinancialYearList");
                cmd.CommandType = CommandType.StoredProcedure;
                _lst = new List<FinancialYear>();

                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new FinancialYear
                    {
                        FID = DBNull.Value != reader["FMid"] ? (int)reader["FMid"] : default(int),
                        FYear = DBNull.Value != reader["FinancialYear"] ? (string)reader["FinancialYear"] : default(string),
                        SelectedYear = DBNull.Value != reader["SelectedYear"] ? (bool)reader["SelectedYear"] : default(bool),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetLeaveReport)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }
        public GEO GetGeoDetails(string PinCode)
        {
            GEO Obj = new GEO();
            List<CityMaster> _CityList = new List<CityMaster>();
            List<DistrictMaster> _DistrictList = new List<DistrictMaster>();
            List<StateMaster> _StateList = new List<StateMaster>();
            List<CountryMaster> _CountryList = new List<CountryMaster>();
            try
            {
                cmd = new SqlCommand("GETGEODETAILS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PinCode", PinCode);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds.Tables.Count == 5)
                {
                    int i = 1;
                    foreach (string tablename in ds.Tables[0].Rows[0][0].ToString().Split(','))
                    {
                        ds.Tables[i++].TableName = tablename.Trim();
                    }

                    var CityTable = ds.Tables["City"];
                    var DistrictTable = ds.Tables["District"];
                    var StateTable = ds.Tables["State"];
                    var CountryTable = ds.Tables["Country"];
                    for (int C = 0; C < CityTable.Rows.Count; C++)
                    {

                        _CityList.Add(new CityMaster
                        {
                            CITYCODE = Convert.ToString(CityTable.Rows[C]["CITYCODE"]),
                            CITYDESC = Convert.ToString(CityTable.Rows[C]["CITYDESC"]),
                            CITYNAME = Convert.ToString(CityTable.Rows[C]["CITYNAME"]),
                            CITY_ID = Convert.ToInt32(CityTable.Rows[C]["CITY_ID"]),
                            CREATEDBY = Convert.ToString(CityTable.Rows[C]["CREATEDBY"]),
                            CREATEDDATE = Convert.ToDateTime(CityTable.Rows[C]["CREATEDDATE"]),
                            ISACTIVE = Convert.ToBoolean(CityTable.Rows[C]["ISACTIVE"]),
                            MODIFIEDBY = Convert.ToString(CityTable.Rows[C]["MODIFIEDBY"]),
                            MODIFIEDDATE = Convert.ToDateTime(CityTable.Rows[C]["MODIFIEDDATE"]),
                            STATE_ID = Convert.ToInt32(CityTable.Rows[C]["STATE_ID"]),
                        });
                    }
                    for (int D = 0; D < DistrictTable.Rows.Count; D++)
                    {
                        _DistrictList.Add(new DistrictMaster
                        {
                            Active = Convert.ToBoolean(DistrictTable.Rows[D]["Active"]),
                            CREATEDBY = Convert.ToString(DistrictTable.Rows[D]["CREATEDBY"]),
                            MODIFIEDDATE = Convert.ToDateTime(DistrictTable.Rows[D]["MODIFIEDDATE"]),
                            MODIFIEDBY = Convert.ToString(DistrictTable.Rows[D]["MODIFIEDBY"]),
                            CREATEDDATE = Convert.ToDateTime(DistrictTable.Rows[D]["CREATEDDATE"]),
                            DID = Convert.ToInt32(DistrictTable.Rows[D]["DID"]),
                            DistrictCode = Convert.ToString(DistrictTable.Rows[D]["DistrictCode"]),
                            DistrictDesc = Convert.ToString(DistrictTable.Rows[D]["DistrictDesc"]),
                            DistrictName = Convert.ToString(DistrictTable.Rows[D]["DistrictName"]),
                            StateCode = Convert.ToString(DistrictTable.Rows[D]["StateCode"]),
                        });
                    }
                    for (int S = 0; S < StateTable.Rows.Count; S++)
                    {
                        _StateList.Add(new StateMaster
                        {
                            COUNTRY_ID = Convert.ToInt32(StateTable.Rows[S]["COUNTRY_ID"]),
                            CREATEDDATE = Convert.ToDateTime(StateTable.Rows[S]["CREATEDDATE"]),
                            MODIFIEDBY = Convert.ToString(StateTable.Rows[S]["MODIFIEDBY"]),
                            MODIFIEDDATE = Convert.ToDateTime(StateTable.Rows[S]["MODIFIEDDATE"]),
                            CREATEDBY = Convert.ToString(StateTable.Rows[S]["CREATEDBY"]),
                            ISACTIVE = Convert.ToBoolean(StateTable.Rows[S]["ISACTIVE"]),
                            STATECODE = Convert.ToString(StateTable.Rows[S]["STATECODE"]),
                            STATEDESC = Convert.ToString(StateTable.Rows[S]["STATEDESC"]),
                            STATENAME = Convert.ToString(StateTable.Rows[S]["STATENAME"]),
                            STATE_ID = Convert.ToInt32(StateTable.Rows[S]["STATE_ID"]),
                        });
                    }
                    for (int C = 0; C < CountryTable.Rows.Count; C++)
                    {
                        _CountryList.Add(new CountryMaster
                        {
                            ISACTIVE = Convert.ToBoolean(CountryTable.Rows[C]["ISACTIVE"]),
                            CREATEDBY = Convert.ToString(CountryTable.Rows[C]["CREATEDBY"]),
                            MODIFIEDDATE = Convert.ToDateTime(CountryTable.Rows[C]["MODIFIEDDATE"]),
                            MODIFIEDBY = Convert.ToString(CountryTable.Rows[C]["MODIFIEDBY"]),
                            CREATEDDATE = Convert.ToDateTime(CountryTable.Rows[C]["CREATEDDATE"]),
                            COUNTRY_ID = Convert.ToInt32(CountryTable.Rows[C]["COUNTRY_ID"]),
                            COUNTRYCODE = Convert.ToString(CountryTable.Rows[C]["COUNTRYCODE"]),
                            COUNTRYDESC = Convert.ToString(CountryTable.Rows[C]["COUNTRYDESC"]),
                            COUNTRYNAME = Convert.ToString(CountryTable.Rows[C]["COUNTRYNAME"]),
                        });
                    }
                }
                Obj._CityList = _CityList;
                Obj._DistrictList = _DistrictList;
                Obj._StateList = _StateList;
                Obj._CountryList = _CountryList;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetGeoDetails)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return Obj;
        }

        public DataSet GetAttendancestudentlst(int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetStudentbaseCS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CMid", CMid);
                cmd.Parameters.AddWithValue("@SecMid", SecMid);
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
                cmd.Parameters.AddWithValue("@Fmid", Fmid);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAttendancestudentlst)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }

        public DataSet GetmonthalyAttendancestudentlst(int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_GetMonthalyAttendancesp");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CMid", CMid);
                cmd.Parameters.AddWithValue("@SecMid", SecMid);
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
                cmd.Parameters.AddWithValue("@Fmid", Fmid);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetmonthalyAttendancestudentlst)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }
        public DataSet GetEmployeeTbl(int IsActive, int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_TBLGetEmployeeList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_TBLGetEmployeeList)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }
        public string StudentAttendanceopration(List<StudentAttendanceMaster> StudentAttendancelist, string optype)
        {
            int retype = -1;
            DataTable _studentattendance = new DataTable();
            int _srno = 1;
            if (StudentAttendancelist != null)
            {
                _studentattendance.Columns.Add("Samid", typeof(int));
                _studentattendance.Columns.Add("Smid", typeof(int));
                _studentattendance.Columns.Add("Sattendancedate", typeof(string));
                _studentattendance.Columns.Add("SIndatetime", typeof(string));
                _studentattendance.Columns.Add("SOutdatetime", typeof(string));
                _studentattendance.Columns.Add("SAtstatus", typeof(string));
                _studentattendance.Columns.Add("ClassId", typeof(int));
                _studentattendance.Columns.Add("SectionId", typeof(int));
                _studentattendance.Columns.Add("Rollno", typeof(string));
                _studentattendance.Columns.Add("RFID", typeof(string));
                _studentattendance.Columns.Add("MachineSecialno", typeof(string));
                _studentattendance.Columns.Add("Ipaddress", typeof(string));
                _studentattendance.Columns.Add("FMid", typeof(int));
                _studentattendance.Columns.Add("SchoolId", typeof(int));
                _studentattendance.Columns.Add("Reason", typeof(string));
                _studentattendance.Columns.Add("Createddate", typeof(string));
                _studentattendance.Columns.Add("Createdby", typeof(string));
                _studentattendance.Columns.Add("Modifieddate", typeof(string));
                _studentattendance.Columns.Add("Modifiedby", typeof(string));
            }
            foreach (var item in StudentAttendancelist)
            {
                DataRow dr = _studentattendance.NewRow();
                dr[0] = _srno;
                dr[1] = item.Smid;
                dr[2] = Convert.ToDateTime(item.Sattendancedate).ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                dr[3] = Convert.ToDateTime(item.SIndatetime).ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                dr[4] = Convert.ToDateTime(item.SOutdatetime).ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                dr[5] = item.SAtstatus;
                dr[6] = item.ClassId;
                dr[7] = item.SectionId;
                dr[8] = item.Rollno;
                dr[9] = item.RFID;
                dr[10] = item.MachineSecialno;
                dr[11] = item.Ipaddress;
                dr[12] = item.FMid;
                dr[13] = item.SchoolId;
                dr[14] = item.Reason;
                dr[15] = Convert.ToDateTime(item.Createddate).ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                dr[16] = item.Createdby;
                dr[17] = Convert.ToDateTime(item.Modifieddate).ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                dr[18] = item.Modifiedby;
                _studentattendance.Rows.Add(dr);
                _srno = _srno + 1;
            }
            SqlCommand cmd = new SqlCommand("SP_StudentAttendancepush");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentAttendance", _studentattendance);
                cmd.Parameters.AddWithValue("@optype", optype);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(StudentAttendanceopration)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype.ToString();



        }

        public DataSet GetAttendancesheetlst(DateTime attendate, int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_MonthalyAttendance");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Classid", CMid);
                cmd.Parameters.AddWithValue("@sectionid", SecMid);
                cmd.Parameters.AddWithValue("@date", attendate);
                cmd.Parameters.AddWithValue("@PSchoolID", SchoolId);
                cmd.Parameters.AddWithValue("@PFinancialYear", Fmid);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAttendancesheetlst)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }

        public DataSet GetAttendancesummary(DateTime attendate, DateTime toattendate, int CMid, int SecMid, int SchoolId, int Fmid)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Sp_AttendanceSummary");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", CMid);
                cmd.Parameters.AddWithValue("@SectionId", SecMid);
                cmd.Parameters.AddWithValue("@Stdate", attendate);
                cmd.Parameters.AddWithValue("@Enddate", toattendate);
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", Fmid);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAttendancesheetlst)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }
        public List<StudentMaster> GetStudentList(int SchoolID, int FinancialYearID, int ClassID = 0, int SectionID = 0, int RouteID = 0, string AdmissionNo = "")
        {
            List<StudentMaster> _stulist = new List<StudentMaster>();
            SqlCommand cmd = new SqlCommand("SP_GetStudentList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", FinancialYearID);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@SectionID", SectionID);
                cmd.Parameters.AddWithValue("@AdmissionNo", AdmissionNo);
                cmd.Parameters.AddWithValue("@RouteID", RouteID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _stulist.Add(new StudentMaster()
                    {
                        Smid = Convert.ToInt32(dr["Smid"]),
                        Enquiryno = dr["Enquiryno"] == DBNull.Value ? "" : Convert.ToString(dr["Enquiryno"]),
                        Firstname = dr["Firstname"] == DBNull.Value ? "" : Convert.ToString(dr["Firstname"]),
                        // Lastname = Convert.ToString(dr["Lastname"]),
                        CMid = dr["CMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CMid"]),
                        SecMid = dr["SecMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SecMid"]),
                        RollNo = dr["RollNo"] == DBNull.Value ? "0" : Convert.ToString(dr["RollNo"]),
                        RouteMid = dr["RouteMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["RouteMid"]),
                        BusMid = dr["BusMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BusMid"]),
                        Castmid = dr["Castmid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Castmid"]),
                        Categmid = dr["Categmid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Categmid"]),
                        HouseMid = dr["HouseMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HouseMid"]),
                        GMid = dr["GMid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["GMid"]),
                        Bd_address1 = dr["Bd_address1"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_address1"]),
                        Bd_address2 = dr["Bd_address2"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_address2"]),
                        Bd_City = dr["Bd_City"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_City"]),
                        Bd_contactno = dr["Bd_contactno"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_contactno"]),
                        Bd_dob = dr["Bd_dob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_dob"]),
                        Bd_fathername = dr["Bd_fathername"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fathername"]),
                        Bd_fathermob = dr["Bd_fathermob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fathermob"]),
                        Bd_qualification = dr["Bd_qualification"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_qualification"]),
                        Bd_fatheroccuption = dr["Bd_fatheroccuption"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fatheroccuption"]),
                        Bd_fathdob = dr["Bd_fathdob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_fathdob"]),
                        Bd_mothername = dr["Bd_mothername"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_mothername"]),
                        Bd_mothermob = dr["Bd_mothermob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_mothermob"]),
                        Bd_motherqualification = dr["Bd_motherqualification"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_motherqualification"]),
                        Bd_Motheroccuption = dr["Bd_Motheroccuption"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_Motheroccuption"]),
                        Bd_Mothredob = dr["Bd_Mothredob"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_Mothredob"]),
                        Bd_dateofannversy = dr["Bd_dateofannversy"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_dateofannversy"]),
                        Bd_Emailid = dr["Bd_Emailid"] == DBNull.Value ? "" : Convert.ToString(dr["Bd_Emailid"]),
                        Off_lastschool = dr["Off_lastschool"] == DBNull.Value ? "" : Convert.ToString(dr["Off_lastschool"]),
                        Off_remarks = dr["Off_remarks"] == DBNull.Value ? "" : Convert.ToString(dr["Off_remarks"]),
                        Off_Examgiven = dr["Off_Examgiven"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Examgiven"]),
                        Off_Year = dr["Off_Year"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Year"]),
                        Off_Status = dr["Off_Status"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Status"]),
                        Off_marks = dr["Off_marks"] == DBNull.Value ? "" : Convert.ToString(dr["Off_marks"]),
                        Off_boardoruniversity = dr["Off_boardoruniversity"] == DBNull.Value ? "" : Convert.ToString(dr["Off_boardoruniversity"]),
                        Off_bloodgroup = dr["Off_bloodgroup"] == DBNull.Value ? "" : Convert.ToString(dr["Off_bloodgroup"]),
                        VisionLeft = dr["VisionLeft"] == DBNull.Value ? "" : Convert.ToString(dr["VisionLeft"]),
                        Off_grno = dr["Off_grno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_grno"]),
                        Off_Visionright = dr["Off_Visionright"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Visionright"]),
                        Off_heightweight = dr["Off_heightweight"] == DBNull.Value ? "" : Convert.ToString(dr["Off_heightweight"]),
                        // Off_weight = Convert.ToString(dr["Off_weight"]),
                        Off_Dentailhygine = dr["Off_Dentailhygine"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Dentailhygine"]),
                        Off_Hosalroomno = dr["Off_Hosalroomno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Hosalroomno"]),
                        Off_bedno = dr["Off_bedno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_bedno"]),
                        Off_Scholarshipno = dr["Off_Scholarshipno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Scholarshipno"]),
                        Off_TC = dr["Off_TC"] == DBNull.Value ? "" : Convert.ToString(dr["Off_TC"]),
                        Off_CC = dr["Off_CC"] == DBNull.Value ? "" : Convert.ToString(dr["Off_CC"]),
                        Off_ReportC = dr["Firstname"] == DBNull.Value ? "" : Convert.ToString(dr["Off_ReportC"]),
                        Off_Dobcertificate = dr["Off_Dobcertificate"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Dobcertificate"]),
                        Off_admissionno = dr["Off_admissionno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_admissionno"]),
                        Off_dateofadmission = dr["Off_dateofadmission"] == DBNull.Value ? "" : Convert.ToString(dr["Off_dateofadmission"]),
                        Off_Ledgerbalance = dr["Off_Ledgerbalance"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Ledgerbalance"]),
                        Off_feesbalance = dr["Off_feesbalance"] == DBNull.Value ? "" : Convert.ToString(dr["Off_feesbalance"]),
                        Off_Comments = dr["Off_Comments"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Comments"]),
                        Off_Aadharno = dr["Off_Aadharno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_Aadharno"]),
                        Off_biometricno = dr["Off_biometricno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_biometricno"]),
                        Off_childuniqueno = dr["Off_childuniqueno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_childuniqueno"]),
                        Off_sessionno = dr["Off_sessionno"] == DBNull.Value ? "" : Convert.ToString(dr["Off_sessionno"]),
                        Off_family = dr["Off_family"] == DBNull.Value ? "" : Convert.ToString(dr["Off_family"]),
                        Off_stausinschool = dr["Off_stausinschool"] == DBNull.Value ? "" : Convert.ToString(dr["Off_stausinschool"]),
                        Off_discontinuethedate = dr["Off_discontinuethedate"] == DBNull.Value ? "" : Convert.ToString(dr["Off_discontinuethedate"]),
                        studentimage = dr["studentimage"] == DBNull.Value ? "" : Convert.ToString(dr["studentimage"]),
                        motherimage = dr["motherimage"] == DBNull.Value ? "" : Convert.ToString(dr["motherimage"]),
                        fatherimage = dr["fatherimage"] == DBNull.Value ? "" : Convert.ToString(dr["fatherimage"]),
                        IsActive = dr["IsActive"] == DBNull.Value ? true : Convert.ToBoolean(dr["IsActive"]),
                        SchoolID = dr["SchoolID"] == DBNull.Value ? 1 : Convert.ToInt32(dr["SchoolID"]),
                        Createdate = dr["Createdate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Createdate"]),
                        Createdby = dr["Createdby"] == DBNull.Value ? "Admin" : Convert.ToString(dr["Createdby"]),
                        Modifiedby = dr["Modifiedby"] == DBNull.Value ? "Admin" : Convert.ToString(dr["Modifiedby"]),
                        Modifieddate = dr["Modifieddate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["Modifieddate"]),
                        FeeGroup = dr["FeeGroup"] == DBNull.Value ? "" : Convert.ToString(dr["FeeGroup"]),
                        TPCost = dr["TPCost"] == DBNull.Value ? false : Convert.ToBoolean(dr["TPCost"]),
                        TPCostID = dr["TPCostID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TPCostID"]),
                        Adminssionno = dr["Adminssionno"] == DBNull.Value ? "" : Convert.ToString(dr["Adminssionno"]),
                        CastName = dr["CastName"] == DBNull.Value ? "" : Convert.ToString(dr["CastName"]),
                        CatName = dr["CatName"] == DBNull.Value ? "" : Convert.ToString(dr["CatName"]),
                        CITYNAME = dr["CITYNAME"] == DBNull.Value ? "" : Convert.ToString(dr["CITYNAME"]),
                        CITY_ID = dr["CITY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CITY_ID"]),
                        ClassName = dr["ClassName"] == DBNull.Value ? "" : Convert.ToString(dr["ClassName"]),
                        COUNTRYNAME = dr["COUNTRYNAME"] == DBNull.Value ? "" : Convert.ToString(dr["COUNTRYNAME"]),
                        COUNTRY_ID = dr["COUNTRY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["COUNTRY_ID"]),
                        FinancialYear = dr["FinancialYear"] == DBNull.Value ? 0 : Convert.ToInt32(dr["FinancialYear"]),
                        FinancialYearName = dr["FinancialYearName"] == DBNull.Value ? "" : Convert.ToString(dr["FinancialYearName"]),
                        GName = dr["GName"] == DBNull.Value ? "" : Convert.ToString(dr["GName"]),
                        Oname = dr["Oname"] == DBNull.Value ? "" : Convert.ToString(dr["Oname"]),
                        ReligionName = dr["ReligionName"] == DBNull.Value ? "" : Convert.ToString(dr["ReligionName"]),
                        Relmid = dr["Relmid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Relmid"]),
                        SectionName = dr["SectionName"] == DBNull.Value ? "" : Convert.ToString(dr["SectionName"]),
                        STATENAME = dr["STATENAME"] == DBNull.Value ? "" : Convert.ToString(dr["STATENAME"]),
                        STATE_ID = dr["STATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["STATE_ID"]),
                        FeeConID = dr["FeeConID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["FeeConID"]),
                        RouteName = dr["RouteName"] == DBNull.Value ? "" : Convert.ToString(dr["RouteName"]),
                        PickDropPointName = dr["PickDropPointName"] == DBNull.Value ? "" : Convert.ToString(dr["PickDropPointName"]),
                        TPFee = dr["TPFee"] == DBNull.Value ? "" : Convert.ToString(dr["TPFee"]),
                        VehicleNumber = dr["VehicleNumber"] == DBNull.Value ? "" : Convert.ToString(dr["VehicleNumber"]),
                        StudentStatus = dr["StudentStatus"] == DBNull.Value ? "" : Convert.ToString(dr["StudentStatus"]),
                        PaidMonths = dr["PaidMonths"] == DBNull.Value ? "" : Convert.ToString(dr["PaidMonths"]),

                        Check = true,
                    });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStudentList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _stulist;
        }
        public DashboardDetails GetDashboardDetails(int OrgnisationID, int Financialyearid, DateTime CurrentDate)
        {
            DashboardDetails Obj = new DashboardDetails();
            List<StudentDashboard> StudentList = new List<StudentDashboard>();
            List<ChartFields> ChartData = new List<ChartFields>();
            try
            {
                cmd = new SqlCommand("GetDashboardDetails");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", OrgnisationID);
                cmd.Parameters.AddWithValue("@FinancialYearID", Financialyearid);
                cmd.Parameters.AddWithValue("@CurrentDate", CurrentDate.ToString("MM/dd/yyyy"));
                DataSet ds = DBHelper.ExecuteDataSet(cmd);
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Obj.TotalStudents = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalStudents"].ToString());
                        Obj.TotalPresentStudents = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalPresentStudents"].ToString());
                        Obj.TotalAbsentStudents = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalAbsentStudents"].ToString());
                        Obj.TotalLeaveStudents = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalLeaveStudents"].ToString());

                        Obj.TotalStaff = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalStaff"].ToString());
                        Obj.TotalPresentStaff = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalPresentStaff"].ToString());
                        Obj.TotalAbsentStaff = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalAbsentStaff"].ToString());
                        Obj.TotalLeaveStaff = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalLeaveStaff"].ToString());

                        Obj.NewAdmission = Convert.ToInt32(ds.Tables[0].Rows[i]["NewAdmission"].ToString());
                        Obj.TodayFeeCollection = Convert.ToDouble(ds.Tables[0].Rows[i]["TodayFeeCollection"].ToString());
                        Obj.TotalFeePending = Convert.ToDouble(ds.Tables[0].Rows[i]["TotalFeePending"].ToString());
                    }
                }
                catch (Exception ex) { ExecptionLogger.FileHandling("DALCommon(GetDashboardDetails_TBL0)", "Error_014", ex, "DALCommon"); }
                try
                {
                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {
                        StudentList.Add(new StudentDashboard
                        {
                            AdmissionDate = Convert.ToString(ds.Tables[1].Rows[j]["AdmissionDate"].ToString()),
                            StudentName = Convert.ToString(ds.Tables[1].Rows[j]["StudentName"].ToString()),
                            StudentImage = Convert.ToString(ds.Tables[1].Rows[j]["StudentImage"].ToString()),
                            StudentID = Convert.ToInt32(ds.Tables[1].Rows[j]["StudentID"].ToString()),
                        });
                    }
                    Obj.StudentList = StudentList;
                }
                catch(Exception ex) { ExecptionLogger.FileHandling("DALCommon(GetDashboardDetails_TBL1)", "Error_014", ex, "DALCommon"); }
                try
                {
                    for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                    {
                        ChartData.Add(new ChartFields
                        {
                            Month = Convert.ToString(ds.Tables[2].Rows[j]["Month"].ToString()),
                            MonthFee = Convert.ToString(ds.Tables[2].Rows[j]["MonthFee"].ToString()),
                            Year = Convert.ToString(ds.Tables[2].Rows[j]["Year"].ToString()),
                        });
                    }
                    Obj.ChartData = ChartData.OrderBy(s => DateTime.ParseExact(s.Month, "MMMM", new System.Globalization.CultureInfo("en-US"))).OrderBy(s => Convert.ToInt32(s.Year)).ToList();

                    //Obj.ChartData = ChartData.OrderBy(x=>x.Year).OrderBy(x=>x.Month).ToList();
                }
                catch  (Exception ex) { ExecptionLogger.FileHandling("DALCommon(GetDashboardDetails_TBL2)", "Error_014", ex, "DALCommon"); }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetDashboardDetails)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return Obj;
        }
        public bool ValidateEmailExist(string EmailID, string UserType, int SchoolID)
        {
            bool _results = false;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = "ValidateEmailExist";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                cmd.Parameters.AddWithValue("@UserType", UserType);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                _results = Convert.ToInt32(DBHelper.ExecuteScalar(cmd)) == 0 ? false : true;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(ValidateEmailExist)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        public DataSet GetMasterForBulkStudentRegistration(int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("GetMasterForBulkStudentRegistration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(SP_TBLGetStudentList)", "Error_014", ex, "DALCommon");
            }
            return _ds;
        }
        public DataTable StudentRegistration(DataTable DTStudent)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SP_RegisterStudentBulk");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentRegistrationData", DTStudent);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStudentbaseonAdmissionno)", "Error_014", ex, "DALCommon");
            }
            return _ds.Tables[0];
        }
        #region CourseManagementSystem
        public DataTable GetSubjectListTBL(int SchoolID)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_SubjectList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectListTBL)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public SubjectMaster GetSubjectBySumid(int Sumid)
        {
            SubjectMaster obj = new SubjectMaster();
            SqlCommand cmd = new SqlCommand("USP_GetSubjectBySumid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sumid", Sumid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Sumid = Convert.ToInt32(dr["Sumid"]);
                    obj.Subjectname = Convert.ToString(dr["Subjectname"]);
                    obj.Subjectdescription = Convert.ToString(dr["Subjectdescription"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectBySumid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int AddSubject(SubjectMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_Addsubjectmaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Sumid", _master.Sumid);
                cmd.Parameters.AddWithValue("@Subjectname", _master.Subjectname);
                cmd.Parameters.AddWithValue("@Subjectdescription", _master.Subjectdescription);
                cmd.Parameters.AddWithValue("@SchoolId", _master.schoolid);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddSubject)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public List<SubjectMaster> GetSubjectList(int SchoolID)
        {
            List<SubjectMaster> obj = new List<SubjectMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetSubjectList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new SubjectMaster
                        {
                            Sumid = Convert.ToInt32(ds.Tables[0].Rows[l]["Sumid"]),
                            Subjectname = Convert.ToString(ds.Tables[0].Rows[l]["Subjectname"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        /*------Subject Chapter Listing-----------*/
        public DataTable GetSubjectChapterListTBL(int SchoolID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_subjectchapterlist");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectChapterListTBL)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public SubjectchapterMaster GetSubjectChapterBySumid(int Scmid)
        {
            SubjectchapterMaster obj = new SubjectchapterMaster();
            SqlCommand cmd = new SqlCommand("USP_GetSubjectChapterBySumid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Scmid", Scmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Scmid = Convert.ToInt32(dr["Scmid"]);
                    obj.Chaptername = Convert.ToString(dr["Chaptername"]);
                    obj.Chapterdes = Convert.ToString(dr["Chapterdes"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.CMid = Convert.ToInt32(dr["CMid"]);
                    obj.Sumid = Convert.ToInt32(dr["Sumid"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectChapterBySumid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int AddSubjectchapter(SubjectchapterMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_Mst_Subject_Chapter");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Scmid", _master.Scmid);
                cmd.Parameters.AddWithValue("@Sumid", _master.Sumid);
                cmd.Parameters.AddWithValue("@CMid", _master.CMid);
                cmd.Parameters.AddWithValue("@Chaptername", _master.Chaptername);
                cmd.Parameters.AddWithValue("@Chapterdes", _master.Chapterdes);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@schoolid", _master.schoolid);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddSubjectchapter)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public List<SubjectchapterMaster> GetSubjectChapterList(int SchoolID)
        {
            List<SubjectchapterMaster> obj = new List<SubjectchapterMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetSubjectChapterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new SubjectchapterMaster
                        {
                            Scmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Scmid"]),
                            Sumid = Convert.ToInt32(ds.Tables[0].Rows[l]["Sumid"]),
                            CMid = Convert.ToInt32(ds.Tables[0].Rows[l]["CMid"]),
                            Chaptername = Convert.ToString(ds.Tables[0].Rows[l]["Chaptername"]),
                            Chapterdes = Convert.ToString(ds.Tables[0].Rows[l]["Chapterdes"]),
                            Createdate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createdate"]),
                            Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]),
                            Createdby = Convert.ToString(ds.Tables[0].Rows[l]["Createdby"]),
                            Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            schoolid = Convert.ToInt32(ds.Tables[0].Rows[l]["schoolid"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectChapterList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        /*-------------End Chapter opration-----------*/
        /*-----------Start Period Opration-----------*/
        public DataTable GetPeriodmasterTblList(int SchoolID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_PeriodmasterTblList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetPeriodmasterTblList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public List<PeriodMaster> GetPeriodMasterList(int SchoolID)
        {
            List<PeriodMaster> obj = new List<PeriodMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetPeriodMasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new PeriodMaster
                        {
                            Pmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Pmid"]),
                            PeriodName = Convert.ToString(ds.Tables[0].Rows[l]["PeriodName"]),
                            Perioddesc = Convert.ToString(ds.Tables[0].Rows[l]["PeriodName"]) + "- " + Convert.ToString(ds.Tables[0].Rows[l]["PeriodStart"]) + "- " + Convert.ToString(ds.Tables[0].Rows[l]["PeriodEnd"]),
                            PeriodStart = Convert.ToString(ds.Tables[0].Rows[l]["PeriodStart"]),
                            PeriodEnd = Convert.ToString(ds.Tables[0].Rows[l]["PeriodEnd"]),
                            Createdate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createdate"]),
                            Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]),
                            Createdby = Convert.ToString(ds.Tables[0].Rows[l]["Createdby"]),
                            Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            schoolid = Convert.ToInt32(ds.Tables[0].Rows[l]["schoolid"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetPeriodMasterList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public PeriodMaster GetPeriodByPmid(int Pmid)
        {
            PeriodMaster obj = new PeriodMaster();
            SqlCommand cmd = new SqlCommand("USP_GetPeriodByPmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pmid", Pmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Pmid = Convert.ToInt32(dr["Pmid"]);
                    obj.PeriodName = Convert.ToString(dr["PeriodName"]);
                    obj.Perioddesc = Convert.ToString(dr["Perioddesc"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.PeriodStart = Convert.ToString(dr["PeriodStart"]);
                    obj.PeriodEnd = Convert.ToString(dr["PeriodEnd"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetPeriodByPmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int AddPeriod(PeriodMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddPeriodMaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pmid", _master.Pmid);
                cmd.Parameters.AddWithValue("@PeriodName", _master.PeriodName);
                cmd.Parameters.AddWithValue("@Perioddesc", _master.Perioddesc);
                cmd.Parameters.AddWithValue("@PeriodStart", _master.PeriodStart);
                cmd.Parameters.AddWithValue("@PeriodEnd", _master.PeriodEnd);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@schoolid", _master.schoolid);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddPeriod)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        /*-----------Start Period Opration-----------*/
        /*------Asssign Period teacher opration-----------*/

        public List<EmployeeMaster> GetTeacherList(int IsActive, int SchoolID)
        {
            List<EmployeeMaster> _emplist = new List<EmployeeMaster>();
            SqlCommand cmd = new SqlCommand("USP_GetTeacherList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    _emplist.Add(new EmployeeMaster() { EMP_ID = Convert.ToInt32(dr["EMP_ID"]), FIRSTNAME = Convert.ToString(dr["EMPCODE"]) + "-" + Convert.ToString(dr["FIRSTNAME"]) });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetEmployeeList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _emplist;
        }
        public DataTable GetAssignperiodteacherListTBL(int SchoolID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetAssignperiodteacherListTBL");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAssignperiodteacherListTBL)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }

        public Assignperiodteacher GetAssignperiodteacherByApttmid(int Apttmid)
        {
            Assignperiodteacher obj = new Assignperiodteacher();
            SqlCommand cmd = new SqlCommand("USP_GetAssignperiodteacherByApttmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Apttmid", Apttmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Apttmid = Convert.ToInt32(dr["Apttmid"]);
                    obj.EMP_ID = Convert.ToInt32(dr["EMP_ID"]);
                    obj.Pmid = Convert.ToInt32(dr["Pmid"]);
                    obj.CMid = Convert.ToInt32(dr["CMid"]);
                    obj.Secmid = Convert.ToInt32(dr["Secmid"]);
                    obj.Perioddescription = Convert.ToString(dr["Perioddescription"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAssignperiodteacherByApttmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int AddAssignperiodteacher(Assignperiodteacher _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddAssign_Periodto_Teacher");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Apttmid", _master.Apttmid);
                cmd.Parameters.AddWithValue("@EMP_ID", _master.EMP_ID);
                cmd.Parameters.AddWithValue("@Pmid", _master.Pmid);
                cmd.Parameters.AddWithValue("@CMid", _master.CMid);
                cmd.Parameters.AddWithValue("@Secmid", _master.Secmid);
                cmd.Parameters.AddWithValue("@Perioddescription", _master.Perioddescription);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@schoolid", _master.schoolid);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddAssignperiodteacher)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public List<Assignperiodteacher> GetAssignperiodteacherList(int SchoolID)
        {
            List<Assignperiodteacher> obj = new List<Assignperiodteacher>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetAssignperiodteacherList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new Assignperiodteacher
                        {
                            Apttmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Apttmid"]),
                            EMP_ID = Convert.ToInt32(ds.Tables[0].Rows[l]["EMP_ID"]),
                            Pmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Pmid"]),
                            CMid = Convert.ToInt32(ds.Tables[0].Rows[l]["CMid"]),
                            Secmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Secmid"]),
                            Perioddescription = Convert.ToString(ds.Tables[0].Rows[l]["Perioddescription"]),
                            Createdate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createdate"]),
                            Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]),
                            Createdby = Convert.ToString(ds.Tables[0].Rows[l]["Createdby"]),
                            Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            schoolid = Convert.ToInt32(ds.Tables[0].Rows[l]["schoolid"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAssignperiodteacherList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        /*-----------------Assign Lecture content-------------------*/
        public DataTable AssigncontentinlectureTblList(int SchoolID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_AssigncontentinlectureTblList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AssigncontentinlectureTblList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public Assigncontentinlecture GetAssigncontentinlectureByAcdtmid(int Acdtmid)
        {
            Assigncontentinlecture obj = new Assigncontentinlecture();
            SqlCommand cmd = new SqlCommand("USP_GetAssigncontentinlectureByAcdtmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Acdtmid", Acdtmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Acdtmid = Convert.ToInt32(dr["Acdtmid"]);
                    obj.Apttmid = Convert.ToInt32(dr["Apttmid"]);
                    obj.Sumid = Convert.ToInt32(dr["Sumid"]);
                    obj.Scmid = Convert.ToInt32(dr["Scmid"]);
                    obj.Topic = Convert.ToString(dr["Topic"]);
                    obj.Contents = Convert.ToString(dr["Contents"]);
                    obj.Files = Convert.ToString(dr["Files"]);
                    obj.Onlineurl = Convert.ToString(dr["Onlineurl"]);
                    obj.Dates = Convert.ToDateTime(dr["Dates"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetAssignperiodteacherByApttmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddAssigncontentinlecture(Assigncontentinlecture _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddAssigncontentinlecture");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Acdtmid", _master.Acdtmid);
                cmd.Parameters.AddWithValue("@Apttmid", _master.Apttmid);
                cmd.Parameters.AddWithValue("@Sumid", _master.Sumid);
                cmd.Parameters.AddWithValue("@Scmid", _master.Scmid);
                cmd.Parameters.AddWithValue("@Topic", _master.Topic);
                cmd.Parameters.AddWithValue("@Contents", _master.Contents);
                cmd.Parameters.AddWithValue("@Files", _master.Files);
                cmd.Parameters.AddWithValue("@Onlineurl", _master.Onlineurl);
                cmd.Parameters.AddWithValue("@Dates", _master.Dates);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@schoolid", _master.schoolid);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddAssigncontentinlecture)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public List<SubjectchapterMaster> GetchapterbaseonsubjectList(int subjectId)
        {
            List<SubjectchapterMaster> obj = new List<SubjectchapterMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetchapterbaseonsubjectList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new SubjectchapterMaster
                        {
                            Scmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Scmid"]),
                            Sumid = Convert.ToInt32(ds.Tables[0].Rows[l]["Sumid"]),
                            CMid = Convert.ToInt32(ds.Tables[0].Rows[l]["CMid"]),
                            Chaptername = Convert.ToString(ds.Tables[0].Rows[l]["Chaptername"]),
                            Chapterdes = Convert.ToString(ds.Tables[0].Rows[l]["Chapterdes"]),
                            Createdate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createdate"]),
                            Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]),
                            Createdby = Convert.ToString(ds.Tables[0].Rows[l]["Createdby"]),
                            Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            schoolid = Convert.ToInt32(ds.Tables[0].Rows[l]["schoolid"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetchapterbaseonsubjectList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<SubjectMaster> GetSubjectbaseonlectureList(int Apttmid)
        {
            List<SubjectMaster> obj = new List<SubjectMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetSubjectbaseonlectureList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Apttmid", Apttmid);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new SubjectMaster
                        {
                            Sumid = Convert.ToInt32(ds.Tables[0].Rows[l]["Sumid"]),
                            Subjectname = Convert.ToString(ds.Tables[0].Rows[l]["Subjectname"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSubjectList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        #endregion
        #region BellSystem
        public DataTable GetBellSystemListTBL(int SchoolID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetBellSystemListTBL");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetBellSystemListTBL)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public BellSystemMaster GetBellSystemBybmid(int bmid)
        {
            BellSystemMaster obj = new BellSystemMaster();
            SqlCommand cmd = new SqlCommand("USP_GetBellSystemBybmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bmid", bmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.bmid = Convert.ToInt32(dr["bmid"]);
                    obj.Pmid = Convert.ToInt32(dr["Pmid"]);
                    obj.Belltitle = Convert.ToString(dr["Belltitle"]);
                    obj.Bellsongpath = Convert.ToString(dr["Bellsongpath"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetBellSystemBybmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddBellSystem(BellSystemMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddBellSystemMaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bmid", _master.bmid);
                cmd.Parameters.AddWithValue("@Pmid", _master.Pmid);
                cmd.Parameters.AddWithValue("@Belltitle", _master.Belltitle);
                cmd.Parameters.AddWithValue("@Bellsongpath", _master.Bellsongpath);
                cmd.Parameters.AddWithValue("@schoolid", _master.schoolid);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddBellSystem)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        #endregion
        #region API User 
        public int UserAvailablity(string Username, string Password)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("CUSTOMER_usp_User_Authenticate");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(UserAvailablity)", "Error_014", ex, "UserAvailablity");
            }
            return _ds.Tables[0].Rows.Count;
        }

        public int InsertInDataPacket(DataTable objtbl)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_datapacketpush");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@clentattendancedpak", objtbl);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(InsertInDataPacket)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;

        }

        #endregion

        #region User Profile
        public int AddUserprofile(UserMasters _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddUserprofile");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", _master.UserId);
                cmd.Parameters.AddWithValue("@UserName", _master.USERNAME);
                cmd.Parameters.AddWithValue("@Name", _master.FISRTNAME);
                cmd.Parameters.AddWithValue("@Lastname", _master.LASTNAME);
                cmd.Parameters.AddWithValue("@Mobile", _master.Mobile);
                cmd.Parameters.AddWithValue("@EmailId", _master.EMAILID);
                cmd.Parameters.AddWithValue("@IMAGE", _master.IMAGE);
                cmd.Parameters.AddWithValue("@UpdatedBy", _master.MODIFIEDBY);
                cmd.Parameters.AddWithValue("@Updateddate", _master.MODIFIEDDATE);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddStudent)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        #endregion
        #region Use For Student Print
        public List<StudentMaster> GetMultipleStudentslist(string  Smid_multiplevalue)
        {
            List<StudentMaster> _studlist = new List<StudentMaster>();
            SqlCommand cmd = new SqlCommand("USP_GetMultipleStudentslist");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@multiple_studentid", Smid_multiplevalue);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    StudentMaster _stud = new StudentMaster()
                    {
                        Smid = Convert.ToInt32(dr["Smid"]),
                        Adminssionno = dr["Adminssionno"] != DBNull.Value ? Convert.ToString(dr["Adminssionno"]) : default(string),
                        Enquiryno = dr["Enquiryno"] != DBNull.Value ? Convert.ToString(dr["Enquiryno"]) : default(string),
                        Firstname = dr["Firstname"] != DBNull.Value ? Convert.ToString(dr["Firstname"]) : default(string),
                        CMid = dr["CMid"] != DBNull.Value ? Convert.ToInt32(dr["CMid"]) : default(int),

                        SecMid = dr["SecMid"] != DBNull.Value ? Convert.ToInt32(dr["SecMid"]) : default(int),

                        RollNo = dr["RollNo"] != DBNull.Value ? Convert.ToString(dr["RollNo"]) : default(string),
                        RouteMid = dr["RouteMid"] != DBNull.Value ? Convert.ToInt32(dr["RouteMid"]) : default(int),
                        BusMid = dr["BusMid"] != DBNull.Value ? Convert.ToInt32(dr["BusMid"]) : default(int),
                        Castmid = dr["Castmid"] != DBNull.Value ? Convert.ToInt32(dr["Castmid"]) : default(int),
                        Categmid = dr["Categmid"] != DBNull.Value ? Convert.ToInt32(dr["Categmid"]) : default(int),
                        HouseMid = dr["HouseMid"] != DBNull.Value ? Convert.ToInt32(dr["HouseMid"]) : default(int),
                        Relmid = dr["Relmid"] != DBNull.Value ? Convert.ToInt32(dr["Relmid"]) : default(int),
                        GMid = dr["GMid"] != DBNull.Value ? Convert.ToInt32(dr["GMid"]) : default(int),
                        CITY_ID = dr["CITY_ID"] != DBNull.Value ? Convert.ToInt32(dr["CITY_ID"]) : default(int),
                        STATE_ID = dr["STATE_ID"] != DBNull.Value ? Convert.ToInt32(dr["STATE_ID"]) : default(int),
                        COUNTRY_ID = dr["COUNTRY_ID"] != DBNull.Value ? Convert.ToInt32(dr["COUNTRY_ID"]) : default(int),
                        Bd_address1 = dr["Bd_address1"] != DBNull.Value ? Convert.ToString(dr["Bd_address1"]) : default(string),
                        Bd_address2 = dr["Bd_address2"] != DBNull.Value ? Convert.ToString(dr["Bd_address2"]) : default(string),
                        Bd_City = dr["Bd_City"] != DBNull.Value ? Convert.ToString(dr["Bd_City"]) : default(string),
                        Bd_contactno = dr["Bd_contactno"] != DBNull.Value ? Convert.ToString(dr["Bd_contactno"]) : default(string),
                        Bd_dob = dr["Bd_dob"] != DBNull.Value ? Convert.ToString(dr["Bd_dob"]) : default(string),//?null: Convert.ToDateTime(dr["Bd_dob"]),
                        Bd_fathername = dr["Bd_fathername"] != DBNull.Value ? Convert.ToString(dr["Bd_fathername"]) : default(string),
                        Bd_fathermob = dr["Bd_fathermob"] != DBNull.Value ? Convert.ToString(dr["Bd_fathermob"]) : default(string),
                        Bd_qualification = dr["Bd_qualification"] != DBNull.Value ? Convert.ToString(dr["Bd_qualification"]) : default(string),
                        Bd_fatheroccuption = dr["Bd_fatheroccuption"] != DBNull.Value ? Convert.ToString(dr["Bd_fatheroccuption"]) : default(string),
                        Bd_fathdob = dr["Bd_fathdob"] != DBNull.Value ? Convert.ToString(dr["Bd_fathdob"]) : default(string),
                        Bd_mothername = dr["Bd_mothername"] != DBNull.Value ? Convert.ToString(dr["Bd_mothername"]) : default(string),
                        Bd_mothermob = dr["Bd_mothermob"] != DBNull.Value ? Convert.ToString(dr["Bd_mothermob"]) : default(string),
                        Bd_motherqualification = dr["Bd_motherqualification"] != DBNull.Value ? Convert.ToString(dr["Bd_motherqualification"]) : default(string),
                        Bd_Motheroccuption = dr["Bd_Motheroccuption"] != DBNull.Value ? Convert.ToString(dr["Bd_Motheroccuption"]) : default(string),
                        Bd_Mothredob = dr["Bd_Mothredob"] != DBNull.Value ? Convert.ToString(dr["Bd_Mothredob"]) : default(string),
                        Bd_dateofannversy = dr["Bd_dateofannversy"] != DBNull.Value ? Convert.ToString(dr["Bd_dateofannversy"]) : default(string),
                        Bd_Emailid = dr["Bd_Emailid"] != DBNull.Value ? Convert.ToString(dr["Bd_Emailid"]) : default(string),
                        Off_lastschool = dr["Off_lastschool"] != DBNull.Value ? Convert.ToString(dr["Off_lastschool"]) : default(string),
                        Off_remarks = dr["Off_remarks"] != DBNull.Value ? Convert.ToString(dr["Off_remarks"]) : default(string),
                        Off_Examgiven = dr["Off_Examgiven"] != DBNull.Value ? Convert.ToString(dr["Off_Examgiven"]) : default(string),
                        Off_Year = dr["Off_Year"] != DBNull.Value ? Convert.ToString(dr["Off_Year"]) : default(string),
                        Off_Status = dr["Off_Status"] != DBNull.Value ? Convert.ToString(dr["Off_Status"]) : default(string),
                        Off_marks = dr["Off_marks"] != DBNull.Value ? Convert.ToString(dr["Off_marks"]) : default(string),
                        Off_boardoruniversity = dr["Off_boardoruniversity"] != DBNull.Value ? Convert.ToString(dr["Off_boardoruniversity"]) : default(string),
                        Off_bloodgroup = dr["Off_bloodgroup"] != DBNull.Value ? Convert.ToString(dr["Off_bloodgroup"]) : default(string),
                        VisionLeft = dr["VisionLeft"] != DBNull.Value ? Convert.ToString(dr["VisionLeft"]) : default(string),
                        Off_grno = dr["Off_grno"] != DBNull.Value ? Convert.ToString(dr["Off_grno"]) : default(string),
                        Off_Visionright = dr["Off_Visionright"] != DBNull.Value ? Convert.ToString(dr["Off_Visionright"]) : default(string),
                        Off_heightweight = dr["Off_heightweight"] != DBNull.Value ? Convert.ToString(dr["Off_heightweight"]) : default(string),
                        Off_Dentailhygine = dr["Off_Dentailhygine"] != DBNull.Value ? Convert.ToString(dr["Off_Dentailhygine"]) : default(string),
                        Off_Hosalroomno = dr["Off_Hosalroomno"] != DBNull.Value ? Convert.ToString(dr["Off_Hosalroomno"]) : default(string),
                        Off_bedno = dr["Off_bedno"] != DBNull.Value ? Convert.ToString(dr["Off_bedno"]) : default(string),
                        Off_Scholarshipno = dr["Off_Scholarshipno"] != DBNull.Value ? Convert.ToString(dr["Off_Scholarshipno"]) : default(string),
                        Off_TC = dr["Off_TC"] != DBNull.Value ? Convert.ToString(dr["Off_TC"]) : default(string),
                        Off_CC = dr["Off_CC"] != DBNull.Value ? Convert.ToString(dr["Off_CC"]) : default(string),
                        Off_ReportC = dr["Off_ReportC"] != DBNull.Value ? Convert.ToString(dr["Off_ReportC"]) : default(string),
                        Off_Dobcertificate = dr["Off_Dobcertificate"] != DBNull.Value ? Convert.ToString(dr["Off_Dobcertificate"]) : default(string),
                        Off_admissionno = dr["Off_admissionno"] != DBNull.Value ? Convert.ToString(dr["Off_admissionno"]) : default(string),
                        Off_dateofadmission = dr["Off_dateofadmission"] != DBNull.Value ? Convert.ToString(dr["Off_dateofadmission"]) : default(string),
                        Off_Ledgerbalance = dr["Off_Ledgerbalance"] != DBNull.Value ? Convert.ToString(dr["Off_Ledgerbalance"]) : default(string),
                        Off_feesbalance = dr["Off_feesbalance"] != DBNull.Value ? Convert.ToString(dr["Off_feesbalance"]) : default(string),
                        Off_Comments = dr["Off_Comments"] != DBNull.Value ? Convert.ToString(dr["Off_Comments"]) : default(string),
                        Off_Aadharno = dr["Off_Aadharno"] != DBNull.Value ? Convert.ToString(dr["Off_Aadharno"]) : default(string),
                        Off_biometricno = dr["Off_biometricno"] != DBNull.Value ? Convert.ToString(dr["Off_biometricno"]) : default(string),
                        Off_nationality = dr["Off_nationality"] != DBNull.Value ? Convert.ToString(dr["Off_nationality"]) : default(string),
                        Off_childuniqueno = dr["Off_childuniqueno"] != DBNull.Value ? Convert.ToString(dr["Off_childuniqueno"]) : default(string),
                        Off_sessionno = dr["Off_sessionno"] != DBNull.Value ? Convert.ToString(dr["Off_sessionno"]) : default(string),
                        Off_family = dr["Off_family"] != DBNull.Value ? Convert.ToString(dr["Off_family"]) : default(string),
                        Off_stausinschool = dr["Off_stausinschool"] != DBNull.Value ? Convert.ToString(dr["Off_stausinschool"]) : default(string),
                        Off_discontinuethedate = dr["Off_discontinuethedate"] != DBNull.Value ? Convert.ToString(dr["Off_discontinuethedate"]) : default(string),
                        studentimage = dr["studentimage"] != DBNull.Value ? Convert.ToString(dr["studentimage"]) : default(string),
                        motherimage = dr["motherimage"] != DBNull.Value ? Convert.ToString(dr["motherimage"]) : default(string),
                        fatherimage = dr["fatherimage"] != DBNull.Value ? Convert.ToString(dr["fatherimage"]) : default(string),
                        IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : default(bool),
                        SchoolID = dr["SchoolID"] != DBNull.Value ? Convert.ToInt32(dr["SchoolID"]) : default(int),
                        Createdate = dr["Createdate"] != DBNull.Value ? Convert.ToDateTime(dr["Createdate"]) : default(DateTime),
                        Createdby = dr["Createdby"] != DBNull.Value ? Convert.ToString(dr["Createdby"]) : default(string),
                        Modifiedby = dr["Modifiedby"] != DBNull.Value ? Convert.ToString(dr["Modifiedby"]) : default(string),
                        Modifieddate = dr["Modifieddate"] != DBNull.Value ? Convert.ToDateTime(dr["Modifieddate"]) : default(DateTime),
                        TPCost = dr["TPCost"] != DBNull.Value ? Convert.ToBoolean(dr["TPCost"]) : default(bool),
                        FeeGroup = dr["FeeGroup"] != DBNull.Value ? Convert.ToString(dr["FeeGroup"]) : default(string),
                        FinancialYear = dr["SchoolID"] != DBNull.Value ? Convert.ToInt32(dr["FinancialYear"]) : default(int),
                        ClassName = dr["ClassName"] != DBNull.Value ? Convert.ToString(dr["ClassName"]) : default(string),
                        SectionName = dr["SectionName"] != DBNull.Value ? Convert.ToString(dr["SectionName"]) : default(string),
                        ReligionName = dr["ReligionName"] != DBNull.Value ? Convert.ToString(dr["ReligionName"]) : default(string),
                        CastName = dr["CastName"] != DBNull.Value ? Convert.ToString(dr["CastName"]) : default(string),
                        CatName = dr["CatName"] != DBNull.Value ? Convert.ToString(dr["CatName"]) : default(string),
                        GName = dr["GName"] != DBNull.Value ? Convert.ToString(dr["GName"]) : default(string),
                        CITYNAME = dr["CityName"] != DBNull.Value ? Convert.ToString(dr["CityName"]) : default(string),
                        DistrictName = dr["DistrictName"] != DBNull.Value ? Convert.ToString(dr["DistrictName"]) : default(string),
                        STATENAME = dr["StateName"] != DBNull.Value ? Convert.ToString(dr["StateName"]) : default(string),
                        COUNTRYNAME = dr["CountryName"] != DBNull.Value ? Convert.ToString(dr["CountryName"]) : default(string),
                        RouteName = dr["RouteName"] != DBNull.Value ? Convert.ToString(dr["RouteName"]) : default(string),
                        PickDropPointName = dr["PickDropPointName"] != DBNull.Value ? Convert.ToString(dr["PickDropPointName"]) : default(string),
                        BloodGroupName = dr["BloodGroupName"] != DBNull.Value ? Convert.ToString(dr["BloodGroupName"]) : default(string),
                        VehicleNumber = dr["VehicleNumber"] != DBNull.Value ? Convert.ToString(dr["VehicleNumber"]) : default(string),
                    };

                    _studlist.Add(_stud);
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStudentslist)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _studlist;
        }
        #endregion
        public string PromoteStudent(DataTable DT, int PrevFYID, int NextFYID, int ClassID, int SectionID)
        {
            string res = "";
            SqlCommand cmd = new SqlCommand("SP_PromoteStudent");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@List", DT);
                cmd.Parameters.AddWithValue("@PrevFYID", PrevFYID);
                cmd.Parameters.AddWithValue("@NextFYID", NextFYID);
                cmd.Parameters.AddWithValue("@NextClassID", ClassID);
                cmd.Parameters.AddWithValue("@NextSectionId", SectionID);

                res = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                res = "No record updated.";
                ExecptionLogger.FileHandling("DALCommon(PromoteStudent)", "Error_014", ex, "DALCommon");

            }
            return res;
        }

        public List<Studentddl> GetStudentddl(Boolean isActive,int Schoolid,int FinancialYear)
        {
            List<Studentddl> _studdl = new List<Studentddl>();
            SqlCommand cmd = new SqlCommand("USP_Studentddl");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@SchoolID", Schoolid);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    Studentddl _stud = new Studentddl()
                    {
                        Smid = Convert.ToInt32(dr["Smid"]),
                        Studentfulldetail = Convert.ToString(dr["Studentfulldetail"])
                    };
                    _studdl.Add(_stud);
                }
            }
            catch (Exception ex)
            {

            }
            return _studdl;
        }


        public Studentleavedetail GetStudentleavedetailBySlmid(int Slmid)
        {
            Studentleavedetail obj = new Studentleavedetail();
            SqlCommand cmd = new SqlCommand("GetStudentleavedetailBySlmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Slmid", Slmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Slmid = Convert.ToInt32(dr["Slmid"]);
                    obj.Smid = Convert.ToInt32(dr["Smid"]);
                    obj.AdmissionNo = Convert.ToString(dr["AdmissionNo"]);
                    obj.Leavestartdate = Convert.ToDateTime(dr["Leavestartdate"]);
                    obj.Leaveenddate = Convert.ToDateTime(dr["Leaveenddate"]);
                    obj.LeaveRreason = Convert.ToString(dr["LeaveRreason"]);
                    obj.Leavedocument = Convert.ToString(dr["Leavedocument"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.FMid = Convert.ToInt32(dr["FMid"]);
                    obj.LeaveStatus = Convert.ToString(dr["LeaveStatus"]);
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                    obj.Createddate = Convert.ToDateTime(dr["CreatedDate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStudentleavedetailBySlmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        
        public int AddStudentleave(Studentleavedetail _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AMDStudentleavedetail");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Slmid", _master.Slmid);
                cmd.Parameters.AddWithValue("@Smid", _master.Smid);
                cmd.Parameters.AddWithValue("@AdmissionNo", _master.AdmissionNo);
                cmd.Parameters.AddWithValue("@Leavestartdate", _master.Leavestartdate);
                cmd.Parameters.AddWithValue("@Leaveenddate", _master.Leaveenddate);
                cmd.Parameters.AddWithValue("@LeaveRreason", _master.LeaveRreason);
                cmd.Parameters.AddWithValue("@Leavedocument", _master.Leavedocument);
                cmd.Parameters.AddWithValue("@SchoolId", _master.SchoolId);
                cmd.Parameters.AddWithValue("@FMid", _master.FMid);
                cmd.Parameters.AddWithValue("@CreatedBy", _master.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", _master.Createddate);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@LeaveStatus", _master.LeaveStatus);
                cmd.Parameters.AddWithValue("@Remarks", _master.Remarks);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddStudentleave)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public DataTable GetStudentleaveTbl(Studentleavedetail _objeleave, Nullable<int> CMid, Nullable<int> SecMid, int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_Studentleaverecords");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@CMid", CMid);
                cmd.Parameters.AddWithValue("@SecMid", SecMid);
                cmd.Parameters.AddWithValue("@LeaveStatus", _objeleave.LeaveStatus);
                cmd.Parameters.AddWithValue("@Leavestartdate", _objeleave.Leavestartdate);
                cmd.Parameters.AddWithValue("@Leaveenddate", _objeleave.Leaveenddate);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetStudentleaveTbl)", "Error_014", ex, "DALCommon");
            }
            return _ds.Tables[0];
        }

        #region HR Management System
        public DataTable GetSalaryheadTbl( int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_GetSalaryheadrecords");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSalaryheadTbl)", "Error_014", ex, "DALCommon");
            }
            return _ds.Tables[0];
        }
        public List<Salaryheadmaster> GetSalaryheadlist(int SchoolID, int FinancialYear,int EMP_ID=0)
        {
            List<Salaryheadmaster> _salheadlist = new List<Salaryheadmaster>();
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_GetSalaryheadrecords");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@EMP_ID", EMP_ID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    Salaryheadmaster _stud = new Salaryheadmaster()
                    {
                        Shmid = Convert.ToInt32(dr["Shmid"]),
                        Headname = Convert.ToString(dr["Headname"]),
                        Alias = Convert.ToString(dr["Alias"]),
                        Displayname = Convert.ToString(dr["Displayname"]),
                        Headtype = Convert.ToString(dr["Headtype"]),
                        Defaultvalue = Convert.ToDouble(dr["Defaultvalue"]),
                        Leavededucation = Convert.ToString(dr["Leavededucation"]),
                        Esiapplicable = Convert.ToString(dr["Esiapplicable"]),
                        Epfapplicable = Convert.ToString(dr["Epfapplicable"]),
                        Calculationtype = Convert.ToString(dr["Calculationtype"]),
                        Displaysequence = Convert.ToString(dr["Displaysequence"]),
                        SchoolId = Convert.ToInt32(dr["SchoolId"]),
                        FinancialYear = Convert.ToInt32(dr["FinancialYear"]),
                        Createdate = Convert.ToDateTime(dr["Createdate"]),
                        Modifieddate = Convert.ToDateTime(dr["Modifieddate"]),
                        Createdby = Convert.ToString(dr["Createdby"]),
                        Modifiedby = Convert.ToString(dr["Modifiedby"])
                    };
                    _salheadlist.Add(_stud);
                }
            }
            catch (Exception ex)
            {

            }
            return _salheadlist;
        }
        public Salaryheadmaster GetSalaryheaddetailbyShmid(int Shmid)
        {
            Salaryheadmaster obj = new Salaryheadmaster();
            SqlCommand cmd = new SqlCommand("Usp_GetSalaryheaddetailbyShmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shmid", Shmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Shmid = Convert.ToInt32(dr["Shmid"]);
                    obj.Headname = Convert.ToString(dr["Headname"]);
                    obj.Alias = Convert.ToString(dr["Alias"]);
                    obj.Displayname = Convert.ToString(dr["Displayname"]);
                    obj.Headtype = Convert.ToString(dr["Headtype"]);
                    obj.Defaultvalue = Convert.ToDouble(dr["Defaultvalue"]);
                    obj.Leavededucation = Convert.ToString(dr["Leavededucation"]);
                    obj.Esiapplicable = Convert.ToString(dr["Esiapplicable"]);
                    obj.Epfapplicable = Convert.ToString(dr["Epfapplicable"]);
                    obj.Calculationtype = Convert.ToString(dr["Calculationtype"]);
                    obj.Displaysequence = Convert.ToString(dr["Displaysequence"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.FinancialYear = Convert.ToInt32(dr["FinancialYear"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSalaryheaddetailbyShmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddSalaryheaddetail(Salaryheadmaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("Usp_Salaryheadopration");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shmid", _master.Shmid);
                cmd.Parameters.AddWithValue("@Headname", _master.Headname);
                cmd.Parameters.AddWithValue("@Alias", _master.Alias);
                cmd.Parameters.AddWithValue("@Displayname", _master.Displayname);
                cmd.Parameters.AddWithValue("@Headtype", _master.Headtype);
                cmd.Parameters.AddWithValue("@Defaultvalue", _master.Defaultvalue);
                cmd.Parameters.AddWithValue("@Leavededucation", _master.Leavededucation);
                cmd.Parameters.AddWithValue("@Esiapplicable", _master.Esiapplicable);
                cmd.Parameters.AddWithValue("@Epfapplicable", _master.Epfapplicable);
                cmd.Parameters.AddWithValue("@Calculationtype", _master.Calculationtype);
                cmd.Parameters.AddWithValue("@Displaysequence", _master.Displaysequence);
                cmd.Parameters.AddWithValue("@SchoolId", _master.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", _master.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddSalaryheaddetail)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public DataTable GetSalaryheadassignTbl(int SchoolID, int FinancialYear)
        {
            DataSet _ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Usp_GetSalaryheadassign");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                _ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSalaryheadassignTbl)", "Error_014", ex, "DALCommon");
            }
            return _ds.Tables[0];
        }

        public int Assignsalaryheadtoemployee(DataTable Salaryheadtoemployee, Assignheadtoemployee _objassign, string optype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_Assignsalaryheadtoemployee");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Salaryheadtoemployee", Salaryheadtoemployee);
                cmd.Parameters.AddWithValue("@EMP_ID", _objassign.EMP_ID);
                cmd.Parameters.AddWithValue("@IsPF", _objassign.IsPF);
                cmd.Parameters.AddWithValue("@IsPFType", _objassign.IsPFType);
                cmd.Parameters.AddWithValue("@IsESIType", _objassign.IsESIType);
                cmd.Parameters.AddWithValue("@PFEmployeeamt", _objassign.PFEmployeeamt);
                cmd.Parameters.AddWithValue("@PFEmployeramt", _objassign.PFEmployeramt);
                cmd.Parameters.AddWithValue("@IsESI", _objassign.IsESI);
                cmd.Parameters.AddWithValue("@ESIEmployeeamt", _objassign.ESIEmployeeamt);
                cmd.Parameters.AddWithValue("@ESIEmployeramt", _objassign.ESIEmployeramt);
                cmd.Parameters.AddWithValue("@IsTDS", _objassign.IsTDS);
                cmd.Parameters.AddWithValue("@Taxcategory", _objassign.Taxcategory);
                cmd.Parameters.AddWithValue("@Shfittype", _objassign.Shfittype);
        
                cmd.Parameters.AddWithValue("@SchoolId", _objassign.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", _objassign.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", _objassign.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _objassign.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _objassign.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _objassign.Modifiedby);
                cmd.Parameters.AddWithValue("@optype", optype);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Assignsalaryheadtoemployee)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public TDSSettingmaster GetTDSSettingByTdsmid(int Tdsmid)
        {
            TDSSettingmaster obj = new TDSSettingmaster();
            SqlCommand cmd = new SqlCommand("USP_GetTDSSettingByTdsmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tdsmid", Tdsmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Tdsmid = Convert.ToInt32(dr["Tdsmid"]);
                    obj.Taxcategory = Convert.ToString(dr["Taxcategory"]);
                    obj.TYear = Convert.ToInt32(dr["TYear"]);
                    obj.ApplicableFrom = Convert.ToDateTime(dr["ApplicableFrom"]);
                    obj.TaxRate = Convert.ToDouble(dr["TaxRate"]);
                    obj.Tminamount = Convert.ToDouble(dr["Tminamount"]);
                    obj.Tmaxamount = Convert.ToDouble(dr["Tmaxamount"]);

                    obj.Schoolid = Convert.ToInt32(dr["Schoolid"]);
                   
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetReligionByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<TDSSettingmaster> GetTDSSettingList(int SchoolID, int FinancialYearID)
        {
            List<TDSSettingmaster> obj = new List<TDSSettingmaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetTDSSettingList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", FinancialYearID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new TDSSettingmaster
                        {
                            Tdsmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Tdsmid"]),
                            Taxcategory = Convert.ToString(ds.Tables[0].Rows[l]["Tax Category"]),
                            TYear = Convert.ToInt32(ds.Tables[0].Rows[l]["Year"]),
                            ApplicableFrom = Convert.ToDateTime(ds.Tables[0].Rows[l]["Applicable From"]),
                            TaxRate = Convert.ToDouble(ds.Tables[0].Rows[l]["Tax Rate"]),
                            Tminamount = Convert.ToDouble(ds.Tables[0].Rows[l]["Minimum Amount"]),
                            Tmaxamount = Convert.ToDouble(ds.Tables[0].Rows[l]["Maximum Amount"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetTDSSettingList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public DataTable GetTDSSettingListTbl(int SchoolID, int FinancialYearID)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetTDSSettingList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", FinancialYearID);
                ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetTDSSettingList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }

        public int AddTDSSetting(TDSSettingmaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddTDSSetting");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tdsmid", _master.Tdsmid);
                cmd.Parameters.AddWithValue("@Taxcategory", _master.Taxcategory);
                cmd.Parameters.AddWithValue("@TYear", _master.TYear);
                cmd.Parameters.AddWithValue("@ApplicableFrom", _master.ApplicableFrom);
                cmd.Parameters.AddWithValue("@TaxRate", _master.TaxRate);
                cmd.Parameters.AddWithValue("@Tminamount", _master.Tminamount);
                cmd.Parameters.AddWithValue("@Tmaxamount", _master.Tmaxamount);
                cmd.Parameters.AddWithValue("@Schoolid", _master.Schoolid);
                cmd.Parameters.AddWithValue("@FinancialYear", _master.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddTDSSetting)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        #endregion

        public DataTable GetShiftmasterTblList(int SchoolID)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_ShiftmasterTblList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetShiftmasterTblList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public List<ShiftMaster> GetShiftmasterList(int SchoolID)
        {
            List<ShiftMaster> obj = new List<ShiftMaster>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetShiftmasterList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables.Count == 1)
                {
                    for (int l = 0; l < ds.Tables[0].Rows.Count; l++)
                    {
                        obj.Add(new ShiftMaster
                        {
                            Shiftmid = Convert.ToInt32(ds.Tables[0].Rows[l]["Shiftmid"]),
                            ShiftName = Convert.ToString(ds.Tables[0].Rows[l]["ShiftName"]),
                            Shiftdesc = Convert.ToString(ds.Tables[0].Rows[l]["ShiftName"]) + "- " + Convert.ToString(ds.Tables[0].Rows[l]["ShiftStart"]) + "- " + Convert.ToString(ds.Tables[0].Rows[l]["ShiftEnd"]),
                            ShiftStart = Convert.ToString(ds.Tables[0].Rows[l]["ShiftStart"]),
                            ShiftEnd = Convert.ToString(ds.Tables[0].Rows[l]["ShiftEnd"]),
                            Createdate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Createdate"]),
                            Modifieddate = Convert.ToDateTime(ds.Tables[0].Rows[l]["Modifieddate"]),
                            Createdby = Convert.ToString(ds.Tables[0].Rows[l]["Createdby"]),
                            Modifiedby = Convert.ToString(ds.Tables[0].Rows[l]["Modifiedby"]),
                            IsActive = Convert.ToBoolean(ds.Tables[0].Rows[l]["IsActive"]),
                            schoolid = Convert.ToInt32(ds.Tables[0].Rows[l]["schoolid"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetPeriodMasterList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public ShiftMaster GetShiftByShiftmid(int Shiftmid)
        {
            ShiftMaster obj = new ShiftMaster();
            SqlCommand cmd = new SqlCommand("USP_GetShiftByShiftmid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shiftmid", Shiftmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Shiftmid = Convert.ToInt32(dr["Shiftmid"]);
                    obj.ShiftName = Convert.ToString(dr["ShiftName"]);
                    obj.Shiftdesc = Convert.ToString(dr["Shiftdesc"]);
                    obj.schoolid = Convert.ToInt32(dr["schoolid"]);
                    obj.ShiftStart = Convert.ToString(dr["ShiftStart"]);
                    obj.ShiftEnd = Convert.ToString(dr["ShiftEnd"]);
                    obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetShiftByShiftmid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddShift(ShiftMaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_AddShift");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Shiftmid", _master.Shiftmid);
                cmd.Parameters.AddWithValue("@ShiftName", _master.ShiftName);
                cmd.Parameters.AddWithValue("@Shiftdesc", _master.Shiftdesc);
                cmd.Parameters.AddWithValue("@ShiftStart", _master.ShiftStart);
                cmd.Parameters.AddWithValue("@ShiftEnd", _master.ShiftEnd);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@IsActive", _master.IsActive);
                cmd.Parameters.AddWithValue("@schoolid", _master.schoolid);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddPeriod)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public DataTable GetQuizeexamListTBL(int SchoolID, int FinancialYear)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_GetQuizeexamList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetQuizeexamListTBL)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }

        public Quizeexammaster GetQuizeexamByid(int Qzmid)
        {
            Quizeexammaster obj = new Quizeexammaster();
            SqlCommand cmd = new SqlCommand("Usp_GetQuizeexamByid");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Qzmid", Qzmid);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Qzmid = Convert.ToInt32(dr["Qzmid"]);
                    obj.Examtitle = Convert.ToString(dr["Examtitle"]);
                    obj.Classid = Convert.ToInt32(dr["Classid"]);
                    obj.Subjectid = Convert.ToInt32(dr["Subjectid"]);
                    obj.Rightque = Convert.ToInt32(dr["Rightque"]);
                    obj.Wrongque = Convert.ToInt32(dr["Wrongque"]);
                    obj.ExamTime = Convert.ToInt32(dr["ExamTime"]);
                    obj.Description = Convert.ToString(dr["Description"]);
                    obj.Totalquestion = Convert.ToInt32(dr["Totalquestion"]);
                    obj.Examdate = DBNull.Value != dr["Examdate"] ? (DateTime)dr["Examdate"] : default(DateTime); ;
                    //obj.Examdate = Convert.ToDateTime(dr["Examdate"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.FinancialYear = Convert.ToInt32(dr["FinancialYear"]);
                    obj.Createdate = Convert.ToDateTime(dr["Createdate"]);
                    obj.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                    obj.Createdby = Convert.ToString(dr["Createdby"]);
                    obj.Modifieddate = Convert.ToDateTime(dr["Modifieddate"]);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetReligionByid)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public int AddQuizeexam(Quizeexammaster _master, string Otype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("Usp_Quizeexammaster");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Qzmid", _master.Qzmid);
                cmd.Parameters.AddWithValue("@Examtitle", _master.Examtitle);
                cmd.Parameters.AddWithValue("@Classid", _master.Classid);
                cmd.Parameters.AddWithValue("@Subjectid", _master.Subjectid);
                cmd.Parameters.AddWithValue("@Rightque", _master.Rightque);
                cmd.Parameters.AddWithValue("@Wrongque", _master.Wrongque);
                cmd.Parameters.AddWithValue("@ExamTime", _master.ExamTime);
                cmd.Parameters.AddWithValue("@Description", _master.Description);
                cmd.Parameters.AddWithValue("@Totalquestion", _master.Totalquestion);
                cmd.Parameters.AddWithValue("@Examdate", _master.Examdate);
                cmd.Parameters.AddWithValue("@SchoolId", _master.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", _master.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", _master.Createdate);
                cmd.Parameters.AddWithValue("@Createdby", _master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _master.Modifiedby);
                cmd.Parameters.AddWithValue("@Modifieddate", _master.Modifieddate);
                cmd.Parameters.AddWithValue("@Otype", Otype);
                var _result = DBHelper.ExecuteScalar(cmd);
                retype = Convert.ToInt32(_result);


            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(AddReligion)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }


        public int Addquestionans(DataTable dtquestionans, Quizeexammaster _quizrec, string optype)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_Addquestionanswithoptions");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@questionoptionanswerlst", dtquestionans);
                cmd.Parameters.AddWithValue("@Qzmid", _quizrec.Qzmid);
                cmd.Parameters.AddWithValue("@Choice", 4);
                cmd.Parameters.AddWithValue("@SchoolId", _quizrec.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", _quizrec.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", _quizrec.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _quizrec.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _quizrec.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _quizrec.Modifiedby);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Addquestionans)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }

        public int Quizrecording(DataTable dtQuizrecording, Quizeexammaster _userrec)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_Quizrecording");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dtQuizrecording", dtQuizrecording);
                cmd.Parameters.AddWithValue("@SchoolId", _userrec.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", _userrec.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", _userrec.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", _userrec.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", _userrec.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", _userrec.Modifiedby);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Addquestionans)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
        public DataTable GetQuizequestionList(int Qzmid)
        {

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Usp_GetQuizequestionList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Qzmid", Qzmid);
                ds = DBHelper.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetQuizequestionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }

        public DataTable GetQuestionoptionList(int Qmid)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Usp_GetQuestionoptionList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Qmid", Qmid);
                ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Usp_GetQuestionoptionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }

        public int Assignstudent4online(Onlineexamstudent master, string studentlist)
        {
            int retype = -1;
            SqlCommand cmd = new SqlCommand("USP_Assignstudent4online");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@studentlist", studentlist);
                cmd.Parameters.AddWithValue("@quazid", master.quazid);
                cmd.Parameters.AddWithValue("@SchoolId", master.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", master.FinancialYear);
                cmd.Parameters.AddWithValue("@Createdate", master.Createdate);
                cmd.Parameters.AddWithValue("@Modifieddate", master.Modifieddate);
                cmd.Parameters.AddWithValue("@Createdby", master.Createdby);
                cmd.Parameters.AddWithValue("@Modifiedby", master.Modifiedby);
                retype = DBHelper.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(Assignstudent4online)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return retype;
        }
    }
}
