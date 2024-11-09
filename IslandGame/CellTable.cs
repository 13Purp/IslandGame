using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IslandGame
{
    internal class CellTable
    {

        private Automata[,] _cells;
        private int _height;
        private int _width;
        private int _sizeOfCell = 30;

        public CellTable(int x, int y, int sizeOfCell, int type=0) {

            _sizeOfCell = sizeOfCell;
            _width = x / _sizeOfCell;
            _height = y / _sizeOfCell;
            _cells =new Automata[_width, _height];

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {

                    switch (type)
                    {
                        case 0:
                        {
                            _cells[i, j] = new Conway(i * _sizeOfCell, j * _sizeOfCell);
                            break;
                        }
                        case 1:
                        {
                            _cells[i, j] = new Brian(i * _sizeOfCell, j * _sizeOfCell);
                            break;
                        }
                        case 2:
                        {
                            _cells[i, j] = new Seeds(i * _sizeOfCell, j * _sizeOfCell);
                            break;
                        }
                        case 3:
                        {
                                _cells[i, j] = new DayNight(i * _sizeOfCell, j * _sizeOfCell);
                                break;
                        }
                        case 4:
                        {
                                _cells[i, j] = new Maze(i * _sizeOfCell, j * _sizeOfCell);
                                break;
                        }
                        default:
                        {
                            _cells[i, j] = new Conway(i * _sizeOfCell, j * _sizeOfCell);
                            break;
                        }

                }
                    
                   
                }
            }
        }

        public void UpdateState()
        {

            //completely aware that there is no reason for this to run in parallel, just experimenting a bit 
            Parallel.For(0, _width, i =>
            {
                for (int j = 0; j < _height; j++)
                {
                    _cells[i, j].ReserveState(_cells, i, j);
                }
            });

            Parallel.ForEach(_cells.Cast<Automata>(), ce => ce.ConfirmState());

        }

        public void FlipStateOfCell(int x, int y)
        {
            x = (x - x % _sizeOfCell) / _sizeOfCell;
            y = (y - y % _sizeOfCell) / _sizeOfCell;
            if(x<_width &&  y<_height)
                _cells[x, y].FlipState();
        }

        public Automata? GetCell(int x, int y)
        {
            x = (x - x % _sizeOfCell) / _sizeOfCell;
            y = (y - y % _sizeOfCell) / _sizeOfCell;
            if (x < _width && y < _height)
                return _cells[x, y];
            else
                return null;
        }

        public Automata[,] GetTable() => _cells;

        public void SetTable(Automata[,] cells) => _cells = cells;

        public void Randomize()
        {
            Random random = new Random();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {


                    _cells[i, j].State= random.Next(0, 2);


                }
            }
        }

    }
}
