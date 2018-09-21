using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class COMAI
    {
        private Table table;
        private FormGame formGame;
        private GraphicCell[,] cells, cellHelp;
        int mone = 0;
        private IJ ij, lastIJ;
        private Random rnd = new Random();
        Stack<GraphicCell> stackOptimalBombCells = new Stack<GraphicCell>();
        public List<Stack<GraphicCell>> listArrCell = new List<Stack<GraphicCell>>();
        int numOfCellsInList;
        int numRnd;
        int i,i1=1;
        int j,j1=1;

        public COMAI(Table table, FormGame formGame, GraphicCell[,] cells)
        {
            this.table = table;
            this.formGame = formGame;
            this.cells = cells;
        }
        public void ComputerTurn()
        {
            ScanTheBordMarkTheNtCellsBomb(cells);//הופך את התאים לאמת אם פצצה לא יכולה להיות שם
            listArrCell=CheckTheCellsNumbersAndGetInToList(cells);//עובר על כל הלוח ומכניס לרשימה את כל התאים שיכולים להיות פצצה
            if (listArrCell.Count!=0)
            {
                CheckStacksInList(listArrCell);////הפעולה עוברת על כל הרשימה ובודקת האם ישנם מחסניות שלא צריכות להיות(שכל הפצצות התגלו
            }
            if (listArrCell.Count != 0)
            {
                stackOptimalBombCells = listArrCell[0];
                int index = 0;
                while (stackOptimalBombCells.Count == 0 && index == 0)//כל עוד המחסנית ריקה
                {

                    if (stackOptimalBombCells.Count == 0)
                    {
                        listArrCell.Remove(listArrCell[0]);
                    }
                    if (listArrCell.Count == 0)
                    {
                        index = 1;
                    }
                    if (index == 0)
                        stackOptimalBombCells = listArrCell[0];

                }
                if (index == 0)//אם האינדקס שווה ל0 כלומר הרשימה לא ריקה אז תבחר את התא הראשון ותעשה עליו פעולות
                {
                    stackOptimalBombCells = listArrCell[0];
                    i = stackOptimalBombCells.Peek().getI();
                    j = stackOptimalBombCells.Peek().getJ();
                    lastIJ = ij;
                    stackOptimalBombCells.Pop();
                }
            }
            if (listArrCell.Count == 0)//אם ההרשימה ריקה אז תעבור על הלוח עד שאתה מוצא תא ריק ושהוא אופטימלי לפצצה
            {
                while (cells[i1, j1].IsShown == true || cells[i1, j1].BombCantBeOnTheCell == true)
                {
                    i1++;
                    if (i1 == 17)
                    {
                        i1 = 1;
                        j1++;
                    }

                }
                i = i1;
                j = j1;
                j1 = 1;
                i1 = 1;
            }   
            if (formGame.Turn == false)
            {
                if (cells[i, j].getStatus() == GraphicCell.Status.Empty && cells[i, j].IsFlagShow == false)//אם התא ריק ולא הוצג
                {
                    formGame.FindNumbers(cells[i, j].getI(), cells[i, j].getJ());
                    formGame.DrawOnlyTrueEmptyCells();
                    formGame.Turn = true;
                    cells[i, j].IsShown = true;
                }

                if (cells[i, j].getStatus() == GraphicCell.Status.Mine && cells[i, j].IsFlagShow == false)
                {
                    if (formGame.MouseRightClickHelper(cells[i, j].getI(), cells[i, j].getJ()) == false && cells[i, j].IsShown == false)//אם אין דגל בתא וגם הוא לא נראה עדיין
                    {
                        formGame.CounterCOMFlag(cells[i, j].IsFlagShow);
                        cells[i, j].SetImage(Properties.Resources.RedFlag);
                        cells[i, j].IsFlagShow = true;
                        cells[i, j].IsShown = true;
                        formGame.Turn = false;
                        this.ComputerTurn();
                        //formGame.timerPause.Start();
                    }
                }
                if (cells[i, j].getStatus() == GraphicCell.Status.Number && cells[i, j].IsFlagShow == false)
                {
                    int count = formGame.CheckMineNeerCell(cells[i, j].getI(), cells[i, j].getJ());
                    cells[i, j].IsShown = true;
                    cells[i, j].NoMinesToImage(count);
                    formGame.Turn = true;
                }
            }
        }       
        public bool CheckIfMaximumBombShown(GraphicCell[,] cell, IJ ij) //בודק האם מספר הפצצות המקסימלי ליד מספר התגלו
        {
            int i = ij.getI();
            int j = ij.getJ();           
            int giveI = i;//i שהפעולה מביאה
            int giveJ = j;//j שהפעולה מביאה
            int mone = 0;
            for (int k = i - 1; k <= giveI + 1; k++)
            {
                for (int f = j - 1; f <= giveJ + 1; f++)
                {
                    if (cell[k, f].IsFlagShow == true)
                    {
                        mone++;
                    }
                }
            }
            if (mone == table.CheckMineNeerCell(i, j))
            {
                return true;
            }
            return false;
        }
        //הפעולה מחזירה מחסנית עם התאים האופטימלים לפצצה
        public List<Stack<GraphicCell>> PushStackInToList(Stack<GraphicCell> stackOptimalBombCells)
        {
            listArrCell.Add(stackOptimalBombCells);
            return listArrCell;
        }
        public Stack<GraphicCell> PushOptimalicCellsInToStack(GraphicCell[,] cell, IJ ij, Stack<GraphicCell> stackOptimalBombCells)//פעולה שמקבלת תא ומכניסה למחסנית את התאים שיכולים להיות פצצה
        {
            int i = ij.getI();
            int j = ij.getJ();
            int giveI = i;
            int giveJ = j;            
            for (int k = i - 1; k <= giveI + 1; k++)
            {
                for (int f = j - 1; f <= giveJ + 1; f++)
                {
                    if (k != 0 && f != 0 && cell[k, f].IsShown == false && cell[k, f].IsFlagShow == false&& cell[k, f].BombCantBeOnTheCell==false)
                    {
                        cell[k, f].bUsedOptimalBombCell = true;
                        stackOptimalBombCells.Push(cell[k, f]);
                    }
                }
            }
            return stackOptimalBombCells;
        }
        public void CheckIfTheCellsDontHaveBomb(GraphicCell[,] cell, IJ ij) //אם התאים ידועים שלא יכול להיות בהם פצצה אז סמן אותם
        {
            int i = ij.getI();
            int j = ij.getJ();
            int giveI = i;//i שהפעולה מביאה
            int giveJ = j;//j שהפעולה מביאה
            int mone = 0;
            for (int k = i - 1; k <= giveI + 1; k++)
            {
                for (int f = j - 1; f <= giveJ + 1; f++)
                {
                    if (cell[k, f].IsShown == true || CheckIfMaximumBombShown(cell, ij) == true)
                    {
                        cell[k, f].BombCantBeOnTheCell = true;
                    }
                }
            }
        }
        public List<Stack<GraphicCell>> CheckTheCellsNumbersAndGetInToList(GraphicCell[,] cell) //עוברת על כל הלוח וכאשר היא מגיעה למספר היא מכניסה את התאים מסביבו שיכולים להיות פצצה לתוך מחסנית
        {
            IJ ijHelp;
            List<Stack<GraphicCell>> listArrCell = new List<Stack<GraphicCell>>();
            Stack<GraphicCell> stackOptimalBombCells;
            for (int i = 1; i <= 16; i++)
            {
                for (int j = 1; j <=16; j++)
                {         
                    if(cell[i,j].IsShown==true&&cell[i,j].getStatus()==GraphicCell.Status.Number)
                    {
                        stackOptimalBombCells = new Stack<GraphicCell>();
                        ijHelp = new IJ(i, j);
                        listArrCell.Add(PushOptimalicCellsInToStack(cell, ijHelp, stackOptimalBombCells));//מכניס תאים אופטימלים לתוך מחסנית
                    }
                }
            }
            return listArrCell;
        }        
        public void ScanTheBordMarkTheNtCellsBomb(GraphicCell[,] cell) //הפעולה סורקת על הלוח והופכת לאמת את כל התאים שלא יכול להיות בהם פצצה
        {
            for (int i = 1; i <= 16 ; i++)
            {
                for (int j = 1; j <= 16 ; j++)
                {
                    if(cell[i,j].IsShown==true&& cell[i, j].getStatus()==GraphicCell.Status.Number)
                    {
                        CheckIfTheCellsDontHaveBomb(cell, new IJ(i, j));
                    }
                    if (cell[i, j].IsShown == true)
                    {
                        cell[i, j].BombCantBeOnTheCell = true;
                    }
                }
            }
        }
        ////////////////public bool CheckIfTheCellsCanBeWithBomb(GraphicCell[,] cell,IJ ij) //פעולה הבודקת האם התאים שליד התא המתקבל יכולים להיות עם פפצה אם לפחות אחד כן אז תחזיר אמת אחרת תחזיר שקר
        ////////////////{
        ////////////////    int i = ij.getI();
        ////////////////    int j = ij.getJ();
        ////////////////    //string number = cell[i, j].GetImgName();
        ////////////////    //table.CheckMineNeerCell(i, j).ToString();
        ////////////////    int giveI = i;//i שהפעולה מביאה
        ////////////////    int giveJ = j;//j שהפעולה מביאה
        ////////////////    int mone = 0;
        ////////////////    for (int k = i - 1; k <= giveI + 1; k++)
        ////////////////    {
        ////////////////        for (int f = j - 1; f <= giveJ + 1; f++)
        ////////////////        {
        ////////////////            if(cell[k, f].BombCantBeOnTheCell == false)//אם ישנו תא שהוא יכול להיות פצצה תחזיר אמת אחרת שקר
        ////////////////            {
        ////////////////                return true;
        ////////////////            }
        ////////////////        }
        ////////////////    }
        ////////////////    return false;
        ////////////////}
        public void CheckStacksInList(List<Stack<GraphicCell>> listArrCell) //הפעולה עוברת על כל הרשימה ובודקת האם ישנם מחסניות שלא צריכות להיות(שכל הפצצות התגלו )
        {
            List<Stack<GraphicCell>> HelpList=new List<Stack<GraphicCell>>();
            Stack<GraphicCell> HelpStack = new Stack<GraphicCell>();
            int num = listArrCell.Count();            
           for (int i=0;i<num ; i++)
            {
                HelpStack = CheckStack(listArrCell[i]);
                if (HelpStack.Count==0)
                {
                    listArrCell.Remove(listArrCell[i]);
                    i++;
                    num--;
                }
                if (HelpStack.Count != 0)
                {
                    listArrCell.Add(HelpStack);
                    listArrCell.Remove(listArrCell[i]);
                }
                
            }
        }
        public Stack<GraphicCell> CheckStack(Stack<GraphicCell> stack) //הפעולה בודקת האם הכל התאים במחסנית יכולים להיות פצצה אם כן תשאיר אם לא מחק את התאים  )
        {
            Stack<GraphicCell> HelpStack = new Stack<GraphicCell>();
            int num = stack.Count();
            for (int i = 0; i < num; i++)
            {
                if(stack.Peek().BombCantBeOnTheCell==true)
                {
                    stack.Pop();
                }
                HelpStack.Push(stack.Pop());
            }
            return HelpStack;//מחזיר את המחסנית עם התאים שיכול להיות בהם פצצה
        }
    }
}
