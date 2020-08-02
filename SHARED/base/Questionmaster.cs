using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
 public class Questionmaster
{
        public int Qmid { get; set; }
        public int Qansid { get; set; }
        public string Question { get; set; }
        public List<Questionoptions> Optionlist { get; set; }
    }
    public class Questionoptions
    {
        public int Opmid { get; set; }
        public string Optiontext { get; set; }
    }
}
