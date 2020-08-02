using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class RoleModel:BaseModel
    {
        public int role_id { get; set; }

        [Required(ErrorMessage = "Please enter role name")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Please enter role description")]
        public string RoleDescription { get; set; }

        public string MenuList { get; set; }

        public bool IsActive { get; set; }

        public string ActionName { get; set; }

        public List<RoleMaster> RoleList { get; set; }

        public List<MenuPermissionMapMaster> selectedMenuPermission { get; set; }
    }
}