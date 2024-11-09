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
            diffBox = new ComboBox();
            label3 = new System.Windows.Forms.Label();
            layer1 = new ComboBox();
            layer2 = new ComboBox();
            layer3 = new ComboBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox1 = new GroupBox();
            button1 = new Button();
            colorMap = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)colorMap).BeginInit();
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
            button3.ForeColor = Color.FromArgb(224, 224, 224);
            button3.Location = new Point(13, 609);
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
            label1.ForeColor = Color.FromArgb(120, 81, 169);
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
            Score.ForeColor = Color.FromArgb(120, 81, 169);
            Score.Location = new Point(1341, 974);
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
            hScore.ForeColor = Color.FromArgb(120, 81, 169);
            hScore.Location = new Point(1484, 22);
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
            button4.ForeColor = Color.FromArgb(224, 224, 224);
            button4.Location = new Point(1341, 845);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(522, 109);
            button4.TabIndex = 7;
            button4.Text = "Hint";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // diffBox
            // 
            diffBox.BackColor = Color.FromArgb(18, 18, 18);
            diffBox.FlatStyle = FlatStyle.Flat;
            diffBox.ForeColor = SystemColors.ScrollBar;
            diffBox.FormattingEnabled = true;
            diffBox.Location = new Point(13, 571);
            diffBox.Name = "diffBox";
            diffBox.Size = new Size(141, 23);
            diffBox.TabIndex = 8;
            diffBox.SelectedIndexChanged += diffBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Gray;
            label3.Location = new Point(15, 553);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 13;
            label3.Text = "Difficulty ";
            // 
            // layer1
            // 
            layer1.BackColor = Color.FromArgb(18, 18, 18);
            layer1.FlatStyle = FlatStyle.Flat;
            layer1.ForeColor = Color.Gray;
            layer1.FormattingEnabled = true;
            layer1.Location = new Point(6, 40);
            layer1.Name = "layer1";
            layer1.Size = new Size(141, 23);
            layer1.TabIndex = 9;
            layer1.SelectedIndexChanged += layer1_SelectedIndexChanged;
            // 
            // layer2
            // 
            layer2.BackColor = Color.FromArgb(18, 18, 18);
            layer2.FlatStyle = FlatStyle.Flat;
            layer2.ForeColor = Color.Gray;
            layer2.FormattingEnabled = true;
            layer2.Location = new Point(7, 84);
            layer2.Name = "layer2";
            layer2.Size = new Size(141, 23);
            layer2.TabIndex = 10;
            layer2.SelectedIndexChanged += layer2_SelectedIndexChanged;
            // 
            // layer3
            // 
            layer3.BackColor = Color.FromArgb(18, 18, 18);
            layer3.FlatStyle = FlatStyle.Flat;
            layer3.ForeColor = Color.Gray;
            layer3.FormattingEnabled = true;
            layer3.Location = new Point(7, 128);
            layer3.Name = "layer3";
            layer3.Size = new Size(141, 23);
            layer3.TabIndex = 11;
            layer3.SelectedIndexChanged += layer3_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Gray;
            label4.Location = new Point(7, 19);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 14;
            label4.Text = "First layer:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(7, 66);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 15;
            label5.Text = "Second layer:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Gray;
            label6.Location = new Point(7, 110);
            label6.Name = "label6";
            label6.Size = new Size(69, 15);
            label6.TabIndex = 16;
            label6.Text = "Third layer:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(30, 30, 30);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(layer3);
            groupBox1.Controls.Add(layer2);
            groupBox1.Controls.Add(layer1);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.ForeColor = Color.Gray;
            groupBox1.Location = new Point(6, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(157, 177);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Map generation settings";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(120, 81, 169);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(224, 224, 224);
            button1.Location = new Point(11, 974);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(142, 38);
            button1.TabIndex = 18;
            button1.Text = "Start From Server";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // colorMap
            // 
            colorMap.Location = new Point(1189, 22);
            colorMap.Name = "colorMap";
            colorMap.Size = new Size(35, 990);
            colorMap.TabIndex = 19;
            colorMap.TabStop = false;
            colorMap.Paint += colorMap_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(1904, 1041);
            Controls.Add(colorMap);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(label3);
            Controls.Add(diffBox);
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
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)colorMap).EndInit();
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
        private ComboBox diffBox;
        private System.Windows.Forms.Label label3;
        private ComboBox layer1;
        private ComboBox layer2;
        private ComboBox layer3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private GroupBox groupBox1;
        private Button button1;
        private PictureBox colorMap;
    }

}
