using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SumaraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamuraiApp.Tests
{
   
    [TestClass]   
    public class BuisnessDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraisReturnsCorrectNumberOfInsertedRows()
        {
            var builder = new DbContextOptionsBuilder<SamuraiDbContext>();
            builder.UseInMemoryDatabase("AddMultipleSamurais");

            using (var context = new SamuraiDbContext(builder.Options))
            {
                var bizData = new BuisnessDataLogic(context);
                string[] nameList = new string[] { "first", "second", "third" };
                var result = bizData.AddMultipleSamurais(nameList);

                Assert.AreEqual(result, nameList.Count());
            }
        }

        [TestMethod]
        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder<SamuraiDbContext>();
            builder.UseInMemoryDatabase("InsertSingleSamurai");

            using (var context = new SamuraiDbContext(builder.Options))
            {
                var bizData = new BuisnessDataLogic(context);
                
                //Now that insertSingleSamurai takes single samurai & returns nothing
                bizData.InsertSingleSamurai(new Samurai());

                // to check if the samurai is added we can use another context pointing to same in memory database
                // this way we can ensure that savechanges was called & Samurai indeed was saved
                using (var context2 = new SamuraiDbContext(builder.Options))
                {
                    Assert.AreEqual(context2.Samurais.Count(), 1);
                }
            }
        }
    }
}
