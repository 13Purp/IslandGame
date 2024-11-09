using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
namespace IslandGame
{
    partial class Form1 //partial znaci da moze da se podeli u vise fajlova
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null; //component je container za komponente tipa buttons,label i slicno; za sada je null ali moze drzati komponente kasnije

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) //cleanup kad vise nije potrebna ova forma
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        protected void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            button3 = new Button();
            label1 = new System.Windows.Forms.Label();
            Score = new System.Windows.Forms.Label();
            hScore = new System.Windows.Forms.Label();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(168, 22);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(990, 990);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(120, 81, 169);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Microsoft Sans Serif", 27F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(11, 430);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(142, 109);
            button3.TabIndex = 3;
            button3.Text = "Start";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_ClickAsync;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaption;
            label1.Location = new Point(1569, 974);
            label1.Name = "label1";
            label1.Size = new Size(294, 38);
            label1.TabIndex = 4;
            label1.Text = "Remaining Lives: 3";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Score
            // 
            Score.AutoSize = true;
            Score.Font = new Font("Microsoft Sans Serif", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Score.ForeColor = SystemColors.ActiveCaption;
            Score.Location = new Point(1249, 974);
            Score.Name = "Score";
            Score.Size = new Size(139, 38);
            Score.TabIndex = 5;
            Score.Text = "Score: 0";
            Score.TextAlign = ContentAlignment.TopCenter;
            // 
            // hScore
            // 
            hScore.AutoSize = true;
            hScore.Font = new Font("Microsoft Sans Serif", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hScore.ForeColor = SystemColors.ActiveCaption;
            hScore.Location = new Point(1436, 22);
            hScore.Name = "hScore";
            hScore.Size = new Size(215, 38);
            hScore.TabIndex = 6;
            hScore.Text = "High Score: 0";
            hScore.TextAlign = ContentAlignment.TopCenter;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(120, 81, 169);
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Microsoft Sans Serif", 27F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.Location = new Point(1249, 845);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(614, 109);
            button4.TabIndex = 7;
            button4.Text = "Hint";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(1904, 1041);
            Controls.Add(button4);
            Controls.Add(hScore);
            Controls.Add(Score);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(pictureBox1);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Automate";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 16;
            timer.Tick += Timer_Tick;
            timer.Start(); 
        }


        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer;

#endregion
        private Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.Label hScore;
        private Button button4;
    }

}
