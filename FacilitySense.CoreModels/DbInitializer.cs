
using FacilitySense.DataModels;

namespace FacilitySense.Repositories.SQL
{
    public static class DbInitializer
    {
        public static void Initialize(FacilitySenseDBContext context)
        {
            //if(context.Facilities.Any())
            //{
            //    return;
            //}

            //var facilities = new Facility[]
            //{
            //    new Facility{ Name="Miami Beach Pirate Playground", 
            //        Description="Playground on 77th street",
            //        Longitude=-80.12005689710377,
            //        Latitude=25.862500894020915,
            //        AddressLineOne="Altos Del Mar", 
            //        AddressLineTwo="-", 
            //        City="Miami Beach", 
            //        State="FL", 
            //        ZipCode="33141"},
            //    new Facility { Name = "Allison Park", 
            //        Description = "Playground and park near 65th street",
            //        Longitude=-80.11917076230047,
            //        Latitude=25.847738590836155,
            //        AddressLineOne = "6515 Collins Ave", 
            //        AddressLineTwo = "-", 
            //        City = "Miami Beach", 
            //        State = "FL", 
            //        ZipCode = "33141" }
            //};
            //context.Facilities.AddRange(facilities);
            //context.SaveChanges();

            //var users = new User[]
            //{
            //    new User{ ID=0, Email="admin@admin.com", Username="admin", Phone = "0000"},
            //    new User{ ID=1, Email="userone@one.com", Username="userOne", Phone = "0000"},
            //    new User{ ID=2, Email="usertwo@one.com", Username="userTwo", Phone = "0000"}
            //};
            //context.Users.AddRange(users);
            //context.SaveChanges();

            //var ratings = new Rating[]
            //{
            //    new Rating{ID=0, Comment="Kids love being pirates", Stars=5, FacilityID=0, UserID=1},
            //    new Rating{ID=1, Comment="Drunk Homeless are always hanging around", Stars=1, FacilityID=0, UserID=2},
            //    new Rating{ID=2, Comment="Bathrooms are really clean", Stars=1, FacilityID=1, UserID=2}
            //};
            //context.Ratings.AddRange(ratings);

            //context.SaveChanges();
        }
    }
}
