using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

//using System;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class DASSTest
    {
        public Int32 Test_Id { set; get; }
        public Int32 Test_QuestionsNo { set; get; }
        //[Required]
        [Display(Name = "عنوان تست")]
        public string Test_Title { set; get; }
        [Display(Name = "توضيحات")]
        public string Test_Description { set; get; }
        public List<Question> Test_Questions { set; get; }
        public List<int> DKeys;
        public List<int> AKeys;
        public List<int> SKeys;

        public DASSTest()
        {
            Test_Id = 1;
            Test_QuestionsNo = 0;
            Test_Title = "آزمون افسردگي، اضطراب، استرس";
            Test_Questions = new List<Question>();
            Test_Description = "";
            DKeys = new List<int>();
            DKeys.Add(3); DKeys.Add(5); DKeys.Add(10); DKeys.Add(13); DKeys.Add(16); DKeys.Add(17); DKeys.Add(21);
            AKeys = new List<int>();
            AKeys.Add(2); AKeys.Add(4); AKeys.Add(7); AKeys.Add(9); AKeys.Add(15); AKeys.Add(19); AKeys.Add(20);
            SKeys = new List<int>();
            SKeys.Add(1); SKeys.Add(6); SKeys.Add(8); SKeys.Add(11); SKeys.Add(12); SKeys.Add(14); SKeys.Add(18);
            
            Connection.Connection con = new Connection.Connection();
            //List<t> lmd = new List<UserActivationModel>();
            DataSet ds = new DataSet();
            ds = con.QuestionsData(Test_Id);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Test_QuestionsNo++;
                Test_Questions.Add(new Question
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Qu_Text = dr["Qu_Text"].ToString(),
                    Qu_Row = Convert.ToInt32(dr["Qu_Row"]),
                    Qu_Choice = 0
                });
            }

        }
    }
}

public class Question
{
    public int ID { set; get; }
    public string Qu_Text { set; get; }
    public int Qu_Row { set; get; }
    public int Qu_Choice{ set; get; }
}
