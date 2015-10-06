using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
            graphicsPanel.Size = new Size(1000, 500);
            graphicsPanel.Paint += gameController.OnPaintEvent;

            // Voeg hieronder de overige panels toe, zoals objectbeschrijvingen etc.

            gameWindow.Controls.Add(graphicsPanel);
        }

        public List<GameObject> GameObjects {
            get { return gameObjects; }
        }
    }

    public class ModelLevelSelect : Model
    {
        private ListBox levels;
        private Button goBack;
        private Button playLevel;
        private Label labelLevels;
        private Label labelLevelPreview;
        private Panel alignPanel;
        private Panel gamePanel;

        private ControllerLevelSelect levelSelectController;

        public ModelLevelSelect(ControllerLevelSelect controller) : base(controller) {
            this.levelSelectController = controller;
        }

        public override void ControlsInit(Form gameWindow) {
            alignPanel = new Panel();
            alignPanel.AutoSize = true;


            gamePanel = new Panel();
            gamePanel.Location = new System.Drawing.Point(210, 40);
            gamePanel.Size = new System.Drawing.Size(845, 475);
            gamePanel.BackColor = Color.DarkGray;


            levels = new ListBox();
            levels.Size = new System.Drawing.Size(200, 475);
            levels.Location = new System.Drawing.Point(0, 40);
            string[] fileEntries = Directory.GetFiles("../levels/");
            foreach (string fileName in fileEntries)
                levels.Items.Add(Path.GetFileName(fileName));

            labelLevels = new Label();
            labelLevels.Text = "Levels";
            labelLevels.Font = new Font("Arial", 20);
            labelLevels.Location = new System.Drawing.Point(0, 0);
            labelLevels.Size = new System.Drawing.Size(200, 30);
            labelLevels.TextAlign = ContentAlignment.MiddleCenter;

            labelLevelPreview = new Label();
            labelLevelPreview.Text = "Level Preview";
            labelLevelPreview.Font = new Font("Arial", 20);
            labelLevelPreview.Location = new System.Drawing.Point(210, 0);
            labelLevelPreview.Size = new System.Drawing.Size(845, 30);
            labelLevelPreview.TextAlign = ContentAlignment.MiddleCenter;

            goBack = new Button();
            goBack.Size = new System.Drawing.Size(200, 25);
            goBack.Location = new System.Drawing.Point(0, 525);
            goBack.Text = "Go Back";
            goBack.Click += new EventHandler(levelSelectController.goBack_Click);

            playLevel = new Button();
            playLevel.Size = new System.Drawing.Size(845, 25);
            playLevel.Location = new System.Drawing.Point(210, 525);
            playLevel.Text = "Play Level";
            playLevel.Click += new EventHandler(levelSelectController.goBack_Click);

            gameWindow.Controls.Add(alignPanel);
            alignPanel.Controls.Add(labelLevels);
            alignPanel.Controls.Add(labelLevelPreview);
            alignPanel.Controls.Add(goBack);
            alignPanel.Controls.Add(playLevel);
            alignPanel.Controls.Add(levels);
            alignPanel.Controls.Add(gamePanel);
            alignPanel.Location = new Point(
                (gameWindow.Width / 2 - alignPanel.Size.Width / 2),
                (gameWindow.Height / 2 - alignPanel.Size.Height / 2));
            alignPanel.Anchor = AnchorStyles.None;
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
