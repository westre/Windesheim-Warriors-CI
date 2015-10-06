using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;

namespace WindesHeim_Game
{
    public class Player : GameObject
    {
        private int lives;
        private int speed;
        private int speedDuration = 0;
        private int speedCooldown = 0;
        private int height = 175;
        private int width = 175;

        public Player(Point location, string imageURL) : base (location, imageURL)
        {
            this.speed = 5;
        }

        

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int SpeedDuration
        {
            get { return speedDuration; }
            set { speedDuration = value; }
        }

        public int SpeedCooldown
        {
            get { return speedCooldown; }
            set { speedCooldown = value; }
        }


        public int Height
        {
            get { return height; }
            
        }

        public int Width
        {
            get { return width; }
        }
    }

}
