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
                context.Location.Add(new Location()
                {
                    Address = "Golda Meir 13, Holon",
                    Latitude = 32.012156,
                    Longitude = 34.779135
                });
                context.Location.Add(new Location()
                {
                    Address = "Menahem Begin Road 132, Tel-Aviv",
                    Latitude = 32.075138,
                    Longitude = 34.791357
                });
                context.Location.Add(new Location()
                {
                    Address = "Aba Hillel Silver 301, Ramat Gan",
                    Latitude = 32.098100,
                    Longitude = 34.824953
                });
                context.Location.Add(new Location()
                {
                    Address = "Pikus 99, Modiin",
                    Latitude = 31.900113,
                    Longitude = 35.008358
                });
            }

            if (!context.Store.Any())
            {
                context.Store.Add(new Store()
                {
                    Name = "Azriei Holon Mall",
                    LocationId = 1
                });
                context.Store.Add(new Store()
                {
                    Name = "Azriei Tower",
                    LocationId = 2
                });
                context.Store.Add(new Store()
                {
                    Name = "Ayalon Mall",
                    LocationId = 3
                });
                context.Store.Add(new Store()
                {
                    Name = "Azriei Modiin Mall",
                    LocationId = 4
                });
            }

            context.SaveChanges();
        }
    }
}
