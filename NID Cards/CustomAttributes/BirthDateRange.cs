using System.ComponentModel.DataAnnotations;

namespace NID_Cards.CustomAttributes
{
    public class BirthDateRange:RangeAttribute
    {
        // from { Current Year - 122 = 1900}  To  { today }
        public BirthDateRange() 
            : base(type:typeof(DateTime),
                  DateTime.Now.Date.AddYears(-122).ToString("yyyy-MM-dd"),
                  DateTime.Now.Date.ToString("yyyy-MM-dd"))


        {
        
        }

    }
}
