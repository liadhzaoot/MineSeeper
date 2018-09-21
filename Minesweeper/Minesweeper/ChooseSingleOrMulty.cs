using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class ChooseSingleOrMulty : Form
    {
        public ChooseSingleOrMulty()
        {
            InitializeComponent();
        }
        public ChooseSingleOrMulty(FormGame formgame)
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FormGame formGame = new FormGame(this,16);
            this.Hide();
            formGame.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormGame formGame = new FormGame(this, 8);
            this.Hide();
            formGame.Show();
        }

        private void ChooseSingleOrMulty_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ChooseSingleOrMulty_MouseMove(object sender, MouseEventArgs e)
        {
            
               // this.SetImage(Properties.Resources.chooseCell);
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            // pictureBox2.BackgroundImageLayout(Properties.Resources.chooseCell);
            //this.SetImage(Properties.Resources.chooseCell);
            pictureBox2.BackgroundImage = Properties.Resources.multi_high;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.multiplayer;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.single_high;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.singleplayer;
        }
    }
}
