using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class DistrictSubmitModel
    {        
        public int SumId { get; set; }
        public int DetailId { get; set; }
        public DateTime Date { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int DistrictId { get; set; }
        public string DistrictName { get; set; }

        public int HospitalCount { get; set; }
        public int BedCount { get; set; }
        public int IcuCount { get; set; }
        public int OtherPatientCount { get; set; }
        public int BreathingPatientCount { get; set; }
        public int MaternalCount { get; set; }
        public string RemarksSum{ get; set; }

        public int NewQuarantine { get; set; }
        public int ReleasedQuarantine { get; set; }
        public int DoTestOn { get; set; }
        public int Death { get; set; }

        public string LocalAdminMeasure { get; set; }
        public string Demand { get; set; }
        public string Panic { get; set; }
        public string SocialIssue { get; set; }
        public string BracThink { get; set; }
        public string NgoDoing { get; set; }
        public string NeedyPeople { get; set; }
        public string BracDo { get; set; }
        public string RemarksDetail { get; set; }

        public int MemberCount { get; set; }
        public int MemberUpazillaCount { get; set; }

        public int DistrictTypeValue { get; set; }

        public int ReliefFamily { get; set; }
        //public int ReliefPerson { get; set; }
        public decimal Rice { get; set; }
        public decimal Dal { get; set; }
        public decimal Potato { get; set; }
        public int Money { get; set; }
        //public decimal Onion { get; set; }
        //public decimal Salt { get; set; }
        //public decimal Oil { get; set; }
        //public decimal Soap { get; set; }

        public string ReliefRemarks { get; set; }
    }

    public class DistrictSubmitSubModel
    {
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> Divisions { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
    }

}