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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new System.Windows.Forms.Label();
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
            // button1
            // 
            button1.BackColor = Color.FromArgb(120, 81, 169);
            button1.FlatAppearance.BorderColor = Color.FromArgb(108, 85, 160);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Roboto", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(1286, 71);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(70, 35);
            button1.TabIndex = 1;
            button1.Text = "Kreni";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(120, 81, 169);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Roboto", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(1200, 548);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(115, 35);
            button2.TabIndex = 2;
            button2.Text = "Randomize";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(120, 81, 169);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Roboto", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(1354, 548);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(115, 35);
            button3.TabIndex = 3;
            button3.Text = "Archipelago";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaption;
            label1.Location = new Point(1286, 295);
            label1.Name = "label1";
            label1.Size = new Size(86, 39);
            label1.TabIndex = 4;
            label1.Text = "Hm?";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(1904, 1041);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
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
            timer.Interval = 160;
            timer.Tick += Timer_Tick;
            timer.Start(); 
        }


        private PictureBox pictureBox1;
        private Button button1;
        private System.Windows.Forms.Timer timer;

        #endregion

        private Button button2;
        private Button button3;
        private System.Windows.Forms.Label label1;
    }

}
