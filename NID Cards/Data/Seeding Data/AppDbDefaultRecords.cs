using NID_Cards.Data.DataBase_Context;
using NID_Cards.Models;

namespace NID_Cards.Data.Seeding_Data
{
    public class AppDbDefaultRecords
    {


        /// <summary>
        /// <para>
        ///  The goal of this method is to insert the default data into the Governorate table
        /// </para>
        /// <para>
        ///  This method will insert the default values once you run the application only if the Governorate table is empty
        /// </para>
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // Ensure Database is created and is accessed
                context!.Database.EnsureCreated();

                // Add default recoeds for Governorate 


                // if Governorates is empty
                if (!context.Governorates.Any())
                {
                    var govList = new List<Governorate>
                    {
                        new Governorate{GovernorateID=1,GovernorateName="Cairo"},
                        new Governorate{GovernorateID=2,GovernorateName="Alexandria"},
                        new Governorate{GovernorateID=28,GovernorateName="Aswan"},
                        new Governorate{GovernorateID=25,GovernorateName="Asyut"},
                        new Governorate{GovernorateID=18,GovernorateName="Beheira"},
                        new Governorate{GovernorateID=22,GovernorateName="Beni Suef"},
                        new Governorate{GovernorateID=12,GovernorateName="Dakahlia"},
                        new Governorate{GovernorateID=11,GovernorateName="Damietta"},
                        new Governorate{GovernorateID=23,GovernorateName="Faiyum"},
                        new Governorate{GovernorateID=16,GovernorateName="Gharbia"},
                        new Governorate{GovernorateID=21,GovernorateName="Giza"},
                        new Governorate{GovernorateID=19,GovernorateName="Ismailia"},
                        new Governorate{GovernorateID=15,GovernorateName="Kafr El Sheikh"},
                        new Governorate{GovernorateID=29,GovernorateName="Luxor"},
                        new Governorate{GovernorateID=33,GovernorateName="Matruh"},
                        new Governorate{GovernorateID=24,GovernorateName="Minya"},
                        new Governorate{GovernorateID=17,GovernorateName="Monufia"},
                        new Governorate{GovernorateID=32,GovernorateName="New Valley"},
                        new Governorate{GovernorateID=34,GovernorateName="North Sinai"},
                        new Governorate{GovernorateID=3,GovernorateName="Port Said"},
                        new Governorate{GovernorateID=14,GovernorateName="Qalyubia"},
                        new Governorate{GovernorateID=27,GovernorateName="Qena"},
                        new Governorate{GovernorateID=31,GovernorateName="Red Sea"},
                        new Governorate{GovernorateID=13,GovernorateName="Sharqia"},
                        new Governorate{GovernorateID=26,GovernorateName="Sohag"},
                        new Governorate{GovernorateID=35,GovernorateName="South Sinai"},
                        new Governorate{GovernorateID=4,GovernorateName="Suez"},
                    };

                    context.Governorates.AddRange(govList);
                    context.SaveChanges();

                }// end if condition


                if (!context.BirthSites.Any())
                {
                    var bSList = new List<BirthSite>
                    {
                        new BirthSite{BirthSiteID=1, BirthSiteName="Cairo"},
                        new BirthSite{BirthSiteID=2, BirthSiteName="Alexandria"},
                        new BirthSite{BirthSiteID=28,BirthSiteName="Aswan"},
                        new BirthSite{BirthSiteID=25,BirthSiteName="Asyut"},
                        new BirthSite{BirthSiteID=18,BirthSiteName="Beheira"},
                        new BirthSite{BirthSiteID=22,BirthSiteName="Beni Suef"},
                        new BirthSite{BirthSiteID=12,BirthSiteName="Dakahlia"},
                        new BirthSite{BirthSiteID=11,BirthSiteName="Damietta"},
                        new BirthSite{BirthSiteID=23,BirthSiteName="Faiyum"},
                        new BirthSite{BirthSiteID=16,BirthSiteName="Gharbia"},
                        new BirthSite{BirthSiteID=21,BirthSiteName="Giza"},
                        new BirthSite{BirthSiteID=19,BirthSiteName="Ismailia"},
                        new BirthSite{BirthSiteID=15,BirthSiteName="Kafr El Sheikh"},
                        new BirthSite{BirthSiteID=29,BirthSiteName="Luxor"},
                        new BirthSite{BirthSiteID=33,BirthSiteName="Matruh"},
                        new BirthSite{BirthSiteID=24,BirthSiteName="Minya"},
                        new BirthSite{BirthSiteID=17,BirthSiteName="Monufia"},
                        new BirthSite{BirthSiteID=32,BirthSiteName="New Valley"},
                        new BirthSite{BirthSiteID=34,BirthSiteName="North Sinai"},
                        new BirthSite{BirthSiteID=3, BirthSiteName="Port Said"},
                        new BirthSite{BirthSiteID=14,BirthSiteName="Qalyubia"},
                        new BirthSite{BirthSiteID=27,BirthSiteName="Qena"},
                        new BirthSite{BirthSiteID=31,BirthSiteName="Red Sea"},
                        new BirthSite{BirthSiteID=13,BirthSiteName="Sharqia"},
                        new BirthSite{BirthSiteID=26,BirthSiteName="Sohag"},
                        new BirthSite{BirthSiteID=35,BirthSiteName="South Sinai"},
                        new BirthSite{BirthSiteID=4, BirthSiteName="Suez"},
                        new BirthSite{BirthSiteID=88,BirthSiteName="Outside the Egypt"},
                    };

                    context.BirthSites.AddRange(bSList);
                    context.SaveChanges();

                }// end if condition
            }
        }

    }
}
