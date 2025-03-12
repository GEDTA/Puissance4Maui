using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puissance4.Models.Interfaces;

namespace Puissance4.Models
{
    public class Player : IPlayer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Color { get; set; }
        public Player(int id)
        {
            Name = "Joueur " + id.ToString();
            Id = id;
            Color = id == 1 ? "red" : "yellow";
        }
       

    }
}
