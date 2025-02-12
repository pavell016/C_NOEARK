using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace parchis
{
    using System.Text.Json.Serialization;

    public class Player
    {
        [JsonPropertyName("playerID")]  // Match JSON property name
        public int PlayerID { get; set; }

        [JsonPropertyName("playerColor")]
        public string PlayerColor { get; set; }
    }
}
