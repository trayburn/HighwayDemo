using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework101
{
    class Program
    {
        static void Main(string[] args)
        {
            // ORM
            // Object Relational Mapper
            var context = new MovieDbContext();

            // ######################################################
            // CREATE
            // ######################################################
            context.Movies.Add(new Movie
                {
                    Name = "Transformers 4",
                    PublishedYear = 2014,
                    Genres = new List<Genre> {
                        new Genre
                        {
                            Name = "Action"
                        },
                        new Genre
                        {
                            Name = "Giant Mecha-Robots"
                        }
                    }
                });

            context.SaveChanges();

            // ######################################################
            // READ
            // ######################################################
            //var movies = context.Movies.Include(e => e.Genres);

            //foreach (var movie in movies)
            //{
            //    Console.WriteLine(movie.Name);
            //    foreach (var genre in movie.Genres)
            //    {
            //        Console.WriteLine(">>> {0}", genre.Name);
            //    }
            //}

            // ######################################################
            // UPDATE
            // ######################################################
            //var transformers = context.Movies.First();

            //transformers.Name = "Transformers 4 - Way Too Long";
            //context.SaveChanges();


            // ######################################################
            // DELETE
            // ######################################################

            //var transformers = context.Movies.Include(e => e.Genres).First();
            //transformers.Genres.Clear();
            //context.Movies.Remove(transformers);
            //context.SaveChanges();

            Console.WriteLine("DONE!");
            Console.ReadLine();
        }
    }

    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }

    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublishedYear { get; set; }
        public List<Genre> Genres { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
