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
        public static void Seed(EveryBookContext context, UserManager<ExtendUser> userManager, RoleManager<IdentityRole> roleManager)
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
                roleManager.CreateAsync(new IdentityRole("Admin")
                {
                    Id = "1",
                    NormalizedName = "Admin"
                }).Wait();
                roleManager.CreateAsync(new IdentityRole("User")
                {
                    Id = "2",
                    NormalizedName = "User"
                }).Wait();
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
                    Name = "Azrieli Holon Mall",
                    Location = location1
                }); ;
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Azrieli Tower",
                    Location = location2
                });
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Ayalon Mall",
                    Location = location3
                });
                context.DistributionUnit.Add(new DistributionUnit()
                {
                    Name = "Azrieli Modiin Mall",
                    Location = location4
                });
            }

            var adminUser = new ExtendUser()
            {
                Name = "Dor Eliyahu",
                Email = "aa@aa.aa",
                UserName = "aa@aa.aa"
            };
            if (!context.ExtendUser.Any(u => u.UserName == adminUser.UserName))
            {
                userManager.CreateAsync(adminUser, "Aa123456!").Wait();
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }
            
            var user = new ExtendUser()
            {
                Name = "user",
                Email = "bb@bb.bb",
                UserName = "bb@bb.bb"
            };

            if (!context.ExtendUser.Any(u => u.UserName == user.UserName))
            {
                userManager.CreateAsync(user, "Aa123456!").Wait();
                userManager.AddToRoleAsync(user, "User").Wait();
            }
            
            context.SaveChanges();
        }
    }
}
