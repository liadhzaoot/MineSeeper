using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
	public class GraphicCell
	{
		private bool isDragging = false;        
		bool LeftClickBool = false;
		private int oldMouseX, oldMouseY;
		private System.Windows.Forms.PictureBox pictureBox;
		private FormGame formGame;
		Image img1;
		public enum Status { Mine, Empty, Number, flag };
		private Status status;
		private int i;
		private int j;
		public bool bUsed,//אם התא היה כבר ברשימה
			IsShown = false,
			bUsedOptimalBombCell,//אם התא היה ברשימה האופטימלית למציאת פצצה
			maximumBombShown,
            checkUse = false,//בודק האם התא הוכנס כבר לרשימה של כל התאים עם הפצצות האופטימליות
            BombCantBeOnTheCell = false;//אם פצצה לא יכולה להיות בתא -אמת אחרת שקר//
        public bool IsFlagShow = false;//שקר תור המחשב אמת תור המשתמש;
		private int countCOMFlag = 0;
		private int countUserFlag = 0;
		Table table;
		public GraphicCell(Point location, int width, int height, FormGame gameForm, Image img, int i, int j)
		{
			this.i = i;
			this.j = j;

			this.pictureBox = new System.Windows.Forms.PictureBox();

			this.pictureBox.BackColor = Color.Green;
			this.pictureBox.BackgroundImage = img;
			this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox.Location = new System.Drawing.Point(location.X, location.Y);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(width, height);
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			this.pictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
			this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);

			this.status = Status.Empty;
			gameForm.Controls.Add(this.pictureBox);
			formGame = gameForm;
		}

		public void NoMinesToImage(int number)
        { 
			switch (number)
			{
				case 1:
					this.SetImage(Properties.Resources._1);

					break;

				case 2:
					this.SetImage(Properties.Resources._2);
					break;

				case 3:
					this.SetImage(Properties.Resources._3);
					break;

				case 4:
					this.SetImage(Properties.Resources._4);
					break;

				case 5:
                    this.SetImage(Properties.Resources._5);
                    break;
			}
		}
		//----------------------------------------------------------------------
		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		//----------------------------------------------------------------------
		{
			if (!isDragging)
				return;
			isDragging = false;
		}
		public void SetLocation(int x, int y)
		{
			this.pictureBox.Location = new System.Drawing.Point(x, y);
		}
		public int getLocationX()
		{
			return this.pictureBox.Location.X;
		}
		public int getLocationY()
		{
			return this.pictureBox.Location.Y;
		}
		public void SetSize(int width, int height)
		{
			this.pictureBox.Size = new System.Drawing.Size(width, height);
		}
		public int GetWidth()
		{
			return this.pictureBox.Size.Width;
		}
		public int Getheight()
		{
			return this.pictureBox.Size.Height;
		}
		public void SetImage(Image img)
		{
			this.pictureBox.BackgroundImage = img;
		}
		public Image GetImg()
		{
			return this.pictureBox.BackgroundImage;
		}
        public string GetImgName()
        {
            return this.pictureBox.BackgroundImage.ToString();
        }
		public PictureBox GetPictureBox()
		{
			return pictureBox;
		}
		public void SetVisible()
		{
			this.pictureBox.Visible = true;
		}
		public void SetVisibleToFalse()
		{
			this.pictureBox.Visible = false;
		}
		public void SetStatus(GraphicCell.Status status)
		{
			this.status = status;
		}
		public Status getStatus()
		{
			return this.status;
		}
		public int getI()
		{
			return this.i;
		}
		public int getJ()
		{
			return this.j;
		}
		public void setI(int i)
		{
			this.i = i;
		}
		public void setJ(int j)
		{
			this.j = j;
		}
		private void pictureBox_MouseEnter(object sender, EventArgs e)
		{
			if (this.IsShown == false && this.IsFlagShow == false)
				this.SetImage(Properties.Resources.chooseCell);
		}
		private void pictureBox_MouseLeave(object sender, EventArgs e)
		{
			if (this.IsShown == false && this.IsFlagShow == false)
				this.SetImage(Properties.Resources.cell);
		}
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		//----------------------------------------------------------------------
		{
			if (((PictureBox)sender).Visible) 
			{
                if (formGame.numOfColomLine == 8)// אם זה singe player
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        formGame.timerGame.Start();
                        formGame.timerPause.Start();
                        
                        if (this.getStatus() == GraphicCell.Status.Empty && this.IsFlagShow == false)
                        {
                            formGame.FindNumbers(this.i, this.j);
                        }
                        formGame.DrawOnlyTrueEmptyCells();
                        if (this.getStatus() == GraphicCell.Status.Mine && this.IsFlagShow == false)
                        {
                            formGame.IfClickedOnMineShowAllMines();
                            formGame.lose.Show();
                            formGame.Hide();
                            this.IsShown = true;
                        }
                        if (this.getStatus() == GraphicCell.Status.Number && this.IsFlagShow == false)
                        {
                            int count = formGame.CheckMineNeerCell(this.i, this.j);
                            this.IsShown = true;
                            this.NoMinesToImage(count);
                        }
                    }
                    if (e.Button == MouseButtons.Right)
                    {
                        formGame.timerGame.Start();
                        if (formGame.MouseRightClickHelper(this.i, this.j) == true)//אם יש דגל בתא
                        {
                            formGame.CounterUserFlags(this.IsFlagShow);//מעביר לו 
                            this.SetImage(Properties.Resources.cell);
                            this.IsFlagShow = false;
                            if (this.getStatus() == GraphicCell.Status.Mine)
                            {
                                formGame.WinCount--;
                            }
                            return;
                        }
                        if (formGame.MouseRightClickHelper(this.i, this.j) == false && this.IsShown == false)
                        {
                            formGame.CounterUserFlags(this.IsFlagShow);
                            this.SetImage(Properties.Resources.BlueFlag);
                            this.IsFlagShow = true;
                            if(this.getStatus() == GraphicCell.Status.Mine)
                            {
                                formGame.WinCount++;
                            }
                            if(formGame.WinCount == 10 && formGame.counterUserFlag == 10)
                            {
                                formGame.win.Show();
                                formGame.Hide();
                            }
                        }
                    }
                }
            }// עבור סינגל פלייר
			if (formGame.numOfColomLine == 16)//עבור מולטי פלייר
			{
				if (formGame.Turn == true)
				{
					if (e.Button == MouseButtons.Left)
					{
						if (this.IsShown == false)//שהלחיצה תתאפשר רק בתאים שעוד לא לחצו עליהם
						{
							formGame.timerGame.Start();

							if (this.getStatus() == GraphicCell.Status.Empty && this.IsFlagShow == false)
							{
								formGame.FindNumbers(this.i, this.j);
								formGame.Turn = false;
                                formGame.TurnHelp = false;
                                this.IsShown = true;/////////
                            }
							formGame.DrawOnlyTrueEmptyCells();
							if (this.getStatus() == GraphicCell.Status.Mine && this.IsFlagShow == false)
							{
								if (formGame.MouseRightClickHelper(this.i, this.j) == false && this.IsShown == false)
								{
									formGame.CounterUserFlags(this.IsFlagShow);
									this.SetImage(Properties.Resources.BlueFlag);
									this.IsFlagShow = true;
									formGame.Turn = true;
                                    formGame.TurnHelp = true;

                                }
							}
							if (this.getStatus() == GraphicCell.Status.Number && this.IsFlagShow == false)
							{
								int count = formGame.CheckMineNeerCell(this.i, this.j);
								this.IsShown = true;
								this.NoMinesToImage(count);
								formGame.Turn = false;
                                formGame.TurnHelp = false;
                            }
						}
						if (formGame.Turn == false)
						{
                            formGame.table.comAI.ComputerTurn();
                            //formGame.timerPause.Start();
                            //formGame.Turn = true;

                        }
					}
				}
			}

		}

	}
}






