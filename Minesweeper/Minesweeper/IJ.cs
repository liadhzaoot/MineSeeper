using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
   public class IJ
    {
        int i;
        int j;
        public IJ(int i,int j)
        {
            this.i = i;
            this.j = j;
        }
        public int getI()
        {
            return this.i;
        }
        public int getJ()
        {
            return this.j;
        }
    }
}
