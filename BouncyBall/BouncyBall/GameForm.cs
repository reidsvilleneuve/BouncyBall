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
        //TODO: Set these variables in Ball class rather than in-form.
        int newLocationX = 0;
        int newLocationY = 0;

        int BallXMomentum = 0;
        int BallYMomentum = 0;
        int RollCounter = 0; //Cycles 0-30 and will only slow ball down on 0.

        //These are needed for border hit detection.
        const int _physWidth = 16; 
        const int _physHeight = 39; //TODO: Research why these aren't the same (They should just be an even 50).

        List<PictureBox> ObstacleList = new List<PictureBox>() { /*Put your stuff here */};
        List<PictureBox> BallList = new List<PictureBox>();

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Move(object sender, EventArgs e)
        {
            //Detect form movement and compensate.
            if (newLocationX != this.Location.X)
                Ball.Left -= this.Location.X - newLocationX;

            if (newLocationY != this.Location.Y)
                Ball.Top -= this.Location.Y - newLocationY;

            BoarderMovementPhysics(); //Bounce ball off boarder if applicable

            newLocationX = this.Location.X;
            newLocationY = this.Location.Y;
        }

        private void SlowBallX()
        {
            RollCounter++;

            if (RollCounter > 30)
                RollCounter = 0;

            if (RollCounter == 0)
            {
                if (BallXMomentum > 0)
                    BallXMomentum--;
                else if (BallXMomentum < 0)
                    BallXMomentum++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BallYMomentum++; //"Gravity." A greater momentum drags the ball downward.

            Ball.Top += BallYMomentum;
            Ball.Left += BallXMomentum;

            BoarderIdlePhysics();
            foreach (PictureBox i in ObstacleList)
            {
                ObstacleIdlePhysics(i);
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            newLocationX = this.Location.X;
            newLocationY = this.Location.Y;

            ObstacleList.Add(obstacle1); //Add all obstacle objects.
            BallList.Add(Ball); //Add all ball objects.
        }

        private void BoarderIdlePhysics()
        {
            //Reverse momentum when ball hits the ground.
            if (!(Ball.Top < this.Height - (Ball.Height + _physHeight)))
            {
                BallYMomentum = (int)(BallYMomentum / 1.5);
                BallYMomentum = -BallYMomentum;
                SlowBallX();
            }

            if (!(Ball.Top < this.Height - (Ball.Height + _physHeight)))
            {
                Ball.Top = this.Height - (Ball.Height + _physHeight);
            }

            //Reverse momentum when ball hits the ceiling.
            if (!(Ball.Top > 0))
            {
                BallYMomentum = (int)(BallYMomentum / 1.5);
                BallYMomentum = -BallYMomentum;
                SlowBallX();
            }

            if (!(Ball.Top > 0))
            {
                Ball.Top = 0;
            }

            //Reverse momentum when ball hits the left wall.
            if (!(Ball.Left > 0))
            {
                BallXMomentum = (int)(BallXMomentum / 1.5);
                BallXMomentum = -BallXMomentum;
            }

            if (!(Ball.Left > 0))
            {
                Ball.Left = 0;
            }

            //Reverse momentum when ball hits the right wall.
            if (!(Ball.Left < this.Width - (Ball.Width + _physWidth)))
            {
                BallXMomentum = (int)(BallXMomentum / 1.5);
                BallXMomentum = -BallXMomentum;
            }

            if (!(Ball.Left < this.Width - (Ball.Width + _physWidth)))
            {
                Ball.Left = this.Width - (Ball.Width + _physWidth);
            }

        }

        private void BoarderMovementPhysics()
        {
            //Ball at top
            if (Ball.Top < 0)
            {
                BallYMomentum += this.Location.Y - newLocationY; //Greater momentum on greater movement.
                Ball.Top = 0;
            }

            //Ball at left
            if (Ball.Left < 0)
            {
                BallXMomentum += this.Location.X - newLocationX;
                Ball.Left = 0; //Prevents ball from gaining extra momentum for being in the walls for > 1 frame.
            }

            //Ball at right
            if (Ball.Left > (this.Width - (Ball.Width + _physWidth)))
            {
                BallXMomentum += this.Location.X - newLocationX;
                Ball.Left = this.Width - (Ball.Width + _physWidth);
            }

            //Ball at bottom.
            if (Ball.Top > (this.Height - (Ball.Height + _physHeight)))
            {

                BallYMomentum += this.Location.Y - newLocationY;
                BallXMomentum += (this.Location.X - newLocationX) / 20;
                Ball.Top = (this.Height - (Ball.Height + _physHeight));
            }
        }

        private void ObstacleIdlePhysics(PictureBox obstacle)
        {
            //Y - axis physics(Bigger obstacle)
            //if (Ball.Width < obstacle.Width)
            //{
                if (Ball.Left + Ball.Width > obstacle.Left && Ball.Left < (obstacle.Left + obstacle.Width))
                {
                    //Ball hits top
                    if (Ball.Top + Ball.Height > obstacle.Top)
                    {
                        BallYMomentum = (int)(BallYMomentum / 1.5);
                        BallYMomentum = -BallYMomentum;
                        SlowBallX();
                    }

                    if (Ball.Top + Ball.Height > obstacle.Top)
                    {
                        Ball.Top = obstacle.Top - (Ball.Height);
                    }
                //}
            }
        }

        private void ObstacleMovementPhysics(PictureBox obstacle)
        {

        }

        /// <summary>
        /// Reduces graphical flickering.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
