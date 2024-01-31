using NID_Cards.CustomAttributes;
using NID_Cards.Data.Enums;
using NID_Cards.Models;
using System.ComponentModel.DataAnnotations;

namespace NID_Cards.FormsModel
{
    public class FormsCitizen
    {
        //,RegularExpression("^[0-9]{14}$")
        //[Required,RegularExpression("^[0-9]{14}$", ErrorMessage = "Enter your unique 5 numbers in your NID")]// National ID consists of 14 numbers
        public long NID { get; set; }

        [Required, RegularExpression("^[0-9]{5}$", ErrorMessage = "Enter your unique 5 numbers in your NID")]// National ID consists of 14 numbers
        [Display(Name = "NID Number")]
        public string NIDManualPart { get; set; }

        public string? NIDAuto{ get; set; }

        [Required, StringLength(250)] //nvarchar(250)
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required, Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(700)] //nvarchar(700)
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Required , Display(Name = "Issue Date")]
        public DateTime DateOfIssue { get; set; }

        [Required, StringLength(150), Display(Name = "Place Of Issue")]  //nvarchar(150)
        public string PlaceOfIssue { get; set; }

        [Required, StringLength(250), Display(Name = "Job Title")] //nvarchar(250)
        public string JobTitle { get; set; }

        [Required, StringLength(1)] //nvarchar(1) M => Muslim , C => Christian
        public string Religion { get; set; }

        [Required, StringLength(1)] //nvarchar(1) M => Male , F => Female
        public string Gender { get; set; }

        [StringLength(250), Display(Name = "Husband Name")] // nvarchar(250) Nullable
        public string? HusbandName { get; set; }

        [Display(Name = "Personal Photo")] // varbinary(max) "Can't create index on it"
        public byte[]? PersonalPhoto { get; set; }

        [Display(Name = "NID Card Front Image")] // varbinary(max) "Can't create index on it"
        public byte[]? NIDFrontImage { get; set; }


        [Display(Name = "NID Card Back Image")] // varbinary(max)  "Can't create index on it"
        public byte[]? NIDBackImage { get; set; }

        [Required, Display(Name = "Marital Status")]
        public MaritalStatusEnum MaritalStatus { get; set; }

        [Display(Name= "Governorate")]
        public byte GovernorateID { get; set; }

        [Display(Name = "Birth Site")]
        public byte BirthSiteID { get; set; }

    }
}
