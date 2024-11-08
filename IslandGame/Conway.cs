using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    internal class Conway : Automata
    {
        
        protected int _reserved;
        public Conway(int x, int y) : base(x, y)
        {
            _state = 0;
            _reserved = 0;
            _neighbours = 0;
        }
        public Conway(Automata other) : base(x: other.Xkord, y: other.Ykord)
        {
        
            this._neighbours = other.Neighbours;
            this._state = other.State;
        }


        public override void ReserveState(Automata[,] cells, int i, int j)
        {
            int neighbours=0;
            int m = cells.GetLength(1), n = cells.GetLength(0);
            if (i - 1 >= 0 && j - 1 >= 0 && cells[i - 1, j - 1].State == 1) 
                neighbours++;
            if (i - 1 >= 0 && cells[i - 1, j].State == 1) 
                neighbours++;
            if (i - 1 >= 0 && j + 1 < m && cells[i - 1, j + 1].State == 1) 
                neighbours++;
            if (j - 1 >= 0 && cells[i, j - 1].State == 1) 
                neighbours++;
            if (j + 1 < m && cells[i, j + 1].State == 1) 
                neighbours++;
            if (i + 1 < n && j - 1 >= 0 && cells[i + 1, j - 1].State == 1) 
                neighbours++;
            if (i + 1 < n && cells[i + 1, j].State == 1) 
                neighbours++;
            if (i + 1 < n && j + 1 < m && cells[i + 1, j + 1].State == 1) 
                neighbours++;

            _neighbours= neighbours;
            if (_state == 1 && (neighbours < 2 || neighbours > 3))
            {
                _reserved = 0;
            }
            else if (_state == 1 && (neighbours == 2 || neighbours == 3))
            {
                _reserved = 1;
            }
            else if (_state == 0 && neighbours == 3)
            {
                _reserved = 1;
            }
            else _reserved = _state;
        }
        
        public override void ConfirmState()
        {
            _state = _reserved; 
        }

        public override void FlipState()
        {
            if (_state == 1) { _state = 0; }
            else _state = 1;
        }
    }

    internal class Brian : Automata
    {

        protected int _reserved;
        public Brian(int x, int y) : base(x, y)
        {
            _state = 0;
            _reserved = 0;
        }

        public override void ReserveState(Automata[,] cells, int i, int j)
        {
            int neighbours=0;
            int m = cells.GetLength(1), n = cells.GetLength(0);
            if (i - 1 >= 0 && j - 1 >= 0 && cells[i - 1, j - 1].State == 1) 
                neighbours++;
            if (i - 1 >= 0 && cells[i - 1, j].State == 1) 
                neighbours++;
            if (i - 1 >= 0 && j + 1 < m && cells[i - 1, j + 1].State == 1) 
                neighbours++;
            if (j - 1 >= 0 && cells[i, j - 1].State == 1) 
                neighbours++;
            if (j + 1 < m && cells[i, j + 1].State == 1) 
                neighbours++;
            if (i + 1 < n && j - 1 >= 0 && cells[i + 1, j - 1].State == 1) 
                neighbours++;
            if (i + 1 < n && cells[i + 1, j].State == 1) 
                neighbours++;
            if (i + 1 < n && j + 1 < m && cells[i + 1, j + 1].State == 1) 
                neighbours++;
            if (_state == 0 && neighbours == 2)
            {
                _reserved = 1;
            }
            else if (_state == 1) _reserved = -1;
            else if (_state == -1) _reserved = 0;
            else _reserved = _state;
        }

        public override void ConfirmState()
        {
            _state = _reserved;
        }

        public override void FlipState()
        {
            if (_state == 1) { _state = -1; }
            else if(_state==-1) { _state = 0; }
            else _state = 1;
        }
    }

    internal class DayNight : Automata
    {

        protected int _reserved;
        public DayNight(int x, int y) : base(x, y)
        {
            _state = 0;
            _reserved = 0;
        }

        public override void ReserveState(Automata[,] cells, int i, int j)
        {
            int neighbours = 0;
            int m = cells.GetLength(1), n = cells.GetLength(0);
            if (i - 1 >= 0 && j - 1 >= 0 && cells[i - 1, j - 1].State == 1) neighbours++;
            if (i - 1 >= 0 && cells[i - 1, j].State == 1) neighbours++;
            if (i - 1 >= 0 && j + 1 < m && cells[i - 1, j + 1].State == 1) neighbours++;
            if (j - 1 >= 0 && cells[i, j - 1].State == 1) neighbours++;
            if (j + 1 < m && cells[i, j + 1].State == 1) neighbours++;
            if (i + 1 < n && j - 1 >= 0 && cells[i + 1, j - 1].State == 1) neighbours++;
            if (i + 1 < n && cells[i + 1, j].State == 1) neighbours++;
            if (i + 1 < n && j + 1 < m && cells[i + 1, j + 1].State == 1) neighbours++;
            if (_state == 0 && (neighbours == 3 || neighbours == 6 || neighbours == 7 || neighbours == 8))
            {
                _reserved = 1;
            }
            else if (_state == 1 && (neighbours == 1 || neighbours == 2 || neighbours == 5 || neighbours == 0))
            {
                _reserved = 0;
            }
            else _reserved = _state;
        }

        public override void ConfirmState()
        {
            _state = _reserved;
        }

        public override void FlipState()
        {
            if (_state == 1) { _state = 0; }
            else _state = 1;
        }
    }

    internal class Seeds : Automata
    {

        protected int _reserved;
        public Seeds(int x, int y) : base(x, y)
        {
            _state = 0;
            _reserved = 0;
        }

        public override void ReserveState(Automata[,] cells, int i, int j)
        {
            int neighbours = 0;
            int m = cells.GetLength(1), n = cells.GetLength(0);
            if (i - 1 >= 0 && j - 1 >= 0 && cells[i - 1, j - 1].State == 1) neighbours++;
            if (i - 1 >= 0 && cells[i - 1, j].State == 1) neighbours++;
            if (i - 1 >= 0 && j + 1 < m && cells[i - 1, j + 1].State == 1) neighbours++;
            if (j - 1 >= 0 && cells[i, j - 1].State == 1) neighbours++;
            if (j + 1 < m && cells[i, j + 1].State == 1) neighbours++;
            if (i + 1 < n && j - 1 >= 0 && cells[i + 1, j - 1].State == 1) neighbours++;
            if (i + 1 < n && cells[i + 1, j].State == 1) neighbours++;
            if (i + 1 < n && j + 1 < m && cells[i + 1, j + 1].State == 1) neighbours++;

            if (_state == 0 && neighbours == 2)
            {
                _reserved = 1;
            }
            else 
                _reserved = 0;
        }

        public override void ConfirmState()
        {
            _state = _reserved;
        }

        public override void FlipState()
        {
            if (_state == 1) { _state = 0; }
            else _state = 1;
        }
    }

    internal class Maze : Automata
    {

        protected int _reserved;
        public Maze(int x, int y) : base(x, y)
        {
            _state = 0;
            _reserved = 0;
        }

        public override void ReserveState(Automata[,] cells, int i, int j)
        {
            int neighbours = 0;
            int m = cells.GetLength(1), n = cells.GetLength(0);
            if (i - 1 >= 0 && j - 1 >= 0 && cells[i - 1, j - 1].State == 1) neighbours++;
            if (i - 1 >= 0 && cells[i - 1, j].State == 1) neighbours++;
            if (i - 1 >= 0 && j + 1 < m && cells[i - 1, j + 1].State == 1) neighbours++;
            if (j - 1 >= 0 && cells[i, j - 1].State == 1) neighbours++;
            if (j + 1 < m && cells[i, j + 1].State == 1) neighbours++;
            if (i + 1 < n && j - 1 >= 0 && cells[i + 1, j - 1].State == 1) neighbours++;
            if (i + 1 < n && cells[i + 1, j].State == 1) neighbours++;
            if (i + 1 < n && j + 1 < m && cells[i + 1, j + 1].State == 1) neighbours++;
            if (_state == 1 && (neighbours >= 1 && neighbours <= 4))
            {
                _reserved = 1;
            }
            else if (_state == 0 && neighbours == 3)
                _reserved = 1;
            else
                _reserved = 0;
        }

        public override void ConfirmState()
        {
            _state = _reserved;
        }

        public override void FlipState()
        {
            if (_state == 1) { _state = 0; }
            else _state = 1;
        }
    }
}
