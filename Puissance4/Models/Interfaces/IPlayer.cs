using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puissance4.Models.Interfaces
{
    interface IPlayer
    {
        string Name { get; set; }
        string Color { get; set; }
        int Id { get; set; }
    }
}
