using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBall
{
    public partial class GameForm : Form
    {
        int newLocationX = 0;
        int newLocationY = 0;

        int BallXMomentum = 0;
        int BallYMomentum = 0;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Move(object sender, EventArgs e)
        {
            if (newLocationX != this.Location.X)
                Ball.Left -= this.Location.X - newLocationX;

            if (newLocationY != this.Location.Y)
                Ball.Top -= this.Location.Y - newLocationY;

            if (Ball.Top < 0)
                Ball.Top = 0;

            if (Ball.Left < 0)
            {
                //BallXMomentum = 0 - 
                Ball.Left = 0;
            }

            if (Ball.Left > (this.Width - (int)(Ball.Width * 1.32)))
                Ball.Left = this.Width - (int)(Ball.Width * 1.32);
            
            if (Ball.Top > (this.Height - (int)(Ball.Height * 1.78)))
            {

                BallYMomentum += this.Location.Y - newLocationY;
                Ball.Top =(this.Height - (int)(Ball.Height * 1.78));
            }
  
            newLocationX = this.Location.X;
            newLocationY = this.Location.Y;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BallYMomentum++;
            Ball.Top += BallYMomentum;

            if (!(Ball.Top < this.Height - (int)(Ball.Height * 1.78)))
            {
                    BallYMomentum = (int)(BallYMomentum / 1.5);
                    BallYMomentum = -BallYMomentum;
            }

            if (!(Ball.Top < this.Height - (int)(Ball.Height * 1.78)))
            {
                Ball.Top = this.Height - (int)(Ball.Height * 1.78);
            }
           
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            newLocationX = this.Location.X;
            newLocationY = this.Location.Y;
        }

        private void GameForm_Move()
        {

        }
    }
}
