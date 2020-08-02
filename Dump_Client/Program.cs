using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dump_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("strat program");
            CustomerClient _objclient = new CustomerClient();
            _objclient.SyncClientdata();
            Console.WriteLine("end program");
        }
    }
}
