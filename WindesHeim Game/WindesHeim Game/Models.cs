﻿using System;
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

            this.tempPlay.Click += new EventHandler(menuController.button_Click);

            gameWindow.Controls.Add(play);
            gameWindow.Controls.Add(editor);
            gameWindow.Controls.Add(highscore);
            gameWindow.Controls.Add(tempPlay);
        }

        public Button PlayButton
        {
            get { return play; }
        }

        public Button EditorButton
        {
            get { return editor; }
        }

        public Button HighScoreButton
        {
            get { return highscore; }
        }
    }

    public class ModelGame : Model
    {
        private ControllerGame gameController;

        public Player player = new Player(new Point(10,10), "../Player.png");
        
        public ModelGame(ControllerGame controller) : base(controller)
        {
            this.gameController = controller;
        }

        public override void ControlsInit(Form gameWindow)
        {
            gameWindow.Controls.Add(player.Image);

            gameWindow.KeyPress += gameController.OnKeyPress;
        }
    }
    public class ModelLevelSelect : Model
    {
        private ControllerLevelSelect levelSelectController;

        public ModelLevelSelect(ControllerLevelSelect controller) : base(controller)
        {
            this.levelSelectController = controller;
        }

        public override void ControlsInit(Form gameWindow)
        {
        }
    }
}
