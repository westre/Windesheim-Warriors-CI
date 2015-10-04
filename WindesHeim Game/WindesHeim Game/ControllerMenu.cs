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
        private Model view;
        private GameWindow form;
        public Controller(GameWindow form)
        {
            this.form = form;
            this.view = new Model(form, this);
        }

        public virtual void RunController()
        {
            ScreenInit();
        }

        public virtual void ScreenInit()
        {
            form.Controls.Clear();
            view.ControlsInit();
        }

        public virtual void GraphicsInit(Graphics g)
        {
            view.GraphicsInit(g);
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
        private GameWindow form;
        private Controller controller;

        public Model(GameWindow form, Controller controller)
        {
            this.controller = controller;
            this.form = form;
        }

        public virtual void ControlsInit()
        {
        }

        public virtual void GraphicsInit(Graphics g)
        {
            Rectangle rect = new Rectangle(50, 30, 100, 100);
            LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.Red, Color.Yellow, LinearGradientMode.BackwardDiagonal);
            g.FillRectangle(lBrush, rect);
        }
    }

    public class ModelHighscore : Model
    {
        public ModelHighscore(GameWindow form, ControllerHighscore controller) : base(form,controller)
        {

        }
    }
}
