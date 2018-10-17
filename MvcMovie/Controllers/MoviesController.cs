using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        //Returns all Genres currently in the database
        public JsonResult GetAvailableGenres()
        {
            using (MovieDBContext db = new MovieDBContext())
            {
                var genres = from genre in db.Genres
                             select new { id = genre.TmdbGenreId, name = genre.Name };

                //Expected JSON is in format: {[{}, {}, {}]}, i.e. array fo objects wrapped in an object
                var returnObj = new
                {
                    genres = genres.ToList()
                };

                return Json(returnObj, JsonRequestBehavior.AllowGet);
            }
        }

        //Returns all movies that match the searchString and movieGenre
        public JsonResult SearchMovies(string movieGenre, string searchString)
        {
            //If movieGenre is "---", then make sure it's unusable in movie selection
            int movieGenreInt = -1;
            bool ignoreMovieGenre = movieGenre.Equals("---");
            if (!ignoreMovieGenre)
            {
                movieGenreInt = Int32.Parse(movieGenre);
            }


            //The Context class should be disposed of after usage
            using (MovieDBContext db = new MovieDBContext())
            {
                //Two steps of selection are applied throughout the method - first - by searchString, later - by genre
                //Select all movies that contain the searchString
                var movies = from movie in db.Movies
                             where movie.Title.Contains(searchString)
                             from movieGen in db.MovieGenres
                             select movie;

                movies = movies.Distinct();
                var moviesList = movies.ToList();
                //I can't iterate through a list and delete as I go, by checking a condition.
                //Therefore, put the Movies that I want to del to a separate list
                var moviesToDelete = new List<Movie>();
                IQueryable<Genre> genres = null;
                //Iterate through the movies that previously matched the searchString
                foreach (var currentMovie in moviesList)
                {
                    genres = db.MovieGenres.Where(movieGenr => movieGenr.MovieId == currentMovie.Id).Select(x => x.Genre);
                    //If user did not select "---", then we want to select movies by genre
                    if (!ignoreMovieGenre)
                    {
                        //If the list of genres for a particular movie contains the genre provided by user
                        //Then add the whole list of genres of the particular movie (from a different table)
                        //Add it to the currentMovie.
                        //Movie class has a field Genre but it's not automatically populated based on the bridge table
                        //So I need to do it
                        if (genres.Any(x => x.TmdbGenreId == movieGenreInt))
                        {
                            currentMovie.Genres.AddRange(genres);
                        }
                        //If the list of genres corresp to the current movie does not contain genre provided by the user,
                        //Then it means current movie doesnt fit the user's query. Therefore, queue it for deletion
                        else
                        {
                            moviesToDelete.Add(currentMovie);
                        }
                    }
                    //If the user selected "---", then we don't care about the genres, only about the searchString which was handled before
                    //We don't care about the movie matching a particular genre
                    //Therefore, just add the movie's genres to the movie
                    else
                    {
                        currentMovie.Genres.AddRange(genres);
                    }
                }

                //Actually delete the movies queued for deletion
                foreach (var currentToDel in moviesToDelete)
                {
                    moviesList.Remove(currentToDel);
                }

                //To match the expected JSON structure
                var returnObj = new
                {
                    results = moviesList.Select(x => new
                    {
                        vote_count = x.VoteCount,
                        id = x.TmdbId,
                        vote_average = x.VoteAverage,
                        title = x.Title,
                        poster_path = x.PosterPath,
                        genre_ids = x.Genres.Select(g => g.TmdbGenreId),
                        overview = x.Overview,
                        release_date = x.ReleaseDate.Year
                    })

                };

                return Json(returnObj, JsonRequestBehavior.AllowGet);
            }

            //WORKS!
            //var genres = db.MovieGenres.Where(movieGenr => movieGenr.MovieId == currentMovie.Id)
            //            .Select(genre => new { Id = genre.GenreId, Name = genre.Genre.Name });


        }
        // GET: /Movies/
        /*public ActionResult Index(string movieGenre, string searchString)
        {
            
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            if (Request.IsAjaxRequest())
            {
                return Json(movies);
            }
            return View(movies);
        }*/

        /*
MovieDBContext db = new MovieDBContext();
Movie movie = new Movie();
movie.Title = "Gone with the Wind";
db.Movies.Add(movie);
db.SaveChanges();        // <= Will throw server side validation exception  
         * */

        // GET: /Movies/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

                // GET: /Movies/Create
                public ActionResult Create()
                {
                    return View(new Movie
                    {
                        Genre = "Comedy",
                        Price = 3.99M,
                        ReleaseDate = DateTime.Now,
                        Rating = "G",
                        Title = "Ghotst Busters III"
                    });
                }*/
        /*
public ActionResult Create()
{
    return View();
}

 */
        // POST: /Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Movies.Add(movie);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(movie);
        //}

        //// GET: /Movies/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Movie movie = db.Movies.Find(id);
        //    if (movie == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(movie);
        //}

        //// POST: /Movies/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(movie).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(movie);
        //}

        //// GET: /Movies/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Movie movie = db.Movies.Find(id);
        //    if (movie == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(movie);
        //}

        //// POST: /Movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Movie movie = db.Movies.Find(id);
        //    db.Movies.Remove(movie);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //        protected override void Dispose(bool disposing)
        //        {
        //            if (disposing)
        //            {
        //                db.Dispose();
        //            }
        //            base.Dispose(disposing);
        //        }
    }
}
