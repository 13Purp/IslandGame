using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
namespace IslandGame
{
    public partial class Form1 : Form
    {

   

        private GameLogic _gameLogic;



        public Form1()
        {

            InitializeComponent();
           
            _gameLogic =new GameLogic(pictureBox1);

            Form1_Load(null, null);
            InitializeTimer();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
            int result = _gameLogic.HandleGuess(e);
           
          
            if(result==-2)
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
            _gameLogic = new GameLogic(pictureBox1);
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
    }
}
