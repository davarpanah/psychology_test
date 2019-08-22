using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class DASSResultModel
    {
        public int ClassNo { set; get; }
        [Required]
        [Display(Name = "كلمه عبور")]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public long ID { get; set; }

        public List<int> Answers;
        //public bool IsParticipated;
        public int DepResult { set; get; }
        public int AnxResult { set; get; }
        public int StressResult { set; get; }

        [Display(Name = "ميزان افسردگي شما عبارت است از")]
        public int DepStatus { set; get; }
        [Display(Name = "ميزان نگراني شما عبارت است از")]
        public int AnxStatus { set; get; }
        [Display(Name = "ميزان استرس شما عبارت است از")]
        public int StressStatus { set; get; }

        [Display(Name = "تاريخ تست")]
        public string TestDate { set; get; }

        [Display(Name = "ساعت شروع ")]
        public string StartTime { set; get; }
        [Display(Name = "ساعت پايان")]
        public string EndTime { set; get; }
        public DASSTest DASS { set; get; }
        public DASSResultModel()
        {
            //IsParticipated = false;
            Answers = new List<int>();
            DASS = new DASSTest();
        }
    }
}