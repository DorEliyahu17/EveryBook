using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using EveryBook.Models;
using System.Threading.Tasks;

namespace EveryBook.Data
{
    public class DbSeed
    {
        public static void Seed(EveryBookContext context)
        {
            var location1 = new Location()
            {
                Address = "Golda Meir 13, Holon",
                Latitude = 32.012156,
                Longitude = 34.779135
            };

            var location2 = new Location()
            {
                Address = "Menahem Begin Road 132, Tel-Aviv",
                Latitude = 32.075138,
                Longitude = 34.791357
            };

            var location3 = new Location()
            {
                Address = "Aba Hillel Silver 301, Ramat Gan",
                Latitude = 32.098100,
                Longitude = 34.824953
            };

            var location4 = new Location()
            {
                Address = "Pikus 99, Modiin",
                Latitude = 31.900113,
                Longitude = 35.008358
            };

            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole("Admin")
                {
                    Id = "1",
                    NormalizedName = "Admin"
                });
                context.Roles.Add(new IdentityRole("User")
                {
                    Id = "2",
                    NormalizedName = "User"
                });
            }

            if (!context.Genre.Any())
            {
                context.Genre.Add(new Genre()
                {
                    Name = "Action"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Adventure"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Classics"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Comic Book"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Fantasy"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Horror"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Romance"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Science Fiction"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Short Stories"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Thrillers"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Biographies"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Cookbooks"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "History"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Poetry"
                });
            }

            if (!context.Location.Any())
            {

                context.Location.Add(location1);
                context.Location.Add(location2);
                context.Location.Add(location3);
                context.Location.Add(location4);
            }


            if (!context.DistributionUnit.Any())
            {
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Azriei Holon Mall",
                    Location = location1
                }); ;
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Azriei Tower",
                    Location = location2
                });
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Ayalon Mall",
                    Location = location3
                });
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Azriei Modiin Mall",
                    Location = location4
                });
            }
            context.SaveChanges();
        }
    }
}
