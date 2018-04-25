using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;

namespace Projet
{
    public partial class Form1 : Form
    {
        Graphics g = null;
        Point lastPoint;
        public static int X1;
        public static int Y1;
        public static int X2;
        public static int Y2;
        public static int X3;
        public static int Y3;
        public static int width1;
        public static int height1;
        public static int AngleD;
        public static int AngleA;
        public static int Longueur;
        public static int Largeur;
        public static int longueurPanel;
        public static int largeurPanel;
        private int saveX1;
        private int saveY1;
        private float Xcercle;
        private float Ycercle;
        private bool test = false;
        private bool test1 = false;
        public bool down = true;
        private bool test2 = false;
        //private bool abra = false;
        private Pen pen = new Pen(Color.Red);
        private byte[] content;
        private double divisionAngle;
        private double AngleDepart;
        private Bitmap Img = new Bitmap("C:\\Users\\Baslique\\Documents\\Projet6\\Projet\\aigle.jpg");



        public static List<List<Point>> listePoints;
        public static List<RectangleF> listeEllipse;
        public static List<List<float>> listeArc;
        public static List<double> listeArcX;
        public static List<double> listeArcY;
        public static List<Point> listeDraw;
        public static List<Point> listeImg;
        public class NetworkShareAccesser : IDisposable
        {
            private string _remoteUncName;
            private string _remoteComputerName;

            public string RemoteComputerName
            {
                get
                {
                    return this._remoteComputerName;
                }
                set
                {
                    this._remoteComputerName = value;
                    this._remoteUncName = @"\\" + this._remoteComputerName;
                }
            }

            public string UserName
            {
                get;
                set;
            }
            public string Password
            {
                get;
                set;
            }

            #region Consts

            private const int RESOURCE_CONNECTED = 0x00000001;
            private const int RESOURCE_GLOBALNET = 0x00000002;
            private const int RESOURCE_REMEMBERED = 0x00000003;

            private const int RESOURCETYPE_ANY = 0x00000000;
            private const int RESOURCETYPE_DISK = 0x00000001;
            private const int RESOURCETYPE_PRINT = 0x00000002;

            private const int RESOURCEDISPLAYTYPE_GENERIC = 0x00000000;
            private const int RESOURCEDISPLAYTYPE_DOMAIN = 0x00000001;
            private const int RESOURCEDISPLAYTYPE_SERVER = 0x00000002;
            private const int RESOURCEDISPLAYTYPE_SHARE = 0x00000003;
            private const int RESOURCEDISPLAYTYPE_FILE = 0x00000004;
            private const int RESOURCEDISPLAYTYPE_GROUP = 0x00000005;

            private const int RESOURCEUSAGE_CONNECTABLE = 0x00000001;
            private const int RESOURCEUSAGE_CONTAINER = 0x00000002;


            private const int CONNECT_INTERACTIVE = 0x00000008;
            private const int CONNECT_PROMPT = 0x00000010;
            private const int CONNECT_REDIRECT = 0x00000080;
            private const int CONNECT_UPDATE_PROFILE = 0x00000001;
            private const int CONNECT_COMMANDLINE = 0x00000800;
            private const int CONNECT_CMD_SAVECRED = 0x00001000;

            private const int CONNECT_LOCALDRIVE = 0x00000100;

            #endregion

            #region Errors

            private const int NO_ERROR = 0;

            private const int ERROR_ACCESS_DENIED = 5;
            private const int ERROR_ALREADY_ASSIGNED = 85;
            private const int ERROR_BAD_DEVICE = 1200;
            private const int ERROR_BAD_NET_NAME = 67;
            private const int ERROR_BAD_PROVIDER = 1204;
            private const int ERROR_CANCELLED = 1223;
            private const int ERROR_EXTENDED_ERROR = 1208;
            private const int ERROR_INVALID_ADDRESS = 487;
            private const int ERROR_INVALID_PARAMETER = 87;
            private const int ERROR_INVALID_PASSWORD = 1216;
            private const int ERROR_MORE_DATA = 234;
            private const int ERROR_NO_MORE_ITEMS = 259;
            private const int ERROR_NO_NET_OR_BAD_PATH = 1203;
            private const int ERROR_NO_NETWORK = 1222;

            private const int ERROR_BAD_PROFILE = 1206;
            private const int ERROR_CANNOT_OPEN_PROFILE = 1205;
            private const int ERROR_DEVICE_IN_USE = 2404;
            private const int ERROR_NOT_CONNECTED = 2250;
            private const int ERROR_OPEN_FILES = 2401;

            #endregion

            #region PInvoke Signatures

            [DllImport("Mpr.dll")]
            private static extern int WNetUseConnection(
                IntPtr hwndOwner,
                NETRESOURCE lpNetResource,
                string lpPassword,
                string lpUserID,
                int dwFlags,
                string lpAccessName,
                string lpBufferSize,
                string lpResult
                );

            [DllImport("Mpr.dll")]
            private static extern int WNetCancelConnection2(
                string lpName,
                int dwFlags,
                bool fForce
                );

            [StructLayout(LayoutKind.Sequential)]
            private class NETRESOURCE
            {
                public int dwScope = 0;
                public int dwType = 0;
                public int dwDisplayType = 0;
                public int dwUsage = 0;
                public string lpLocalName = "";
                public string lpRemoteName = "";
                public string lpComment = "";
                public string lpProvider = "";
            }

            #endregion

            /// <summary>
            /// Creates a NetworkShareAccesser for the given computer name. The user will be promted to enter credentials
            /// </summary>
            /// <param name="remoteComputerName"></param>
            /// <returns></returns>
            public static NetworkShareAccesser Access(string remoteComputerName)
            {
                return new NetworkShareAccesser(remoteComputerName);
            }

            /// <summary>
            /// Creates a NetworkShareAccesser for the given computer name using the given domain/computer name, username and password
            /// </summary>
            /// <param name="remoteComputerName"></param>
            /// <param name="domainOrComuterName"></param>
            /// <param name="userName"></param>
            /// <param name="password"></param>
            public static NetworkShareAccesser Access(string remoteComputerName, string domainOrComuterName, string userName, string password)
            {
                return new NetworkShareAccesser(remoteComputerName,
                                                domainOrComuterName + @"\" + userName,
                                                password);
            }

            /// <summary>
            /// Creates a NetworkShareAccesser for the given computer name using the given username (format: domainOrComputername\Username) and password
            /// </summary>
            /// <param name="remoteComputerName"></param>
            /// <param name="userName"></param>
            /// <param name="password"></param>
            public static NetworkShareAccesser Access(string remoteComputerName, string userName, string password)
            {
                return new NetworkShareAccesser(remoteComputerName,
                                                userName,
                                                password);
            }

            private NetworkShareAccesser(string remoteComputerName)
            {
                RemoteComputerName = remoteComputerName;

                this.ConnectToShare(this._remoteUncName, null, null, true);
            }

            private NetworkShareAccesser(string remoteComputerName, string userName, string password)
            {
                RemoteComputerName = remoteComputerName;
                UserName = userName;
                Password = password;

                this.ConnectToShare(this._remoteUncName, this.UserName, this.Password, false);
            }

            private void ConnectToShare(string remoteUnc, string username, string password, bool promptUser)
            {
                NETRESOURCE nr = new NETRESOURCE
                {
                    dwType = RESOURCETYPE_DISK,
                    lpRemoteName = remoteUnc
                };

                int result;
                if (promptUser)
                {
                    result = WNetUseConnection(IntPtr.Zero, nr, "", "", CONNECT_INTERACTIVE | CONNECT_PROMPT, null, null, null);
                }
                else
                {
                    result = WNetUseConnection(IntPtr.Zero, nr, password, username, 0, null, null, null);
                }

                if (result != NO_ERROR)
                {
                    throw new Win32Exception(result);
                }
            }

            private void DisconnectFromShare(string remoteUnc)
            {
                int result = WNetCancelConnection2(remoteUnc, CONNECT_UPDATE_PROFILE, false);
                if (result != NO_ERROR)
                {
                    throw new Win32Exception(result);
                }
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                this.DisconnectFromShare(this._remoteUncName);
            }
        }
        public Form1()
        {
            InitializeComponent();
            listePoints = new List<List<Point>>();
            listeEllipse = new List<RectangleF>();
            listeArc = new List<List<float>>();
            listeArcX = new List<double>();
            listeArcY = new List<double>();
            listeDraw = new List<Point>();
            listeImg = new List<Point>();

            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string L = textBox1.Text;
                string l = textBox2.Text;
                Longueur = Int32.Parse(L);
                Largeur = Int32.Parse(l);
                longueurPanel = panel1.Width;
                largeurPanel = panel1.Height;
                panel1.Invalidate();
                panel1.Update();
            }
            catch
            {
                MessageBox.Show("Problème de dimension");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //CREATION CERCLE
            Form5 form5 = new Form5();
            DialogResult dr = form5.ShowDialog();
            if (dr == DialogResult.OK)
            {
                listeEllipse.AddRange(form5.L);
                form5.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form5.Close();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //CREATION CARRE
            Form3 form3 = new Form3();
            DialogResult dr = form3.ShowDialog();

            if (dr == DialogResult.OK)
            {
                listePoints.Add(form3.L);
                form3.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form3.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //CREATION TRAIT
            Form2 form2 = new Form2();
            DialogResult dr = form2.ShowDialog();

            if (dr == DialogResult.OK)
            {
                listePoints.Add(form2.L);
                form2.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form2.Close();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            if (down)
            {
                listeDraw.Add(new Point(999, 999));
                down = false;
            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                g = panel1.CreateGraphics();
                checkButton();                
                g.DrawLine(pen, lastPoint, e.Location);
                lastPoint = e.Location;
                if (!down)
                {
                    listeDraw.Add(e.Location);
                }
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!down)
            {
                listeDraw.Add(new Point(-999, -999));
                down = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int x = 2, y = 2;
            string s = textBox1.Text, s2 = textBox2.Text;
            if (s != null && s != "" && s2 != null && s2 != "") {
                if(!Int32.TryParse(s, out x)) x = 2;
                if (!Int32.TryParse(s2, out y)) y = 2;
            }
            createArea(x, y);            

            createPolygon(listePoints);

            createEllipse(listeEllipse);

            createArc(listeArc);
        }

        private void createPolygon(List<List<Point>> l)
        {
            //FONCTION CREATION D'UN POLYGON
            Point[] tabp;
            g = panel1.CreateGraphics();

            foreach (List<Point> lp in l)
            {
                checkButton();
                tabp = lp.ToArray();
                g.DrawPolygon(pen, tabp);
            }
        }
        private void createEllipse(List<RectangleF> l)
        {
            //FONCTION CREATION D'ELLIPSE
            g = panel1.CreateGraphics();

            foreach (RectangleF r in l)
            {
                checkButton();

                g.DrawEllipse(pen, r);
            }
        }
        private void createArc(List<List<float>> l)
        {
            //FONCTION CREATION D'ARC
            float[] tabF;
            g = panel1.CreateGraphics();

            foreach (List<float> lf in l)
            {
                tabF = lf.ToArray();
                checkButton();

                g.DrawArc(pen, tabF[0], tabF[1], tabF[2], tabF[3], tabF[4], tabF[5]);
            }
        }
        //COULEURS 
        private void checkButton()
        {
            if (radioButton1.Checked)
            {
                pen = new Pen(Color.Red);
            }
            else if (radioButton2.Checked)
            {
                pen = new Pen(Color.Blue);
            }
            else if (radioButton3.Checked)
            {
                pen = new Pen(Color.Green);
            }
            else if (radioButton4.Checked)
            {
                pen = new Pen(Color.Yellow);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //RECTANGLE
        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            DialogResult dr = form4.ShowDialog();
            if (dr == DialogResult.OK)
            {
                listePoints.Add(form4.L);
                form4.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else   
            {
                form4.Close();
            }
            
        }
        //TRIANGLE
        private void button6_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            DialogResult dr = form6.ShowDialog();
            if (dr == DialogResult.OK)
            {
                listePoints.Add(form6.L);
                form6.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form6.Close();
            }
        }
        //ARC DE CERCLE
        private void button9_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            DialogResult dr = form7.ShowDialog();
            if (dr == DialogResult.OK)
            {
                listeArc.Add(form7.L);
                form7.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form7.Close();
            }
        }

        public void createArea(int lon, int larg)
        {
            //FONCTION CREATION ZONE DE DESSIN
            g = panel1.CreateGraphics();
            for (int i = 0; i < panel1.Height; i = i + panel1.Height / lon)
            {
                g.DrawLine(Pens.LightGray, 0, i, panel1.Width, i);
            }

            for (int i = 0; i < panel1.Width; i = i + panel1.Width / larg)
            {
                g.DrawLine(Pens.LightGray, i, 0, i, panel1.Height);
            }
        }
        private void ClearColor(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Teal);
        }
        //CARRE
        private void button10_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            DialogResult dr = form8.ShowDialog();

            if (dr == DialogResult.OK)
            {
                form8.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form8.Close();
            }
        }
        //G-CODE
        private void button11_Click(object sender, EventArgs e)
        {
            string tmp = "";           
            StreamWriter writer = new StreamWriter("C:\\Users\\Baslique\\Documents\\Projet7\\Projet\\Test.g");
            writer.WriteLine("G21 M6 T1 F200");
            writer.WriteLine("G97 M3 S10000"); //Mise en marche de l'arrosage à une vitesse constante
            writer.WriteLine("G4 P10"); //Temps d'attente de 10ms
            writer.WriteLine("G0 Z3");
            writer.WriteLine("G0 X0 Y0"); // Positionnement de la buse en 0 0
            foreach (List<Point> lp in Form1.listePoints)   //GENERATION GCODE DES POLYGONES DESSINES
            {
                foreach (Point p in lp)
                {
                    tmp += "G1 " + "X" + ((Form1.Longueur * p.X) / Form1.longueurPanel)*10 + "" + " Y" + ((Form1.Largeur * p.Y) / Form1.largeurPanel)*10 + "\n";

                    //RECUPERATION DU POINT DE DEPART DU POYLGONE ET DEMARRAGE COUPE
                    if (test == false)
                    {
                        tmp += "G0 Z-3\n";
                        saveX1 = ((Form1.Longueur * p.X) / Form1.longueurPanel)*10;
                        saveY1 = ((Form1.Largeur * p.Y) / Form1.largeurPanel)*10;
                        test = true;
                    }
                }
                test = false;
                tmp += "G1 " + "X" + saveX1 + "" + " Y" + saveY1 + "\n";
                tmp += ("G0 Z3 \n"); //STOP DE LA COUPE 
                writer.WriteLine(tmp);
                tmp = "";
                //STOP DE L ABRA
                //abra = false;

            }
            foreach (RectangleF lp in Form1.listeEllipse)   //GENERATION GCODE DES CERCLES DESSINES
            {
                Xcercle = (((Form1.Longueur * lp.X ) / Form1.longueurPanel) + (Form1.Longueur * (lp.Width/2) / Form1.longueurPanel)) * 10; 
                Ycercle = (((Form1.Largeur * lp.Y) / Form1.largeurPanel) + (Form1.Largeur * (lp.Height / 2) / Form1.largeurPanel)) * 10;
                if (test == false)
                {
                    test = true;
                }
                else
                {
                    tmp += "G0 Z3\n";
                }
                tmp += "G1 " + "X" + Xcercle + " " + "Y" + Ycercle + "\n";
                tmp += "G0 Z-3\n"; 
                tmp += "G2 I" + ((Form1.Longueur * lp.Width) /Form1.longueurPanel)* 10 /2;
                writer.WriteLine(tmp);
                tmp = "";
            }
            foreach (List<float> lp in Form1.listeArc)  //GENERATION GCODE DES ARC DESSINES
            {
                divisionAngle = Math.Round(6.28 / 360, 4);
                AngleDepart = Math.Round((6.28 * lp[4] / 360), 4);
                //BOUCLE PERMETTANT DE RECUPERER CHAQUE VALEUR DE L ARC DE CERCLE
                for (int i = 0; i < lp[5]; i = i+2)
                {
                    if (test1 == false)
                    {
                        tmp += "G0 Z3\n";
                        tmp += "G1 X" + ((100 * lp[0] / Form1.longueurPanel) + (100 * lp[2] / Form1.longueurPanel / 2) + Math.Round((Form1.Longueur * lp[2] / Form1.longueurPanel) * 5 * Math.Cos(divisionAngle * i + AngleDepart), 2)).ToString(CultureInfo.GetCultureInfo("en-US")) + " Y" + ((100 * lp[1] / Form1.largeurPanel) + (100 * lp[3] / Form1.largeurPanel / 2) + Math.Round((Form1.Largeur * lp[3] / Form1.largeurPanel) * 5 * Math.Sin(divisionAngle * i + AngleDepart) , 2)).ToString(CultureInfo.GetCultureInfo("en-US")) + "\n";
                        tmp += "G0 Z-3\n";
                        test1 = true;
                    }
                    else
                    {
                        //RECUPERATION COORDONNEES AVEC TRANSFORMATION CULTURE EN-US POUR LES DECIMALES 
                        tmp += "G1 X" + ((100 * lp[0] / Form1.longueurPanel) + (100 * lp[2] / Form1.longueurPanel / 2)  + Math.Round((Form1.Longueur * lp[2] / Form1.longueurPanel) * 5 * Math.Cos(divisionAngle * i + AngleDepart), 2)).ToString(CultureInfo.GetCultureInfo("en-US")) + " Y" +((100 * lp[1] / Form1.largeurPanel) + (100 * lp[3] / Form1.largeurPanel / 2) + Math.Round((Form1.Largeur * lp[3] /Form1.largeurPanel) * 5 * Math.Sin(divisionAngle * i + AngleDepart), 2)).ToString(CultureInfo.GetCultureInfo("en-US")) + "\n";
                    }
                }
                test1 = false;
                writer.WriteLine(tmp);
                tmp = "";
            }
            for(int i=0; i < Form1.listeDraw.Count() - 1; i++)
            {
                if (listeDraw[i].X == 999 && !test2)
                {
                    tmp += "G1 X" + Math.Round(System.Convert.ToDouble(Form1.Longueur * listeDraw[i + 1].X) / Form1.longueurPanel * 10, 3).ToString(CultureInfo.GetCultureInfo("en-US")) + " Y" + Math.Round(System.Convert.ToDouble(Form1.Largeur * listeDraw[i + 1].Y) / Form1.largeurPanel * 10, 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    tmp += "\n";
                    tmp += "G0 Z-3\n";

                    writer.WriteLine(tmp);
                    tmp = "";
                }
                else if(listeDraw[i].X == 999 & test2)
                {
                    tmp += "G0 Z-3\n";
                    writer.WriteLine(tmp);
                    tmp = "";
                }
                else if (listeDraw[i].X == -999)
                {
                    tmp += "G0 Z3\n";
                    tmp += "G1 X" + Math.Round(System.Convert.ToDouble(Form1.Longueur * listeDraw[i + 2].X) / Form1.longueurPanel * 10, 3).ToString(CultureInfo.GetCultureInfo("en-US")) + " Y" + Math.Round(System.Convert.ToDouble(Form1.Largeur * listeDraw[i + 2].Y) / Form1.largeurPanel * 10, 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    writer.WriteLine(tmp);
                    tmp = "";
                }
                else if (listeDraw[i].X > -999 && listeDraw[i].X < 999)
                {
                    tmp += "G1 X" + Math.Round(System.Convert.ToDouble(Form1.Longueur * listeDraw[i].X) / Form1.longueurPanel * 10, 3).ToString(CultureInfo.GetCultureInfo("en-US")) + " Y" + Math.Round(System.Convert.ToDouble(Form1.Largeur * listeDraw[i].Y) / Form1.largeurPanel * 10, 3).ToString(CultureInfo.GetCultureInfo("en-US"));
                    writer.WriteLine(tmp);
                    tmp = "";
                }
            }
            writer.Close();
            MessageBox.Show("Le Gcode a été généré!");
        }
        //EFFACER TOUT
        private void button12_Click(object sender, EventArgs e)
        {
            listePoints.RemoveRange(0, listePoints.Count());
            listeEllipse.RemoveRange(0, listeEllipse.Count());
            listeArc.RemoveRange(0, listeArc.Count());
            listeArcX.RemoveRange(0, listeArcX.Count());
            listeArcY.RemoveRange(0, listeArcY.Count());
            listeDraw.RemoveRange(0, listeDraw.Count());
            panel1.Invalidate();
            panel1.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            using (NetworkShareAccesser.Access("BEAGLEBONE","192.168.8.1", "machinekit", "machinekit"))
            {
                File.Delete(@"\\BEAGLEBONE\Gcode\Test.g");

                File.Copy(@"C:\Users\Baslique\Documents\Projet7\Projet\Test.g", @"\\BEAGLEBONE\Gcode\Test.g");
            }         
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            DialogResult dr = form9.ShowDialog();
            if (dr == DialogResult.OK)
            {
                form9.Close();
                panel1.Invalidate();
                panel1.Update();
            }
            else
            {
                form9.Close();
            }
        }
    }
}
