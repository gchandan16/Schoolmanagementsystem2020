using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class SalaryheadModel: BaseModel
    {
        public int Shmid { get; set; }
        public string Headname { get; set; }
        public string Alias { get; set; }
        public string Displayname { get; set; }
        public string Headtype { get; set; }
        public Double Defaultvalue { get; set; }
        public string Leavededucation { get; set; }
        public string Esiapplicable { get; set; }
        public string Epfapplicable { get; set; }
        public string Calculationtype { get; set; }
        public string Displaysequence { get; set; }
        public string ActionName { get; set; }
        public List<Salaryheadmaster> SalaryheadList { get; set; }
        public DataTable SalaryheadTblList { get; set; }
    }
}