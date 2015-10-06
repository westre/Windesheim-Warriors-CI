using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindesHeim_Game {

    class Explosion : GameObject {

        private DateTime timeStamp = DateTime.Now;

        public Explosion(Point location) : base (location)
        {
            base.ImageURL = "../explosion.png";
        }

        public DateTime TimeStamp {
            get { return timeStamp; }
        }
    }
}
