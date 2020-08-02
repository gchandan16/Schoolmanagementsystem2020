using SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class StartexaminationModel : BaseModel
    {
        public int Qzmid { get; set; }
        public string Examtitle { get; set; }
        public string ActionName { get; set; }
        public List<Questionmaster> Questionlist { get; set; }

    }
}