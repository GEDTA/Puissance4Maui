using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puissance4.Models;

namespace Puissance4.Models.Interfaces
{
    public interface IGameModel
    {
        
        int Rows { get; }
        int Columns { get; }
       
        bool PlaceToken(int column);
        void SwitchPlayer();
        bool CheckVictory(int lastRow, int lastCol);
    }
}
