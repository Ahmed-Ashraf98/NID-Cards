namespace NID_Cards.Extension_Methods
{
    public static class DiffDatesExtensionMethod
    {
        /// <summary>
        /// 
        /// <para>.</para>
        /// <para>
        /// This Method is designed to compare <paramref name="NIDExpiryDate"/> with the Current Date ( DateTime.Now )
        /// </para>
        /// <para>
        /// The goal of this Method is to determine if the National ID Card  is Active or not 
        ///</para>
        /// <para> Return Values :</para>
        /// <list type="bullet">
        /// <listheader></listheader>
        /// <item>
        /// <term>False</term>
        /// <description>means that the card is expired</description>
        /// </item>
        /// <item>
        /// <term>True</term>
        /// <description>means that the card is Active</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="NIDExpiryDate"></param>
        /// <returns><code>true or false</code></returns>
        public static bool FromCurrentDate(this DateTime NIDExpiryDate)
        {
            var diffYears = NIDExpiryDate.Year - DateTime.Now.Year;
            var diffMonth = NIDExpiryDate.Month - DateTime.Now.Month;

            if (diffYears <= 0 && diffMonth <= 0 )
            {
                return false;
            }
            return true;
        }
    }
}
