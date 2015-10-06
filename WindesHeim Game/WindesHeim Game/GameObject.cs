using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindesHeim_Game
{
    public class GameObject
    {
        private Point location;
        private string imageURL;

        public GameObject(Point location, string imageURL)
        {
            this.imageURL = imageURL;
            this.location = location;
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public string ImageURL {
            get { return imageURL; }
            set { imageURL = value; }
        }
    }
}
