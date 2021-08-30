using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schachfiguren
{
    class EigenerButton : Button
    {
        /*  int reiche;
          int spalte; */
        char figura; 

        public int Reiche { get; set; }
        public int Spalte { get; set; }
        public char Figura
        {
            get => figura;
            set
            {
                if ("BTSLDK ".Contains(value))
                {
                    figura = value;
                    this.figuraZurueck();
                }
            }
        }
        public void farbeStellen()
        {
            if (this.Reiche % 2 == 0 && this.Spalte % 2 == 0 || this.Reiche % 2 != 0 && this.Spalte % 2 != 0)
            {
                this.BackColor = Color.LightGray;
                this.BackgroundImage = null;
            }
            else
            {
                this.BackColor = Color.DarkGray;
                this.BackgroundImage = null;
            }
            this.figuraZurueck();
        }
        public void figuraZurueck()
        {
            if ("BTSLDK".Contains(this.Figura))
            {
                try
                {
                    string pfad = "Bild\\" + figura + ".png";
                    this.BackgroundImage = Image.FromFile(pfad);
                }
                catch (System.IO.FileNotFoundException)
                {
                    this.Text = "" + figura;
                }
            }
        }
    }
}
