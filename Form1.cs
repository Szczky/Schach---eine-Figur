using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Schachfiguren
{
    public partial class Form1 : Form
    {
        int klicken = 1;
        int[] ausgangsFeld = new int[2];
        EigenerButton[,] buttonArray = new EigenerButton[8, 8];
        public Form1()
        {
            InitializeComponent();
            Brett();
        }
        
        private void Brett()
        {

            Random r = new Random();
            for (int reiche = 1; reiche <= 8; reiche++)
            {
                for (int spalte = 1; spalte <= 8; spalte++)
                {
                    EigenerButton b = new EigenerButton();
                    b.Size = new Size(50, 50);
                    b.Location = new Point((spalte - 1) * 50, (reiche - 1) * 50);

                    b.Reiche = reiche;
                    b.Spalte = spalte;
                    b.Figura = ' ';
                    b.farbeStellen();
                    b.Click += ButtonKlicken;
                    buttonArray[reiche - 1, spalte - 1] = b;
                    panel1.Controls.Add(b);
                }
            }
            switch (r.Next(0, 6))
            {
                case 0: buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'B'; //Bauer
                    break;
                case 1:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'T'; //Turm
                    break;
                case 2:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'S'; //Springer
                    break;
                case 3:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'L'; //Läufer
                    break;
                case 4:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'D'; //Dame
                    break;
                case 5:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'K'; //König
                    break;
            }
            
        }

        private void ButtonKlicken(object s, EventArgs ea)
        {
            EigenerButton b = s as EigenerButton;
            if (klicken % 2 == 1 && (b.Figura != ' '))
            {
                ausgangsFeld[0] = b.Reiche;
                ausgangsFeld[1] = b.Spalte;
                b.BackColor = Color.LightBlue;
                klicken++;
            }
            else if (klicken % 2 == 0)
            {
                if (ausgangsFeld[0] == b.Reiche && ausgangsFeld[1] == b.Spalte)
                {
                    b.farbeStellen();
                    klicken++;
                    return;
                }
                if (istGueltig(b))
                {
                    klicken++;

                    b.Figura = buttonArray[ausgangsFeld[0] - 1, ausgangsFeld[1] - 1].Figura;
                    buttonArray[ausgangsFeld[0] - 1, ausgangsFeld[1] - 1].Figura = ' ';
                    buttonArray[ausgangsFeld[0] - 1, ausgangsFeld[1] - 1].farbeStellen();
                }
                else
                {
                    MessageBox.Show("Ungültiger Zug!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Nimm eines Feld mit Figure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void bNeuFigur_Click(object sender, EventArgs e)
        {
            foreach (EigenerButton b in buttonArray)
            {
                b.Figura = ' ';
                b.farbeStellen();
            }
            klicken = 1;
            Random r = new Random();
            switch (r.Next(0, 6))
            {
                case 0:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'B'; //Bauer
                    break;
                case 1:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'T'; //Turm
                    break;
                case 2:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'S'; //Springer
                    break;
                case 3:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'L'; //Läufer
                    break;
                case 4:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'D'; //Dame
                    break;
                case 5:
                    buttonArray[r.Next(0, 7), r.Next(0, 7)].Figura = 'K'; //König
                    break;
            }
        }

        private bool istGueltig(EigenerButton b)
        {
            bool gueltig = false;
            switch (buttonArray[ausgangsFeld[0] - 1, ausgangsFeld[1] - 1].Figura)
            {
                case 'B':
                    if ((ausgangsFeld[0] == b.Reiche) && (ausgangsFeld[1] + 1 == b.Spalte))
                        gueltig= true;
                    break;
                case 'T':
                    {
                        if ((ausgangsFeld[0] == b.Reiche) || (ausgangsFeld[1] == b.Spalte))
                            gueltig = true;
                    }
                    break;
                case 'S':
                    {
                        if (((ausgangsFeld[0] + 2) == b.Reiche && (ausgangsFeld[1] - 1) == b.Spalte) ||
                            ((ausgangsFeld[0] + 2) == b.Reiche && (ausgangsFeld[1] + 1) == b.Spalte) ||
                            ((ausgangsFeld[0] + 1) == b.Reiche && (ausgangsFeld[1] - 2) == b.Spalte) ||
                            ((ausgangsFeld[0] + 1) == b.Reiche && (ausgangsFeld[1] + 2) == b.Spalte) ||
                            ((ausgangsFeld[0] - 1) == b.Reiche && (ausgangsFeld[1] - 2) == b.Spalte) ||
                            ((ausgangsFeld[0] - 1) == b.Reiche && (ausgangsFeld[1] + 2) == b.Spalte) ||
                            ((ausgangsFeld[0] - 2) == b.Reiche && (ausgangsFeld[1] - 1) == b.Spalte) ||
                            ((ausgangsFeld[0] - 2) == b.Reiche && (ausgangsFeld[1] + 1) == b.Spalte))
                            gueltig = true;
                    }
                    break;
                case 'L':
                    if (Math.Abs(ausgangsFeld[0] - b.Reiche) == Math.Abs(ausgangsFeld[1] - b.Spalte))
                        gueltig = true;
                    break;
                case 'D':
                    if ((ausgangsFeld[0] == b.Reiche) || (ausgangsFeld[1] == b.Spalte) ||
                       (Math.Abs(ausgangsFeld[0] - b.Reiche) == Math.Abs(ausgangsFeld[1] - b.Spalte)))
                        gueltig = true;
                    break;
                case 'K':
                    if (((ausgangsFeld[0] + 1) == b.Reiche && (ausgangsFeld[1] - 1) == b.Spalte) ||
                        ((ausgangsFeld[0] + 1) == b.Reiche && ausgangsFeld[1] == b.Spalte) ||
                        ((ausgangsFeld[0] + 1) == b.Reiche && (ausgangsFeld[1] + 1) == b.Spalte) ||
                        (ausgangsFeld[0] == b.Reiche && (ausgangsFeld[1] - 1) == b.Spalte) ||
                        (ausgangsFeld[0] == b.Reiche && (ausgangsFeld[1] + 1) == b.Spalte) ||
                        ((ausgangsFeld[0] - 1) == b.Reiche && (ausgangsFeld[1] - 1) == b.Spalte) ||
                        ((ausgangsFeld[0] - 1) == b.Reiche && ausgangsFeld[1] == b.Spalte) ||
                        ((ausgangsFeld[0] - 1) == b.Reiche && (ausgangsFeld[1] + 1) == b.Spalte))
                        gueltig = true;
                    break;
            }
            return gueltig;
        }
        private void bHilfe_Click(object sender, EventArgs e)
        {
            if (klicken %2 == 1)
            {
                MessageBox.Show("Nimm eines Feld mit Figure und klick wieder auf Hilfe Button die mögliche Züge zu anschauen!");
            }
        }


        private void bHilfe_MouseDown(object sender, MouseEventArgs e)
        {

            if (klicken % 2 == 0)
            {
                foreach (EigenerButton b in buttonArray)
                {
                    if (istGueltig(b))
                    {
                        b.BackColor = Color.LightGreen;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nimm eines Feld mit Figure und klick wieder auf Hilfe Button die mögliche Züge zu anschauen!");
            }
        }

        private void bHilfe_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (EigenerButton b in buttonArray)
            {
                b.farbeStellen();
            }
            buttonArray[ausgangsFeld[0] - 1, ausgangsFeld[1] - 1].BackColor = Color.LightBlue;
        }
    }


    
}

