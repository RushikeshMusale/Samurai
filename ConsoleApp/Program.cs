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

            InsertMultipleSamurais();

            //GetSamurais("Before Add:");
            //AddSamurai();
            //GetSamurais("After Add:");
            Console.Write("Press any key...");
            Console.ReadKey();
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
