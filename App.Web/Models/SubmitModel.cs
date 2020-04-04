using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppProj.Web.Models
{
    public class SubmitModel
    {
        [Key]
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int DistrictId { get; set; }
        public int UpazillaId { get; set; }
        public int ReachCount { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Nullable<int> GenderId { get; set; }

        [Required(ErrorMessage ="Age is required")]
        public Nullable<int> Age { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{5})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        public bool IsFever { get; set; }
        public bool IsDryCough { get; set; }
        public bool IsBreadth { get; set; }
        public bool IsContact { get; set; }
                
        public string CollectedBy { get; set; }

        public int RiskBySS { get; set; }
        public int RiskBySK { get; set; }
        public int RiskByPK { get; set; }
        public int RiskByPA { get; set; }
        public int RiskByAM { get; set; }
        public int CaseCnt { get; set; }
        public int BracMeeting { get; set; }
        public int BracParticipant { get; set; }
        public int GovtMeeting { get; set; }
        public int GovtParticipant { get; set; }
        public int Leaflet { get; set; }
        public int Sticker { get; set; }

        public IEnumerable<SelectListItem> Sources { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
        public IEnumerable<SelectListItem> Upazillas { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        public List<MultiData> MultiDatas { get; set; }
    }

    public class MultiData
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public int Count { get; set; }
        public string CollectedBy { get; set; }
    }
}