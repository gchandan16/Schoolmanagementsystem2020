using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    public class Transport
    {
    }
    public class VehicleDetails : BaseModel
    {
        public int VehicleID { get; set; }
        public string State { get; set; }
        public string Sequential { get; set; }
        public string RTO { get; set; }
        public string UniqueNo { get; set; }
        public int TotalSeats { get; set; }
        public int AllowedSeats { get; set; }
        public string OwnershipType { get; set; }
        public string InsuranceExpiry { get; set; }
        public string PollutionExpiry { get; set; }
        public string GPS { get; set; }
        public string CCTV { get; set; }
        public string VehicleSpecification { get; set; }
        public string IsActive { get; set; }
        public string ActionName { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string CreatedBy { get; set; }
    }
    public class Vehicle : BaseModel
    {
        public List<VehicleDetails> VehicleDetailList { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string ActionName { get; set; }
    }
    public class Driver : BaseModel
    {
        public List<DriverDetails> DriverDetailList { get; set; }
        public List<VehicleDetails> VehicleDetailList { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string ActionName { get; set; }
    }

    public class DriverDetails : BaseModel
    {
        public int DriverID { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Phone { get; set; }
        public string LicenseNo { get; set; }
        public string IsEmployee { get; set; }
        public int EmployeeID { get; set; }
        public int VehicleID { get; set; }
        public VehicleDetails VehicleDetail { get; set; }
        public EmployeeMaster EmployeeDetail { get; set; }

        public string ActionName { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string CreatedBy { get; set; }
        public string IsActive { get; set; }
    }
    public class Route : BaseModel
    {
        public List<RouteDetails> RouteDetailList { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string ActionName { get; set; }
    }
    public class RouteDetails : BaseModel
    {
        public int RouteID { get; set; }
        public string RouteName { get; set; }
        public int VehicleID { get; set; }

        public VehicleDetails VehicleDetail { get; set; }
        public string ActionName { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string CreatedBy { get; set; }
        public string IsActive { get; set; }
        public List<PickDropPoint> PickDropPointLst { get; set; }
    }
    public class PickDropPoint
    {
        public int PickDropID { get; set; }
        public string PickDropPointName { get; set; }
        public string PickupTime { get; set; }
        public string DropTime { get; set; }
        public double Fee { get; set; }
        public string IsActive { get; set; }
        public int RouteID { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public string Action { get; set; }
        public string CreatedBy { get; set; }
    }

    public class AssignTransport : BaseModel
    {

    }
    public class TransportDetails
    {
        public int TPCostID { get; set; }
        public int StudentID { get; set; }
        public int SchoolID { get; set; }
        public int FinancialYearID { get; set; }
        public int RouteID { get; set; }
        public int PickDropPointID { get; set; }
        public decimal Fee { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public string Action { get; set; }
    }
    public class AllocationReport : BaseModel
    {
       
    }
}
