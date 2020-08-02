using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using SHARED;

namespace BAL
{
    public class BALStudent
    {
        string ConStr = "";
        public BALStudent(string ConnectionString)
        {
            ConStr = ConnectionString;
        }
        public List<ClassMaster> GetClassList(int SchoolId)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetClassList(SchoolId);
        }
        public List<SectionMaster> GetSectionList(int SchoolID, int Classid)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.GetSectionList(SchoolID, Classid);
        }
        public string PromoteStudent(DataTable DT, int PrevFYID, int NextFYID, int ClassID, int SectionID)
        {
            DALCommon dal = new DALCommon(ConStr);
            return dal.PromoteStudent(DT, PrevFYID, NextFYID, ClassID, SectionID);
        }
    }
}
