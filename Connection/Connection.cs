using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace test1.Connection
{
    public class Connection
    {
        public Boolean ExecutionGeneral(string RunningCommand)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PsychologyConnection"].ToString());
                con.Open();
                SqlCommand Cm = new SqlCommand("", con);
                Cm.CommandText = RunningCommand;
                Cm.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                return (false);
            }
            return (true);
        }

        public DataSet GeneralData(string SelectCommand)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PsychologyConnection"].ToString());
            SqlCommand cmd = new SqlCommand(SelectCommand, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet myrec = new DataSet();
            da.Fill(myrec);
            return myrec;
        }

        public bool CheckUserPass(string UserName, string Password)
        {
            DataSet ds = new DataSet();
            ds = GeneralData(String.Format("select distinct [ID],[Name],[LastName], [UserType] from [dbo].[User] where [UserName]='{0}' and [Password]='{1}'", UserName, Password));
            if(ds.Tables[0].Rows.Count>0)
            {
                DataRow dr=ds.Tables[0].Rows[0];
                test1.Models.UserTest.ValidUser = dr["Id"].ToString();
                test1.Models.UserTest.Name = dr["Name"].ToString();
                test1.Models.UserTest.LastName = dr["LastName"].ToString();
                test1.Models.UserTest.UserType = Convert.ToBoolean(dr["UserType"]);
                return true;
            }
            else
               return false;
        }

        public DataSet QuestionsData(int Test_Id)
        {
            DataSet myrec = GeneralData(String.Format("select [ID], [Qu_Row], [Qu_Text] from [dbo].[Question] where [Test_ID]={0}", Test_Id));
            return myrec;
        }
        public DataSet TestResultData(string UserId, string TestId)
        {
            DataSet myrec = GeneralData(String.Format("select top 1 [ID] from [dbo].[HeldTest] where [UserID]={0} and [TestNo]={1} ORDER BY [ID] DESC", UserId, TestId));
            return myrec;
        }

        public Boolean SaveTestResult(Models.DASSResultModel model, string UserId)
        {
            Boolean Status = ExecutionGeneral(String.Format("insert into [dbo].[HeldTest] ([TestDate], [StartTime], [EndTime], [DepResult], [AnxResult], [StressResult], [DepStatus], [AnxStatus], [StressStatus], [TestNo], [UserID])"
                                                            + " values('{0}', '{1}', '{2}', {3}, {4}, {5}, {6},{7}, {8}, {9}, {10});", model.TestDate, model.StartTime, model.EndTime, model.DepResult, model.AnxResult, model.StressResult, model.DepStatus, model.AnxStatus, model.StressStatus, model.DASS.Test_Id, UserId));
            return Status;
        }

        public DataSet MyResultData(string UserId, string TestId)
        {
            DataSet myrec = GeneralData(String.Format("select top 1 u.[Name] as Name, u.LastName as LastName, HT.AnxResult as AnxResult, "
                                                      +" HT.AnxStatus as AnxStatus, HT.TestDate as TestDate, HT.DepResult as DepResult," 
                                                      +" HT.DepStatus as DepStatus, HT.EndTime as EndTime,HT.ID as ID, HT.StartTime as StartTime," 
                                                      +" HT.StressResult as StressResult, HT.StressStatus as StressStatus  "
                                                      +" from [dbo].[HeldTest] as HT inner join [dbo].[User] as u on HT.[UserID]=u.[ID]"
                                                      + " where HT.TestNo={0} and HT.UserID={1} ORDER BY HT.[ID] DESC;", TestId, UserId));
            return myrec;
        }
        public DataSet TheResultListData(string TestId)
        {
            DataSet myrec = GeneralData(String.Format("select u.[ClassNo] as ClassNo, u.[Name] as Name, u.LastName as LastName, HT.AnxResult as AnxResult,  HT.AnxStatus as AnxStatus, HT.TestDate as TestDate, HT.DepResult as DepResult, HT.DepStatus as DepStatus, HT.EndTime as EndTime,u.UserName as ID, HT.StartTime as StartTime, HT.StressResult as StressResult, HT.StressStatus as StressStatus "   
                                                      +" from [dbo].[HeldTest] as HT inner join [dbo].[User] as u on HT.[UserID]=u.[ID] "
                                                      + " where HT.TestNo={0} order by u.ClassNo, u.UserName", TestId));
            return myrec;
        }
    }
}