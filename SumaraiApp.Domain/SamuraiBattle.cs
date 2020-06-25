using System;
using System.Collections.Generic;
using System.Text;

namespace SumaraiApp.Domain
{
    public class SamuraiBattle
    {
        // These two ids are required
        public int SamuraiId { get; set; }
        public int BattleId { get; set; }

        // these are optional
        public Samurai Samurai { get; set; }
        public Battle Battle { get; set; }
    }
}
