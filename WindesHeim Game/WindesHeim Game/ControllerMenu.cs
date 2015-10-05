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
    public enum ScreenStates
    {
        menu,
        gameSelect,
        game,
        editorSelect,
        editor,
        highscore
    }
    public class Controller
    {
        private Model model;
        private GameWindow gameWindow;
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

    public class ControllerHighscore : Controller
    {
        public ControllerHighscore(GameWindow form) : base(form)
        {

        }
    }



    public class Model
    {
        private Controller controller;

        public Model(Controller controller)
        {
            this.controller = controller;
        }

        public virtual void ControlsInit(Form GameWindow)
        {
        }

        public virtual void GraphicsInit(Graphics g)
        {
        }
    }
}
