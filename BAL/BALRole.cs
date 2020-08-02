using DAL;
using SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
  public class BALRole
    {
        string ConStr = "";
        public BALRole(string ConnectionString)
        {
            ConStr = ConnectionString;
        }
        public void AddRole(RoleMaster rolemaster, List<MenuPermissionMapMaster> menupermissionList)
        {
            RoleDB Rdb = new RoleDB(ConStr);
            Rdb.AddRole(rolemaster, menupermissionList);
        }
        public void UpdateRole(RoleMaster rolemaster, List<MenuPermissionMapMaster> menupermissionList)
        {
            RoleDB Rdb = new RoleDB(ConStr);
            Rdb.UpdateRole(rolemaster, menupermissionList);
        }
        public RoleMaster GetByRoleId(int roleId,int schoolid)
        {
            RoleDB Rdb = new RoleDB(ConStr);
           return Rdb.GetByRoleId(roleId, schoolid); 
        }
    }
}
