using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindesHeim_Game {

    public class Obstacle : GameObject 
    {
        public Obstacle(Point location, string imageURL) : base (location, imageURL)
        {
            
        }
    }
}
