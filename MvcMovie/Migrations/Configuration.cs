using System.Collections.Generic;

namespace MvcMovie.Migrations
{
    using MvcMovie.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcMovie.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MvcMovie.Models.MovieDBContext context)
        {
            context.Movies.AddOrUpdate(i => i.Title,
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-1-11"),
                    Price = 7.99M,
                    Rating = "PG",
                    TmdbId = 639,
                    VoteCount = 999,
                    VoteAverage = 5.6,
                    Overview = "BLAH-BLAH1",
                    PosterPath = ""
                    //Genres = new List<Genre> { new Genre{TmdbGenreId = 16, Name = "Animation"}, new Genre{TmdbGenreId = 17, Name="Comedy"}}
                },

                 new Movie
                 {
                     Title = "Ghostbusters ",
                     ReleaseDate = DateTime.Parse("1984-3-13"),
                     Price = 8.99M,
                     Rating = "G",
                     TmdbId = 620,
                     VoteCount = 999,
                     VoteAverage = 5.6,
                     Overview = "BLAH-BLAH2",
                     PosterPath = ""
                 },

                 new Movie
                 {
                     Title = "Ghostbusters 2",
                     ReleaseDate = DateTime.Parse("1986-2-23"),
                     Price = 9.99M,
                     Rating = "G",
                     TmdbId = 2978,
                     VoteCount = 999,
                     VoteAverage = 5.6,
                     Overview = "BLAH-BLAH3",
                     PosterPath = ""
                 },

               new Movie
               {
                   Title = "Rio Bravo",
                   ReleaseDate = DateTime.Parse("1959-4-15"),
                   Price = 3.99M,
                   Rating = "None",
                   TmdbId = 301,
                   VoteCount = 999,
                   VoteAverage = 5.6,
                   Overview = "BLAH-BLAH4",
                   PosterPath = ""
               }
           );
            List<Client> clients = new List<Client>();
            clients.Add(new Client("fname1", "last", "addr", "city", "state", "zip", "1231231212", "asfd", "asdf"));
            foreach (var currentClient in clients)
            {
                context.Clients.AddOrUpdate(currentClient);
            }

            context.Genres.AddOrUpdate(i => i.Name,
                new Genre { Name = "Fantasy", TmdbGenreId = 14 },
                new Genre { Name = "Horror", TmdbGenreId = 27 },
                new Genre { Name = "Animation", TmdbGenreId = 16 }
            );

            context.MovieGenres.AddOrUpdate(i => i.GenreId,
                new MovieGenre { MovieId = 1, GenreId = 1 },
                new MovieGenre { MovieId = 1, GenreId = 2 },
                new MovieGenre { MovieId = 2, GenreId = 3 }
            );
        }

    }
}
