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
    public class Model
    {
        protected Controller controller;

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

    public class ModelMenu : Model
    {
        private ControllerMenu menuController;

        private Button play;
        private Button editor;
        private Button highscore;
        private Button tempPlay;

        public ModelMenu(ControllerMenu controller) : base(controller)
        {
            this.menuController = controller;
        }

        public override void ControlsInit(Form gameWindow)
        {
            this.play = new System.Windows.Forms.Button();
            this.editor = new System.Windows.Forms.Button();
            this.highscore = new System.Windows.Forms.Button();
            this.tempPlay = new System.Windows.Forms.Button();

            this.play.Location = new System.Drawing.Point(254, 52);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(259, 33);
            this.play.TabIndex = 0;
            this.play.Text = "Play";
            this.play.UseVisualStyleBackColor = true;

            this.editor.Location = new System.Drawing.Point(254, 91);
            this.editor.Name = "editor";
            this.editor.Size = new System.Drawing.Size(259, 33);
            this.editor.TabIndex = 1;
            this.editor.Text = "Editor";
            this.editor.UseVisualStyleBackColor = true;

            this.highscore.Location = new System.Drawing.Point(254, 130);
            this.highscore.Name = "highscore";
            this.highscore.Size = new System.Drawing.Size(259, 33);
            this.highscore.TabIndex = 2;
            this.highscore.Text = "highscore";
            this.highscore.UseVisualStyleBackColor = true;

            this.tempPlay.Location = new System.Drawing.Point(254, 169);
            this.tempPlay.Name = "play test";
            this.tempPlay.Size = new System.Drawing.Size(259, 33);
            this.tempPlay.TabIndex = 2;
            this.tempPlay.Text = "play test";
            this.tempPlay.UseVisualStyleBackColor = true;

            this.play.Click += new EventHandler(menuController.play_Click);

            this.tempPlay.Click += new EventHandler(menuController.button_Click);

            gameWindow.Controls.Add(play);
            gameWindow.Controls.Add(editor);
            gameWindow.Controls.Add(highscore);
            gameWindow.Controls.Add(tempPlay);

            //XML loading test
            //XML test = new XML("");
            //test.Write();
            //test.Read();            
        }
    }

    public class ModelGame : Model {
        private ControllerGame gameController;

        // Houdt alle dynamische gameobjecten vast
        private List<GameObject> gameObjects;
        
        // Er is maar 1 speler
        public Player player = new Player(new Point(10, 10), "../Player.png");

        // Graphicspaneel
        public PictureBox graphicsPanel = new PictureBox();

        // Obstakelpanels
        public Panel obstaclePanel1 = new Panel();

        public Panel obstaclePanel2 = new Panel();

        // Titelpaneel
        public Panel gameTitlePanel = new Panel();

        // Karakterpaneel
        public Panel characterPanel = new Panel();

        // Bedieningspaneel
        public Panel controlsPanel = new Panel();

        public ModelGame(ControllerGame controller) : base(controller)
        {
            this.gameController = controller;

            gameObjects = new List<GameObject>();
            
            // Toevoegen aan list, zodat we het kunnen volgen
            gameObjects.Add(new FollowingObstacle(new Point(20, 20), "../Player.png"));
            gameObjects.Add(new FollowingObstacle(new Point(360, 20), "../Player.png"));
            gameObjects.Add(new FollowingObstacle(new Point(120, 520), "../Player.png"));
        }

        public override void ControlsInit(Form gameWindow)
        {
            // Registreer key events voor de player
            gameWindow.KeyDown += gameController.OnKeyDown;
            gameWindow.KeyPress += gameController.OnKeyPress;
            gameWindow.KeyUp += gameController.OnKeyUp;

            // Voeg graphicspaneel toe voor het tekenen van gameobjecten
            graphicsPanel.BackColor = Color.SeaGreen; // testje
            graphicsPanel.Location = new Point(0, 0);
            graphicsPanel.Size = new Size(845, 480);
            graphicsPanel.Paint += gameController.OnPaintEvent;

            // Overige panels

            obstaclePanel1.Location = new Point(845, 0);
            obstaclePanel1.Size = new Size(445, 240);
            obstaclePanel1.BackColor = Color.White;

            obstaclePanel2.Location = new Point(845, 240);
            obstaclePanel2.Size = new Size(445, 240);
            obstaclePanel2.BackColor = Color.White;

            gameTitlePanel.Location = new Point(845, 480);
            gameTitlePanel.Size = new Size(445, 240);
            gameTitlePanel.BackColor = Color.White;

            controlsPanel.Location = new Point(0, 480);
            controlsPanel.Size = new Size(445, 240);
            controlsPanel.BackColor = Color.White;

            characterPanel.Location = new Point(445, 480);
            characterPanel.Size = new Size(445, 240);
            characterPanel.BackColor = Color.White;

            // Voeg hieronder de overige panels toe, zoals objectbeschrijvingen etc.

            gameWindow.Controls.Add(graphicsPanel);
            gameWindow.Controls.Add(obstaclePanel1);
            gameWindow.Controls.Add(obstaclePanel2);
            gameWindow.Controls.Add(gameTitlePanel);
            gameWindow.Controls.Add(controlsPanel);
            gameWindow.Controls.Add(characterPanel);
        }

        public List<GameObject> GameObjects {
            get { return gameObjects; }
        }
    }

    public class ModelLevelSelect : Model
    {
        private ListBox levels;
        private Button goBack;
        private Panel alignPanel;

        private ControllerLevelSelect levelSelectController;

        public ModelLevelSelect(ControllerLevelSelect controller) : base(controller) {
            this.levelSelectController = controller;
        }

        public override void ControlsInit(Form gameWindow) {
            alignPanel = new Panel();
            alignPanel.Location = new System.Drawing.Point(60, 60);
            alignPanel.Size = new System.Drawing.Size(500, 500);


            levels = new ListBox();
            levels.Size = new System.Drawing.Size(200, 100);
            levels.Location = new System.Drawing.Point(10, 10);
            for (int i = 0; i < 11; i ++)
            {
                levels.Items.Add("Level " + i);
            }

            goBack = new Button();
            goBack.Size = new System.Drawing.Size(200, 25);
            goBack.Location = new System.Drawing.Point(10, 115);
            goBack.Text = "Go Back";
            goBack.Click += new EventHandler(levelSelectController.goBack_Click);

            gameWindow.Controls.Add(alignPanel);
            alignPanel.Controls.Add(goBack);
            alignPanel.Controls.Add(levels);
        }
    }

    public class ModelHighscores : Model
    {
        public ModelHighscores(Controller controller) : base(controller)
        {

        }

        public override void ControlsInit(Form gameWindow)
        {
        // todo
        }
    }
}
