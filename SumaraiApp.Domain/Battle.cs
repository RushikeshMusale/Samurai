using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SumaraiApp.Domain
{
    public class Battle
    {
        public Battle()
        {
            SamuraiBattles = new List<SamuraiBattle>();
        }
        public int BattleId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<SamuraiBattle> SamuraiBattles { get; set; }

    }
}
