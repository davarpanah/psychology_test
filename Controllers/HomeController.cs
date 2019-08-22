using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using test1.Models;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace test1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exit()
        {
            test1.Models.UserTest.IsValidUser = false;
            return RedirectToAction("Login", "Account");
        }
        public ActionResult DASSTest()
        {
            DASSResultModel lmd = new DASSResultModel();
            Connection.Connection con = new Connection.Connection();
            if (test1.Models.UserTest.IsValidUser != false)
            {
                DataSet ds = new DataSet();
                ds = con.TestResultData(test1.Models.UserTest.ValidUser, "1");//hashem
                if (ds.Tables[0].Rows.Count == 0 || test1.Models.UserTest.UserType)
                {
                    lmd.StartTime = DateTime.Now.ToString("HH:mm");
                    lmd.Name = test1.Models.UserTest.Name;
                    lmd.LastName = test1.Models.UserTest.LastName;
                    return View(lmd);
                }
                else
                {
                    return RedirectToAction("MyResult", "Home", null);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public test1.Models.DASSResultModel Calculate_Result(test1.Models.DASSResultModel model)
        {
            model.AnxResult = 0;
            model.DepResult = 0;
            model.StressResult = 0;
            model.Answers.Clear();
            foreach (Question Qu in model.DASS.Test_Questions)
            {
                int Adding = 0;
                if (Qu.Qu_Choice == 0) { Adding = 0; model.Answers.Add(0); }
                else if (Qu.Qu_Choice == 1) { Adding = 2; model.Answers.Add(1); }
                else if (Qu.Qu_Choice == 2) { Adding = 4; model.Answers.Add(2); }
                else if (Qu.Qu_Choice == 3) { Adding = 6; model.Answers.Add(3); }

                if (model.DASS.DKeys.Contains(Qu.Qu_Row))
                {
                    model.DepResult += Adding;
                }
                else if (model.DASS.AKeys.Contains(Qu.Qu_Row))
                {
                    model.AnxResult += Adding;
                }
                else if (model.DASS.SKeys.Contains(Qu.Qu_Row))
                {
                    model.StressResult += Adding;
                }
            }
            if (model.DepResult <= 9) model.DepStatus = 0;
            else if (model.DepResult <= 13) model.DepStatus = 1;
            else if (model.DepResult <= 20) model.DepStatus = 2;
            else if (model.DepResult <= 27) model.DepStatus = 3;
            else model.DepStatus = 4;

            if (model.AnxResult <= 7) model.AnxStatus = 0;
            else if (model.AnxResult <= 9) model.AnxStatus = 1;
            else if (model.AnxResult <= 14) model.AnxStatus = 2;
            else if (model.AnxResult <= 19) model.AnxStatus = 3;
            else model.AnxStatus = 4;

            if (model.StressResult <= 14) model.StressStatus = 0;
            else if (model.StressResult <= 18) model.StressStatus = 1;
            else if (model.StressResult <= 25) model.StressStatus = 2;
            else if (model.StressResult <= 33) model.StressStatus = 3;
            else model.StressStatus = 4;

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DASSTest(DASSResultModel lmd)
        {
            bool TheError = false;
            Connection.Connection con = new Connection.Connection();
            if (test1.Models.UserTest.IsValidUser != false)
            {
                lmd.TestDate = DateTime.Now.Date.ToString("d");
                lmd.EndTime = DateTime.Now.ToString("HH:mm");
                lmd.DASS.Test_Id = 1;
                lmd = Calculate_Result(lmd);
                TheError = con.SaveTestResult(lmd, test1.Models.UserTest.ValidUser);

            }
            if (TheError == true)
            {
                ViewBag.Message = "";
                return RedirectToAction("MyResult", "Home", lmd);
            }
            else
            {
                ViewBag.Message = "خطا در ثبت اطلاعات";
                return View(lmd);
            }
        }
        public ActionResult MyResult(DASSResultModel model)
        {
            if (test1.Models.UserTest.IsValidUser == false)
            {
                return RedirectToAction("Index", "Home");
            }
            if (model.Name == null)
            {
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.MyResultData(test1.Models.UserTest.ValidUser, "1");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    model.ID = Convert.ToInt64(dr["id"]);
                    model.AnxResult = Convert.ToInt32(dr["AnxResult"]);
                    model.AnxStatus = Convert.ToInt32(dr["AnxStatus"]);
                    model.DepResult = Convert.ToInt32(dr["DepResult"]);
                    model.DepStatus = Convert.ToInt32(dr["DepStatus"]);
                    model.EndTime = dr["EndTime"].ToString();
                    model.LastName = dr["LastName"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.StartTime = dr["StartTime"].ToString();
                    model.TestDate = dr["TestDate"].ToString();
                    model.StressResult = Convert.ToInt32(dr["StressResult"]);
                    model.StressStatus = Convert.ToInt32(dr["StressStatus"]);
                }
            }
            return View(model);
        }
        public ActionResult TestListReport()
        {
            if (test1.Models.UserTest.IsValidUser == true && test1.Models.UserTest.UserType == true)
            {
                List<DASSResultModel> lmd = new List<DASSResultModel>();
                Connection.Connection con = new Connection.Connection();
                DataSet ds = new DataSet();
                ds = con.TheResultListData("1");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lmd.Add(new DASSResultModel
                    {
                        ID = Convert.ToInt64 (dr["id"]),
                        AnxResult = Convert.ToInt32(dr["AnxResult"]),
                        AnxStatus = Convert.ToInt32(dr["AnxStatus"]),
                        DepResult = Convert.ToInt32(dr["DepResult"]),
                        DepStatus = Convert.ToInt32(dr["DepStatus"]),
                        EndTime = dr["EndTime"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Name = dr["Name"].ToString(),
                        StartTime = dr["StartTime"].ToString(),
                        TestDate = dr["TestDate"].ToString(),
                        StressResult = Convert.ToInt32(dr["StressResult"]),
                        ClassNo = Convert.ToInt32(dr["ClassNo"]),
                        StressStatus = Convert.ToInt32(dr["StressStatus"])
                    });
                }
                return View(lmd);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "شما عزیزان می توانید از طرق زیر با مسئولین شركت ابزاردانش غرب در ارتباط باشید.";

            return View();
        }
    }
}