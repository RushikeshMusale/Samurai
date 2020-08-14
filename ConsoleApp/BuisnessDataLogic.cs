using SamuraiApp.Data;
using SumaraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class BuisnessDataLogic
    {
        private readonly SamuraiDbContext _context;

        public BuisnessDataLogic()
        {
            _context = new SamuraiDbContext();
        }

        public BuisnessDataLogic(SamuraiDbContext context)
        {
            _context = context;
        }

        public int AddMultipleSamurais(string[] nameList)
        {
            foreach (var name in nameList)
            {
                var samurai = new Samurai { Name = name };
                _context.Samurais.Add(samurai);
            }

            return _context.SaveChanges();
        }

        public int InsertSingleSamurai(string name)
        {
            var samurai = new Samurai { Name = name };
            _context.Samurais.Add(samurai);
            return _context.SaveChanges();
        }
    }
}
