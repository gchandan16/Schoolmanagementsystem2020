using BAL;
using DumpAPI.ActionFilters;
using SHARED;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClientAPI.Controllers
{
    [ApiAuthenticationFilter]
    public class ClientdataController : ApiController
    {
        public HttpResponseMessage Datapacket([FromBody] List<PacketEntity> packetlist)
        {
            // Log.Debug("Post packet start at side");
            //List<PacketEntity> packetlist = JsonConvert.DeserializeObject<List<PacketEntity>>(value);
            string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            BALCommon CSvc = new BALCommon(ConStr);
            #region DataTable
            // DataTable for MCode table
            DataTable populateTable = new DataTable();
            populateTable.Columns.Add("Id", typeof(int));
            populateTable.Columns.Add("student_biometricid", typeof(string));
            populateTable.Columns.Add("student_RFId", typeof(string));
            populateTable.Columns.Add("Punchdatetime", typeof(DateTime));
            populateTable.Columns.Add("student_cardnumber", typeof(string));
            populateTable.Columns.Add("student_paycode", typeof(string));
            populateTable.Columns.Add("Customercode", typeof(string));
            populateTable.Columns.Add("Macaddress", typeof(string));
            populateTable.Columns.Add("Ipaddress", typeof(string));

            #endregion

            foreach (var downobjet in packetlist)
            {
                DataRow Row = populateTable.NewRow();
                Row[0] = downobjet.Id;
                Row[1] = downobjet.student_biometricid;
                Row[2] = downobjet.student_RFId;
                Row[3] = downobjet.Punchdatetime;
                Row[4] = downobjet.student_cardnumber;
                Row[5] = downobjet.student_paycode;
                Row[6] = downobjet.Customercode;
                Row[7] = downobjet.Macaddress;
                Row[8] = downobjet.Ipaddress;
                populateTable.Rows.Add(Row);
            }
            // Log.Debug(string.Format("Insert records Cout{0}", populateTable.Rows.Count.ToString()));
            int serverid = CSvc.InsertInDataPacket(populateTable);
            // Log.Debug(string.Format("update record below to id={0}", serverid.ToString()));
            if (serverid != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, serverid);
            }
            return Request.CreateResponse(HttpStatusCode.NoContent, default(int));
        }

    }
}
