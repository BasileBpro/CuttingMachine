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
    public partial class Form7 : Form
    {
        private List<float> l;

        public Form7()
        {
            InitializeComponent();
            l = new List<float>();

        }
        public List<float> L
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
            string s = textBox1.Text, s1 = textBox2.Text, s2 = textBox5.Text, s3 = textBox6.Text, s4 = textBox8.Text , s5 = textBox9.Text;

            float i, i1, i2, i3, i4, i5;

            if (float.TryParse(s, out i) && float.TryParse(s1, out i1) && float.TryParse(s2, out i2) && float.TryParse(s3, out i3) && float.TryParse(s4, out i4) && float.TryParse(s5, out i5)) 
            {
                l.Add(Form1.longueurPanel * i / Form1.Longueur);
                l.Add(Form1.largeurPanel * i1 / Form1.Largeur);
                l.Add(Form1.longueurPanel * i2 / Form1.Longueur);
                l.Add(Form1.largeurPanel * i3 / Form1.Largeur);
                l.Add(i4);
                l.Add(i5);

            }
        }
    }
}
