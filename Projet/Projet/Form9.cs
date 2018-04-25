using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet
{
    public partial class Form9 : Form
    {
        Bitmap Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\aigle.jpg");
        private List<Point> listeImgBis;
        private int test;
        private int num;
        public Form9()
        {
            InitializeComponent();
            listeImgBis = new List<Point>();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Paint(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //Radiocheck();
            //pictureBox1.Image = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\aigle.jpg");

        }
        private void Radiocheck()
        {
            if (radioButton1.Checked == true)
            {
                pictureBox1.Image = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\aigle.jpg");
                Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\aigle.jpg");
            }
            else if (radioButton2.Checked == true)
            {
                pictureBox1.Image = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\tortue.png");
                Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\tortue.png");
            }
            else if (radioButton3.Checked == true)
            {
                pictureBox1.Image = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\troll.png");
                Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\troll.png");
            }
            else if (radioButton4.Checked == true)
            {
                pictureBox1.Image = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\sushi.png");
                Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\sushi.png");
            }
            else if (radioButton5.Checked == true)
            {
                pictureBox1.Image = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\manette.png");
                Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\manette.png");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.listeImg.RemoveRange(0, Form1.listeImg.Count());
            Recuperation_Pix();
            Tri_Liste();
            //TEST CONVERSION GCODE IMAGE
            string tmp = "";
            StreamWriter writer = new StreamWriter("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\Test.g");
            writer.WriteLine("G21 M6 T1 F200");
            writer.WriteLine("G97 M3 S10000"); //Mise en marche de l'arrosage à une vitesse constante
            writer.WriteLine("G4 P10"); //Temps d'attente de 10ms
                                        //writer.WriteLine("G0 Z3");
                                        //writer.WriteLine("G0 X0 Y0"); // Positionnement de la buse en 0 0

            tmp += "G0 Z3\n";
            tmp += "G1 X" + listeImgBis[0].X + " Y" + listeImgBis[0].Y + "\n";
            tmp += "G0 Z-3\n";
            tmp += "G1 X" + listeImgBis[0].X + " Y" + listeImgBis[0].Y + "\n";
            writer.WriteLine(tmp);
            tmp = "";
            for (int i=1; i < listeImgBis.Count -1; i++)
            {
                if(Math.Sqrt(Math.Pow((listeImgBis[i].X - listeImgBis[i+1].X), 2) + Math.Pow((listeImgBis[i].Y - listeImgBis[i+1].Y), 2)) < 2)
                {
                    tmp += "G1 X" + listeImgBis[i].X + " Y" + listeImgBis[i].Y;
                    writer.WriteLine(tmp);
                    tmp = "";
                }
                else
                {
                    tmp += "G1 X" + listeImgBis[i].X + " Y" + listeImgBis[i].Y + "\n";
                    tmp += "G0 Z3\n";
                    tmp += "G1 X" + listeImgBis[i+1].X + " Y" + listeImgBis[i+1].Y + "\n";
                    tmp += "G0 Z-3\n";
                    tmp += "G1 X" + listeImgBis[i+1].X + " Y" + listeImgBis[i+1].Y + "\n";
                    writer.WriteLine(tmp);
                    tmp = "";
                }
            }
            writer.Close();
                     
        }

        void Recuperation_Pix()
        {
            //RECUPERATION PIXELS
            float width = Img.Width;
            float height = Img.Height;
            for (int i = 1; i < width - 1; i++)
            {
                for (int j = 1; j < height - 1; j++)
                {
                    Color pixelColor = Img.GetPixel(i, j);
                    if (Img.GetPixel(i - 1, j).ToString() == "Color [A=255, R=0, G=0, B=0]" && Img.GetPixel(i + 1, j).ToString() == "Color [A=255, R=0, G=0, B=0]" && Img.GetPixel(i, j + 1).ToString() == "Color [A=255, R=0, G=0, B=0]" && Img.GetPixel(i, j - 1).ToString() == "Color [A=255, R=0, G=0, B=0]")
                    {

                    }
                    else if(Img.GetPixel(i,j).ToString() == "Color [A=255, R=0, G=0, B=0]")
                    {
                        Point pt = new Point(i, j);
                        Form1.listeImg.Add(pt);
                    }
                }
            }
        }
        void Tri_Liste()
        {
            listeImgBis.Add(Form1.listeImg.ElementAt(0));
            Form1.listeImg.RemoveAt(0);
            int taille = Form1.listeImg.Count;
            for(int i=0;i<taille; i++)
            {
                double distance = 2000;
                for(int j=0; j<taille - test; j++)
                {
                    //int distanceatm = Math.Abs((listeImgBis.ElementAt(i).X - Form1.listeImg.ElementAt(j).X) + (listeImgBis.ElementAt(i).Y - Form1.listeImg.ElementAt(j).Y));
                    double distanceatm = Math.Sqrt(Math.Pow((listeImgBis.ElementAt(i).X - Form1.listeImg.ElementAt(j).X), 2) + Math.Pow((listeImgBis.ElementAt(i).Y - Form1.listeImg.ElementAt(j).Y), 2));
                    if (distanceatm < distance)
                    {
                        num = j;
                        distance = distanceatm;
                    }                   
                }
                listeImgBis.Add(Form1.listeImg.ElementAt(num));
                test = test + 1;
                Form1.listeImg.RemoveAt(num);
                num = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Radiocheck();
        }
    }
}
