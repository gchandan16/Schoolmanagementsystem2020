using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHARED;
using System.Data.SqlClient;
using System.Data;
using ERROR;

namespace DAL
{
    public class DALFee
    {
        private SqlDatabase DBHelper;
        private SqlCommand cmd = null;
        DataSet ds = null;
        public DALFee(string connectionstring)
        {
            DBHelper = new SqlDatabase(connectionstring);
        }
        public List<FeeHead> GetFeeHeads(FeeHead FH)
        {
            List<FeeHead> obj = new List<FeeHead>();
            try
            {
                cmd = new SqlCommand("USP_FEE_HEADS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", FH.SchoolID);
                cmd.Parameters.AddWithValue("@CreatedBy", FH.CreatedBy);
                cmd.Parameters.AddWithValue("@Action", FH.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new FeeHead
                    {
                        FeeID = Convert.ToString(dr["FeeID"]),
                        SchoolID = Convert.ToInt32(dr["SchoolID"]),
                        FeeHeadName = Convert.ToString(dr["FeeHeadName"]),
                        Active = Convert.ToBoolean(dr["Active"]),
                        Frequency = Convert.ToString(dr["Frequency"]),
                        Refundable = Convert.ToString(dr["Refundable"]),
                        StartDate = Convert.ToString(dr["StartDate"]),
                        EndDate = Convert.ToString(dr["EndDate"]),
                        DueDate = Convert.ToString(dr["DueDate"]),
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                        months = new Months()
                        {
                            January = Convert.ToBoolean(dr["January"]),
                            February = Convert.ToBoolean(dr["February"]),
                            March = Convert.ToBoolean(dr["March"]),
                            April = Convert.ToBoolean(dr["April"]),
                            May = Convert.ToBoolean(dr["May"]),
                            June = Convert.ToBoolean(dr["June"]),
                            July = Convert.ToBoolean(dr["July"]),
                            August = Convert.ToBoolean(dr["August"]),
                            September = Convert.ToBoolean(dr["September"]),
                            October = Convert.ToBoolean(dr["October"]),
                            November = Convert.ToBoolean(dr["November"]),
                            December = Convert.ToBoolean(dr["December"]),
                        }

                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public string SaveFeeHead(FeeHead FH)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_FEE_HEADS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeID", string.IsNullOrEmpty(FH.FeeID) ? "0" : FH.FeeID);
                cmd.Parameters.AddWithValue("@FeeHeadName", FH.FeeHeadName);
                cmd.Parameters.AddWithValue("@Frequency", FH.Frequency);
                cmd.Parameters.AddWithValue("@Refundable", FH.Refundable);
                cmd.Parameters.AddWithValue("@StartDate", FH.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", FH.EndDate);
                cmd.Parameters.AddWithValue("@DueDate", FH.DueDate);
                cmd.Parameters.AddWithValue("@January", FH.months.January);
                cmd.Parameters.AddWithValue("@February", FH.months.February);
                cmd.Parameters.AddWithValue("@March", FH.months.March);
                cmd.Parameters.AddWithValue("@April", FH.months.April);
                cmd.Parameters.AddWithValue("@May", FH.months.May);
                cmd.Parameters.AddWithValue("@June", FH.months.June);
                cmd.Parameters.AddWithValue("@July", FH.months.July);
                cmd.Parameters.AddWithValue("@August", FH.months.August);
                cmd.Parameters.AddWithValue("@September", FH.months.September);
                cmd.Parameters.AddWithValue("@October", FH.months.October);
                cmd.Parameters.AddWithValue("@November", FH.months.November);
                cmd.Parameters.AddWithValue("@December", FH.months.December);
                cmd.Parameters.AddWithValue("@Active", FH.Active);
                cmd.Parameters.AddWithValue("@SchoolID", FH.SchoolID);
                cmd.Parameters.AddWithValue("@CreatedBy", FH.CreatedBy);
                cmd.Parameters.AddWithValue("@Action", FH.Action);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public string DeleteFeeHead(string FeeID)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_FEE_HEADS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeID", FeeID);
                cmd.Parameters.AddWithValue("@Action", "DEL");
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(DeleteFeeHeadDescription)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public List<FeeHeadDescription> GetFeeHeadDescription(FeeHeadDescription FHD)
        {
            List<FeeHeadDescription> obj = new List<FeeHeadDescription>();
            try
            {
                cmd = new SqlCommand("USP_FEE_HEADS_DESC");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeID", FHD.FeeID);
                cmd.Parameters.AddWithValue("@Action", FHD.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new FeeHeadDescription
                    {
                        FeeDescID = Convert.ToString(dr["FeeDescID"]),
                        FeeID = Convert.ToString(dr["FeeID"]),
                        FeeDescName = Convert.ToString(dr["FeeDescName"]),
                        Months = Convert.ToString(dr["Months"]),
                        StartDate = Convert.ToString(dr["StartDate"]),
                        EndDate = Convert.ToString(dr["EndDate"]),
                        DueDate = Convert.ToString(dr["DueDate"]),
                        Active = Convert.ToBoolean(dr["Active"]),
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public string SaveFeeHeadDescription(FeeHeadDescription FHD)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_FEE_HEADS_DESC");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeID", FHD.FeeID);
                cmd.Parameters.AddWithValue("@FeeDescName", FHD.FeeDescName);
                cmd.Parameters.AddWithValue("@StartDate", FHD.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", FHD.EndDate);
                cmd.Parameters.AddWithValue("@DueDate", FHD.DueDate);
                cmd.Parameters.AddWithValue("@Months", FHD.Months);
                cmd.Parameters.AddWithValue("@CreatedBy", FHD.CreatedBy);
                cmd.Parameters.AddWithValue("@Active", FHD.Active);
                cmd.Parameters.AddWithValue("@Action", FHD.Action);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeHeadDescription)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public string DeleteFeeHeadDescription(FeeHeadDescription FHD)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_FEE_HEADS_DESC");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeID", FHD.FeeID);
                cmd.Parameters.AddWithValue("@FeeDescID", FHD.FeeDescID);
                cmd.Parameters.AddWithValue("@Action", FHD.Action);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(DeleteFeeHeadDescription)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public List<FeeGroup> GetFeeGroup(FeeGroup FG)
        {
            List<FeeGroup> obj = new List<FeeGroup>();
            try
            {
                cmd = new SqlCommand("USP_FEE_GROUP");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", FG.SchoolID);
                cmd.Parameters.AddWithValue("@CreatedBy", FG.CreatedBy);
                cmd.Parameters.AddWithValue("@Action", FG.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    FeeHeadList FH = new FeeHeadList();
                    FH.Apr = Convert.ToString(dr["Apr"]);
                    FH.May = Convert.ToString(dr["May"]);
                    FH.Jun = Convert.ToString(dr["Jun"]);
                    FH.Jul = Convert.ToString(dr["Jul"]);
                    FH.Aug = Convert.ToString(dr["Aug"]);
                    FH.Sep = Convert.ToString(dr["Sep"]);
                    FH.Oct = Convert.ToString(dr["Oct"]);
                    FH.Nov = Convert.ToString(dr["Nov"]);
                    FH.Dec = Convert.ToString(dr["Dec"]);
                    FH.Jan = Convert.ToString(dr["Jan"]);
                    FH.Feb = Convert.ToString(dr["Feb"]);
                    FH.Mar = Convert.ToString(dr["Mar"]);
                    obj.Add(new FeeGroup
                    {
                        FeeGroupID = Convert.ToString(dr["FeeGroupID"]),
                        FeeGroupName = Convert.ToString(dr["FeeGroupName"]),
                        FeeHeadName = Convert.ToString(dr["FeeHeadName"]),
                        FeeHeadID = Convert.ToString(dr["FeeHeadID"]),
                        Amount = Convert.ToString(dr["Amount"]),
                        feeHead = FH,
                        SchoolID = Convert.ToInt32(dr["SchoolID"]),
                        Active = Convert.ToBoolean(dr["Active"]),
                        Frequency = Convert.ToString(dr["Frequency"]),
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(GetFeeGroup)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public string SaveFeeGroup(FeeGroup FG)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_FEE_GROUP");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeGroupID", string.IsNullOrEmpty(FG.FeeGroupID) ? "0" : FG.FeeGroupID);
                cmd.Parameters.AddWithValue("@FeeHeadName", FG.FeeHeadName);
                cmd.Parameters.AddWithValue("@FeeHeadID", FG.FeeHeadID);
                cmd.Parameters.AddWithValue("@FeeGroupName", FG.FeeGroupName);
                cmd.Parameters.AddWithValue("@Frequency", FG.Frequency);
                cmd.Parameters.AddWithValue("@Amount", FG.Amount);
                cmd.Parameters.AddWithValue("@Apr", string.IsNullOrEmpty(FG.feeHead.Apr) ? "0" : FG.feeHead.Apr);
                cmd.Parameters.AddWithValue("@May", string.IsNullOrEmpty(FG.feeHead.May) ? "0" : FG.feeHead.May);
                cmd.Parameters.AddWithValue("@Jun", string.IsNullOrEmpty(FG.feeHead.Jun) ? "0" : FG.feeHead.Jun);
                cmd.Parameters.AddWithValue("@Jul", string.IsNullOrEmpty(FG.feeHead.Jul) ? "0" : FG.feeHead.Jul);
                cmd.Parameters.AddWithValue("@Aug", string.IsNullOrEmpty(FG.feeHead.Aug) ? "0" : FG.feeHead.Aug);
                cmd.Parameters.AddWithValue("@Sep", string.IsNullOrEmpty(FG.feeHead.Sep) ? "0" : FG.feeHead.Sep);
                cmd.Parameters.AddWithValue("@Oct", string.IsNullOrEmpty(FG.feeHead.Oct) ? "0" : FG.feeHead.Oct);
                cmd.Parameters.AddWithValue("@Nov", string.IsNullOrEmpty(FG.feeHead.Nov) ? "0" : FG.feeHead.Nov);
                cmd.Parameters.AddWithValue("@Dec", string.IsNullOrEmpty(FG.feeHead.Dec) ? "0" : FG.feeHead.Dec);
                cmd.Parameters.AddWithValue("@Jan", string.IsNullOrEmpty(FG.feeHead.Jan) ? "0" : FG.feeHead.Jan);
                cmd.Parameters.AddWithValue("@Feb", string.IsNullOrEmpty(FG.feeHead.Feb) ? "0" : FG.feeHead.Feb);
                cmd.Parameters.AddWithValue("@Mar", string.IsNullOrEmpty(FG.feeHead.Mar) ? "0" : FG.feeHead.Mar);
                cmd.Parameters.AddWithValue("@Active", FG.Active);
                cmd.Parameters.AddWithValue("@SchoolID", FG.SchoolID);
                cmd.Parameters.AddWithValue("@CreatedBy", FG.CreatedBy);
                cmd.Parameters.AddWithValue("@Action", FG.Action);
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeGroup)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public string DeleteFeeGroup(string FeeGroupID, string FeeGroupName)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_FEE_GROUP");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeGroupID", FeeGroupID);
                cmd.Parameters.AddWithValue("@FeeGroupName", FeeGroupName);
                cmd.Parameters.AddWithValue("@Action", "DEL");
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(DeleteFeeHeadDescription)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public List<selectList> GetDDList(string Module, int SchoolID)
        {
            List<selectList> obj = new List<selectList>();
            try
            {
                cmd = new SqlCommand("GET_DDList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@Module", Module);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new selectList
                    {
                        Value = Convert.ToString(dr["Value"]),
                        Text = Convert.ToString(dr["Text"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<FeeHeadList> GetFeeHeadListForFeeGroup(string FeeGroupName, int SchoolID, string AdmissionNo,int FinancialYear)
        {
            List<FeeHeadList> obj = new List<FeeHeadList>();
            try
            {
                cmd = new SqlCommand("GetFeeHeadListForFeeGroup");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FeeGroupName", FeeGroupName);
                cmd.Parameters.AddWithValue("@AdmissionNo", AdmissionNo);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new FeeHeadList
                    {
                        ID = Convert.ToString(dr["FeeGroupID"]),
                        Check = true,
                        Amount = Convert.ToString(dr["Amount"]),
                        Name = Convert.ToString(dr["FeeHeadName"]),
                        Apr = Convert.ToString(dr["Apr"]),
                        May = Convert.ToString(dr["May"]),
                        Jun = Convert.ToString(dr["Jun"]),
                        Jul = Convert.ToString(dr["Jul"]),
                        Aug = Convert.ToString(dr["Aug"]),
                        Sep = Convert.ToString(dr["Sep"]),
                        Oct = Convert.ToString(dr["Oct"]),
                        Nov = Convert.ToString(dr["Nov"]),
                        Dec = Convert.ToString(dr["Dec"]),
                        Jan = Convert.ToString(dr["Jan"]),
                        Feb = Convert.ToString(dr["Feb"]),
                        Mar = Convert.ToString(dr["Mar"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public int UpdateStudentFeeGroupName(string FeeGroupName, int SectionId, int StudentID, int SchoolId)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_FeeGroup");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeGroupName", FeeGroupName);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);

                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(UpdateStudentFeeGroupName)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public int FeeDeposit(FeeDeposit FeeDepositDetails)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_FEEDEPOSIT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", FeeDepositDetails.StudentID);
                cmd.Parameters.AddWithValue("@StudentName", FeeDepositDetails.StudentName);
                cmd.Parameters.AddWithValue("@AdmissionNo", FeeDepositDetails.AdmissionNo);
                cmd.Parameters.AddWithValue("@Months", FeeDepositDetails.months);
                cmd.Parameters.AddWithValue("@TotalFees", FeeDepositDetails.TotalFees);
                cmd.Parameters.AddWithValue("@Fine", FeeDepositDetails.Fine);
                cmd.Parameters.AddWithValue("@Concession", FeeDepositDetails.Concession);
                cmd.Parameters.AddWithValue("@WaiveOff", FeeDepositDetails.WaiveOff);
                cmd.Parameters.AddWithValue("@Payment", FeeDepositDetails.Payment);
                cmd.Parameters.AddWithValue("@Balance", FeeDepositDetails.Balance);
                cmd.Parameters.AddWithValue("@Remarks", FeeDepositDetails.Remarks);
                cmd.Parameters.AddWithValue("@MODPayment", FeeDepositDetails.MOP);
                cmd.Parameters.AddWithValue("@PaymentRemark", FeeDepositDetails.MOPRemark);
                cmd.Parameters.AddWithValue("@TotalFeesBreakUp", FeeDepositDetails.TotalFeesBreakUp);
                cmd.Parameters.AddWithValue("@Action", FeeDepositDetails.Action);
                cmd.Parameters.AddWithValue("@FeeGroup", FeeDepositDetails.FeeGroup);
                cmd.Parameters.AddWithValue("@SchoolId", FeeDepositDetails.SchoolId);
                cmd.Parameters.AddWithValue("@Arrears", FeeDepositDetails.Arrears);
                cmd.Parameters.AddWithValue("@TransportCost", FeeDepositDetails.TransportCost);
                cmd.Parameters.AddWithValue("@FinancialYear", FeeDepositDetails.FinancialYear);
                cmd.Parameters.AddWithValue("@CreatedBy", FeeDepositDetails.CreatedBy);
                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(FeeDeposit)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public List<FeeDeposit> GetFeeDeposit(FeeDeposit FeeDepositDetails)
        {
            List<FeeDeposit> obj = new List<FeeDeposit>();
            try
            {
                cmd = new SqlCommand("USP_FEEDEPOSIT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentID", FeeDepositDetails.StudentID);
                cmd.Parameters.AddWithValue("@AdmissionNo", FeeDepositDetails.AdmissionNo);
                cmd.Parameters.AddWithValue("@FinancialYear", FeeDepositDetails.FinancialYear);
                cmd.Parameters.AddWithValue("@SchoolId", FeeDepositDetails.SchoolId);
                cmd.Parameters.AddWithValue("@Action", FeeDepositDetails.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new FeeDeposit
                    {
                        Action = Convert.ToString(dr["Action"]),
                        AdmissionNo = Convert.ToString(dr["AdmissionNo"]),
                        Balance = Convert.ToString(dr["Balance"]),
                        Concession = Convert.ToString(dr["Concession"]),
                        DepositDate = Convert.ToString(dr["DepositDate"]),
                        StudentID = Convert.ToInt16(dr["StudentID"]),
                        TotalFees = Convert.ToString(dr["TotalFees"]),
                        months = Convert.ToString(dr["Months"]),
                        FeeGroup = Convert.ToString(dr["FeeGroup"]),
                        Fine = Convert.ToString(dr["Fine"]),
                        MOP = Convert.ToString(dr["MODPayment"]),
                        MOPRemark = Convert.ToString(dr["PaymentRemark"]),
                        Payment = Convert.ToString(dr["Payment"]),
                        Remarks = Convert.ToString(dr["Remarks"]),
                        StudentName = Convert.ToString(dr["StudentName"]),
                        TotalFeesBreakUp = Convert.ToString(dr["TotalFeesBreakUp"]),
                        WaiveOff = Convert.ToBoolean(dr["WaiveOff"]),
                        ReceiptNo = Convert.ToString(dr["ReceiptNo"]),
                        SchoolId = Convert.ToInt32(dr["SchoolId"]),
                        Arrears = Convert.ToString(dr["Arrears"]),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        public List<StudentMaster> SearchStudentList(string searchVal, int schoolID, int FYID)
        {
            List<StudentMaster> obj = new List<StudentMaster>();
            try
            {
                cmd = new SqlCommand("SP_GETSTUDENT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchInput", searchVal);
                cmd.Parameters.AddWithValue("@SchoolID", schoolID);
                cmd.Parameters.AddWithValue("@FYID", FYID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new StudentMaster
                    {
                        Adminssionno = DBNull.Value != dr["Adminssionno"] ? (string)dr["Adminssionno"] : default(string),
                        Firstname = DBNull.Value != dr["Firstname"] ? (string)dr["Firstname"] : default(string),
                        Bd_fathername = DBNull.Value != dr["Bd_fathername"] ? (string)dr["Bd_fathername"] : default(string),
                        ClassName = DBNull.Value != dr["ClassName"] ? (string)dr["ClassName"] : default(string),
                        SectionName = DBNull.Value != dr["SectionName"] ? (string)dr["SectionName"] : default(string),
                        FeeGroup = DBNull.Value != dr["FeeGroup"] ? (string)dr["FeeGroup"] : default(string),
                        Smid = DBNull.Value != dr["Smid"] ? (int)dr["Smid"] : default(int),
                        SecMid = DBNull.Value != dr["SecMid"] ? (int)dr["SecMid"] : default(int),
                        studentimage = DBNull.Value != dr["studentimage"] ? (string)dr["studentimage"] : default(string),
                        //     TransportCost = DBNull.Value != dr["TransportCost"] ? (decimal)dr["TransportCost"] : default(decimal),

                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        public List<ClassMaster> GetClassList(string SchoolID)
        {
            List<ClassMaster> _lst = new List<ClassMaster>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_ClassMasterList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new ClassMaster
                    {
                        CMid = DBNull.Value != reader["CMid"] ? (int)reader["CMid"] : default(int),
                        ClassName = DBNull.Value != reader["ClassName"] ? (string)reader["ClassName"] : default(string),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetClassList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }

        public List<StudentMaster> GetStudentList(int SectionID, int StudentID)
        {
            List<StudentMaster> _lst = new List<StudentMaster>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("SP_StudentMasterList");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SectionID", SectionID);
                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new StudentMaster
                    {
                        Smid = DBNull.Value != reader["SMid"] ? (int)reader["SMid"] : default(int),
                        SecMid = DBNull.Value != reader["SecMid"] ? (int)reader["SecMid"] : default(int),
                        Firstname = DBNull.Value != reader["Firstname"] ? (string)reader["Firstname"] : default(string),
                        Enquiryno = DBNull.Value != reader["Enquiryno"] ? (string)reader["Enquiryno"] : default(string),
                        Check = true,
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSectionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }
        public List<ClassSectionMaster> GetClassSection(int ClassID, int SchoolID)
        {
            List<ClassSectionMaster> _lst = new List<ClassSectionMaster>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("USP_GETALLCLASSWITHSECTION");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new ClassSectionMaster
                    {
                        SectionID = DBNull.Value != reader["SMid"] ? (int)reader["SMid"] : default(int),
                        ClassName = DBNull.Value != reader["ClassName"] ? (string)reader["ClassName"] : default(string),
                        SectionName = DBNull.Value != reader["SectionName"] ? (string)reader["SectionName"] : default(string),
                        Check = true,
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetSectionList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }

        public FeeDeposit GetFeeDepositByReceiptNo(FeeDeposit FeeDepositDetails)
        {
            FeeDeposit obj = new FeeDeposit();
            try
            {
                cmd = new SqlCommand("USP_FEEDEPOSIT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", FeeDepositDetails.SchoolId);
                cmd.Parameters.AddWithValue("@ReceiptNo", FeeDepositDetails.ReceiptNo);
                cmd.Parameters.AddWithValue("@FinancialYear", FeeDepositDetails.FinancialYear);
                cmd.Parameters.AddWithValue("@Action", FeeDepositDetails.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Action = Convert.ToString(dr["Action"]);
                    obj.AdmissionNo = Convert.ToString(dr["AdmissionNo"]);
                    obj.Balance = Convert.ToString(dr["Balance"]);
                    obj.Concession = Convert.ToString(dr["Concession"]);
                    obj.DepositDate = Convert.ToString(dr["DepositDate"]);
                    obj.StudentID = Convert.ToInt16(dr["StudentID"]);
                    obj.TotalFees = Convert.ToString(dr["TotalFees"]);
                    obj.months = Convert.ToString(dr["Months"]);
                    obj.FeeGroup = Convert.ToString(dr["FeeGroup"]);
                    obj.Fine = Convert.ToString(dr["Fine"]);
                    obj.MOP = Convert.ToString(dr["MODPayment"]);
                    obj.MOPRemark = Convert.ToString(dr["PaymentRemark"]);
                    obj.Payment = Convert.ToString(dr["Payment"]);
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                    obj.StudentName = Convert.ToString(dr["StudentName"]);
                    obj.TotalFeesBreakUp = Convert.ToString(dr["TotalFeesBreakUp"]);
                    obj.WaiveOff = Convert.ToBoolean(dr["WaiveOff"]);
                    obj.ReceiptNo = Convert.ToString(dr["ReceiptNo"]);
                    obj.SchoolId = Convert.ToInt32(dr["SchoolId"]);
                    obj.Arrears = Convert.ToString(dr["Arrears"]);
                    obj.TransportCost = Convert.ToString(dr["TransportCost"]);
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        #region Ledger
        public List<Ledger> GerLedgerReport(Ledger obj)
        {
            List<Ledger> _lst = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("USP_LEDGER");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LDid", obj.LDid);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@DescriptionType", obj.DescriptionType);
                cmd.Parameters.AddWithValue("@EffectiveDate", obj.EffectiveDate);
                cmd.Parameters.AddWithValue("@SchoolId", obj.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
                cmd.Parameters.AddWithValue("@EndDate", obj.EndDate);
                cmd.Parameters.AddWithValue("@Action", "GET");
                _lst = new List<Ledger>();

                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new Ledger
                    {
                        LDid = DBNull.Value != reader["LDid"] ? (int)reader["LDid"] : default(int),
                        Description = DBNull.Value != reader["Description"] ? (string)reader["Description"] : default(string),
                        DescriptionType = DBNull.Value != reader["DescriptionType"] ? (string)reader["DescriptionType"] : default(string),
                        Debit = DBNull.Value != reader["Debit"] ? (decimal)reader["Debit"] : default(decimal),
                        Credit = DBNull.Value != reader["Credit"] ? (decimal)reader["Credit"] : default(decimal),
                        EffectiveDate = DBNull.Value != reader["EffectiveDate"] ? (string)Convert.ToString(reader["EffectiveDate"]) : default(string),
                        SchoolId = DBNull.Value != reader["SchoolId"] ? (int)reader["SchoolId"] : default(int),
                        FinancialYear = DBNull.Value != reader["FinancialYear"] ? (int)reader["FinancialYear"] : default(int),
                        CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string),
                        CreatedDate = DBNull.Value != reader["CreatedDate"] ? (string)Convert.ToString(reader["CreatedDate"]) : default(string),
                    });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GerLedgerReport)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _lst;
        }
        public bool InsertLedgerEntry(Ledger obj)
        {
            bool _results = false;
            try
            {
                cmd = new SqlCommand("USP_LEDGER");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LDid", obj.LDid);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@DescriptionType", obj.DescriptionType);
                cmd.Parameters.AddWithValue("@Debit", obj.Debit);
                cmd.Parameters.AddWithValue("@Credit", obj.Credit);
                cmd.Parameters.AddWithValue("@EffectiveDate", obj.EffectiveDate);
                cmd.Parameters.AddWithValue("@SchoolId", obj.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                cmd.Parameters.AddWithValue("@CreatedBy", obj.CreatedBy);
                cmd.Parameters.AddWithValue("@Action", "INS");


                _results = DBHelper.ExecuteNonQuery(cmd) == 0 ? false : true;

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(InsertLedgerEntry)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        #endregion Ledger
        #region Salary
        public List<EmployeeMaster> GetEmployeeList(int IsActive, int SchoolID)
        {
            List<EmployeeMaster> _emplist = new List<EmployeeMaster>();
            SqlCommand cmd = new SqlCommand("SP_GetEmployeeList");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                var reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _emplist.Add(new EmployeeMaster()
                    {
                        EMP_ID = DBNull.Value != reader["EMP_ID"] ? (int)reader["EMP_ID"] : default(int),
                        FIRSTNAME = DBNull.Value != reader["FIRSTNAME"] ? (string)reader["FIRSTNAME"] : default(string),
                        BANKACNO = DBNull.Value != reader["BANKACNO"] ? (string)reader["BANKACNO"] : default(string),
                        BANKBRANCH = DBNull.Value != reader["BANKBRANCH"] ? (string)reader["BANKBRANCH"] : default(string),
                        BANKNAME = DBNull.Value != reader["BANKNAME"] ? (string)reader["BANKNAME"] : default(string),
                        BIRTHDATE = DBNull.Value != reader["BIRTHDATE"] ? (DateTime)reader["BIRTHDATE"] : default(DateTime?),
                        BLOODGROUP = DBNull.Value != reader["BLOODGROUP"] ? (string)reader["BLOODGROUP"] : default(string),
                        CONFIRMATIONDATE = DBNull.Value != reader["CONFIRMATIONDATE"] ? (DateTime)reader["CONFIRMATIONDATE"] : default(DateTime?),
                        CONTACTTITLE = DBNull.Value != reader["CONTACTTITLE"] ? (int)reader["CONTACTTITLE"] : default(int),
                        CREATEDBY = DBNull.Value != reader["CREATEDBY"] ? (string)reader["CREATEDBY"] : default(string),
                        CREATEDDATE = DBNull.Value != reader["CREATEDDATE"] ? (DateTime)reader["CREATEDDATE"] : default(DateTime?),
                        DEPARTMENT_ID = DBNull.Value != reader["DEPARTMENT_ID"] ? (int)reader["DEPARTMENT_ID"] : default(int),
                        DESIGNATION_ID = DBNull.Value != reader["DESIGNATION_ID"] ? (int)reader["DESIGNATION_ID"] : default(int),
                        EMAIL = DBNull.Value != reader["EMAIL"] ? (string)reader["EMAIL"] : default(string),
                        EMERGENCY_CONT_NO = DBNull.Value != reader["EMERGENCY_CONT_NO"] ? (string)reader["EMERGENCY_CONT_NO"] : default(string),
                        EMERGENCY_CONT_PRS = DBNull.Value != reader["EMERGENCY_CONT_PRS"] ? (string)reader["EMERGENCY_CONT_PRS"] : default(string),
                        EMPCODE = DBNull.Value != reader["EMPCODE"] ? (string)reader["EMPCODE"] : default(string),
                        EMPIMGPATH = DBNull.Value != reader["EMPIMGPATH"] ? (string)reader["EMPIMGPATH"] : default(string),
                        FATHER_SPOUSE = DBNull.Value != reader["FATHER_SPOUSE"] ? (string)reader["FATHER_SPOUSE"] : default(string),
                        GENDER = DBNull.Value != reader["GENDER"] ? (string)reader["GENDER"] : default(string),
                        GROSS_BASIC = DBNull.Value != reader["GROSS_BASIC"] ? (int)reader["GROSS_BASIC"] : default(int),
                        GROSS_CAR_AMOUNT = DBNull.Value != reader["GROSS_CAR_AMOUNT"] ? (int)reader["GROSS_CAR_AMOUNT"] : default(int),
                        GROSS_CHILDREN_EDUCATION = DBNull.Value != reader["GROSS_CHILDREN_EDUCATION"] ? (int)reader["GROSS_CHILDREN_EDUCATION"] : default(int),
                        GROSS_CONVEYANCE = DBNull.Value != reader["GROSS_CONVEYANCE"] ? (int)reader["GROSS_CONVEYANCE"] : default(int),
                        GROSS_HRA = DBNull.Value != reader["GROSS_HRA"] ? (int)reader["EMGROSS_HRAP_ID"] : default(int),
                        GROSS_SALARY = DBNull.Value != reader["GROSS_SALARY"] ? (int)reader["GROSS_SALARY"] : default(int),
                        GROSS_SPECIAL_ALLOWANCE = DBNull.Value != reader["GROSS_SPECIAL_ALLOWANCE"] ? (int)reader["GROSS_SPECIAL_ALLOWANCE"] : default(int),
                        GROSS_UNIFORM_MAINTENANCE = DBNull.Value != reader["GROSS_UNIFORM_MAINTENANCE"] ? (int)reader["GROSS_UNIFORM_MAINTENANCE"] : default(int),
                        IFSC_CODE = DBNull.Value != reader["IFSC_CODE"] ? (string)reader["IFSC_CODE"] : default(string),
                        ISACTIVE = DBNull.Value != reader["ISACTIVE"] ? Convert.ToBoolean(Convert.ToInt32(reader["ISACTIVE"])) : false,
                        JOININGDATE = DBNull.Value != reader["JOININGDATE"] ? (DateTime)reader["JOININGDATE"] : default(DateTime?),
                        LASTNAME = DBNull.Value != reader["LASTNAME"] ? (string)reader["LASTNAME"] : default(string),
                        LEAVE_CL_ENTITLED = DBNull.Value != reader["LEAVE_CL_ENTITLED"] ? (string)reader["LEAVE_CL_ENTITLED"] : default(string),
                        LEAVE_PL_ENTITLED = DBNull.Value != reader["LEAVE_PL_ENTITLED"] ? (string)reader["LEAVE_PL_ENTITLED"] : default(string),
                        LEAVE_SL_ENTITLED = DBNull.Value != reader["LEAVE_SL_ENTITLED"] ? (string)reader["LEAVE_SL_ENTITLED"] : default(string),
                        LEAVINGDATE = DBNull.Value != reader["LEAVINGDATE"] ? (DateTime)reader["LEAVINGDATE"] : default(DateTime?),
                        MARITALSTATUS = DBNull.Value != reader["MARITALSTATUS"] ? (string)reader["MARITALSTATUS"] : default(string),
                        MIDDLENAME = DBNull.Value != reader["MIDDLENAME"] ? (string)reader["MIDDLENAME"] : default(string),
                        MOBILENUMBER = DBNull.Value != reader["MOBILENUMBER"] ? (string)reader["MOBILENUMBER"] : default(string),
                        MODIFIEDBY = DBNull.Value != reader["MODIFIEDBY"] ? (string)reader["MODIFIEDBY"] : default(string),
                        MODIFIEDDATE = DBNull.Value != reader["MODIFIEDDATE"] ? (DateTime)reader["MODIFIEDDATE"] : default(DateTime?),
                        PANIMGPATH = DBNull.Value != reader["PANIMGPATH"] ? (string)reader["PANIMGPATH"] : default(string),
                        PANNO = DBNull.Value != reader["PANNO"] ? (string)reader["PANNO"] : default(string),
                        ParentID = DBNull.Value != reader["ParentID"] ? (int)reader["ParentID"] : default(int),
                        PFNO = DBNull.Value != reader["PFNO"] ? (string)reader["PFNO"] : default(string),
                        PF_ESTD_CODE = DBNull.Value != reader["PF_ESTD_CODE"] ? (string)reader["PF_ESTD_CODE"] : default(string),
                        PF_UAN = DBNull.Value != reader["PF_UAN"] ? (string)reader["PF_UAN"] : default(string),
                        QUALIFICATION1 = DBNull.Value != reader["QUALIFICATION1"] ? (string)reader["QUALIFICATION1"] : default(string),
                        QUALIFICATION2 = DBNull.Value != reader["QUALIFICATION2"] ? (string)reader["QUALIFICATION2"] : default(string),
                        REMARKS = DBNull.Value != reader["REMARKS"] ? (string)reader["REMARKS"] : default(string),
                        REPORTING_MANAGER = DBNull.Value != reader["REPORTING_MANAGER"] ? (int)reader["REPORTING_MANAGER"] : default(int),
                        SALARY = DBNull.Value != reader["SALARY"] ? (decimal)reader["SALARY"] : default(decimal),
                        SPOUSE = DBNull.Value != reader["SPOUSE"] ? (string)reader["SPOUSE"] : default(string),
                        VPF_CONTB_RATE = DBNull.Value != reader["VPF_CONTB_RATE"] ? (int)reader["VPF_CONTB_RATE"] : default(int),
                        ModeOfPayment = DBNull.Value != reader["ModeOfPayment"] ? (string)reader["ModeOfPayment"] : default(string),
                        IsPF = DBNull.Value != reader["IsPF"] ? (((string)reader["IsPF"]).Trim().ToUpper() == "YES" ? true : false) : false,
                        PFEmployeeAmount = DBNull.Value != reader["PFEmployeeamt"] ? Convert.ToDouble(reader["PFEmployeeamt"]) : default(double),
                        PFEmployerAmount = DBNull.Value != reader["PFEmployeramt"] ? Convert.ToDouble(reader["PFEmployeramt"]) : default(double),
                        IsESI = DBNull.Value != reader["IsESI"] ? (((string)reader["IsESI"]).Trim().ToUpper() == "YES" ? true : false) : default(bool),
                        ESIEmployeeAmount = DBNull.Value != reader["ESIEmployeeamt"] ? Convert.ToDouble(reader["ESIEmployeeamt"]) : default(double),
                        ESIEmployerAmount = DBNull.Value != reader["ESIEmployeramt"] ? Convert.ToDouble(reader["ESIEmployeramt"]) : default(double),
                        IsTDS = DBNull.Value != reader["IsTDS"] ? (((string)reader["IsTDS"]).Trim().ToUpper() == "YES" ? true : false) : default(bool),
                        TaxCategory = DBNull.Value != reader["Taxcategory"] ? (string)reader["Taxcategory"] : default(string),
                        ShiftType = DBNull.Value != reader["Shfittype"] ? (int)Convert.ToInt32(reader["Shfittype"]) : default(int),
                        ESIType = DBNull.Value != reader["IsESIType"] ? (string)reader["IsESIType"] : default(string),
                        PFType = DBNull.Value != reader["IsPFType"] ? (string)reader["IsPFType"] : default(string),
                    });

                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetEmployeeList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _emplist;
        }
        public DataTable GetEmployeeAttendance(int SchoolID, int FinancialYear, int EmployeeID, int Month, int Year)
        {
            DataSet ds = new DataSet();
            try
            {
                cmd = new SqlCommand("USP_EmployeeAttendance");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", FinancialYear);
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Action", "GET");
                ds = DBHelper.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(GetEmployeeAttendance)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return ds.Tables[0];
        }
        public int InsertSalaryEntry(EmployeeSalary obj)
        {
            int _results = 0;
            try
            {
                for (int l = 0; l < obj.SalaryDetailsList.Count(); l++)
                {
                    cmd = new SqlCommand("USP_EmployeeSalary");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SchoolID", obj.SchoolID);
                    cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                    cmd.Parameters.AddWithValue("@EMP_ID", obj.SalaryDetailsList[l].EMP_ID);
                    cmd.Parameters.AddWithValue("@EMP_CODE", obj.SalaryDetailsList[l].EMP_CODE);
                    cmd.Parameters.AddWithValue("@EmployeeName", obj.SalaryDetailsList[l].EmployeeName.Trim());
                    cmd.Parameters.AddWithValue("@MonthYear", obj.MonthYear);
                    cmd.Parameters.AddWithValue("@TotalDays", obj.TotalDays);
                    cmd.Parameters.AddWithValue("@WorkingDays", obj.SalaryDetailsList[l].WorkingDays);
                    cmd.Parameters.AddWithValue("@Present", obj.SalaryDetailsList[l].Present);
                    cmd.Parameters.AddWithValue("@Absent", obj.SalaryDetailsList[l].Absent);
                    cmd.Parameters.AddWithValue("@Holiday", obj.SalaryDetailsList[l].Holiday);
                    cmd.Parameters.AddWithValue("@PaidLeave", obj.SalaryDetailsList[l].PaidLeave);
                    cmd.Parameters.AddWithValue("@UnPaidLeave", obj.SalaryDetailsList[l].UnPaidLeave);
                    cmd.Parameters.AddWithValue("@WeekOff", obj.SalaryDetailsList[l].WeekOff);
                    cmd.Parameters.AddWithValue("@PresentDays", obj.SalaryDetailsList[l].PresentDays);
                    cmd.Parameters.AddWithValue("@AbsentDays", obj.SalaryDetailsList[l].AbsentDays);
                    cmd.Parameters.AddWithValue("@HolidayDays", obj.SalaryDetailsList[l].HolidayDays);
                    cmd.Parameters.AddWithValue("@PaidLeaveDays", obj.SalaryDetailsList[l].PaidLeaveDays);
                    cmd.Parameters.AddWithValue("@UnPaidLeaveDays", obj.SalaryDetailsList[l].UnPaidLeaveDays);
                    cmd.Parameters.AddWithValue("@WeekOffDays", obj.SalaryDetailsList[l].WeekOffDays);
                    cmd.Parameters.AddWithValue("@MonthSalary", obj.SalaryDetailsList[l].MonthSalary);
                    cmd.Parameters.AddWithValue("@PaidSalary", obj.SalaryDetailsList[l].PaidSalary);
                    cmd.Parameters.AddWithValue("@TotalEarning", obj.SalaryDetailsList[l].TotalEarning);
                    cmd.Parameters.AddWithValue("@TotalDeduction", obj.SalaryDetailsList[l].TotalDeduction);
                    cmd.Parameters.AddWithValue("@EmployeePFAmount", obj.SalaryDetailsList[l].EmployeePFAmount);
                    cmd.Parameters.AddWithValue("@EmployerPFAmount", obj.SalaryDetailsList[l].EmployerPFAmount);
                    cmd.Parameters.AddWithValue("@EmployeeESIAmount", obj.SalaryDetailsList[l].EmployeeESIAmount);
                    cmd.Parameters.AddWithValue("@EmployerESIAmount", obj.SalaryDetailsList[l].EmployerESIAmount);
                    cmd.Parameters.AddWithValue("@EmployeeTDS", obj.SalaryDetailsList[l].EmployeeTDS);
                    cmd.Parameters.AddWithValue("@DaySalary", obj.SalaryDetailsList[l].DaySalary);
                    cmd.Parameters.AddWithValue("@User", obj.SalaryDetailsList[l].User);
                    cmd.Parameters.AddWithValue("@ModeOfPayment", obj.SalaryDetailsList[l].ModeOfPayment);
                    cmd.Parameters.AddWithValue("@BankName", obj.SalaryDetailsList[l].BankName);
                    cmd.Parameters.AddWithValue("@AccountNo", obj.SalaryDetailsList[l].AccountNo);
                    cmd.Parameters.AddWithValue("@Action", "INS");
                    _results += Convert.ToString(DBHelper.ExecuteScalar(cmd)) == "1" ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(InsertSalaryEntry)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        public EmployeeSalary GetEmployeeSalaryList(EmployeeSalary employeeSalary, string EmpCode)
        {
            List<EmployeeSalaryDetails> _empSalDetailslist = new List<EmployeeSalaryDetails>();
            SqlCommand cmd = new SqlCommand("USP_EmployeeSalary");
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", employeeSalary.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYear", employeeSalary.FinancialYear);
                cmd.Parameters.AddWithValue("@MonthYear", employeeSalary.MonthYear);
                cmd.Parameters.AddWithValue("@EMP_CODE", EmpCode);
                cmd.Parameters.AddWithValue("@Action", "GET");
                var reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _empSalDetailslist.Add(new EmployeeSalaryDetails()
                    {
                        Counter = DBNull.Value != reader["counter"] ? (int)reader["counter"] : default(int),
                        EMP_ID = DBNull.Value != reader["EMP_ID"] ? (int)reader["EMP_ID"] : default(int),
                        EMP_CODE = DBNull.Value != reader["EMP_CODE"] ? (string)reader["EMP_CODE"] : default(string),
                        EmployeeName = DBNull.Value != reader["EmployeeName"] ? (string)reader["EmployeeName"] : default(string),
                        WorkingDays = DBNull.Value != reader["WorkingDays"] ? (int)reader["WorkingDays"] : default(int),
                        Present = DBNull.Value != reader["Present"] ? (decimal)reader["Present"] : default(decimal),
                        Absent = DBNull.Value != reader["Absent"] ? (int)reader["Absent"] : default(int),
                        Holiday = DBNull.Value != reader["Holiday"] ? (int)reader["Holiday"] : default(int),
                        PaidLeave = DBNull.Value != reader["PaidLeave"] ? (decimal)reader["PaidLeave"] : default(decimal),
                        UnPaidLeave = DBNull.Value != reader["UnPaidLeave"] ? (decimal)reader["UnPaidLeave"] : default(decimal),
                        WeekOff = DBNull.Value != reader["WeekOff"] ? (int)reader["WeekOff"] : default(int),
                        PresentDays = DBNull.Value != reader["PresentDays"] ? (string)reader["PresentDays"] : default(string),
                        AbsentDays = DBNull.Value != reader["AbsentDays"] ? (string)reader["AbsentDays"] : default(string),
                        HolidayDays = DBNull.Value != reader["HolidayDays"] ? (string)reader["HolidayDays"] : default(string),
                        PaidLeaveDays = DBNull.Value != reader["PaidLeaveDays"] ? (string)reader["PaidLeaveDays"] : default(string),
                        UnPaidLeaveDays = DBNull.Value != reader["UnPaidLeaveDays"] ? (string)reader["UnPaidLeaveDays"] : default(string),
                        WeekOffDays = DBNull.Value != reader["WeekOffDays"] ? (string)reader["WeekOffDays"] : default(string),
                        MonthSalary = DBNull.Value != reader["MonthSalary"] ? (decimal)reader["MonthSalary"] : default(decimal),
                        PaidSalary = DBNull.Value != reader["PaidSalary"] ? (decimal)reader["PaidSalary"] : default(decimal),
                        ModeOfPayment = DBNull.Value != reader["ModeOfPayment"] ? (string)reader["ModeOfPayment"] : default(string),
                        PaymentRemark = DBNull.Value != reader["PaymentRemark"] ? (string)reader["PaymentRemark"] : default(string),
                        BankName = DBNull.Value != reader["BankName"] ? (string)reader["BankName"] : default(string),
                        AccountNo = DBNull.Value != reader["AccountNo"] ? (string)reader["AccountNo"] : default(string),
                        CreatedDate = DBNull.Value != reader["CreatedDate"] ? (DateTime)reader["CreatedDate"] : default(DateTime),
                        PaymentDate = DBNull.Value != reader["PaymentDate"] ? (DateTime)reader["PaymentDate"] : default(DateTime),
                        UpdatedDate = DBNull.Value != reader["UpdatedDate"] ? (DateTime)reader["UpdatedDate"] : default(DateTime),
                        CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string),
                        PaymentBy = DBNull.Value != reader["PaymentBy"] ? (string)reader["PaymentBy"] : default(string),
                        Active = DBNull.Value != reader["Active"] ? (bool)reader["Active"] : default(bool),
                        DaySalary = DBNull.Value != reader["DaySalary"] ? (decimal)reader["DaySalary"] : default(decimal),
                        PaymentStatus = DBNull.Value != reader["PaymentStatus"] ? (bool)reader["PaymentStatus"] : default(bool),
                    });
                }
                employeeSalary.SalaryDetailsList = _empSalDetailslist;
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetEmployeeList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return employeeSalary;
        }
        public int UpdateSalary(EmployeeSalary obj)
        {
            int _results = 0;
            try
            {
                for (int l = 0; l < obj.SalaryDetailsList.Count(); l++)
                {
                    cmd = new SqlCommand("USP_EmployeeSalary");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SchoolID", obj.SchoolID);
                    cmd.Parameters.AddWithValue("@FinancialYear", obj.FinancialYear);
                    cmd.Parameters.AddWithValue("@Counter", obj.SalaryDetailsList[l].Counter);
                    cmd.Parameters.AddWithValue("@EMP_ID", obj.SalaryDetailsList[l].EMP_ID);
                    cmd.Parameters.AddWithValue("@EMP_CODE", obj.SalaryDetailsList[l].EMP_CODE);
                    cmd.Parameters.AddWithValue("@MonthYear", obj.MonthYear);
                    cmd.Parameters.AddWithValue("@PaymentRemark", obj.SalaryDetailsList[l].PaymentRemark);
                    cmd.Parameters.AddWithValue("@PaymentDate", obj.SalaryDetailsList[l].PaymentDate);
                    cmd.Parameters.AddWithValue("@PaymentBy", obj.SalaryDetailsList[l].PaymentBy);
                    cmd.Parameters.AddWithValue("@PaymentStatus", obj.SalaryDetailsList[l].PaymentStatus);
                    cmd.Parameters.AddWithValue("@User", obj.SalaryDetailsList[l].User);
                    cmd.Parameters.AddWithValue("@ModeOfPayment", obj.SalaryDetailsList[l].ModeOfPayment);
                    cmd.Parameters.AddWithValue("@BankName", obj.SalaryDetailsList[l].BankName);
                    cmd.Parameters.AddWithValue("@AccountNo", obj.SalaryDetailsList[l].AccountNo);
                    cmd.Parameters.AddWithValue("@Action", "UPDSAL");
                    _results += Convert.ToString(DBHelper.ExecuteScalar(cmd)) == "1" ? 1 : 0;
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(InsertSalaryEntry)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }
            return _results;
        }
        #endregion Salary
        public List<EmployeeMaster> SearchEmployeeList(string searchVal, int schoolID)
        {
            List<EmployeeMaster> obj = new List<EmployeeMaster>();
            try
            {
                cmd = new SqlCommand("SP_GETEMPLOYEE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchInput", searchVal);
                cmd.Parameters.AddWithValue("@SchoolID", schoolID);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.Add(new EmployeeMaster
                    {
                        EMP_ID = DBNull.Value != dr["EMP_ID"] ? (int)dr["EMP_ID"] : default(int),
                        EMPCODE = DBNull.Value != dr["EMPCODE"] ? (string)dr["EMPCODE"] : default(string),
                        FIRSTNAME = DBNull.Value != dr["FIRSTNAME"] ? (string)dr["FIRSTNAME"] : default(string),
                        MIDDLENAME = DBNull.Value != dr["MIDDLENAME"] ? (string)dr["MIDDLENAME"] : default(string),
                        LASTNAME = DBNull.Value != dr["LASTNAME"] ? (string)dr["LASTNAME"] : default(string),
                        BIRTHDATE = DBNull.Value != dr["BIRTHDATE"] ? (DateTime)dr["BIRTHDATE"] : default(DateTime),
                        MOBILENUMBER = DBNull.Value != dr["MOBILENUMBER"] ? (string)dr["MOBILENUMBER"] : default(string),
                        PresentAddress = DBNull.Value != dr["PresentAddress"] ? (string)dr["PresentAddress"] : default(string),
                        PermanentAddress = DBNull.Value != dr["PermanentAddress"] ? (string)dr["PermanentAddress"] : default(string),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

        #region Salary
        public int AssignFeeConcession(FeeConcessionDetails feeConcessionDetails)
        {
            int result = 0;
            try
            {
                cmd = new SqlCommand("USP_AssignFeeConcession");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FeeConID", feeConcessionDetails.FeeConID);
                cmd.Parameters.AddWithValue("@SchoolID", feeConcessionDetails.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", feeConcessionDetails.FinancialYearID);
                cmd.Parameters.AddWithValue("@StudentID", feeConcessionDetails.StudentID);
                cmd.Parameters.AddWithValue("@Amount", feeConcessionDetails.Amount);
                cmd.Parameters.AddWithValue("@Apr", feeConcessionDetails.Apr);
                cmd.Parameters.AddWithValue("@May", feeConcessionDetails.May);
                cmd.Parameters.AddWithValue("@Jun", feeConcessionDetails.Jun);
                cmd.Parameters.AddWithValue("@Jul", feeConcessionDetails.Jul);
                cmd.Parameters.AddWithValue("@Aug", feeConcessionDetails.Aug);
                cmd.Parameters.AddWithValue("@Sep", feeConcessionDetails.Sep);
                cmd.Parameters.AddWithValue("@Oct", feeConcessionDetails.Oct);
                cmd.Parameters.AddWithValue("@Nov", feeConcessionDetails.Nov);
                cmd.Parameters.AddWithValue("@Dec", feeConcessionDetails.Dec);
                cmd.Parameters.AddWithValue("@Jan", feeConcessionDetails.Jan);
                cmd.Parameters.AddWithValue("@Feb", feeConcessionDetails.Feb);
                cmd.Parameters.AddWithValue("@Mar", feeConcessionDetails.Mar);
                cmd.Parameters.AddWithValue("@Action", feeConcessionDetails.Action);
                result = Convert.ToInt32(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(AssignFeeConcession)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public FeeConcessionDetails GetAssignFeeConcession(FeeConcessionDetails model)
        {
            FeeConcessionDetails obj = new FeeConcessionDetails();

            try
            {
                cmd = new SqlCommand("USP_AssignFeeConcession");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolID", model.SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                cmd.Parameters.AddWithValue("@FeeConID", model.FeeConID);
                cmd.Parameters.AddWithValue("@Action", model.Action);
                var dr = DBHelper.ExecuteReader(cmd);
                while (dr.Read())
                {
                    obj.FeeConID = Convert.ToInt32(dr["FeeConID"]);
                    obj.FinancialYearID = Convert.ToInt32(dr["FinancialYearID"]);
                    obj.Amount = Convert.ToDecimal(dr["Amount"]);
                    obj.Apr = Convert.ToDecimal(dr["Apr"]);
                    obj.May = Convert.ToDecimal(dr["May"]);
                    obj.Jun = Convert.ToDecimal(dr["Jun"]);
                    obj.Jul = Convert.ToDecimal(dr["Jul"]);
                    obj.Aug = Convert.ToDecimal(dr["Aug"]);
                    obj.Sep = Convert.ToDecimal(dr["Sep"]);
                    obj.Oct = Convert.ToDecimal(dr["Oct"]);
                    obj.Nov = Convert.ToDecimal(dr["Nov"]);
                    obj.Dec = Convert.ToDecimal(dr["Dec"]);
                    obj.Jan = Convert.ToDecimal(dr["Jan"]);
                    obj.Feb = Convert.ToDecimal(dr["Feb"]);
                    obj.Mar = Convert.ToDecimal(dr["Mar"]);
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(GetAssignFeeConcession)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }
        #endregion Salary


        public List<IndisciplineDetails> GetIndisciplineList(int SchoolID, int FinancialYearID)
        {
            List<IndisciplineDetails> _lst = new List<IndisciplineDetails>();
            ds = new DataSet();
            try
            {
                cmd = new SqlCommand("USP_Indiscipline");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", SchoolID);
                cmd.Parameters.AddWithValue("@FinancialYearId", FinancialYearID);
                cmd.Parameters.AddWithValue("@Action", "GET");
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    _lst.Add(new IndisciplineDetails
                    {
                        Counter = DBNull.Value != reader["Counter"] ? (int)reader["Counter"] : default(int),
                        AdmissionNo = DBNull.Value != reader["AdmissionNo"] ? (string)reader["AdmissionNo"] : default(string),
                        StudentName = DBNull.Value != reader["StudentName"] ? (string)reader["StudentName"] : default(string),
                        FatherName = DBNull.Value != reader["FatherName"] ? (string)reader["FatherName"] : default(string),
                        ClassName = DBNull.Value != reader["ClassName"] ? (string)reader["ClassName"] : default(string),
                        SectionName = DBNull.Value != reader["SectionName"] ? (string)reader["SectionName"] : default(string),
                        FineAmount = DBNull.Value != reader["FineAmount"] ? (decimal)reader["FineAmount"] : default(decimal),
                        Status = DBNull.Value != reader["Status"] ? (string)reader["Status"] : default(string),
                        Remark = DBNull.Value != reader["Remarks"] ? (string)reader["Remarks"] : default(string),
                        CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string),
                        CreatedDate = DBNull.Value != reader["CreatedDate"] ? (DateTime)reader["CreatedDate"] : default(DateTime),
                    });
                }
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALCommon(GetIndisciplineList)", "Error_014", ex, "DALCommon");
            }
            finally
            {
                cmd.Dispose();
            }

            return _lst;
        }
        public string SaveIndiscipline(IndisciplineDetails ID)
        {
            string result = "";
            try
            {
                cmd = new SqlCommand("USP_Indiscipline");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdmissionNo", ID.AdmissionNo);
                cmd.Parameters.AddWithValue("@Status", ID.Status);
                cmd.Parameters.AddWithValue("@FineAmount", ID.FineAmount);
                cmd.Parameters.AddWithValue("@Remark", ID.Remark);
                cmd.Parameters.AddWithValue("@SchoolId", ID.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYearId", ID.FinancialYearID);
                cmd.Parameters.AddWithValue("@CreatedBy", ID.CreatedBy);
                cmd.Parameters.AddWithValue("@Action", "INS");
                result = Convert.ToString(DBHelper.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveIndiscipline)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return result;
        }
        public IndisciplineDetails GetIndisciplineFeeDepositByReceiptNo(IndisciplineDetails Details)
        {
            IndisciplineDetails obj = new IndisciplineDetails();
            try
            {
                cmd = new SqlCommand("USP_Indiscipline");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", Details.SchoolId);
                cmd.Parameters.AddWithValue("@FinancialYearId", Details.FinancialYearID);
                cmd.Parameters.AddWithValue("@Counter", Details.Counter);
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                IDataReader reader = DBHelper.ExecuteReader(cmd);
                while (reader.Read())
                {
                    obj.Counter = DBNull.Value != reader["Counter"] ? (int)reader["Counter"] : default(int);
                    obj.AdmissionNo = DBNull.Value != reader["AdmissionNo"] ? (string)reader["AdmissionNo"] : default(string);
                    obj.StudentName = DBNull.Value != reader["StudentName"] ? (string)reader["StudentName"] : default(string);
                    obj.FatherName = DBNull.Value != reader["FatherName"] ? (string)reader["FatherName"] : default(string);
                    obj.ClassName = DBNull.Value != reader["ClassName"] ? (string)reader["ClassName"] : default(string);
                    obj.SectionName = DBNull.Value != reader["SectionName"] ? (string)reader["SectionName"] : default(string);
                    obj.FineAmount = DBNull.Value != reader["FineAmount"] ? (decimal)reader["FineAmount"] : default(decimal);
                    obj.Status = DBNull.Value != reader["Status"] ? (string)reader["Status"] : default(string);
                    obj.Remark = DBNull.Value != reader["Remarks"] ? (string)reader["Remarks"] : default(string);
                    obj.CreatedBy = DBNull.Value != reader["CreatedBy"] ? (string)reader["CreatedBy"] : default(string);
                    obj.CreatedDate = DBNull.Value != reader["CreatedDate"] ? (DateTime)reader["CreatedDate"] : default(DateTime);
                }

            }
            catch (Exception ex)
            {
                ExecptionLogger.FileHandling("DALFee(SaveFeeTerm)", "Error_014", ex, "DALFee");
            }
            finally
            {
                cmd.Dispose();
            }
            return obj;
        }

    }
}
