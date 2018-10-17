using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using MvcMovie.Models;

namespace MvcMovie.Models
{
    public class Movie
    {
        public Movie()
        {
            Genres = new List<Genre>();

        }

        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        /*[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }*/

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [StringLength(5)]
        public string Rating { get; set; }

        public int TmdbId { get; set; }
        public List<Genre> Genres { get; set; }
        //public byte[] ImagePath { get; set; }
        public int VoteCount { get; set; }

        //[Range(1, 10)]
        public double VoteAverage { get; set; }

        public string Overview { get; set; }
        public string PosterPath { get; set; }

        ////Additional props: imagePath
        ////genre_ids
        ////id
        ////title
        ////vote_average
        ////vote_count
        ////release_date
        ////overview
    }

    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MvcMovie.Models.MovieGenre> MovieGenres { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Genre>().Property(g => g.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        //}


        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }

}
