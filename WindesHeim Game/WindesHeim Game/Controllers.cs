using System;
using System.Drawing;
using System.Threading;
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
            gameWindow.setController(ScreenStates.game);
        }
    }

    public class ControllerLevelSelect : Controller
    {
        public ControllerLevelSelect(GameWindow form) : base(form)
        {
            this.model = new ModelLevelSelect(this);
        }
    }

    public class ControllerGame : Controller
    {
        // Timer voor de gameloop
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private bool pressedLeft = false;
        private bool pressedRight = false;
        private bool pressedUp = false;
        private bool pressedDown = false;
        private bool isKeyDown = false;

        public ControllerGame(GameWindow form) : base(form)
        {
            this.model = new ModelGame(this);

            timer.Tick += new EventHandler(GameLoop);
            timer.Interval = 16;
            timer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            ProcessUserInput();
            ProcessObstacles();

            ModelGame mg = (ModelGame)model;
            mg.graphicsPanel.Invalidate();
        }

        private void ProcessUserInput() 
        {
            ModelGame mg = (ModelGame) model;

            if (pressedDown && isKeyDown) {
                mg.player.Location = new Point(mg.player.Location.X, mg.player.Location.Y + mg.player.Speed);
            }
            if (pressedUp && isKeyDown) {
                mg.player.Location = new Point(mg.player.Location.X, mg.player.Location.Y - mg.player.Speed);
            }
            if (pressedLeft && isKeyDown) {
                mg.player.Location = new Point(mg.player.Location.X - mg.player.Speed, mg.player.Location.Y);
            }
            if (pressedRight && isKeyDown) {
                mg.player.Location = new Point(mg.player.Location.X + mg.player.Speed, mg.player.Location.Y);
            }
        }

        private void ProcessObstacles() 
        {
            ModelGame mg = (ModelGame) model;

            // Loop door alle FollowingObstacle objecten en roep methode aan
            foreach(FollowingObstacle followingObstacle in mg.GameObjects) {
                followingObstacle.ChasePlayer(mg.player);
            }      
        }

        public override void RunController()
        {
            base.RunController();
        }

        public void OnPaintEvent(object sender, PaintEventArgs pe) {
            Graphics g = pe.Graphics;
            ModelGame mg = (ModelGame)model;

            // Teken player
            g.DrawImage(Image.FromFile(mg.player.ImageURL), new Point(mg.player.Location.X, mg.player.Location.Y));

            // Teken andere gameobjects
            foreach (FollowingObstacle followingObstacle in mg.GameObjects) {
                g.DrawImage(Image.FromFile(followingObstacle.ImageURL), new Point(followingObstacle.Location.X, followingObstacle.Location.Y));
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e) {
            if(!isKeyDown) {
                isKeyDown = true;

                pressedDown = false;
                pressedLeft = false;
                pressedRight = false;
                pressedUp = false;
            }

            Console.WriteLine("KeyDown");
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e) {
            ModelGame mg = (ModelGame)model;

            // Dit werkt nog niet fijn
            if (e.KeyChar == 'w') {
                pressedUp = true;
            }
            if (e.KeyChar == 's') {
                pressedDown = true;
            }
            if (e.KeyChar == 'a') {
                pressedLeft = true;
                mg.player.ImageURL = "../PlayerLeft.png";
            }
            if (e.KeyChar == 'd') {
                pressedRight = true;
                mg.player.ImageURL = "../Player.png";
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e) {
            if (isKeyDown) {
                isKeyDown = false;
            }

            Console.WriteLine("KeyUp");
        }
    }

    class ControllerHighscores : Controller
    {
        public ControllerHighscores(GameWindow form) : base(form)
        {
            this.model = new ModelHighscores(this);
        }
    }
}
