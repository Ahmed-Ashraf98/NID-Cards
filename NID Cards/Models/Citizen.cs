using NID_Cards.CustomAttributes;
using NID_Cards.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NID_Cards.Models
{
    public class Citizen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Enter your unique 5 numbers in your NID")]// National ID consists of 14 numbers
        public long NID { get; set; }
        
        [Required,MaxLength(250)] //nvarchar(250)
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name ="Birthdate")]
        public DateTime BirthDate { get; set; }

        [Required,MaxLength(700)] //nvarchar(700)
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfIssue { get; set; }
        
        [Required,MaxLength(150)]  //nvarchar(150)
        public string PlaceOfIssue { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required, MaxLength(250)] //nvarchar(250)
        public string JobTitle { get; set; }

        [Required, MaxLength(1)] //nvarchar(1) M => Muslim , C => Christian
        public string Religion { get; set; }

        [Required,MaxLength(1)] //nvarchar(1) M => Male , F => Female
        public string Gender { get; set; } 

        [MaxLength(250)] // nvarchar(250) Nullable
        public string HusbandName { get; set; }

        [Required] // bit , not null
        public bool NIDIsActive { get; set; }  // => Active / Not Active
        
        [Required] // varbinary(max) "Can't create index on it"
        public byte[] PersonalPhoto { get; set; }

        [Required] // varbinary(max) "Can't create index on it"
        public byte[] NIDFrontImage { get; set; }


        [Required] // varbinary(max)  "Can't create index on it"
        public byte[] NIDBackImage { get; set; }

        [Required]
        public MaritalStatusEnum MaritalStatus { get; set; }

        // FKs ---------- Foreign keys ------------

        public byte GovernorateID { get; set; }

        public Governorate Governorate { get; set; }


        public byte BirthSiteID { get; set; }

        public BirthSite BirthSite { get; set; }

    }
}
