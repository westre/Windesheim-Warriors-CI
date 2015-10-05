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
        private PictureBox image;

        public GameObject(Point location, string imageURL)
        {
            this.imageURL = imageURL;
            this.location = location;
            this.image = new PictureBox();

            imageLoad();
        }

        private void imageLoad()
        {
            this.image.Load(imageURL);
            this.image.Location = location;
            this.image.Size = new Size(128, 128);
            this.image.BackColor = Color.Transparent;
        }

        public PictureBox Image
        {
            get { return image; }
        }

        public Point Location
        {
            get { return location; }
            set {
                image.Location = value;
                location = value;
            }
        }
    }
}
