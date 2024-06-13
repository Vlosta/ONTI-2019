using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ONTI2019
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Form1.con.Open();
        }
        int idCarte = 1;
        string path="";
        Bitmap imgMare;
        PictureBox pb;
        private void Form2_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 10;
            path = System.IO.Directory.GetCurrentDirectory();
            path += "\\ONTI_C#_2019_resurse\\";
            Random rand = new Random();
            idCarte = rand.Next(1, 24);
            SqlCommand sel = new SqlCommand("Select Titlu,Autor,Nrpag from Carti where IdCarte=@p1",Form1.con);
            sel.Parameters.Add("@p1", idCarte);
            SqlDataReader rd=sel.ExecuteReader();
            rd.Read();
            textBox1.Text = rd[0].ToString();
            textBox2.Text = rd[1].ToString();
            textBox3.Text = rd[2].ToString();
            Bitmap img = new Bitmap(path + "\\Imagini\\carti\\" + idCarte.ToString() + ".jpg");
            img = new Bitmap(img, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;

            rd.Close();
            pb = new PictureBox();
            
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //label5.Text = trackBar1.Value.ToString();
            if (trackBar1.Value <= 0)
                trackBar1.Value = 1;
            Bitmap img = new Bitmap(path + "\\Imagini\\carti\\" + idCarte.ToString() + ".jpg");
            img = new Bitmap(img, (pictureBox1.Width*trackBar1.Value)/10, (pictureBox1.Height*trackBar1.Value)/ 10);
            pictureBox1.Image = img;
        }

        private void Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
