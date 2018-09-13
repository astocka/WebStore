using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Context;

namespace WebStore.Models
{
    public static class SeedData
    {
        public static void Seed(IServiceProvider services)
        {
            AppDbContext context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange
                (
                     new Product()
                     {
                         Title = "Inception",
                         Author = "Christopher Nolan",
                         Created = DateTime.Parse("2010-07-30"),
                         Price = 25.50M,
                         Description = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb's rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible - inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming. Written by Warner Bros. Pictures",
                         Category = Categories["Movies"],
                         ImagePath = "inception_t.jpg"
                     },
                    new Product()
                    {
                        Title = "Léon",
                        Author = "Luc Besson",
                        Created = DateTime.Parse("1995-05-26"),
                        Price = 23.80M,
                        Description = "After her father, mother, older sister and little brother are killed by her father's employers, the 12-year-old daughter of an abject drug dealer is forced to take refuge in the apartment of a professional hitman who at her request teaches her the methods of his job so she can take her revenge on the corrupt DEA agent who ruined her life by killing her beloved brother. Written by J. S. Golden",
                        Category = Categories["Movies"],
                        ImagePath = "leon_t.jpg"
                    },
                    new Product()
                    {
                        Title = "The Shawshank Redemption",
                        Author = "Frank Darabont",
                        Created = DateTime.Parse("1995-04-16"),
                        Price = 22.50M,
                        Description = "Chronicles the experiences of a formerly successful banker as a prisoner in the gloomy jailhouse of Shawshank after being found guilty of a crime he did not commit. The film portrays the man's unique way of dealing with his new, torturous life; along the way he befriends a number of fellow prisoners, most notably a wise long-term inmate named Red. Written by J-S-Golden",
                        Category = Categories["Movies"],
                        ImagePath = "shawshank_t.jpg"
                    },
                    new Product()
                    {
                        Title = "Glasshouse",
                        Author = "Jessie Ware",
                        Created = DateTime.Parse("2017-10-20"),
                        Price = 35.50M,
                        Description = "Tracklist: 01.Midnight,  02.Thinking About You,  03.Stay Awake, Wait for Me, 04.Your Domino, 05.Alone,  06.Selfish Love, 07.First Time, 08.Hearts, 09.Slow Me Down, 10.Finish What We Started, 11.Last of the True Believers, 12.Sam",
                        Category = Categories["Music"],
                        ImagePath = "glasshouse_t.jpg"
                    },
                    new Product()
                    {
                        Title = "Grace",
                        Author = "Jeff Buckley",
                        Created = DateTime.Parse("1994-08-23"),
                        Price = 30.20M,
                        Description = "Tracklist: 01.Mojo Pin, 02.Grace, 03.Last Goodbye, 04.Lilac Wine, 05.So Real, 06.Hallelujah, 07.Lover, You Should've Come Over, 08.Corpus Christi Carol, 09.Eternal Life, 10.Dream Brother",
                        Category = Categories["Music"],
                        ImagePath = "grace_t.jpg"
                    },
                    new Product()
                    {
                        Title = "Wasting Light",
                        Author = "Foo Fighters",
                        Created = DateTime.Parse("2011-04-12"),
                        Price = 33.50M,
                        Description = "Tracklist: 01.Bridge Burning, 02.Rope, 03.Dear Rosemary, 04.White Limo, 05.Arlandria, 06.These Days, 07.Back & Forth, 08.A Matter Of Time, 09.Miss The Misery, 10.I Should Have Known, 11.Walk",
                        Category = Categories["Music"],
                        ImagePath = "wastinglight_t.jpg"
                    },
                    new Product()
                    {
                        Title = "Shodo: The Quiet Art of Japanese Zen Calligraphy",
                        Author = "Shozo Sato",
                        Created = DateTime.Parse("2014-03-11"),
                        Price = 85.50M,
                        Description = "In this beautiful and extraordinary zen calligraphy book, Shozo Sato, an internationally recognized master of traditional Zen arts, teaches the art of Japanese calligraphy through the power and wisdom of Zen poetry.",
                        Category = Categories["Books"],
                        ImagePath = "shodo_t.jpg"
                    },
                    new Product()
                    {
                        Title = "The Little Prince",
                        Author = "Antoine de Saint-Exupéry",
                        Created = DateTime.Parse("1943-04-06"),
                        Price = 20.50M,
                        Description = "The Little Prince, first published in April 1943, is a novella, the most famous work of French aristocrat, writer, poet, and pioneering aviator Antoine de Saint-Exupéry. The novella is one of the most - translated books in the world and has been voted the best book of the 20th century in France.Translated into 300[3] languages and dialects, selling nearly two million copies annually, and with year - to - date sales of over 140 million copies worldwide, it has become one of the best - selling books ever published. After the outbreak of the Second World War, Saint - Exupéry escaped to North America.Despite personal upheavals and failing health, he produced almost half of the writings for which he would be remembered, including a tender tale of loneliness, friendship, love, and loss, in the form of a young prince visiting Earth.An earlier memoir by the author had recounted his aviation experiences in the Sahara Desert, and he is thought to have drawn on those same experiences in The Little Prince. Since its first publication, the novella has been adapted to numerous art forms and media, including audio recordings, radio plays, live stage, film, television, ballet, and opera. [Wikipedia]",
                        Category = Categories["Books"],
                        ImagePath = "littleprince_t.jpg"
                    },
                    new Product()
                    {
                        Title = "The Alchemist",
                        Author = "Paulo Coelho",
                        Created = DateTime.Parse("1988-01-01"),
                        Price = 23.80M,
                        Description = "The Alchemist is a novel by Brazilian author Paulo Coelho that was first published in 1988. Originally written in Portuguese, it became an international bestseller translated into some 70 languages as of 2016. An allegorical novel, The Alchemist follows a young Andalusian shepherd in his journey to the pyramids of Egypt, after having a recurring dream of finding a treasure there. Over the years there have been film and theatrical adaptations of the work and musical interpretations of it. [Wikipedia]",
                        Category = Categories["Books"],
                        ImagePath = "alchemist_t.jpg"
                    });

                context.SaveChanges();
            }


            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
                context.SaveChanges();
            }

            if (!context.OrderStatuses.Any())
            {
                context.OrderStatuses.AddRange(OrderStatuses.Select(c => c.Value));
                context.SaveChanges();
            }


        }
        private static Dictionary<string, Category> categories;
        private static Dictionary<string, OrderStatus> orderStatuses;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category {Name = "Music"},
                        new Category {Name = "Books"},
                        new Category {Name = "Movies"}
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.Name, genre);
                    }
                }

                return categories;
            }
        }

        public static Dictionary<string, OrderStatus> OrderStatuses
        {
            get
            {
                if (orderStatuses == null)
                {
                    var status = new OrderStatus[]
                    {
                        new OrderStatus{Name = "Awaiting Payment"},
                        new OrderStatus{Name = "Awaiting Fulfillment"},
                        new OrderStatus{Name = "Shipped"},
                        new OrderStatus{Name = "Cancelled"},
                        new OrderStatus{Name = "Completed"}
                    };

                    orderStatuses = new Dictionary<string, OrderStatus>();

                    foreach (OrderStatus st in status)
                    {
                        orderStatuses.Add(st.Name, st);
                    }
                }

                return orderStatuses;
            }
        }
    }
}
