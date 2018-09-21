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
    public partial class FormStartWindows : Form
    {
        public FormStartWindows()
        {
            InitializeComponent();
        }
        public FormStartWindows(FormGame formGame)
        {
            InitializeComponent();
        }
        private void START_Click(object sender, EventArgs e)
        {
            ChooseSingleOrMulty chooseSingleOrMulty = new ChooseSingleOrMulty();
            this.Hide();
            chooseSingleOrMulty.Show();
            


        }

        private void FormStartWindows_Load(object sender, EventArgs e)
        {

        }

  


    }
}
