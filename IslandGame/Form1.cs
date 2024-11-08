using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
namespace IslandGame
{
    public partial class Form1 : Form
    {

        private CellTable _cellTable;
        private bool _state = false;
        private int _sizeOfCell = 20;
        private int _padding = 3;
        private Island? _hashedIsland;
        private ArchipelAlgo _archipelago;
        private float _maxHeight;
        private Dictionary<(int, int), bool> _highlighted = new Dictionary<(int, int), bool>();
        private SolidBrush _brush = new SolidBrush(Color.FromArgb(0, 58, 158));
        private SolidBrush _brush2 = new SolidBrush(Color.FromArgb(10, 80, 10));
        public Form1()
        {

            InitializeComponent();
            _sizeOfCell = pictureBox1.Size.Width / 30;
            _padding = 1;


            _cellTable = new CellTable(pictureBox1.Size.Width, pictureBox1.Size.Height, _sizeOfCell);

            Form1_Load(null, null);
            InitializeTimer();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_state)
            {
                int x = e.X;
                int y = e.Y;

                Automata? cell=_cellTable.GetCell(x, y);
                if (cell != null && cell.Island!=null)
                {
                    if(cell.Island.GetAverageHeight()==_maxHeight)
                    {
                        label1.Text = "Brao";
                    }
                    else
                    {
                        label1.Text = "Jok";
                        Shake();
                    }

                }

                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            Automata[,] cells = _cellTable.GetTable();
            int highlightOffset;

            foreach (Automata ce in cells)
            {

                int x = ce.Xkord, y = ce.Ykord;

                if (_highlighted.ContainsKey((x, y)))
                    highlightOffset = 100;
                else
                    highlightOffset = 0;

                int gradientPart = ce.State / 250;
                if (ce.State == 0)
                    g.FillRectangle(_brush, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                else if (ce.State < 0)
                {
                    _brush2.Color = Color.FromArgb(255, 0, 0);
                    g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                }
                else
                {
                    switch (gradientPart)
                    {
                        case 0:
                            {
                                _brush2.Color = Color.FromArgb(10 + (ce.State / 2 + 1)+highlightOffset, 120, 10);
                                g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                                break;
                            };
                        case 1:
                            {
                                _brush2.Color = (ce.State < 375) ? Color.FromArgb(130 + highlightOffset, 120 - (ce.State / 8 + 1), 10) : Color.FromArgb(130 - (ce.State / 8 + 1) + highlightOffset, 60 - (ce.State / 16 + 1), 10);
                                g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                                break;
                            };
                        case 2:
                            {
                                _brush2.Color = Color.FromArgb(40 + ce.State / 4 , 40 + ce.State / 4, 40 + ce.State / 4);
                                g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                                break;
                            };
                        case 3:
                            {
                                break;
                            };

                        default: break;



                            //_brush2.Color = Color.FromArgb(10, 120 / ce.State, 10);
                            //g.FillRectangle(_brush2, x + _padding, y + _padding, _sizeOfCell - _padding, _sizeOfCell - _padding);
                    }
                }
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int n, m;
            n = pictureBox1.Size.Width / _sizeOfCell;
            m = pictureBox1.Size.Height / _sizeOfCell;
            if (_state)
            {

                _cellTable.UpdateState();
            }
            pictureBox1.Invalidate(); // This will force the PictureBox to repaint
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

            pictureBox1.BackColor = Color.FromArgb(30, 30, 30);
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(pictureBox1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            _state = !_state;
            if (_state)
            {
                button1.Text = "Pauziraj";
            }
            else
            {
                button1.Text = "Kreni";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cellTable.Randomize();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _archipelago = new ArchipelAlgo(pictureBox1.Size.Width, pictureBox1.Size.Height, _sizeOfCell);
            _archipelago.generate();
            _cellTable = _archipelago.GetCellTable();
            _maxHeight=_archipelago.getMaxHeight();
            label1.Text = "hm?";

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Automata? cell=_cellTable.GetCell(x, y);

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
                _highlighted[(c.Xkord, c.Ykord)] = true;
            }



            pictureBox1.Invalidate();
        }

        private void Shake()
        {
            var original = pictureBox1.Location;
            var rnd = new Random(1337);
            const int amplitute = 10;
            for (int i = 0; i < 10; i++)
            {
                pictureBox1.Location = new Point(original.X + rnd.Next(-amplitute, amplitute), original.Y + rnd.Next(-amplitute, amplitute));
                System.Threading.Thread.Sleep(15);
            }
            pictureBox1.Location = original;
        }
    }
}
