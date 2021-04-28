using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EventCatalogAPI.Data
{
    public class EventSeed
    {
        public static void Seed(EventContext eventContext)
        {
            //eventContext.Database.Migrate();

            if (!eventContext.Addresses.Any())
            {
                eventContext.Addresses.AddRange(GetAddress());
                eventContext.SaveChanges();
            }
            if (!eventContext.EventTypes.Any())
            {
                eventContext.EventTypes.AddRange(GetEventTypes());
                eventContext.SaveChanges();
            }
            if (!eventContext.EventCategories.Any())
            {
                eventContext.EventCategories.AddRange(GetEventCategories());
                eventContext.SaveChanges();
            }
            if (!eventContext.EventItems.Any())
            {
                eventContext.EventItems.AddRange(GetEventItems());
                eventContext.SaveChanges();
            }
        }
        private static IEnumerable<EventType> GetEventTypes()
        {
            return new List<EventType>()
            {
                new EventType() { Type = "Online"},
                new EventType() { Type = "In person"},
                new EventType() { Type = "Online & In person"},

            };
        }


        private static IEnumerable<EventAddress> GetAddress()
        {
            return new List<EventAddress>
            {
                new EventAddress
                {
                    City = " Woodinville", State = "WA ", ZipCode =98055  , StreetAddress = " 12345 SE 100th Eve"
                },
                new EventAddress
                {
                    City = " Redmond", State = "WA ", ZipCode =98011  , StreetAddress = " 12463 NE 109th Eve"
                },
                new EventAddress
                {
                    City = " Bellevue", State = "WA ", ZipCode =98076  , StreetAddress = " 12100 SE 201st way"
                },
                new EventAddress
                {
                    City = " Seattle", State = "WA ", ZipCode =98030  , StreetAddress = " 12302 SE 108th Eve"
                },
                new EventAddress
                {
                    City = " Renton", State = "WA ", ZipCode =98044  , StreetAddress = " 12903 SE 202nd way"
                },
                new EventAddress
                {
                    City = " Bellevue", State = "WA ", ZipCode =98046  , StreetAddress = " 12908 NE 102nd way"
                },
                new EventAddress
                {
                    City = " Sammamish", State = "WA ", ZipCode =98350  , StreetAddress = " 12803 SE 113th St"
                },
                new EventAddress
                {
                    City = " Seattle", State = "WA ", ZipCode =98121  , StreetAddress = " 12460 NE 106th way"
                }
            };
        }

        private static IEnumerable<EventCategory> GetEventCategories()
        {
            return new List<EventCategory>()
            {
                new EventCategory { Category = "Music" },
                new EventCategory { Category = "Sports" },
                new EventCategory { Category = "Food and Drink" },
                new EventCategory { Category = "Book Club" },
                new EventCategory { Category = "Kids Festival" },
                new EventCategory { Category = "Business" },
                new EventCategory { Category = "Job Fair" },
                new EventCategory { Category = "Movies" },
                new EventCategory { Category = "Tech" },
                new EventCategory { Category = "Car Show" },
                new EventCategory { Category = "Festival" },
                new EventCategory { Category = "Other" }

            };
        }
        private static IEnumerable<EventItem> GetEventItems()
        {
            return new List<EventItem>()
            {
                new EventItem
                {
                    CategoryId = 10,
                    AddressId = 3,
                    TypeId = 2,
                    EventName = "Toyota bash 4 Annual carshow ",
                 // Description = "",
                 // Price = "",
                    EventStartTime = Convert.ToDateTime("12/12/2021"),
                    EventEndTime = Convert.ToDateTime("12/12/2021"),
                    OrganizerName = "Tyota",
                    OrganzierPhoneNumber = "1-629-162-6660",
                    EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/10"
                },
                   new EventItem
                 {
                    CategoryId = 10,
                    AddressId = 2,
                    TypeId = 1,
                    EventName = "CARS AND CARBON ",
                    Description = "	An event every month that begins at 7:00 pm ",
                 // Price = "",
                    EventStartTime = Convert.ToDateTime("12/05/2021"),
                    EventEndTime = Convert.ToDateTime("12/05/2021"),
                    OrganizerName = "US car show ",
                    OrganzierPhoneNumber = "1-629-167-6460",
                    EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/11"
                },
                   new EventItem {
                     CategoryId = 8,
                     AddressId = 2,
                     TypeId = 2,
                     EventName = "FireWrok ",
                     Description = "Fouth of July celebration ",
                     Price = 22.45M,
                     EventStartTime =  Convert.ToDateTime("12/13/2021"),//mm/dd/yyy
                     EventEndTime = Convert.ToDateTime("12/05/2021"),
                     OrganizerName = "Adam Silver " ,
                     OrganzierPhoneNumber = "1-629-167-6460",
                     EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/3"},
                   new EventItem {
                     CategoryId = 4,
                     AddressId = 4,
                     TypeId = 2,
                     EventName = "Pumpkin Decorating For Dammy ",
                     Description = "Cool way to decorate pumpkin for Halloween ",
                     Price = 10.00M,
                     EventStartTime = Convert.ToDateTime("04/05/2021"),
                     EventEndTime =Convert.ToDateTime("04/07/2021"),
                     OrganizerName = "Marta Stewart " ,
                     OrganzierPhoneNumber = "1-629-167-6460",
                     EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/2"},
                          new EventItem {
                     CategoryId = 11,
                     AddressId = 2,
                     TypeId = 2,
                     EventName = "Memorial Day ",
                     Description = "Memorial Day Remembrance Walk" ,
                     Price = 0.00M,
                     EventStartTime = Convert.ToDateTime("04/03/2021"),
                     EventEndTime =Convert.ToDateTime("04/15/2021"),
                     OrganizerName = "Barack Obama ",
                     OrganzierPhoneNumber = "1-629-197-6460",
                     EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/13" },
                    new   EventItem {
                       CategoryId = 3,
                       AddressId = 2,
                       TypeId = 1,
                       EventName = "Seminar ",
                       Description = "How to gradute without student loan ",
                       Price = 99.99M,
                       EventStartTime = Convert.ToDateTime("04/05/2021"),
                       EventEndTime = Convert.ToDateTime("04/05/2021"),
                       OrganizerName = "Dr.oza " ,
                       OrganzierPhoneNumber = "1-449-197-6460",
                       EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/4"},
                 new EventItem {
                       CategoryId = 8,
                       AddressId = 2,
                       TypeId = 1,
                       EventName = "Music Concert ",
                       Description = "Best 80's Party ever ",
                       Price = 125.00M,
                       EventStartTime = Convert.ToDateTime("04/05/2021"),
                       EventEndTime = Convert.ToDateTime("04/05/2021"),
                       OrganizerName = "xz.org ",
                       OrganzierPhoneNumber = "1-449-197-6460",
                       EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/5"},
                 new EventItem {
                       CategoryId = 12,
                       AddressId = 2,
                       TypeId = 3,
                       EventName="Medical conference",
                       Description = "World Congress on Medical and Aromatic Plants",
                       Price =  63.45M,
                       EventStartTime =  Convert.ToDateTime("04/05/2021"),
                       EventEndTime =  Convert.ToDateTime("04/05/2021"),
                       OrganizerName = " Tealdc",
                       OrganzierPhoneNumber = "1-449-197-6460",
                       EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/6" },
            new EventItem {
                       CategoryId = 11,
                       AddressId = 2,
                       TypeId = 1,
                       EventName = "Hindu's festival in Seattle",
                       Description = "Come join us to celebrate life ",
                       Price = 0.00M ,
                       EventStartTime =  Convert.ToDateTime("04/06/2021"),
                       EventEndTime =  Convert.ToDateTime("04/06/2021"),
                       OrganizerName = "Gandi.org  " ,
                       OrganzierPhoneNumber = "1-449-197-6460",
                       EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/7"},
                 new EventItem {
                     CategoryId = 3,
                     AddressId = 2,
                     TypeId = 1,
                     EventName = "Celebrity birthday cake Studio",
                     Description = "Beautifully delicious cakes for birthday",
                     Price = 345.00M,
                     EventStartTime = Convert.ToDateTime("07/06/2021"),
                     EventEndTime = Convert.ToDateTime("07/06/2021"),
                     OrganizerName = "MybirthDay.org ",
                     OrganzierPhoneNumber = "1-449-197-6460",
                     EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/12"},
                  new EventItem {
                      CategoryId = 6,
                      AddressId = 2,
                      TypeId = 1,
                      EventName = "Wine Tasting",
                      Description = "The best Wine in Washington state ",
                      Price = 24.95M,
                      EventStartTime = Convert.ToDateTime("05/06/2021"),
                      EventEndTime = Convert.ToDateTime("05/06/2021"),
                      OrganizerName = " Winebest.org",
                      OrganzierPhoneNumber = "1-449-197-6460",
                      EventImageUrl = "http://externaleventbaseurltoberplaced/api/pic/9" }
            };
        }


    }
}










