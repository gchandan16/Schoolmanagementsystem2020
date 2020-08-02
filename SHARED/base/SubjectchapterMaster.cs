using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public partial class SubjectchapterMaster
{
        public int Scmid { get; set; }
        public int Sumid { get; set; }
        public int CMid { get; set; }
        public string Chaptername { get; set; }
        public string Chapterdes { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public Nullable<System.DateTime> Modifieddate { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public bool IsActive { get; set; }
        public int schoolid { get; set; }
    }
}
