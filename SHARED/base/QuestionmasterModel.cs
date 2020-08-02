using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class QuestionmasterModel : BaseModel
    {
        public int Qzmid { get; set; }
        public string Examtitle { get; set; }
        public string ActionName { get; set; }
        public List<Questionmaster> Questionlist { get; set; }
        
    }
    
}