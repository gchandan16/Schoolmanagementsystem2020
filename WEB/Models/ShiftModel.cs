using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class ShiftModel : BaseModel
    {
        public int Shiftmid { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
        public int schoolid { get; set; }
        public string Shiftdesc { get; set; }
        public string ActionName { get; set; }
        public List<ShiftMaster> ShiftmasterList { get; set; }
        public DataTable ShiftmasterTblList { get; set; }
    }
}