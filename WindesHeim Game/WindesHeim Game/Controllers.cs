using System;
using System.Drawing;
using System.Threading;
using System.Timers;
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

    public class ControllerMenu : Controller
    {
        public ControllerMenu(GameWindow form) : base(form)
        {
            this.model = new ModelMenu(this);
        }

        public void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            gameWindow.setController(ScreenStates.game);

            button.Enabled = false;           
        }
    }

    public class ControllerGame : Controller
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool isDown = false;
        private bool isLeft = false;

        public ControllerGame(GameWindow form) : base(form)
        {
            this.model = new ModelGame(this);

            timer.Tick += new EventHandler(GameLoop);
            timer.Interval = 16;
            timer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            ModelGame mg = (ModelGame)model;

            if (isDown)
            {
                mg.player.Location = new Point(mg.player.Location.X, mg.player.Location.Y - mg.player.Speed);
            }
            if (!isDown)
            {
                mg.player.Location = new Point(mg.player.Location.X, mg.player.Location.Y + mg.player.Speed);
            }
            if (isLeft)
            {
                mg.player.Location = new Point(mg.player.Location.X - mg.player.Speed, mg.player.Location.Y);
            }
            if (!isLeft)
            {
                mg.player.Location = new Point(mg.player.Location.X + mg.player.Speed, mg.player.Location.Y);
            }
        }

        

        public override void RunController()
        {
            base.RunController();
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            ModelGame mg = (ModelGame)model;

            if (e.KeyChar == 'w')
            {
                isDown = false;               
            }
            if (e.KeyChar == 's')
            {
                isDown = true;
            }
            if (e.KeyChar == 'a')
            {
                isLeft = true;
                mg.player.Image.Load("../PlayerLeft.png");
            }
            if (e.KeyChar == 'd')
            {
                isLeft = false;
                mg.player.Image.Load("../Player.png");
            }
        }
    }

}
