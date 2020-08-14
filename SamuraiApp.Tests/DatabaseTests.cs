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
    public class DatabaseTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            using (var context = new SamuraiDbContext())
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var samurai = new Samurai();
                context.Samurais.Add(samurai);

                Debug.WriteLine("Before adding: " + samurai.Id);
                context.SaveChanges();
                Debug.WriteLine("After adding: " + samurai.Id);

                Assert.AreNotEqual(0, samurai.Id);
            }
        }
    }
}
