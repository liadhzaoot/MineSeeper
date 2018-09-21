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
    public partial class FormGame : Form
    {
        public Table table;
        Graphics g;
        public int WinCount = 0;
        int mone = 0;
        public int timerClock = 0;
        bool flagIsVisible;
        public int counterUserFlag=0;//סופר כמה דגלים שם המשתמש
        int counterCOMFlag = 0;
        public int numOfColomLine;
        public Lose lose = new Lose();
        public Win win = new Win();
        public bool Turn = true;//אם זה אמת אז התור הוא של המשתמש אחרת הוא של המחשב
        public bool TurnHelp = true;////אם זה אמת אז התור הוא של המשתמש אחרת הוא של המחשב
        public FormGame()
        {
            InitializeComponent();           
            g = CreateGraphics();
        }
        private Table GetTable()
        {
            return this.table;
        }
        public FormGame(ChooseSingleOrMulty chooseSingleOrMulty,int numOfColomLine)
        {
            InitializeComponent();
            this.numOfColomLine = numOfColomLine;
            g = CreateGraphics();
        }
       private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Resize(object sender, EventArgs e)
        {
          
        }
        private void backRoundCell_Click(object sender, EventArgs e)
        {
        }
        private void MenuBotton_Click(object sender, EventArgs e)
        {
            FormStartWindows FormStartWindows = new FormStartWindows(this);
            this.Hide();
            FormStartWindows.Show();
        }
        private void FormGame_Shown(object sender, EventArgs e)
        {
            g = CreateGraphics();
            Refresh();
            table = new Table((int)(1)
                      , (int)(70)
                      , (int)(this.ClientRectangle.Width * 0.8)
                      , (int)(this.ClientRectangle.Height * 0.8)
                      , (int)((this.ClientRectangle.Width * 0.65))
                      , (int)((this.ClientRectangle.Height * 0.65))
                      , (int)(3)
                      , (int)(numOfColomLine)
                      , (int)(numOfColomLine), this, numOfColomLine);
            table.drawCells();
            table.PutMines();
            table.PutNumbersNeerMines();
            table.BUsed();
            table.bUsedOptimalBombCell();
            this.CountUserFlagLable.Left = table.GetMinX() + table.GetWidth() + (3 * numOfColomLine) + UserLable.Width + CountUserFlagLable.Width+50;
            this.UserLable.Left = table.GetMinX() + table.GetWidth() + (3 * numOfColomLine) + CountUserFlagLable.Width+50;
            this.COMLable.Left = table.GetMinX() + table.GetWidth() + (3 * numOfColomLine) + CountUserFlagLable.Width+50;
            this.CountCOMFlagLable.Left = table.GetMinX() + table.GetWidth() + (3 * numOfColomLine) + UserLable.Width + CountUserFlagLable.Width+50;

        }
        public bool MouseRightClickHelper(int i,int j)
        {
            if (table.ifFlagIsShown(i,j) == true)
                return true;//מחזיר אמת אם יש דגל בתא
            else
                return false;
        }
        
        //...................................................................................................................................................
        public void MouseMoveHelper(object sender, MouseEventArgs e)
        {   
        }
        public void CounterCOMFlag(bool isFlagShown)
        {
            if (isFlagShown == false)
                counterCOMFlag++;
            else if (isFlagShown == true)
                counterCOMFlag--;
            CountCOMFlagLable.Text = Convert.ToString(counterCOMFlag);
            if (counterCOMFlag == 27)
            {                
                this.Hide();
                lose.Show();
            }
        }
        public void CounterUserFlags(bool isFlagShown)
        {
            if (isFlagShown == false)
                counterUserFlag++;
            else if (isFlagShown == true)
                counterUserFlag--;
            CountUserFlagLable.Text = Convert.ToString(counterUserFlag);
            if (counterUserFlag == 27)
            {
                this.Hide();
                win.Show();
            }
        }
        //פעולה שעוזרת לי לקשר בין הגראפיקסל לבין 
        public void FindNumbers(int iStart,int jStart)
        {
            table.FindNumbers(iStart, jStart);
        }
        public void DrawOnlyTrueEmptyCells()
        {
            table.DrawOnlyTrueEmptyCells();
        }
        public void IfClickedOnMineShowAllMines()
        {
            table.IfClickedOnMineShowAllMines();
        }
        public int CheckMineNeerCell(int i, int j)
        {
           int count= table.CheckMineNeerCell( i, j);
           return count;
        } 
        private void FormGame_MouseClick(object sender, MouseEventArgs e)
        {            
            if (table != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                   
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                }
            }
        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            table.Restart();
            timerGame.Start();
            
            counterUserFlag = 0;
            counterCOMFlag = 0;
            CountUserFlagLable.Text = Convert.ToString(counterUserFlag);
            CountCOMFlagLable.Text = Convert.ToString(counterCOMFlag);
            labelTime.ForeColor = Color.FromArgb(255, 255, 192);
        }

        private void FormGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            timerClock++;
            labelTime.Text =Convert.ToString(timerClock);
            if (timerClock > 30)
            {
                labelTime.ForeColor = Color.Red;
            }
        }

        private void User_Click(object sender, EventArgs e)
        {

        }

        private void labelTime_Click(object sender, EventArgs e)
        {

        }

        private void timerPause_Tick(object sender, EventArgs e)
        {
            if (TurnHelp == false)
            {
                //table.comAI.ComputerTurn();
               // timerPause.Stop();
            }
        }
    }
}
