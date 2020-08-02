using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARED
{
    class Student
    {
    }
    public class IDCard : BaseModel
    {
        public OragnisationMaster OragnisationDetails { get; set; }
        public List<StudentMaster> StudentDetailsList { get; set; }
        public string CardType { get; set; }
    }
    public class StudnetRollNo : BaseModel
    {
        public List<ClassMaster> ClassList { get; set; }

        public int ClassID { get; set; }
        public List<SectionMaster> SectionList { get; set; }

        public int SectionID { get; set; }
        public List<StudentMaster> StudentList { get; set; }
    }
    public class PromoteStudent : BaseModel
    {
        public List<ClassMaster> ClassList { get; set; }
        public int PrevClassID { get; set; }
        public int NextClassID { get; set; }

        public List<SectionMaster> SectionList { get; set; }
        public int PrevSectionID { get; set; }
        public int NextSectionID { get; set; }

        public List<FinancialYear> FinancialYearList { get; set; }
        public int PrevFinancialYearID { get; set; }
        public int NextFinancialYearID { get; set; }

        public List<StudentMaster> StudentList { get; set; }
    }

}
