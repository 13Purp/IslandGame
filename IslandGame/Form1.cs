using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
namespace IslandGame
{
    public partial class Form1 : Form
    {



        private GameLogic _gameLogic;
        private int _diff;
        private int _layer1;
        private int _layer2;
        private int _layer3;



        public Form1()
        {

            InitializeComponent();
            _diff = 0;
            _layer1 = 0;
            _layer2 = 0;
            _layer3 = 0;

            diffBox.Items.AddRange(new object[] { "Easy", "Medium", "Hard", "Very Hard", "Impossible" });
            layer1.Items.AddRange(new object[] { "Conway", "Brian", "Seeds", "DayNight", "Maze" });
            layer2.Items.AddRange(new object[] { "Conway", "Brian", "Seeds", "DayNight", "Maze" });
            layer3.Items.AddRange(new object[] { "Conway", "Brian", "Seeds", "DayNight", "Maze" });

            diffBox.SelectedIndex = 0;
            layer1.SelectedIndex = 0;
            layer2.SelectedIndex = 0;
            layer3.SelectedIndex = 0;


            _gameLogic = new GameLogic(pictureBox1);
            colorMap.Invalidate();


            Form1_Load(null, null);
            InitializeTimer();
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            int result = _gameLogic.HandleGuess(e);


            if (result == -2)
            {

                button4.BackColor = Color.FromArgb(120, 81, 169);
            }

            label1.Text = "Remaining Lives: " + _gameLogic.GetLives();
            Score.Text = "Score: " + _gameLogic.GetScore();
            hScore.Text = "High Score: " + _gameLogic.GetHighScore();


            pictureBox1.Invalidate();
        }


        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _gameLogic.handleRefresh(e);


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

            pictureBox1.BackColor = Color.FromArgb(30, 30, 30);
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(pictureBox1);
        }




        private void button3_ClickAsync(object sender, EventArgs e)
        {

            button4.BackColor = Color.FromArgb(120, 81, 169);
            _layer1 = layer1.SelectedIndex;
            _layer2 = layer2.SelectedIndex;
            _layer3 = layer3.SelectedIndex;



            _gameLogic = new GameLogic(pictureBox1, _diff, _layer1, _layer2, _layer3);
            _gameLogic.Generate();
            label1.Text = "Remaining Lives: " + _gameLogic.GetLives();
            Score.Text = "Score: " + _gameLogic.GetScore();
            hScore.Text = "High Score: " + _gameLogic.GetHighScore();

        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            _gameLogic.HandleHover(e);
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

        private void button4_Click(object sender, EventArgs e)
        {


            _gameLogic.HandleHint();

            button4.BackColor = Color.DarkGray;


            pictureBox1.Invalidate();
        }

        private void layer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _layer1 = layer1.SelectedIndex;
        }

        private void layer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _layer2 = layer2.SelectedIndex;

        }

        private void layer3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _layer3 = layer3.SelectedIndex;

        }

        private void diffBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _diff = diffBox.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.FromArgb(120, 81, 169);
            _layer1 = layer1.SelectedIndex;
            _layer2 = layer2.SelectedIndex;
            _layer3 = layer3.SelectedIndex;



            _gameLogic = new GameLogic(pictureBox1, 0);
            _gameLogic.GenerateHttp();
            label1.Text = "Remaining Lives: " + _gameLogic.GetLives();
            Score.Text = "Score: " + _gameLogic.GetScore();
            hScore.Text = "High Score: " + _gameLogic.GetHighScore();
        }

        private void colorMap_Paint(object sender, PaintEventArgs e)
        {
            _gameLogic.drawHeightMap(colorMap, e);
        }
    }
}
