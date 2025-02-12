using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parchis
{
    internal class Player
    {
        public int PlayerID { get; set; }
        public string PlayerColor { get; set; }
        public Player(int id, string color) { 
            this.PlayerID = id;
            this.PlayerColor = color;
        }
        
        
    }
}
