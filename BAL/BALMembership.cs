using SHARED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class BALMembership
    {
        string ConStr = "";
        public BALMembership(string ConnectionString)
        {
            ConStr = ConnectionString;
        }


    }
}
