using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    internal abstract class Automata
    {
        private int _xkord;
        private int _ykord;
        private Island? _island;
        protected int _neighbours;
        protected int _state;
       
        public Automata(int x, int y)
        {
            this._xkord = x;
            this._ykord = y;
            _island = null;
            
        }
    
        public int Xkord { get => _xkord;  set => _xkord = value; }
        public int Ykord { get => _ykord;  set => _ykord = value; }
        public Island Island { get => _island;  set => _island = value; }

        public int State { get => _state; set => _state = value; }
        public int Neighbours { get => _neighbours; }
        public abstract void ReserveState(Automata[,] cells,int i,int j);
        public abstract void ConfirmState();

        public abstract void FlipState();

    }
}
