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
    public partial class Form5 : Form
    {
        private List<RectangleF> l;

        public Form5()
        {
            InitializeComponent();
            l = new List<RectangleF>();

        }
        public List<RectangleF> L
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
            string s = textBox4.Text, s1 = textBox1.Text, s2 = textBox2.Text, s3 = textBox2.Text;

            int i, i1, i2, i3;

            if (int.TryParse(s, out i) && int.TryParse(s1, out i1) && int.TryParse(s2, out i2) && int.TryParse(s3, out i3))
            {
                l.Add(new RectangleF(Form1.longueurPanel * i / Form1.Longueur, Form1.largeurPanel * i1 / Form1.Largeur, Form1.longueurPanel * i2 /Form1.Longueur,  Form1.largeurPanel * i3 / Form1.Largeur));
            }

        }
    }
}
