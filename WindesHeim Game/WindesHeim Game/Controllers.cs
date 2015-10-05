using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindesHeim_Game
{
    public class Controller
    {
        protected Model model;
        protected GameWindow gameWindow;
        public Controller(GameWindow form)
        {
            this.gameWindow = form;
            this.model = new Model(this);
        }
        public virtual void RunController()
        {
            ScreenInit();
        }

        public virtual void ScreenInit()
        {
            gameWindow.Controls.Clear();
            model.ControlsInit(gameWindow);
        }

        public virtual void GraphicsInit(Graphics g)
        {
            model.GraphicsInit(g);
        }
    }

    class ControllerMenu : Controller
    {
        public ControllerMenu(GameWindow form) : base(form)
        {
            this.gameWindow = form;
            this.model = new ModelMenu(this);
        }
    }
}
