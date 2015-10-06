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

        public Player(Point location, string imageURL) : base (location, imageURL)
        {
            this.speed = 5;
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }

}
