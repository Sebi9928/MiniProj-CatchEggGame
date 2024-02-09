using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eggs
{
    public partial class Form1 : Form


    {
        bool goLeft, goRight;
        int speed = 8;
        int score = 0;
        int missed = 0;

        Random randX = new Random();
        Random randY = new Random();
        PictureBox splash = new PictureBox();


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void Player_Click(object sender, EventArgs e)
        {

        }

        private void Main(object sender, EventArgs e)
        {
            txtScore.Text = "Scor: " + score;
            txtMiss.Text = "Pierdute: " + missed;

            if(goLeft==true && Player.Left>0)
            {
                Player.Left -= 12;
                Player.Image = Properties.Resources.chicken_normal2;

            }
            if(goRight==true&& Player.Left+Player.Width<this.ClientSize.Width)
            {
                Player.Left += 12;
                Player.Image = Properties.Resources.chicken_normal;
            }
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag=="eggs")
                {
                    x.Top += speed;

                    if(x.Top+x.Height>this.ClientSize.Height)
                    {
                        splash.Image = Properties.Resources.splash;
                        splash.Location = x.Location;
                        splash.Height = 60;
                        splash.Width = 60;
                        splash.BackColor = Color.Transparent;

                        this.Controls.Add(splash);



                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        missed += 1;
                        Player.Image = Properties.Resources.chicken_hurt;

                    }
                    if(Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        score += 1;
                    }
                }
            }
            if(score>10)
            {
                speed = 10;
            }
            if(missed>5)
            {
                GameTimer.Stop();
                MessageBox.Show("Jocul a luat sfarsit! " + Environment.NewLine + " Apasa OK pentru a incerca iar");
                RestartGame();
            }
               
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if(e.KeyCode==Keys.Right)
            {
                goRight = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }



        private void RestartGame()
        {
            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag=="eggs")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                }
            }
            Player.Left = this.ClientSize.Width / 2;
            Player.Image = Properties.Resources.chicken_normal;

            score = 0;
            missed = 0;
            speed = 8;

            goLeft = false;
            goRight = false;

            GameTimer.Start();
        }
    }
}
