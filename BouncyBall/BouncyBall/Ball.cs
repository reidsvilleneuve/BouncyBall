using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncyBall
{
    class Ball
    {
        //Physical properties
        public int Height { get; set; }
        public int Width { get; set; }
        public PictureBox PictureObject { get; set; }
		public Form FormObject { get; set; }

        //Movement properties
        public int X { get; set; }
        public int Y { get; set; }
        public int XMomentum { get; set; }
        public int YMomentum { get; set; }
        
        //Internal values.
        private int _rollCounter;
 
		//These are needed for border hit detection.
		//TODO: Research why these aren't the same. (They should just be an even 50).
        const int _physWidth = 16; 
        const int _physHeight = 39;



        /// <summary>
        /// Creates a new instance of a Ball. This ball will inherit the the position and size of the PictureBox object.
        /// </summary>
        /// <param name="formObject">PictureBox that represents and is manipulated by this Ball object.</param>
        public Ball(PictureBox pictureObject)
        {
            this.PictureObject = pictureObject;

            this.Height = PictureObject.Height;
            this.Width = PictureObject.Width;
            this.X = PictureObject.Left;
            this.Y = PictureObject.Top;

            this.this.XMomentum = 0;
            this.this.YMomentum = 0;

            this._rollCounter = 0;
        }

		
        public void SlowBallX()
        {
            this._rollcounter++;

            if (this._rollcounter > 30)
                this._rollcounter = 0;

            if (this._rollcounter == 0)
            {
                if (this.XMomentum > 0)
                    this.XMomentum--;
                else if (this.XMomentum < 0)
                    this.XMomentum++;
            }
        }
		
		private void BoarderIdlePhysics()
        {
            //Reverse momentum when ball hits the ground.
            if (this.Y > this.Height - (this.Height + _physHeight))
            {
                this.YMomentum = (int)(this.YMomentum / 1.5);
                this.YMomentum = -this.YMomentum;
                this.SlowBallX();

                this.Y = this.Height - (this.Height + _physHeight);
            }

            //Reverse momentum when ball hits the ceiling.
            if (this.Y < 0)
            {
                this.YMomentum = (int)(this.YMomentum / 1.5);
                this.YMomentum = -this.YMomentum;
                SlowBallX();

                this.Y = 0;
            }

            //Reverse momentum when ball hits the left wall.
            if (this.X < 0)
            {
                this.XMomentum = (int)(this.XMomentum / 1.5);
                this.XMomentum = -this.XMomentum;

                this.X = 0;
            }

            //Reverse momentum when ball hits the right wall.
            if (this.X > this.Width - (this.Width + _physWidth))
            {
                this.XMomentum = (int)(this.XMomentum / 1.5);
                this.XMomentum = -this.XMomentum;

                this.X = this.Width - (this.Width + _physWidth);
            }
        }

        private void BoarderMovementPhysics()
        {
            //Ball at top
            if (this.Y < 0)
            {
                this.YMomentum += FormObject.Location.Y - FormObject.newLocationY; //Greater momentum on greater movement.
                this.Y = 0;
            }

            //Ball at left
            if (this.X < 0)
            {
                this.XMomentum += FormObject.Location.X - FormObject.newLocationX;
                this.X = 0; //Prevents ball from gaining extra momentum for being in the walls for > 1 frame.
            }

            //Ball at right
            if (this.X > (this.Width - (this.Width + _physWidth)))
            {
                this.XMomentum += FormObject.Location.X -FormObject.newLocationX;
                this.X = this.Width - (this.Width + _physWidth);
            }

            //Ball at bottom.
            if (this.Y > (this.Height - (this.Height + _physHeight)))
            {

                this.YMomentum += FormObject.Location.Y -FormObject.newLocationY;
                this.XMomentum += (FormObject.Location.X -FormObject.newLocationX) / 20;
                this.Y = (this.Height - (this.Height + _physHeight));
            }
        }

        private void GetBallDirection(PictureBox obstacle)
        {
            leftDistance = Math.Abs(obstacle.Left - (this.X + this.Width));
            rightDistance = Math.Abs(this.X - (obstacle.Left + obstacle.Width));
            topDistance = Math.Abs(obstacle.Top - (this.Y + this.Height));
            bottomDistance = Math.Abs(this.Y - (obstacle.Top + obstacle.Height));

            toLeft = false;
            toRight = false;
            above = false;
            below = false;

            if (leftDistance < rightDistance && leftDistance < topDistance && leftDistance < bottomDistance)
                toLeft = true;
            else if (rightDistance < leftDistance && rightDistance < topDistance && rightDistance < bottomDistance)
                toRight = true;
            else if (topDistance < rightDistance && topDistance < leftDistance && topDistance < bottomDistance)
                above = true;
            else if (bottomDistance < rightDistance && bottomDistance < leftDistance && bottomDistance < topDistance)
                below = true;
        }

    }
}
