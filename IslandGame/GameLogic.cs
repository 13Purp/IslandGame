using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandGame
{
    internal class GameLogic
    {
        private CellTable _cellTable;
        private int _sizeOfCell = 20;
        private int _padding = 3;
        private Island? _hashedIsland;
        private ArchipelAlgo _archipelago;
        private bool _ready;
        private float _maxHeight;
        private int _score;
        private int _highScore;
        private int _lives;
        private bool _goodGuess;
        private int _diff;
        private bool _usedHint;
        private int _width;
        private int _height;
        private int _typeL1;
        private int _typeL2;
        private int _typeL3;
        private CellTable _heightMap;
        private static readonly HttpClient _client = new HttpClient();

        private PictureBox _pictureBox;
        private Dictionary<(int, int), bool> _highlighted = new Dictionary<(int, int), bool>();
        private SolidBrush _brush = new SolidBrush(Color.FromArgb(0, 58, 158));
        private SolidBrush _brush2 = new SolidBrush(Color.FromArgb(10, 80, 10));
        public GameLogic(PictureBox pictureBox, int diff=0 ,int typeL1 = 0, int typeL2 = 0, int typeL3 = 0)
        {

            _width = pictureBox.Size.Width;
            _height = pictureBox.Size.Height;
            _diff = diff;
            _typeL1 = typeL1;
            _typeL2 = typeL2;
            _typeL3 = typeL3;
            _sizeOfCell = _width /( 30+ diff*30) ;
            _padding = 1;
            _lives = 3;
            _ready = false;
            _score = 0;
            _highScore = 0;
            _goodGuess = false;
            _usedHint = false;
            _pictureBox = pictureBox;
            _archipelago = new ArchipelAlgo(_width, _height, _sizeOfCell, typeL1, typeL2, typeL3, diff+1);


            _cellTable = new CellTable(_width, _height, _sizeOfCell);
        }
        public void drawHeightMap(PictureBox heightMap, System.Windows.Forms.PaintEventArgs e)
        {
            int height = heightMap.Height;
            int width=heightMap.Width;
            int hIndex = 0;
            int sizeOfCell=width/2;
            _heightMap = new CellTable(width, height, sizeOfCell);
            Automata[,] cells=_heightMap.GetTable();

           int i = 0;
            int heightStep = 1000 / (height / sizeOfCell);
           
            for(int  j = height/ sizeOfCell - 1; j >=0; j--)
            {
                cells[i, j].State=hIndex;
                drawCells(e, cells[i, j], 0, 0, sizeOfCell);
                if (hIndex <= 1000)
                    hIndex += heightStep;
                else
                    break;
            }
            
        }

        private void drawCells(System.Windows.Forms.PaintEventArgs e, Automata ce, int selectedHighlightOffset, int winHighlightOffset, int sizeOfCell)
        {
            Graphics g = e.Graphics;
            int x = ce.Xkord;
            int y = ce.Ykord;
            int gradientPart = ce.State / 250;
            if (ce.State == 0)
                g.FillRectangle(_brush, x + _padding, y + _padding, sizeOfCell - _padding, sizeOfCell - _padding);
            else if (ce.State < 0)
            {
                _brush2.Color = Color.FromArgb(255, 0, 0);
                g.FillRectangle(_brush2, x + _padding, y + _padding, sizeOfCell - _padding, sizeOfCell - _padding);
            }
            else
            {
                _brush2.Color = GetColorForHeight(ce.State);
                
                int R = _brush2.Color.R;
                int G = _brush2.Color.G;
                int B = _brush2.Color.B;
                R = (R + selectedHighlightOffset <= 255) ? R + selectedHighlightOffset : 255;
                G = (G + winHighlightOffset <= 255) ? G + winHighlightOffset : 255;
                _brush2.Color = Color.FromArgb(R, G, B);
                g.FillRectangle(_brush2, x + _padding, y + _padding, sizeOfCell - _padding, sizeOfCell - _padding);

            }
        }

        public Color GetColorForHeight(int height)
        {


             if (height <= 250)
            {
                return InterpolateColor(Color.FromArgb(34, 139, 34), Color.FromArgb(85, 107, 47), height / 250.0);
            }
            else if (height <= 500)
            {
                return InterpolateColor(Color.FromArgb(85, 107, 47), Color.FromArgb(210, 180, 140), (height - 250) / 250.0);
            }
            else if (height <= 750)
            {
                Color darkBrown = Color.FromArgb(101, 67, 33);
                return InterpolateColor(Color.FromArgb(210, 180, 140), darkBrown, (height - 500) / 250.0);
            }
            else
            {
                Color darkBrown = Color.FromArgb(101, 67, 33);
                Color darkGray = Color.FromArgb(105, 105, 105);
                if (height <= 875)
                {
                    return InterpolateColor(darkBrown, darkGray, (height - 750) / 125.0);
                }
                else
                {
                    return InterpolateColor(darkGray, Color.White, (height - 875) / 125.0);
                }
            }

        }

        private Color InterpolateColor(Color start, Color end, double fraction)
        {
            int r = (int)(start.R + (end.R - start.R) * fraction);
            int g = (int)(start.G + (end.G - start.G) * fraction);
            int b = (int)(start.B + (end.B - start.B) * fraction);

            return Color.FromArgb(r, g, b);
        }



        public int HandleGuess( MouseEventArgs e)
        {
            if (!_ready)
                return -1;

            int x = e.X;
            int y = e.Y;
            Automata? cell = _cellTable.GetCell(x, y);

            if (cell == null || cell.Island == null)
                return -1;

            if (cell.Island.GetAverageHeight() == _maxHeight)
            {

                List<Automata> cells = cell.Island.GetCells();
                foreach (Automata c in cells)
                {
                    _highlighted[(c.Xkord, c.Ykord)] = true;
                }
                _score++;
                _goodGuess = true;
                if (_score >= _highScore)
                    _highScore = _score;

                _pictureBox.Invalidate();
                DelayGenerate();

                return 1;

            }
            else
            {
                _lives--;
                if (_lives == 0)
                {
                    _lives = 3;
                    _score = 0;
                    _usedHint = false;
                    DelayGenerate();
                    Shake();
                    Shake();
                    return -2;
                }
                Shake();
                return 0;
            }

           
           
        }

        public int GetLives() => _lives;
        public int GetScore() => _score;
        public int GetHighScore() => _highScore;
        public bool UsedHint() => _usedHint;

        private void Shake()
        {
            var original = _pictureBox.Location;
            var rnd = new Random(1337);
            const int amplitute = 10;
            for (int i = 0; i < 10; i++)
            {
                _pictureBox.Location = new Point(original.X + rnd.Next(-amplitute, amplitute), original.Y + rnd.Next(-amplitute, amplitute));
                System.Threading.Thread.Sleep(15);
            }
            _pictureBox.Location = original;
        }


        public void handleRefresh( System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Automata[,] cells = _cellTable.GetTable();
            int selectedHighlightOffset;
            int winHighlightOffset;

            foreach (Automata ce in cells)
            {

                int x = ce.Xkord, y = ce.Ykord;
                bool typeOfHighlight;

                if (_highlighted.TryGetValue((x, y), out typeOfHighlight))
                {
                    if (!typeOfHighlight)
                    {
                        selectedHighlightOffset = 100;
                        winHighlightOffset = 0;
                    }
                    else
                    {
                        selectedHighlightOffset = 0;
                        winHighlightOffset = 100;
                    }
                }
                else
                {
                    selectedHighlightOffset = 0;
                    winHighlightOffset = 0;
                }

                //int gradientPart = ce.State / 250;
                //if (ce.State == 0)
                //    g.FillRectangle(_brush, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                //else if (ce.State < 0)
                //{
                //    _brush2.Color = Color.FromArgb(255, 0, 0);
                //    g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                //}
                //else
                //{
                //    int R, G, B;
                //    switch (gradientPart)
                //    {

                //        case 0:
                //            {
                //                R = 10 + (ce.State / 2 + 1) + selectedHighlightOffset;
                //                G = 120;
                //                B = 10;
                //                _brush2.Color = Color.FromArgb(R, G, B);
                //                g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                //                break;
                //            };
                //        case 1:
                //            {
                //                if ((ce.State < 375))
                //                {
                //                    R = (130 + selectedHighlightOffset<=255) ? 130 + selectedHighlightOffset :255;
                //                    G = 120 - (ce.State / 8 + 1) + winHighlightOffset;
                //                    B = 10;
                //                }
                //                else
                //                {
                //                    R = (130 - (ce.State / 8 + 1) + selectedHighlightOffset<=255) ? 130 - (ce.State / 8 + 1) + selectedHighlightOffset:255;
                //                    G = 60 - (ce.State / 16 + 1) + winHighlightOffset;
                //                    B = 10;
                //                }
                //                _brush2.Color = Color.FromArgb(R, G, B);
                //                g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                //                break;
                //            };
                //        case 2:
                //            {
                //                R = (40 + ce.State / 6 + selectedHighlightOffset > 255) ? 255 : (40 + ce.State / 6 + selectedHighlightOffset);
                //                G = (40 + ce.State / 6 + winHighlightOffset > 255) ? 255 : (40 + ce.State / 6 + winHighlightOffset);
                //                B = 40 + ce.State / 6;
                //                _brush2.Color = Color.FromArgb(R, G, B);
                //                g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                //                break;
                //            };

                //        default: break;


                //    }
                //}

                drawCells(e, ce, selectedHighlightOffset, winHighlightOffset,_sizeOfCell);
            }

        }


        public async void Generate()
        {
            _highlighted = new Dictionary<(int, int), bool>();
            _cellTable = new CellTable(_width, _height, _sizeOfCell);
            _archipelago = new ArchipelAlgo(_width, _height, _sizeOfCell,_typeL1,_typeL2,_typeL3,_diff+1);
            _ready = false;
            _goodGuess = false;
            await Task.Run(() => _archipelago.generate(_cellTable));
            _ready = true;
            _maxHeight = _archipelago.getMaxHeight();
        }

        public async void GenerateHttp()
        {
            try
            {
                _highlighted = new Dictionary<(int, int), bool>();

                string response = await _client.GetStringAsync("https://jobfair.nordeus.com/jf24-fullstack-challenge/test");
                _cellTable = new CellTable(_width, _height, _width / 30);
                _archipelago = new ArchipelAlgo(_width, _height, _width / 30, _typeL1, _typeL2, _typeL3, 1);
                _ready = false;
                _goodGuess = false;
                await Task.Run(() => _archipelago.generateHTTP(_cellTable, response));
                _ready = true;
                _maxHeight = _archipelago.getMaxHeight();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }
        }

        public void HandleHover( MouseEventArgs e)
        {

            if (_goodGuess)
                return;
            if(!_ready) 
                return;
            int x = e.X;
            int y = e.Y;
            Automata? cell = _cellTable.GetCell(x, y);

            if (cell == null)
            {
                _highlighted = new Dictionary<(int, int), bool>();
                _hashedIsland = null;
                return;
            }

            if (cell.Island == null)
            {
                _highlighted = new Dictionary<(int, int), bool>();
                _hashedIsland = null;
                return;
            }

            if (_hashedIsland == cell.Island)
            {
                return;
            }

            _hashedIsland = cell.Island;
            List<Automata> cells = cell.Island.GetCells();
            _highlighted = new Dictionary<(int, int), bool>();

            foreach (Automata c in cells)
            {
                _highlighted[(c.Xkord, c.Ykord)] = false;
            }



        }
        public async void DelayGenerate()
        {
            await Task.Delay(400);
            Generate();
        }

    

        public void HandleHint()
        {
            if (_usedHint)
                return;

            if (!_ready)
                return;

            _usedHint = true;
            Island island= _archipelago.getMaxIsland();
            if (island == null)
                return;

            List<Automata> cells = island.GetCells();
            foreach (Automata c in cells)
            {
                _highlighted[(c.Xkord, c.Ykord)] = true;

            }

            island = _archipelago.getSecondMaxIsland();
            if (island == null)
                return;

            cells = island.GetCells();
            foreach (Automata c in cells)
            {
                _highlighted[(c.Xkord, c.Ykord)] = true;

            }

        }
    }
}
