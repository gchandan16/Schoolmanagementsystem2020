using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED;
using DAL;

namespace BAL
{
    public class BALTransport
    {
        string ConStr = "";
        public BALTransport(string ConnectionString)
        {
            ConStr = ConnectionString;
        }
        public List<VehicleDetails> GetVehicleList(Vehicle model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.GetVehicleList(model);
        }
        public int InsertUpdateVehicleDetails(VehicleDetails model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.InsertUpdateVehicleDetails(model);
        }

        public List<DriverDetails> GetDriverList(Driver model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.GetDriverList(model);
        }
        public int InsertUpdateDriverDetails(DriverDetails model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.InsertUpdateDriverDetails(model);
        }

        public List<RouteDetails> GetRouteList(Route model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.GetRouteList(model);
        }
        public int InsertUpdateRouteDetails(RouteDetails model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.InsertUpdateRouteDetails(model);
        }
        public List<PickDropPoint> GetPickDropPointList(RouteDetails model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.GetPickDropPointList(model);
        }
        public int InsertUpdatePickDropPointDetails(List<PickDropPoint> model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.InsertUpdatePickDropPointDetails(model);
        }
        public int AssignTransport(TransportDetails transportDetail)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.AssignTransport(transportDetail);
        }
        public TransportDetails GetAssignTransport(TransportDetails model)
        {
            DALTransport dal = new DALTransport(ConStr);
            return dal.GetAssignTransport(model);
        }
    }
}
