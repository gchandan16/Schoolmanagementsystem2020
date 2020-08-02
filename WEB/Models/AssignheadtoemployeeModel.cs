using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class AssignheadtoemployeeModel : BaseModel
    {
        public int Ashmid { get; set; }
        public int EMP_ID { get; set; }
        public int Shmid { get; set; }
        public  Double Amount { get; set; }
        public Nullable<DateTime> Applyfrom { get; set; }
        public string IsPF { get; set; }
        public string IsPFType { get; set; }
        public string IsESIType { get; set; }
        public Double PFEmployeeamt { get; set; }
        public Double PFEmployeramt { get; set; }
        public string IsESI { get; set; }
    
        public Double ESIEmployeeamt { get; set; }
        public Double ESIEmployeramt { get; set; }

        public string IsTDS { get; set; }
        public string Taxcategory { get; set; }
        public string Shfittype { get; set; }

        public string ActionName { get; set; }
        public List<ShiftMaster> Shiftlist { get; set; }
        public List<Salaryheadmaster> Salaryheadlist { get; set; }
        public List<EmployeeMaster> EmployeeList { get; set; }
        public List<Assignheadtoemployee> AssignheadtoemployeeList { get; set; }
        public DataTable AssignheadtoemployeetblList { get; set; }

    }
}