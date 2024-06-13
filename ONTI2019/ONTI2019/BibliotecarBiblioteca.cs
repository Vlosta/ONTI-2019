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
    public partial class BibliotecarBiblioteca : Form
    {
        public BibliotecarBiblioteca()
        {
            InitializeComponent();
        }
        int tipUser = 0;
        public string Criptare(string parola)
        {
            char[] arr = new char[29];
            for (int i = 0; i < parola.Length; i++)
            {
                arr[i] = parola[i];
                if (arr[i] >= 'a' & arr[i] < 'z')
                    arr[i]++;
                if (arr[i] == 'z')
                    arr[i] = 'a';
                if (arr[i] > 'A' & arr[i] <= 'Z')
                    arr[i]--;
                if (arr[i] == 'A')
                    arr[i] = 'Z';
                if (arr[i] >= '0' & arr[i] <= '9')
                    arr[i] = (char)(9 - ((int)arr[i] - '0') + '0');

            }
            string alou = new string(arr);

            alou = alou.Substring(0, parola.Length);
            return alou;
        }


        public string Decriptare(string parola)
        {
            char[] arr = new char[1000];
            for (int i = 0; i < parola.Length; i++)
            {
                arr[i] = parola[i];
                if (arr[i] > 'a' & arr[i] <= 'z')
                    arr[i]--;
                if (arr[i] == 'a')
                    arr[i] = 'z';
                if (arr[i] >= 'A' & arr[i] < 'Z')
                    arr[i]++;
                if (arr[i] == 'Z')
                    arr[i] = 'A';
                if (arr[i] >= '0' & arr[i] <= '9')
                    arr[i] = (char)(9 - ((int)arr[i] - '0') + '0');

            }
            string alou = new string(arr);


            return alou;
        }
        private void BibliotecarBiblioteca_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
            label2.Text = "Bibliotecar= " + LogareBiblioteca.user;
            Bitmap pozaProfil = new Bitmap(Form1.path + "Imagini\\utilizatori\\" + LogareBiblioteca.idUser.ToString() + ".jpg");
            pozaProfil = new Bitmap(pozaProfil, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = pozaProfil;
            textBox4.Hide();
            textBox3.Hide();
            label5.Hide();
            label6.Hide();
            tipUser = 2;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Show();
            textBox4.Show();
            label5.Show();
            label6.Show();
            tipUser = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Hide();
            textBox3.Hide();
            label5.Hide();
            label6.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Form1.path+"\\Imagini\\altele";
            openFileDialog1.ShowDialog();

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text;
            SqlCommand cmd = new SqlCommand("Insert into Utilizatori(TipUtilizator,NumePrenume,Email,Parola) values(@p1,@p2,@p3,@p4)",Form1.con);
            cmd.Parameters.Clear();
            int ok = 0;
            cmd.Parameters.Add("@p1", tipUser);
            
            if (email.Contains("@") & email.Contains("."))
            {
                if(tipUser==1)
                    if(textBox3.Text==textBox4.Text & textBox1.Text.Length>1)
                    {
                        cmd.Parameters.Add("@p2",textBox1.Text);
                        cmd.Parameters.Add("@p3", textBox2.Text);
                        cmd.Parameters.Add("@p4", Criptare(textBox4.Text));
                        ok = 1;

                    }
                if (tipUser==2)
                    {
                    if (textBox1.Text.Length > 1)
                    {
                        cmd.Parameters.Add("@p2", textBox1.Text);
                        cmd.Parameters.Add("@p3", textBox2.Text);
                        cmd.Parameters.Add("@p4", "");
                        ok = 1;
                    }

                }
            }
            if (ok == 1)
                cmd.ExecuteNonQuery();
            else
                MessageBox.Show("DATE INCORECTE!");

        }
    }
}
