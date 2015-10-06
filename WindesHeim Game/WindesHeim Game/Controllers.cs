using System;
using System.Collections.Generic;
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
        public void play_Click(object sender, EventArgs e)
        {
            gameWindow.setController(ScreenStates.gameSelect);
        }
    }

    public class ControllerLevelSelect : Controller
    {
        public ControllerLevelSelect(GameWindow form) : base(form)
        {
            this.model = new ModelLevelSelect(this);
        }

        public void goBack_Click(object sender, EventArgs e)
        {
            gameWindow.setController(ScreenStates.menu);
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

            // We moeten een 2e array maken om door heen te loopen
            // Er is kans dat we de array door lopen en ook tegelijkertijd een explosie toevoegen
            // We voegen dan als het ware iets toe en lezen tegelijk, dit mag niet
            List<GameObject> safeListArray = new List<GameObject>(mg.GameObjects);

            // Loop door alle obstacles objecten en roep methode aan
            foreach(GameObject gameObject in safeListArray) {
                if(gameObject is MovingExplodingObstacle) {
                    MovingExplodingObstacle gameObstacle = (MovingExplodingObstacle)gameObject;
                    gameObstacle.ChasePlayer(mg.player);

                    if(gameObstacle.CollidesWith(mg.player)) {
                        mg.player.Location = new Point(0, 0);

                        mg.GameObjects.Add(new Explosion(gameObstacle.Location));
                    }
                }

                if (gameObject is SlowingObstacle) {
                    SlowingObstacle gameObstacle = (SlowingObstacle)gameObject;
                    gameObstacle.ChasePlayer(mg.player);

                    if(gameObstacle.CollidesWith(mg.player)) {
                        mg.player.Speed = 2;
                    }
                    else {
                        mg.player.Speed = 5;
                    }
                }

                if (gameObject is ExplodingObstacle) {
                    ExplodingObstacle gameObstacle = (ExplodingObstacle)gameObject;

                    if (gameObstacle.CollidesWith(mg.player)) {
                        mg.player.Location = new Point(0, 0);
                        mg.GameObjects.Add(new Explosion(gameObstacle.Location));
                    }
                }

                // Check of we de explosie kunnen verwijderen
                if(gameObject is Explosion) {
                    Explosion explosion = (Explosion)gameObject;

                    DateTime nowDateTime = DateTime.Now;
                    DateTime explosionDateTime = explosion.TimeStamp;

                    TimeSpan difference = nowDateTime - explosionDateTime;

                    // Verschil is 3 seconden, dus het bestaat al voor 3 seconden, verwijderen maar!
                    if(difference.TotalSeconds > 3) {
                        mg.GameObjects.Remove(gameObject);
                    }
                }
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
            g.DrawImage(Image.FromFile(mg.player.ImageURL), mg.player.Location.X, mg.player.Location.Y, 64, 64);

            // Teken andere gameobjects
            foreach (GameObject gameObject in mg.GameObjects) {
                if(gameObject is Obstacle) {
                    g.DrawImage(Image.FromFile(gameObject.ImageURL), gameObject.Location.X, gameObject.Location.Y, 64, 64);
                }

                if(gameObject is Explosion) {
                    g.DrawImage(Image.FromFile(gameObject.ImageURL), gameObject.Location.X, gameObject.Location.Y, 64, 64);
                }
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
