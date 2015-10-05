using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindesHeim_Game
{
    public partial class GameWindow : Form
    {
        PlayerController playerController;

        public GameWindow()
        {
            InitializeComponent();

            playerController = new PlayerController(this);

        }

    }
}
