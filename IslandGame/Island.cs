using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    internal class Island
    {
        private List<Automata> _cells;
        private int _combinedHeight;
        private int _count;

        public Island()
        {
            _cells = new List<Automata>();
            _count = 0;
            _combinedHeight = 0;
        }

        public void add(Automata cell)
        {
            _cells.Add(cell);
            cell.Island = this;
            _count++;
            //cell.State = -1;
        }

        public int GetCombinedHeight() => _combinedHeight;
        public float GetAverageHeight()
        {
            return (float)_combinedHeight / (float)_count;
        }
        public int GetCount() => _count;

        public List<Automata> GetCells() => _cells;

        public bool isInIsland(Automata a)
        {
            foreach (Automata b in _cells)
            {
                if(b==a) return true;
            }
            return false;
        }
        public void CalcHeight()
        {
            foreach (Automata b in _cells)
            {
                _combinedHeight += b.State;
            }
          
        }

        public bool CorrrectWater(Automata a)
        {
            bool isSouth = false;
            bool isEast = false;
            bool isWest = false;
            bool isNorth = false;

            foreach (Automata b in _cells)
            {
                if(b.Xkord<a.Xkord && b.Ykord==a.Ykord)
                    isWest = true;
                if (b.Xkord > a.Xkord && b.Ykord == a.Ykord)
                    isEast = true;
                if(b.Ykord < a.Ykord && b.Xkord == a.Xkord)
                    isSouth = true;
                if(b.Ykord > a.Ykord && b.Xkord == a.Xkord)
                    isNorth = true;

                if (isSouth && isNorth && isEast && isWest)
                {
                    add(a);
                    a.State = 1;
                    a.Island = this;
                    return true;
                }
            }

            return false;

        
        }

        


    }
}
