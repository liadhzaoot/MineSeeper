using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{

   public class Table
    {
        private Random rnd = new Random();
        private GraphicCell[,] cell;
        private int height;
        private int width;
        public Pen pen = new Pen(Color.RoyalBlue, 1), penPickCell, penBackColor;// pen=rect penPickCell=כשבוחרים
        public Rectangle rect;
        private int MinX;
        private int MinY;
        int count = 0;
        private  List<GraphicCell> list = new List<GraphicCell>();
        Brush br = new SolidBrush(Color.Silver);
        Brush br2 = new SolidBrush(Color.Red);
        private FormGame formGame;
        bool visual = false;
        public COMAI comAI;
        public int numOfColomLine;// שלב התחלתי ישנו לוח 8 על 8
           
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public Table(int minX, int minY, int width, int hight,
                     int cellW, int cellH, int gap, int noCellsW, int noCellsH, FormGame formGame, int numOfColomLine /*level*/)//פעולה בונה
        {
            this.numOfColomLine = numOfColomLine;
            this.MinX = minX;
            this.MinY = minY;
            this.width = width;
            cell = new GraphicCell[numOfColomLine + 2, numOfColomLine + 2];
            
            int x = minX + gap;
            int y = minY + gap;
            //y = y - cellH - gap;
            cellW = width / noCellsW;
            cellH = hight / noCellsH;
            this.formGame = formGame;
            for (int i = 0; i <= numOfColomLine + 1; i++)
            {

                for (int j = 0; j <= numOfColomLine + 1; j++)
                {
                   
                    cell[i, j] = new GraphicCell(new Point((int)x, (int)y), (int)cellW, (int)cellH, formGame, null, i, j);
                    x = x + gap;
                    x = x + cellW;
                }
                y = y + cellH + gap;
                x = minX + gap;
            }
            for (int i = 0; i <= numOfColomLine + 1; i++)
            {
                for (int j = 0; j <= numOfColomLine + 1; j++)
                {
                    if(i==0)
                    cell[i, j].SetVisibleToFalse();
                    if(i==numOfColomLine + 1)
                     cell[i, j].SetVisibleToFalse();
                    if (j == numOfColomLine + 1)
                        cell[i, j].SetVisibleToFalse();
                    if (j == 0)
                        cell[i, j].SetVisibleToFalse();

                }

            }

            comAI = new COMAI(this, formGame, cell);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public bool Inside(int X, int Y)
        {
            if (X > this.MinX && X < (this.MinX + this.width) &&
                Y > this.MinY && Y < (this.MinY + this.height))
            {
                return true;
            }
            else
                return false;
        }
       
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void drawCells()//מצייר את התאים
        {
            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    cell[i, j].SetImage(Properties.Resources.cell);
                    //TheNuberIsNotShown();
                }
            }
        }
        public bool ifFlagIsShown(int iStart,int jStart)// בודק אם יש דגל בתא שנתנו מחזיר אמת
        {
            if(cell[iStart,jStart].IsFlagShow==true)
            return true;
                return false; 
        }
    
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void IfClickedFill( int i, int j)//פעולה הממלאת את התאים הריקים ברגע לחיצה
        {
            cell[i, j].SetImage(Properties.Resources.enter_cell);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void IfClickedOnMineShowAllMines()//אם נלחץ על פצצה כל הפצצות התגלו
        {
            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    if (cell[i, j].getStatus() == GraphicCell.Status.Mine)
                    {

                        cell[i, j].SetImage(Properties.Resources.redmine);
                        
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void PutMines()//פעולה ששמה פצצות בהתאם למה שהמשתמש בחר
        {
            int mone = 0;
            int i = rnd.Next(1, 9);
            int j = rnd.Next(1, 9);
            if (this.numOfColomLine == 8)
            {
                while (mone <= 9)
                {
                    if (cell[i, j].getStatus() == GraphicCell.Status.Empty)
                    {
                        mone++;
                        cell[i, j].SetStatus(GraphicCell.Status.Mine);
                    }
                    i = rnd.Next(1, 9);
                    j = rnd.Next(1, 9);
                }
            }
            else if (this.numOfColomLine == 16)
            {
                while (mone <= 52)
                {
                    if (cell[i, j].getStatus() == GraphicCell.Status.Empty)
                    {
                        mone++;
                        cell[i, j].SetStatus(GraphicCell.Status.Mine);
                        //g.DrawEllipse(pen, cell[i, j].getMinX(), cell[i, j].getMinY(), cell[i, j].getWidth(), cell[i, j].getHeight());
                    }
                    i = rnd.Next(1, 17);
                    j = rnd.Next(1, 17);
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void PutNumbersNeerMines()//פעולה ששמה מספרים ליד פצצות
        {
            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    int count = CheckMineNeerCell(i, j);
                    if (count > 0)
                    {
                        if (cell[i, j].getStatus() != GraphicCell.Status.Mine)
                        {
                            cell[i, j].SetStatus(GraphicCell.Status.Number);
                        
                        }
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
       
        public int CheckMineNeerCell(int i, int j)//פעולה הבודקת כמה פצצות יש ליד תא ומחזירה את מספר הפצצצות
        {

           
            int giveI = i;//i שהפעולה מביאה
            int giveJ = j;//j שהפעולה מביאה
            int mone = 0;
            for (int k = i - 1; k <= giveI + 1; k++)
            {
                for (int f = j - 1; f <= giveJ + 1; f++)
                {
                    if (cell[k, f].getStatus() == GraphicCell.Status.Mine)
                    {
                        mone++;
                    }
                }
            }
            if (cell[giveI, giveJ].getStatus() == GraphicCell.Status.Mine)
            {
                mone--;
            }
            return mone;

        }
        public void BUsed()
        {
            // (הופך את כל התאים לשקר (מכיוון שעוד לא ביקרתי בהם 
            //מעדכן I AND J
            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    cell[i, j].bUsed = false;
                    cell[i, j].setI(i);
                    cell[i, j].setJ(j);
                }
            }
        }
        public void bUsedOptimalBombCell()
        {
            //  (הופך את כל התאים לשקר (מכיוון שעוד לא ביקרתי בהם 
            // ובהתחלה אף אחד הוא לא אופטימלי לפצצה
            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    cell[i, j].bUsedOptimalBombCell = false;
                    
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------
        public void FindNumbers(int iStart, int jStart)//הפעולה מוצאת את כל התאים הריקים עד שמגיעה למיספרים
        {
            // (הופך את כל התאים לשקר (מכיוון שעוד לא ביקרתי בהם 
                //מעדכן I AND J
                for (int i = 1; i <= numOfColomLine; i++)
                {
                    for (int j = 1; j <= numOfColomLine; j++)
                    {
                        cell[i, j].bUsed = false;
                        cell[i, j].setI(i);
                        cell[i, j].setJ(j);
                    }
                }
            list.Add(cell[iStart, jStart]);
            while (list.Count > 0)
            {
                cell[list[0].getI(), list[0].getJ()].bUsed = true;
                if (list[0].getI() != 0 && list[0].getJ() != 0 && list[0].getI() != numOfColomLine + 1 && list[0].getJ() != numOfColomLine + 1)
                {
                    EnterNeerNumbersInToList(list[0].getI(), list[0].getJ());
                }
                list.Remove(list[0]);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void EnterNeerNumbersInToList(int i, int j)
        {
            //בודק אם התא הוא ריק  וגם האם הוא היה לא כבר ברשימה אם כן הוא יכניס אותו לתוך הרשימה
            if (cell[i, j - 1].getStatus() == GraphicCell.Status.Empty && 
                cell[i, j - 1].bUsed == false                          &&
                cell[i, j - 1].IsFlagShow==false                        )
                list.Add(cell[i, j - 1]);
            if (cell[i, j + 1].getStatus() == GraphicCell.Status.Empty &&
                cell[i, j + 1].bUsed == false &&
                cell[i, j +1].IsFlagShow == false)
                list.Add(cell[i, j + 1]);
            if (cell[i + 1, j].getStatus() == GraphicCell.Status.Empty &&
                cell[i + 1, j].bUsed == false &&
                cell[i+1, j ].IsFlagShow == false)
                list.Add(cell[i + 1, j]);
            if (cell[i - 1, j].getStatus() == GraphicCell.Status.Empty &&
                cell[i - 1, j].bUsed == false &&
                cell[i-1, j ].IsFlagShow == false)
                list.Add(cell[i - 1, j]);
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void DrawOnlyTrueEmptyCells()//פעולה המציירת רק תאים ריקים
        {
            int mone = 0;
            int giveI;
            int giveJ;

            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    if (cell[i, j].bUsed == true)
                    {
                        giveI = i;
                        giveJ = j;
                        for (int k = i - 1; k <= giveI + 1; k++)
                        {
                            for (int f = j - 1; f <= giveJ + 1; f++)
                            {
                                if (cell[k, f].getStatus() == GraphicCell.Status.Number&&cell[k, f].IsFlagShow==false)   
                                {
                                    cell[k, f].IsShown = true;
                                    mone = CheckMineNeerCell(k, f);
                                    cell[k, f].NoMinesToImage(mone);
                                }
                            }
                        }
                        if (cell[i, j].getStatus() != GraphicCell.Status.Mine && 
                            cell[i, j].getStatus() != GraphicCell.Status.Number)
                        {
                            IfClickedFill(i, j);
                            cell[i, j].IsShown = true;
                        }
                    }

                }

            }
        }

       
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        public void Restart()//פעולה המבצעת איפוס למשחק
        {
            for (int i = 1; i <= numOfColomLine; i++)
            {
                for (int j = 1; j <= numOfColomLine; j++)
                {
                    cell[i, j].SetImage(Properties.Resources.cell);
                    cell[i, j].SetStatus(GraphicCell.Status.Empty);
                    cell[i, j].IsShown = false;
                    cell[i, j].IsFlagShow = false;
                    cell[i, j].BombCantBeOnTheCell = false;
                }
            }
            this.PutMines();
            this.PutNumbersNeerMines();
            this.BUsed();
            formGame.timerClock = -1;
        }
        public int GetMinX()
        {
            return this.MinX;
        }
        public int GetWidth()
        {
            return this.width;
        }
        
    }
     

}














