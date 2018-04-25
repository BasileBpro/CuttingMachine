using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet
{
    public partial class Form8 : Form
    {
        public int st;
        public Form8()
        {
            InitializeComponent();
            /*string s = "";
            foreach (List<Point> lp in Form1.listePoints)
            {
                s = "";
                foreach (Point p in lp)
                {
                    s += "X = " + (Form1.Longueur * p.X) / Form1.longueurPanel + "" + "Y = " + (Form1.Largeur * p.Y) / Form1.largeurPanel + " " ;
                }
                checkedListBox1.Items.Add(s);
            }*/
            checkedListBox2.Items.Add("Polygon");
            checkedListBox2.Items.Add("Cercle");
            checkedListBox2.Items.Add("Arc");
        }
        private void checkedListBox2_Click(object sender, EventArgs e)
        {
            List<string> lstr = new List<string>();
            int i = 0;
            foreach (Object o in checkedListBox2.SelectedItems)
            {
                lstr.Add(o.ToString());
                i++;
            }
            string tmp = "";

            foreach (string s in lstr)
            {
                switch (s)
                {
                    case ("Polygon"):
                        checkedListBox1.Items.Clear();
                        foreach (List<Point> lp in Form1.listePoints)
                        {
                            foreach (Point p in lp)
                            {
                                tmp += "X = " + (Form1.Longueur * p.X) / Form1.longueurPanel + "" + "Y = " + (Form1.Largeur * p.Y) / Form1.largeurPanel + " ";
                            }
                            checkedListBox1.Items.Add(tmp);
                            tmp = "";
                            
                        }
                        break;
                    case ("Arc"):
                        checkedListBox1.Items.Clear();
                        foreach (List<float> lp in Form1.listeArc)
                        {
                            tmp += "X1 = " + (Form1.Longueur * lp[0] / Form1.longueurPanel) + " Y1 = " + (Form1.Largeur * lp[1] / Form1.largeurPanel) + " long = " + (Form1.Longueur * lp[2] / Form1.longueurPanel) + " larg = " + (Form1.Largeur * lp[3] / Form1.largeurPanel) + " AngleD = " + lp[4] + " AngleA = " + lp[5] ;
                            checkedListBox1.Items.Add(tmp);
                            tmp = "";
                        }
                        break;
                    case ("Cercle"):
                        checkedListBox1.Items.Clear();
                        foreach (RectangleF lp in Form1.listeEllipse)
                        {
                            tmp += "X1 = " + (Form1.Longueur * lp.X / Form1.longueurPanel) + " Y1 = " + (Form1.Largeur * lp.Y / Form1.largeurPanel) + " longueur = " + (Form1.Longueur * lp.Width / Form1.longueurPanel) + " largeur = " + (Form1.Largeur * lp.Height / Form1.largeurPanel);
                            checkedListBox1.Items.Add(tmp);
                            tmp = "";
                        }
                        break;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
            {
                if (checkedListBox1.GetItemChecked(i) && checkedListBox2.GetItemChecked(0))
                {
                    Form1.listePoints.RemoveAt(i);
                }
                else if (checkedListBox1.GetItemChecked(i) && checkedListBox2.GetItemChecked(2))
                {
                    Form1.listeArc.RemoveAt(i);
                }
                else if (checkedListBox1.GetItemChecked(i) && checkedListBox2.GetItemChecked(1))
                {
                    Form1.listeEllipse.RemoveAt(i);
                }
            }
        }
    }
}
