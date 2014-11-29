﻿using System;
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
        int RollCounter = 0; //Cycles 0-5 and will only slow ball down on 0.

        int _physWidth = 16;
        int _physHeight = 25;

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

            //Bounce ball if applicable
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
                Ball.Left = 0;
            }

            //Ball at right
            if (Ball.Left > (this.Width - (Ball.Width + _physWidth)))
            {
                BallXMomentum += this.Location.X - newLocationX;
                Ball.Left = this.Width - (Ball.Width + _physWidth);
            }

            //Ball at bottom.
            if (Ball.Top > (this.Height - (int)(Ball.Height * 1.78)))
            {

                BallYMomentum += this.Location.Y - newLocationY;
                BallXMomentum += (this.Location.X - newLocationX) / 20;
                Ball.Top =(this.Height - (int)(Ball.Height * 1.78));
            }
  
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

            //Reverse momentum when ball hits the ground.
            if (!(Ball.Top < this.Height - (int)(Ball.Height * 1.78)))
            {
                BallYMomentum = (int)(BallYMomentum / 1.5);
                BallYMomentum = -BallYMomentum;
                 SlowBallX();
            }

            if (!(Ball.Top < this.Height - (int)(Ball.Height * 1.78)))
            {
                Ball.Top = this.Height - (int)(Ball.Height * 1.78);
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
