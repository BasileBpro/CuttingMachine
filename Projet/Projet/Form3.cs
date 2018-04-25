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
    public partial class Form3 : Form
    {
        private List<Point> l;

        public Form3()
        {
            InitializeComponent();
            l = new List<Point>();
        }

        public List<Point> L
        {
            get
            {
                return l;
            }

            set
            {
                l = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox4.Text, s1 = textBox1.Text, s2 = textBox2.Text;

            int i, i1, i2;
            if(int.TryParse(s, out i) && int.TryParse(s1, out i1) && int.TryParse(s2, out i2))
            {
                l.Add(new Point(Form1.longueurPanel * i/Form1.Longueur, Form1.largeurPanel*i1/Form1.Largeur));
                l.Add(new Point(Form1.longueurPanel * (i + i2)/Form1.Longueur, Form1.largeurPanel*i1/Form1.Largeur));
                l.Add(new Point(Form1.longueurPanel * (i + i2)/Form1.Longueur, Form1.largeurPanel*(i1 +i2)/Form1.Largeur));
                l.Add(new Point(Form1.longueurPanel * i /Form1.Longueur, Form1.largeurPanel * (i1 + i2) / Form1.Largeur));
            }

        }
    }
}
