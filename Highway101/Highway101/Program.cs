using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Highway.Data;
using System.Data.Entity;

namespace Highway101
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=.;Database=HighwayDemo;Integrated Security=true";
            var mappingConfig = new MappingConfig();
            var context = new DataContext(connectionString, mappingConfig);
            var repo = new Repository(context);

            repo.Context.Add(new Movie
                {
                    Name = "Wolverine",
                    PublishedYear = 2013,
                    Genres = new List<Genre>
                    {
                        new Genre { Name = "Action" },
                        new Genre { Name = "Comic Book" }
                    }
                });

            repo.Context.Commit();

            // READ

            //var movies = repo.Find(new FindMovieByPublishedYear(2013));
            //foreach (var movie in movies)
            //{
            //    Console.WriteLine(movie.Name);
            //}

            //var wolverine = repo.Find(new FindById(1));
            //Console.WriteLine(wolverine.Name);


            //// UPDATE
            //var wolverine = repo.Find(new FindById(1));
            //wolverine.Name = "Wolverine 2013";
            //repo.Context.Commit();

            //// DELETE
            //repo.Context.Remove(wolverine);
            //repo.Context.Commit();

            Console.WriteLine("DONE!");
            Console.ReadLine();
        }
    }

    public class DemoController
    {
        private IRepository repo;

        public DemoController(IRepository repo)
        {
            this.repo = repo;
        }

        public void Create(string Name, int Year)
        {
            repo.Context.Add(new Movie { Name = Name, PublishedYear = Year });
            repo.Context.Commit();
        }
    }

    public class FindById : Scalar<Movie>
    {
        public FindById(int id)
        {
            ContextQuery = c => c.AsQueryable<Movie>()
                .First(e => e.Id == id);
        }
    }
    public class FindMovieByPublishedYear : Query<Movie>
    {
        public FindMovieByPublishedYear(int year)
        {
            ContextQuery = c => c.AsQueryable<Movie>()
                .Where(e => e.PublishedYear == year);
        }
    }

    public class MappingConfig : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Genres).WithRequired(e => e.Movie);
            modelBuilder.Entity<Movie>().Property(e => e.Name).HasColumnName("MovieName");
        }
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
        public Movie Movie { get; set; }
    }
}
