using Newtonsoft.Json;
using SHARED;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;


namespace Dump_Client
{
  public partial  class CustomerClient
    {
        public string PacketListAddress { get; set; }
        public string PacketUpdateAddress { get; set; }
        public string Authorization { get; set; }
        public string AuthorizationPWD { get; set; }
     public void SyncClientdata()
        {

            string HeaderTitle = ConfigurationManager.AppSettings["HeaderTitle"];
            string HeaderValue = ConfigurationManager.AppSettings["HeaderValue"];
            string PacketListAddress = ConfigurationManager.AppSettings["PacketListAddress"];
            string PacketUpdateAddress = ConfigurationManager.AppSettings["PacketUpdateAddress"];
            CustomerClient p1 = new CustomerClient();
            p1.Authorization = HeaderTitle;
            p1.AuthorizationPWD = HeaderValue;
            p1.PacketListAddress = PacketListAddress;
            p1.PacketUpdateAddress = PacketUpdateAddress;
            GetAllPacket(p1);

        }
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
        public void GetAllPacket(CustomerClient p)
        {
            Console.WriteLine("Start Gettting packet");
            List<PacketEntity> packetlist = new List<PacketEntity>();
            string endpoint = p.PacketListAddress;
            try
            {
                string _constr= ConfigurationManager.AppSettings["con"];
                //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\iAS\TimeWatch.mdb;Jet OLEDB:Database Password=SSS;");
                OleDbConnection con = new OleDbConnection(_constr);
                con.Open();
                string _querystr = "Select * from MachineRawPunch";
                OleDbCommand cmd = new OleDbCommand(_querystr, con);
                int _cntr = 1;
                using (OleDbDataReader reader=cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PacketEntity _packet = new PacketEntity() { Id = _cntr, student_biometricid = Convert.ToString(reader["CARDNO"]), student_RFId = Convert.ToString(reader["CARDNO"]), Punchdatetime = Convert.ToDateTime(reader["OFFICEPUNCH"]), student_cardnumber = Convert.ToString(reader["CARDNO"]), student_paycode = Convert.ToString(reader["PAYCODE"]), Customercode = ConfigurationManager.AppSettings["Customercode"], Macaddress = GetMacAddress().ToString(), Ipaddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString() };
                        packetlist.Add(_packet);
                    };
                        
                        _cntr++;
                    
                }
               //  Log.Debug(string.Format("Record fetched from customer end {0}", packetlist.Count()));
                string output = JsonConvert.SerializeObject(packetlist);
                 var client = new RestClient(endpoint: endpoint, method: HttpVerb.POST, postData: output);
                 string jsondata = client.MakeRequest(p.Authorization, p.AuthorizationPWD);
                //Log.Debug(string.Format("successfull insert till id {0}", jsondata));
                int serverid;
                int.TryParse(jsondata, out serverid);
                Console.WriteLine("Fetch data packed");
            }
            catch (Exception ex)
            {
               // Log.Error(string.Format("exception on get all packet at client side {0}", ex.ToString()));
            }
            

        }
      
    }
}

