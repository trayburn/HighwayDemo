using System;
using System.Linq;
using Highway.Data;
using Highway.Data.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Highway101.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private InMemoryDataContext context;
        private IRepository repo;
        private DemoController controller;

        [TestInitialize]
        public void Setup()
        {
            context = new InMemoryDataContext();
            repo = new Repository(context);
            controller = new DemoController(repo);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            string movieName = "Improving Is Awesome";            

            // Act
            controller.Create(movieName, 2004);

            // Assert
            var movie = context.AsQueryable<Movie>().First();
            Assert.AreEqual(movieName, movie.Name);
            Assert.AreEqual(2004, movie.PublishedYear);
            Console.WriteLine(movie.Id);
        }
    }
}
