using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NID_Cards.Models
{
    public class BirthSite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public byte BirthSiteID { get; set; }

        [Required, MaxLength(150)] // navchar(150) and not null
        public string BirthSiteName { get; set; }

        public ICollection<Citizen> Citizens { get; set; }
    }
}
