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

            var adminUser = new ExtendUser()
            {
                Name = "Admin",
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
                Name = "User",
                Email = "bb@bb.bb",
                UserName = "bb@bb.bb"
            };

            if (!context.ExtendUser.Any(u => u.UserName == user.UserName))
            {
                userManager.CreateAsync(user, "Aa123456!").Wait();
                userManager.AddToRoleAsync(user, "User").Wait();
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
                context.Genre.Add(new Genre()
                {
                    Name = "Investing"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "Computer Science"
                });
                context.Genre.Add(new Genre()
                {
                    Name = "DIY"
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

            if (!context.Book.Any())
            {
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/4088/9781408855652.jpg",
                    Name = "Harry Potter And The Philosopher's Stone",
                    Author = "J.K. Rowling",
                    Price = 15,
                    Description = "Escape to Hogwarts with the unmissable series that has sparked a lifelong reading journey for children and families all over the world.The magic starts here.\nHarry Potter has never even heard of Hogwarts when the letters start dropping on the doormat at number four, Privet Drive.Addressed in green ink on yellowish parchment with a purple seal, they are swiftly confiscated by his grisly aunt and uncle.Then, on Harry's eleventh birthday, a great beetle-eyed giant of a man called Rubeus Hagrid bursts in with some astonishing news: Harry Potter is a wizard, and he has a place at Hogwarts School of Witchcraft and Wizardry. The magic starts here!\nThese editions of the classic and internationally bestselling Harry Potter series feature thrilling jacket artwork by award - winning illustrator Jonny Duddle.They are the perfect starting point for anyone who's ready to lose themselves in the greatest children's story of all time.",
                    AvailableQuantity = 15,
                    GenreId = context.Genre.Where(g => g.Name == "Fantasy").FirstOrDefault().Id
                });
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/4711/9781471158292.jpg",
                    Name = "Band Of Brothers",
                    Author = "Stephen E. Ambrose",
                    Price = 20,
                    Description = "The book that inspired Steven Spielberg's acclaimed TV series, produced by Tom Hanks and starring Damian Lewis.\nIn Band of Brothers, Stephen E. Ambrose pays tribute to the men of Easy Company, a crack rifle company in the US Army. From their rigorous training in Georgia in 1942 to the dangerous parachute landings on D-Day and their triumphant capture of Hitler's 'Eagle's Nest' in Berchtesgaden. Ambrose tells the story of this remarkable company. Repeatedly send on the toughest missions, these brave men fought, went hungry, froze and died in the service of their country.\nA tale of heroic adventures and soul-shattering confrontations, Band of Brothers brings back to life, as only Stephen E. Ambrose can, the profound ties of brotherhood forged in the barracks and on the battlefields.\n'History boldly told and elegantly written . . . Gripping' Wall Street Journal\n'Ambrose proves once again he is a masterful historian . . . spellbinding' People",
                    AvailableQuantity = 10,
                    GenreId = context.Genre.Where(g => g.Name == "History").FirstOrDefault().Id
                });
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/6126/9781612680194.jpg",
                    Name = "Rich Dad Poor Dad",
                    Author = "Robert T.Kiyosaki",
                    Price = 30,
                    Description = "It's been nearly 25 years since Robert Kiyosaki's Rich Dad Poor Dad first made waves in the Personal Finance arena.\nIt has since become the #1 Personal Finance book of all time... translated into dozens of languages and sold around the world.\nRich Dad Poor Dad is Robert's story of growing up with two dads his real father and the father of his best friend, his rich dad and the ways in which both men shaped his thoughts about money and investing. The book explodes the myth that you need to earn a high income to be rich and explains the difference between working for money and having your money work for you.\n20 Years... 20/20 Hindsight\nIn the 20th Anniversary Edition of this classic, Robert offers an update on what we've seen over the past 20 years related to money, investing, and the global economy. Sidebars throughout the book will take readers \"fast forward\" from 1997 to today as Robert assesses how the principles taught by his rich dad have stood the test of time.\nmany ways, the messages of Rich Dad Poor Dad, messages that were criticized and challenged two decades ago, are more meaningful, relevant and important today than they were 20 years ago.\nAs always, readers can expect that Robert will be candid, insightful... and continue to rock more than a few boats in his retrospective.\nWill there be a few surprises? Count on it.\nRich Dad Poor Dad...\nExplodes the myth that you need to earn a high income to become rich\nChallenges the belief that your house is an asset\nShows parents why they can't rely on the school system to teach their kids\nabout money\nDefines once and for all an asset and a liability\nTeaches you what to teach your kids about money for their future financial\nsuccess",
                    AvailableQuantity = 3,
                    GenreId = context.Genre.Where(g => g.Name == "Investing").FirstOrDefault().Id
                });
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9781/8392/9781839214189.jpg",
                    Name = "Data Engineering with Python",
                    Author = "Paul Crickard",
                    Price = 18,
                    Description = "Build, monitor, and manage real-time data pipelines to create data engineering infrastructure efficiently using open-source Apache projects\nKey Features\nBecome well-versed in data architectures, data preparation, and data optimization skills with the help of practical examples\nDesign data models and learn how to extract, transform, and load (ETL) data using Python\nSchedule, automate, and monitor complex data pipelines in production\nBook DescriptionData engineering provides the foundation for data science and analytics, and forms an important part of all businesses. This book will help you to explore various tools and methods that are used for understanding the data engineering process using Python.\nThe book will show you how to tackle challenges commonly faced in different aspects of data engineering. You'll start with an introduction to the basics of data engineering, along with the technologies and frameworks required to build data pipelines to work with large datasets. You'll learn how to transform and clean data and perform analytics to get the most out of your data. As you advance, you'll discover how to work with big data of varying complexity and production databases, and build data pipelines. Using real-world examples, you'll build architectures on which you'll learn how to deploy data pipelines.\nBy the end of this Python book, you'll have gained a clear understanding of data modeling techniques, and will be able to confidently build data engineering pipelines for tracking data, running quality checks, and making necessary changes in production.\nWhat you will learn\nUnderstand how data engineering supports data science workflows\nDiscover how to extract data from files and databases and then clean, transform, and enrich it\nConfigure processors for handling different file formats as well as both relational and NoSQL databases\nFind out how to implement a data pipeline and dashboard to visualize results\nUse staging and validation to check data before landing in the warehouse\nBuild real-time pipelines with staging areas that perform validation and handle failures\nGet to grips with deploying pipelines in the production environment\nWho this book is forThis book is for data analysts, ETL developers, and anyone looking to get started with or transition to the field of data engineering or refresh their knowledge of data engineering using Python. This book will also be useful for students planning to build a career in data engineering or IT professionals preparing for a transition. No previous knowledge of data engineering is required.",
                    AvailableQuantity = 6,
                    GenreId = context.Genre.Where(g => g.Name == "Computer Science").FirstOrDefault().Id
                });
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/0074/9780007428540.jpg",
                    Name = "Game of Thrones",
                    Author = "George R.R. Martin",
                    Price = 28,
                    Description = "HBO's hit series A GAME OF THRONES is based on George R. R. Martin's internationally bestselling series A SONG OF ICE AND FIRE, the greatest fantasy epic of the modern age. A GAME OF THRONES is the first volume in the series.\nKings and queens, knights and renegades, liars, lords and honest men... all will play the GAME OF THRONES.\nSummers span decades. Winter can last a lifetime. And the struggle for the Iron Throne has begun.\nIt will stretch from the south, where heat breeds plot, lusts and intrigues; to the vast and savage eastern lands; all the way to the frozen north, where a 700-foot wall of ice protects the kingdom from the dark forces that lie beyond. The Game of Thrones. You win, or you die.\nBook One of A Song of Ice and Fire begins the greatest fantasy epic of the modern age.\nWinter is coming...",
                    AvailableQuantity = 1,
                    GenreId = context.Genre.Where(g => g.Name == "Fantasy").FirstOrDefault().Id
                });
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9789/1798/9789179850999.jpg",
                    Name = "1000 Origami",
                    Author = "Mayumi Jezewski",
                    Price = 5,
                    Description = "Ett oemotståndligt origamiblock!\nEtt rejält block med tunna och fint mönstrade papper i härliga färger.\nBlocket innehåller även instruktioner till 12 olika origamiprojekt.\nMed 1000 mönstrade sidor är det här ett origamiblock som sticker ut rejält och håller dig sysselsatt i timmar.",
                    AvailableQuantity = 2,
                    GenreId = context.Genre.Where(g => g.Name == "DIY").FirstOrDefault().Id
                });
                context.Book.Add(new Book()
                {
                    PictureUrl = "https://d1w7fb2mkkr3kw.cloudfront.net/assets/images/book/lrg/9780/7364/9780736426701.jpg",
                    Name = "Walt Disney's Alice in Wonderland",
                    Author = "Rh Disney",
                    Price = 11,
                    Description = "Based on Walt Disney's animated classic, this vintage Little Golden Book from 1951 retells the story of Alice's wild adventures in Wonderland.",
                    AvailableQuantity = 8,
                    GenreId = context.Genre.Where(g => g.Name == "Classics").FirstOrDefault().Id
                });
            }

            context.SaveChanges();
        }
    }
}
