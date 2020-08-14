using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SumaraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SamuraiApp.Tests
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder<SamuraiDbContext>();
            builder.UseInMemoryDatabase("CanInsertSamurai");

            using (var context = new SamuraiDbContext(builder.Options))
            {               
                var samurai = new Samurai();
                context.Samurais.Add(samurai);

                //Debug.WriteLine("Before adding: " + samurai.Id);
                //context.SaveChanges();
                //Debug.WriteLine("After adding: " + samurai.Id);

                //Assert.AreNotEqual(0, samurai.Id);
                Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
            }
        }
    }
}
