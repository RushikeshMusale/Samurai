using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SumaraiApp.Domain;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ConsoleApp
{
    class Program
    {
        private static SamuraiDbContext context = new SamuraiDbContext();

        private static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            DisconnectedAttachedAddChild();
            //DisconnectedAddChild();

            //QueryAndUpdate_DisconnecteScenario();
            //DifferenceBetweenFindAndWhere();

            //InsertMultipleSamurais();

            //GetSamurais("Before Add:");
            //AddSamurai();
            //GetSamurais("After Add:");
            Console.Write("Press any key...");
            Console.ReadKey();


        }

        private static void DisconnectedAddChild()
        {
            var samurai = context.Samurais.FirstOrDefault();
            var quote = new Quote { Text = "I have come to save you" };

            samurai.Quotes.Add(quote);

            // disconnected scenario
            using (var dbContext = new SamuraiDbContext())
            {
                // update is smart enough to understand that child object quote has blank identity,
                // and the samurai id (parent)
                // so generated db query:
                //    1. will insert new quote, 
                //    2. update its identity, 
                //    3. set samurai id
                //    4. since we are updating on samurai object, remaining scalar properties will be set in different query
                dbContext.Samurais.Update(samurai);
                dbContext.SaveChanges();
            }
        }

        private static void DisconnectedAttachedAddChild()
        {
            var samurai = context.Samurais.FirstOrDefault();
            var quote = new Quote { Text = "I have come to save you" };

            samurai.Quotes.Add(quote);
            samurai.Name += "2"; // Name will be updated in update(), but not in attach()

            // disconnected scenario
            using (var dbContext = new SamuraiDbContext())
            {
                // Attach is smart enough to understand that child object quote has blank identity,
                // and the samurai id (parent)
                // so generated db query:
                //    1. will insert new quote, 
                //    2. update its identity, 
                //    3. set samurai id
                //    4. But it will NOT update remaining scalar properties will be set in different query               
                dbContext.Samurais.Attach(samurai);
                dbContext.SaveChanges();
            }
        }


        private static void QueryAndUpdate_DisconnecteScenario()
        {
            Battle battle = context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = DateTime.Now.AddDays(2);

            using (var dbcontext = new SamuraiDbContext())
            {
                // this will cause to start tracking the changes
                // it will fire a db query which updates all the scalar fields of battle:
                // NOTE: it will not update complex fields
                dbcontext.Battles.Update(battle);
                dbcontext.SaveChanges();
            }
        }

        private static void DifferenceBetweenFindAndWhere()
        {
            var samurai = context.Samurais.FirstOrDefault(s => s.Id == 1);
            Console.WriteLine("Name of Samurai: "+ samurai.Name);

            //samurai.Name = "Updated back";

            var samuraiFound = context.Samurais.Find(1);
            Console.WriteLine("Check the logs, it will not run any extra query, since we had searched it earlier");

            var samuraiWhere = context.Samurais.Where(s => s.Id == 1).FirstOrDefault();
            Console.WriteLine("Check the logs, this will run one more query");
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Jack" };
            var samurai2 = new Samurai { Name = "Black Jack" };
            var samurai3 = new Samurai { Name = "Ronian" };
            var samurai4 = new Samurai { Name = "Crack" };
            // If the number of samurais are less than 4, then there will be separate command sent to sql server
            // otherwise single command will be executed for inserting
            context.Samurais.AddRange(samurai, samurai2,samurai3, samurai4);

            //context.Samurais.Where(s=> EF.Functions.Like())

            context.SaveChanges();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Julian" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}
