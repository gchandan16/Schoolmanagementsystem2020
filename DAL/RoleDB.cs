using ERROR;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public class RoleDB
    {
        private SqlDatabase DBHelper;
        private SqlCommand cmd = null;
        DataSet ds = null;
        public RoleDB(string connectionstring)
        {
            DBHelper = new SqlDatabase(connectionstring);
        }

        public string  AddRole(RoleMaster rolemaster,List<MenuPermissionMapMaster> menupermissionList)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand();
            try
            {
                DataSet _ds = new DataSet();
                cmd.CommandText = "USP_Addupdrolewithmenu";
                DataTable _mstmenupermission = new DataTable();
                if (menupermissionList != null)
                {
                    _mstmenupermission.Columns.Add("MID", typeof(int));//Auto genrated for sample
                    _mstmenupermission.Columns.Add("MENU_ID", typeof(int));
                    _mstmenupermission.Columns.Add("PERMISSION_ID", typeof(int));
                    int increment = 1;
                    foreach (var item in menupermissionList)
                    {
                        DataRow dr = _mstmenupermission.NewRow();
                        dr[0] = increment;
                        dr[1] = item.MENU_ID;
                        dr[2] = item.PERMISSION_ID;
                        _mstmenupermission.Rows.Add(dr);
                        increment = increment + 1;
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_ID", rolemaster.ROLE_ID);
                cmd.Parameters.AddWithValue("@ROLENAME", rolemaster.ROLENAME);
                cmd.Parameters.AddWithValue("@SchoolID", rolemaster.SchoolID);
                cmd.Parameters.AddWithValue("@ROLEDESCRIPTION", rolemaster.ROLEDESCRIPTION);
                cmd.Parameters.AddWithValue("@ISACTIVE", rolemaster.ISACTIVE);
                cmd.Parameters.AddWithValue("@CREATEDBY", rolemaster.CREATEDBY);
                cmd.Parameters.AddWithValue("@CREATEDDATE", rolemaster.CREATEDDATE);
                cmd.Parameters.AddWithValue("@menupermissionList", _mstmenupermission);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("AddRole(AddRole)", "Error_014", ex, "AddRole");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public string  UpdateRole(RoleMaster rolemaster, List<MenuPermissionMapMaster> menupermissionList)
        {

            string result = "";
            SqlCommand cmd = new SqlCommand("USP_Addupdrolewithmenu");
            try
            {
                DataSet _ds = new DataSet();
               
                DataTable _mstmenupermission = new DataTable();
                if (menupermissionList != null)
                {
                    _mstmenupermission.Columns.Add("MID", typeof(int));//Auto genrated for sample
                    _mstmenupermission.Columns.Add("MENU_ID", typeof(int));
                    _mstmenupermission.Columns.Add("PERMISSION_ID", typeof(int));
                    int increment = 1;
                    foreach (var item in menupermissionList)
                    {
                        DataRow dr = _mstmenupermission.NewRow();
                        dr[0] = increment;
                        dr[1] = item.MENU_ID;
                        dr[2] = item.PERMISSION_ID;
                        _mstmenupermission.Rows.Add(dr);
                        increment = increment + 1;
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ROLE_ID", rolemaster.ROLE_ID);
                cmd.Parameters.AddWithValue("@ROLENAME", rolemaster.ROLENAME);
                cmd.Parameters.AddWithValue("@SchoolID", rolemaster.SchoolID);
                cmd.Parameters.AddWithValue("@ROLEDESCRIPTION", rolemaster.ROLEDESCRIPTION);
                cmd.Parameters.AddWithValue("@ISACTIVE", rolemaster.ISACTIVE);
                cmd.Parameters.AddWithValue("@CREATEDBY", rolemaster.CREATEDBY);
                cmd.Parameters.AddWithValue("@CREATEDDATE", rolemaster.CREATEDDATE);
                cmd.Parameters.AddWithValue("@menupermissionList", _mstmenupermission);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("RoleDB(UpdateRole)", "Error_014", ex, "UpdateRole");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }


      public RoleMaster  GetByRoleId(int roleId,int SchoolID)
        {
            RoleMaster _rmobj = new RoleMaster();
            DataSet _ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_MENUPERMISION_GETBYROLEID");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleId",roleId);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                ds = DBHelper.ExecuteDataSet(cmd);
                if(ds!=null && ds.Tables.Count>0)
                {
                    for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    {
                        _rmobj.ROLE_ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ROLE_ID"]);
                        _rmobj.ROLENAME = Convert.ToString(ds.Tables[0].Rows[i]["ROLENAME"]);
                        _rmobj.ROLEDESCRIPTION = Convert.ToString(ds.Tables[0].Rows[i]["ROLEDESCRIPTION"]);
                        _rmobj.ISACTIVE = Convert.ToBoolean(ds.Tables[0].Rows[i]["ISACTIVE"]);
                        _rmobj.CREATEDBY = Convert.ToString(ds.Tables[0].Rows[i]["CREATEDBY"]);
                        _rmobj.CREATEDDATE = Convert.ToDateTime(ds.Tables[0].Rows[i]["CREATEDDATE"]);
                    }

                   if(ds.Tables[1].Rows.Count>0)
                    {
                        _rmobj.MenuPermissionList = new List<MenuPermissionMapMaster>();
                        for (int j=0;j<ds.Tables[1].Rows.Count;j++)
                        {
                            _rmobj.MenuPermissionList.Add(new MenuPermissionMapMaster {
                                MENU_ID = Convert.ToInt32(ds.Tables[1].Rows[j]["MENU_ID"]),
                                PERMISSION_ID = Convert.ToInt32(ds.Tables[1].Rows[j]["PERMISSION_ID"])
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetByRoleId)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
           return _rmobj;
        }
    }
}
